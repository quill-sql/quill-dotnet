using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Dynamic;
using System.Reflection;
using Quill.Connection;
using Quill.Database;
using Quill.Postgres;
using Quill.RunQueryProcesses;
using Quill.Tenants;
using Quill.Models;

namespace Quill
{
    public class QuillApiException : Exception
    {
        public QuillApiException(string message) : base(message) { }
    }

    public static class DatabaseType
    {
        public const string PostgreSQL = "postgresql";
        public const string Snowflake = "snowflake";
        public const string BigQuery = "bigquery";
        public const string MySQL = "mysql";
        public const string ClickHouse = "clickhouse";

        public static bool IsValid(string type)
        {
            return new[] { PostgreSQL, Snowflake, BigQuery, MySQL, ClickHouse }.Contains(type);
        }
    }

    public class QuillOptions
    {
        public string PrivateKey { get; set; } = string.Empty;
        public string DatabaseType { get; set; } = string.Empty;
        public string? DatabaseConnectionString { get; set; }
        public IDatabaseConfig? DatabaseConfig { get; set; }
        public string? MetadataServerUrl { get; set; }
        public CacheCredentials? Cache { get; set; }

        public bool IsPooled { get; set; } = false;
    }

    public class Quill : IDisposable
    {
        private static readonly string Host = Environment.GetEnvironmentVariable("ENV") == "development"
            ? "http://localhost:8080"
            : "https://quill-344421.uc.r.appspot.com";
        private static readonly HashSet<string> FlagTasks = new HashSet<string> { "dashboard", "report", "item", "report-info", "filter-options" };

        private readonly CachedConnection _targetConnection;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public Quill(QuillOptions options)
        {
            _baseUrl = options.MetadataServerUrl ?? Host;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", options.PrivateKey);

            var credentials = options.DatabaseConfig;
            if (!string.IsNullOrEmpty(options.DatabaseConnectionString))
            {
                credentials = DatabaseHelper.GetDatabaseCredentials(
                    options.DatabaseType,
                    options.DatabaseConnectionString,
                    options.IsPooled
                );
            }

            _targetConnection = new CachedConnection(
                options.DatabaseType,
                credentials ?? throw new ArgumentNullException(nameof(credentials)),
                options.Cache ?? new CacheCredentials()
            );
        }

        public async Task<IDictionary<string, object>> Query(QueryParams parameters)
        {
            var tenants = parameters.Tenants;
            var flags = parameters.Flags;
            var metadata = parameters.Metadata;
            var filters = parameters.Filters;

            if (tenants == null || !tenants.Any())
            {
                throw new ArgumentException("You may not pass an empty tenants array.");
            }
            else
            {
                _targetConnection.TenantIds = TenantUtils.ExtractTenantIds(tenants.ToList());
            }

            if (flags?.Count() == 0)
            {
                throw new ArgumentException("You may not pass an empty flags array.");
            }

            var responseMetadata = new Dictionary<string, object>();

            if (metadata == null || string.IsNullOrEmpty(metadata.TryGetValue("task", out var taskValue) ? taskValue?.ToString() : null))
            {
                return new Dictionary<string, object>
                {
                    ["error"] = "Missing task.",
                    ["status"] = "error",
                    ["data"] = new Dictionary<string, object>()
                };
            }

            HashSet<string> tenantFlags = new();

            try
            {
                var tenant = tenants.FirstOrDefault()?.ToString();
                var taskString = metadata.TryGetValue("task", out var currentTask) ? currentTask?.ToString() : null;
                if (taskString != null && FlagTasks.Contains(taskString) &&
                    tenant != TenantUtils.AllTenants &&
                    tenant != TenantUtils.SingleTenant &&
                    (currentTask?.ToString() != "filter-options" ||
                    !string.IsNullOrEmpty(metadata.TryGetValue("reportId", out var reportIdCheck) ? reportIdCheck?.ToString() : null)))
                {
                    var _response = await PostQuill("tenant-mapped-flags", new
                    {
                        reportId = metadata.TryGetValue("reportId", out var value) ? value
                            : metadata.TryGetValue("dashboardItemId", out value) ? value
                            : null,
                        dashboardName = metadata.TryGetValue("name", out value) ? value : null,
                        tenants,
                        flags
                    });

                    if (_response.ContainsKey("error"))
                    {
                        return new Dictionary<string, object>
                        {
                            ["status"] = "error",
                            ["error"] = _response["error"],
                            ["data"] = _response.ContainsKey("metadata") ? _response["metadata"] : new Dictionary<string, object>()
                        };
                    }

                    JsonElement queriesElement = (JsonElement)_response["queries"];


                    var queries = queriesElement
                                    .EnumerateArray()
                                    .Select(query => query.GetString())
                                    .ToArray();

                    var flagQueryResults = await RunQueries(
                        queries as dynamic[],
                        _targetConnection.DatabaseType
                    );
                    var serializedFlagQueryResults = JsonSerializer.Serialize(flagQueryResults);

                    tenantFlags = new HashSet<string>(
                        ((IEnumerable<dynamic>)flagQueryResults.queryResults)
                            .SelectMany(result => ((IEnumerable<IDictionary<string, object>>)result.Rows)
                                .Select(row => row["quill_flag"]?.ToString())
                            )
                            .Where(flag => flag != null)
                            .Cast<string>()
                    );
                }
                else if (tenants?.Count() > 0 && tenant == "QUILL_SINGLE_TENANT" && flags != null)
                {
                    if (flags.Count() > 0 && !(flags.ElementAt(0) is string))
                    {
                        return new Dictionary<string, object>
                        {
                            { "status", "error" },
                            { "error", "SINGLE_TENANT only supports string[] for the flags parameter" },
                            { "data", new Dictionary<string, object>() }
                        };

                    }
                    tenantFlags = new HashSet<string>(flags.Cast<string>());
                }

                AdditionalProcessing preQueryRunQueryConfig = new AdditionalProcessing();
                if (metadata.TryGetValue("runQueryConfig", out var preQueryRunQueryConfigValue) && preQueryRunQueryConfigValue is JsonElement preQueryRunQueryConfigElement)
                {
                    preQueryRunQueryConfig = JsonSerializer.Deserialize<AdditionalProcessing>(preQueryRunQueryConfigElement.GetRawText()) ?? new AdditionalProcessing();
                }
                else
                {
                    preQueryRunQueryConfig = preQueryRunQueryConfigValue as AdditionalProcessing ?? new AdditionalProcessing();
                }
                var preQueryResults = metadata.TryGetValue("preQueries", out var preQueriesValue) && preQueriesValue is JsonElement preQueriesElement
                    ? await RunQueries(
                        JsonSerializer.Deserialize<string[]>(preQueriesElement.GetRawText()) ?? Array.Empty<string>(),
                        _targetConnection.DatabaseType,
                        metadata.TryGetValue("databaseType", out var dbTypeValue) ? dbTypeValue?.ToString() : null,
                        preQueryRunQueryConfig)
                    : new Dictionary<string, object>();


                if (metadata.TryGetValue("runQueryConfig", out var runQueryConfigObj) &&
                    runQueryConfigObj is JsonElement runQueryConfigElement &&
                    runQueryConfigElement.TryGetProperty("overridePost", out var overridePost) && overridePost.GetBoolean())
                {
                    return new Dictionary<string, object>
                    {
                        { "data", new Dictionary<string, object> { { "queryResults", preQueryResults } } },
                        { "status", "success" }
                    };
                }

                var requestData = metadata
                    .Where(kv => kv.Value != null)
                    .ToDictionary(
                        kv => kv.Key,
                        kv => kv.Value
                    );
                requestData["sdkFilters"] = (filters ?? Enumerable.Empty<QueryFilter>())
                    .Cast<Filter>()
                    .Select(FilterConverter.ConvertCustomFilter);
                if (preQueryResults != null)
                {
                    if (preQueryResults is JsonElement preQueryResultJsonElement)
                    {
                        var preQueryResultsDict = JsonSerializer.Deserialize<Dictionary<string, object>>(preQueryResultJsonElement.GetRawText());
                        if (preQueryResultsDict != null)
                        {
                            foreach (var key in preQueryResultsDict.Keys)
                            {
                                var value = preQueryResultsDict[key];
                                requestData[key] = value;
                            }
                        }
                    }
                    else
                    {
                        var properties = preQueryResults.GetType().GetProperties();
                        foreach (var property in properties)
                        {
                            if (property.GetIndexParameters().Length > 0)
                            {
                                continue; // Skip indexed properties
                            }
                            var key = property.Name;
                            var value = property.GetValue(preQueryResults);
                            requestData[key] = value;
                        }
                    }
                }

                if (tenants != null)
                {
                    requestData["tenants"] = tenants;
                }
                if (tenantFlags != null)
                {
                    requestData["flags"] = tenantFlags.ToArray();
                }

                if (metadata.TryGetValue("preQueries", out var preQueriesCheck))
                {
                    if (preQueriesCheck is JsonElement _preQueriesElement && _preQueriesElement.ValueKind == JsonValueKind.Array)
                    {
                        var firstElement = _preQueriesElement.EnumerateArray().FirstOrDefault();
                        requestData["viewQuery"] = firstElement.ValueKind != JsonValueKind.Undefined
                            ? firstElement.GetString() ?? string.Empty
                            : string.Empty;
                    }
                }

                var response = await PostQuill(
                    metadata.TryGetValue("task", out var finalTask) ? finalTask?.ToString() ?? string.Empty : string.Empty,
                    requestData);

                if (response.ContainsKey("error"))
                {
                    return new Dictionary<string, object>
                    {
                        { "status", "error" },
                        { "error", response["error"] },
                        { "data", response.ContainsKey("metadata") ? response["metadata"] : new Dictionary<string, object>() }
                    };

                }

                if (response.ContainsKey("metadata"))
                {
                    if (response["metadata"] is JsonElement metadataElement)
                    {
                        responseMetadata = JsonSerializer.Deserialize<Dictionary<string, object>>(metadataElement.GetRawText());
                    }
                }
                var _queries = response.ContainsKey("queries")
                    ? JsonSerializer.Deserialize<dynamic[]>(((JsonElement)response["queries"]).GetRawText())
                    : new dynamic[0];
                if (_queries == null)
                {
                    throw new InvalidCastException("response[\"queries\"] is not of type dynamic[].");
                }
                AdditionalProcessing _runQueryConfig = new AdditionalProcessing();
                if (responseMetadata != null && responseMetadata.ContainsKey("runQueryConfig"))
                {
                    if (responseMetadata["runQueryConfig"] is JsonElement runQueryConfigJsonElement)
                    {
                        _runQueryConfig = JsonSerializer.Deserialize<AdditionalProcessing>(runQueryConfigJsonElement.GetRawText()) ?? new AdditionalProcessing();
                    }
                    else
                    {
                        _runQueryConfig = responseMetadata["runQueryConfig"] as AdditionalProcessing ?? new AdditionalProcessing();
                    }
                    if (_runQueryConfig == null)
                    {
                        throw new InvalidCastException("responseMetadata[\"runQueryConfig\"] is not of type AdditionalProcessing.");
                    }
                }

                var databaseType = metadata.TryGetValue("databaseType", out var databaseTypeValue)
                    ? databaseTypeValue?.ToString()
                    : null;

                var queryStrings = _queries.Select(query => query?.ToString()).ToArray();
                var results = await RunQueries(
                    queryStrings.Where(q => q != null).Cast<dynamic>().ToArray(),
                    _targetConnection.DatabaseType,
                    databaseType,
                    _runQueryConfig
                );
                Dictionary<string, object> resultDict = JsonSerializer.Deserialize<Dictionary<string, object>>(
                    JsonSerializer.Serialize(results)
                );
                if (resultDict != null &&
                    resultDict.TryGetValue("mappedArray", out var mappedArrayObj) &&
                    responseMetadata != null && responseMetadata.TryGetValue("runQueryConfig", out var mappedRunQueryConfigObj))
                {
                    List<object> mappedArray = new List<object>();
                    AdditionalProcessing mappedRunQueryConfig = new AdditionalProcessing();

                    if (mappedArrayObj is JsonElement mappedJsonElement)
                    {
                        var deserializedArray = JsonSerializer.Deserialize<List<object>>(mappedJsonElement.GetRawText());
                        if (deserializedArray != null)
                        {
                            mappedArray = deserializedArray;
                        }
                    }
                    else if (mappedArrayObj is List<object> listObj)
                    {
                        mappedArray = listObj;
                    }

                    if (mappedRunQueryConfigObj is JsonElement configJsonElement)
                    {
                        mappedRunQueryConfig = JsonSerializer.Deserialize<AdditionalProcessing>(configJsonElement.GetRawText()) ?? new AdditionalProcessing();
                    }
                    else if (mappedRunQueryConfigObj is AdditionalProcessing config)
                    {
                        mappedRunQueryConfig = config;
                    }

                    if (mappedArray != null &&
                        mappedRunQueryConfig?.ArrayToMap != null &&
                        mappedRunQueryConfig.ArrayToMap.ArrayName != null &&
                        responseMetadata.TryGetValue(mappedRunQueryConfig.ArrayToMap.ArrayName, out var targetArrayObj) &&
                        targetArrayObj is JsonElement targetArrayJsonElement)
                    {
                        List<Dictionary<string, object>> targetArray = targetArrayJsonElement.ValueKind != JsonValueKind.Null
                            ? JsonSerializer.Deserialize<List<Dictionary<string, object>>>(targetArrayJsonElement.GetRawText())!
                            : new List<Dictionary<string, object>>();
                        var modifiedArray = targetArray?.Select((item, index) =>
                        {
                            var newItem = new Dictionary<string, object>(item);
                            if (mappedRunQueryConfig.ArrayToMap.Field != null)
                            {
                                newItem[mappedRunQueryConfig.ArrayToMap.Field] = mappedArray[index];
                            }
                            return newItem;
                        }).ToList();

                        if (modifiedArray != null)
                        {
                            responseMetadata[mappedRunQueryConfig.ArrayToMap.ArrayName] = modifiedArray;
                        }

                        resultDict.Remove("mappedArray");
                    }
                }

                if (resultDict != null && resultDict.TryGetValue("queryResults", out var queryResultsObj))
                {
                    if (queryResultsObj is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Array)
                    {
                        var queryResultsList = jsonElement.EnumerateArray()
                            .Select(element =>
                            {
                                try
                                {
                                    var rawText = element.GetRawText();
                                    return JsonSerializer.Deserialize<QueryResult>(rawText);
                                }
                                catch (Exception)
                                {
                                    return null;
                                }
                            })
                            .Where(x => x != null)
                            .ToList();

                        if (queryResultsList.Count == 1)
                        {
                            var queryResults = queryResultsList[0];

                            if (responseMetadata != null && queryResults?.Rows != null)
                            {
                                responseMetadata["rows"] = queryResults.Rows;
                            }
                            if (responseMetadata != null && queryResults?.Fields != null)
                            {
                                responseMetadata["fields"] = queryResults.Fields;
                            }
                        }
                    }
                }

                return new Dictionary<string, object>
                {
                    { "data", responseMetadata ?? new Dictionary<string, object>() },
                    { "queries", resultDict != null ? resultDict : new Dictionary<string, object>() },
                    { "status", "success" }
                };

            }
            catch (Exception ex)
            {
                if (metadata.TryGetValue("task", out var taskCheck) && taskCheck?.ToString() == "update-view")
                {
                    await PostQuill("set-broken-view", new
                    {
                        table = metadata.TryGetValue("name", out var nameValue) ? nameValue : null,
                        clientId = metadata.TryGetValue("clientId", out var clientIdValue) ? clientIdValue : null,
                        error = ex.Message
                    });
                }

                return new Dictionary<string, object>
                {
                    ["status"] = "error",
                    ["error"] = ex.Message,
                    ["data"] = responseMetadata ?? new Dictionary<string, object>()
                };
            }
        }

        private async Task<dynamic> RunQueries(
            dynamic[] queries, // string or object
            string pkDatabaseType,
            string? databaseType = null,
            AdditionalProcessing? runQueryConfig = null)
        {
            dynamic results = new ExpandoObject();
            if (queries == null || queries.Count() == 0) return new { queryResults = new object[] { } };

            if (!string.IsNullOrEmpty(databaseType) &&
                !string.Equals(databaseType, pkDatabaseType, StringComparison.OrdinalIgnoreCase))
            {
                return new
                {
                    dbMismatched = true,
                    backendDatabaseType = pkDatabaseType,
                    queryResults = new object[] { }
                };
            }

            if (runQueryConfig?.ArrayToMap != null)
            {
                var queriesList = queries.Cast<string>().ToList();
                var mappedArray = await QueryHelper.MapQueriesAsync(queriesList, _targetConnection);
                return new { queryResults = new object[] { }, mappedArray };
            }
            else if (runQueryConfig?.GetColumns == true)
            {
                var queryResult = await _targetConnection.QueryAsync(
                    $"{queries[0].Replace(";", "")} limit 1000");

                var columns = queryResult.Fields.Select(field => new
                {
                    fieldType = PostgresUtils.ConvertTypeToPostgres(field.DataTypeID),
                    name = field.Name,
                    displayName = field.Name,
                    isVisible = true,
                    field = field.Name
                });

                return new { columns };
            }
            else if (runQueryConfig?.GetColumnsForSchema == true)
            {
                var queryResults = await Task.WhenAll(queries.Select(async table =>
                {
                    if (table["viewQuery"] == null || (!table["isSelectStar"] && !table["customFieldInfo"]))
                    {
                        return table;
                    }

                    string limit = runQueryConfig.LimitBy != null ? $" limit {runQueryConfig.LimitBy}" : "";

                    try
                    {
                        var queryResult = await _targetConnection.QueryAsync(
                            $"{table.ViewQuery.Replace(";", "")} {limit}");

                        var columns = queryResult.Fields.Select(field => new
                        {
                            fieldType = PostgresUtils.ConvertTypeToPostgres(field.DataTypeID),
                            name = field.Name,
                            displayName = field.Name,
                            isVisible = true,
                            field = field.Name
                        });
                        var returnData = table.GetType()
                            .GetProperties()
                            .ToDictionary(
                                (Func<PropertyInfo, string>)(prop => prop.Name),
                                (Func<PropertyInfo, object>)(prop => prop.GetValue(table))
                            );
                        returnData["columns"] = columns;
                        returnData["rows"] = queryResult.Rows;
                        return returnData;
                    }
                    catch (Exception ex)
                    {
                        return new
                        {
                            table,
                            error = !string.IsNullOrEmpty(ex.Message)
                                ? $"Error fetching columns: {ex.Message}"
                                : "Error fetching columns"
                        };
                    }
                }));

                results.queryResults = queryResults;

                if (runQueryConfig?.FieldsToRemove != null)
                {
                    results.queryResults = queryResults.Select(table =>
                    {
                        var removedColumns = table.columns.Where(
                            (Func<Dictionary<string, string>, bool>)(column => !runQueryConfig.FieldsToRemove.Contains(column["name"]))
                        );
                        var returnData = table.GetType()
                            .GetProperties()
                            .ToDictionary(
                                (Func<PropertyInfo, string>)(prop => prop.Name),
                                (Func<PropertyInfo, object>)(prop => prop.GetValue(table))
                            );
                        returnData["columns"] = removedColumns;
                        return returnData;
                    });
                }
            }
            else if (runQueryConfig?.GetTables == true)
            {
                var schema = runQueryConfig.SchemaNames?.Any() == true ? runQueryConfig.SchemaNames : new List<string> { runQueryConfig.Schema ?? string.Empty };

                var queryResult = await DatabaseHelper.GetTablesBySchemaAsync(
                    _targetConnection.DatabaseType,
                    _targetConnection.GetPool(),
                    schema);

                if (runQueryConfig.Schema == null)
                {
                    throw new ArgumentNullException(nameof(runQueryConfig.Schema), "Schema cannot be null.");
                }

                var schemaInfo = await DatabaseHelper.GetColumnInfoBySchemaAsync(
                    _targetConnection.DatabaseType,
                    _targetConnection.GetPool(),
                    runQueryConfig.Schema,
                    queryResult);

                return schemaInfo;
            }
            else
            {
                if (runQueryConfig?.LimitThousand == true)
                {
                    queries = queries.Select(q => $"{q.Replace(";", "")} limit 1000;").ToArray();
                }
                else if (runQueryConfig?.LimitBy != null)
                {
                    queries = queries.Select(q => $"{q.Replace(";", "")} limit {runQueryConfig.LimitBy}").ToArray();
                }

                var queryResults = await Task.WhenAll(
                    queries.Select(async query => await _targetConnection.QueryAsync(query.ToString())));

                results.queryResults = queryResults;
                if (runQueryConfig?.FieldsToRemove != null)
                {
                    results.queryResults = queryResults.Select(result =>
                        QueryHelper.RemoveFields(result, runQueryConfig.FieldsToRemove));
                }

                if (runQueryConfig?.ConvertDatatypes == true)
                {
                    results = queryResults.Cast<QueryResult>().Select(result => new
                    {
                        fields = result.Fields.Select(field =>
                        {
                            var newField = new Dictionary<string, object>();

                            newField["name"] = field.Name;
                            newField["dataTypeID"] = field.DataTypeID;


                            newField["fieldType"] = PostgresUtils.ConvertTypeToPostgres(field.DataTypeID);
                            newField["isVisible"] = true;
                            newField["field"] = field.Name;
                            newField["displayName"] = field.Name;
                            newField["name"] = field.Name;

                            return newField.Where(kv => kv.Value != null).ToDictionary(kv => kv.Key, kv => (object)kv.Value!);
                        }),
                        rows = result.Rows
                    });
                }



            }

            return results;
        }
        private async Task<IDictionary<string, object>> PostQuill(string path, object payload)
        {
            try
            {
                var response = await _httpClient.PostAsync(
                    $"{_baseUrl}/sdk/{path}",
                    new StringContent(
                        JsonSerializer.Serialize(payload),
                        System.Text.Encoding.UTF8,
                        "application/json"
                    )
                );

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
                return result ?? new Dictionary<string, object>();
            }
            catch (JsonException ex)
            {
                throw new QuillApiException($"Invalid JSON response: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new QuillApiException(ex.Message);
            }
        }

        public async void Dispose()
        {
            await Close();
        }

        public async Task Close()
        {
            if (_targetConnection != null)
            {
                await _targetConnection.CloseAsync();
            }
        }
    }
}