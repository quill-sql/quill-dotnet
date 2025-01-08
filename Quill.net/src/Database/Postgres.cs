using Npgsql;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using Quill.Database;
using Quill.Connection;
using System.Text.Json;

namespace Quill.Postgres
{
    public class PostgresConnectionConfig : IDatabaseConfig
    {
        public string ConnectionString { get; set; } = string.Empty;
        public PostgresSslConfig? Ssl { get; set; }
        public bool IsPooled { get; set; } = false;
    }

    public class PostgresSslConfig
    {
        public bool RejectUnauthorized { get; set; }
        public string? Ca { get; set; }
        public string? Key { get; set; }
        public string? Cert { get; set; }
    }

    public static class PostgresUtils
    {
        public static IDatabaseConnection ConnectToPostgres(PostgresConnectionConfig config)
        {
            if (config == null || string.IsNullOrEmpty(config.ConnectionString))
            {
                throw new ArgumentException("Invalid configuration for PostgreSQL connection");
            }

            var builder = new NpgsqlConnectionStringBuilder(config.ConnectionString)
            {
                Pooling = true,
                NoResetOnClose = config.IsPooled,
            };

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.ConnectionString);

            var dataSource = dataSourceBuilder.Build();

            return new PostgresDatabaseConnection(dataSource);
        }

        public static async Task DisconnectFromPostgresAsync(NpgsqlDataSource dataSource)
        {
            await Task.Run(() => dataSource.Dispose());
        }

        public static async Task<QueryResult> RunQueryPostgresAsync(string sql, NpgsqlDataSource dataSource)
        {
            try
            {
                await using var command = dataSource.CreateCommand(sql);
                await using var reader = await command.ExecuteReaderAsync();

                var fields = new List<Field>();
                var rows = new List<Dictionary<string, object>>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fields.Add(new Field
                    {
                        Name = reader.GetName(i),
                        DataTypeID = (int)reader.GetPostgresType(i).OID
                    });
                }

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        if (reader.GetDataTypeName(i) == "json" || reader.GetDataTypeName(i) == "jsonb")
                        {
                            var root = JsonDocument.Parse(value?.ToString() ?? string.Empty).RootElement;

                            if (root.ValueKind == JsonValueKind.Array)
                            {
                                value = root.EnumerateArray().Select<JsonElement, object>(jv => jv.ValueKind switch
                                {
                                    JsonValueKind.String => jv.GetString() ?? string.Empty,
                                    JsonValueKind.Number => jv.GetDouble(),
                                    JsonValueKind.True => true,
                                    JsonValueKind.False => false,
                                    _ => jv.GetString() ?? string.Empty
                                }).ToArray();
                            }
                            else if (root.ValueKind == JsonValueKind.Object)
                            {
                                value = root.EnumerateObject().ToDictionary(jv => jv.Name, jv => (object)(jv.Value.ValueKind switch
                                {
                                    JsonValueKind.String => jv.Value.GetString() ?? string.Empty,
                                    JsonValueKind.Number => jv.Value.GetDouble(),
                                    JsonValueKind.True => true,
                                    JsonValueKind.False => false,
                                    _ => jv.Value.GetString() ?? string.Empty
                                }));
                            }
                        }

                        row[reader.GetName(i)] = value!;
                    }
                    rows.Add(row);
                }

                return new QueryResult { Fields = fields, Rows = rows };
            }
            catch (PostgresException pgEx)
            {
                string errorMessage = pgEx.Message;
                int firstColon = errorMessage.IndexOf(':');
                int positionText = errorMessage.IndexOf("POSITION");

                if (firstColon >= 0 && positionText >= 0)
                {
                    errorMessage = errorMessage.Substring(firstColon + 1, positionText - firstColon - 1).Trim();
                }

                throw new Exception(errorMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<string>> GetSchemasPostgresAsync(NpgsqlDataSource dataSource)
        {
            const string sql = @"SELECT schema_name FROM information_schema.schemata 
            WHERE schema_name NOT LIKE 'pg_%' AND schema_name != 'information_schema';";
            var results = await RunQueryPostgresAsync(sql, dataSource);
            return results.Rows.Select(row => (string)row["schema_name"]).ToList();
        }

        public static async Task<List<TableInfo>> GetTablesBySchemaPostgresAsync(
            NpgsqlDataSource dataSource,
            List<string> schemaNames)
        {
            var allTables = new List<TableInfo>();

            foreach (var schema in schemaNames)
            {
                var sql = $@"
                SELECT table_name, table_schema
                FROM information_schema.tables
                WHERE table_schema = '{schema}'
                
                UNION
                
                SELECT c.relname as table_name, n.nspname as table_schema
                FROM pg_class c
                JOIN pg_namespace n ON c.relnamespace = n.oid
                WHERE n.nspname = '{schema}' 
                AND c.relkind = 'm';";

                var results = await RunQueryPostgresAsync(sql, dataSource);
                allTables.AddRange(results.Rows.Select(row => new TableInfo
                {
                    TableName = (string)row["table_name"],
                    SchemaName = (string)row["table_schema"]
                }));
            }

            return allTables;
        }

        public static async Task<List<string>> GetColumnsByTablePostgresAsync(
            NpgsqlDataSource dataSource,
            string schemaName,
            string tableName)
        {
            var sql = $@"
            SELECT a.attname AS column_name 
            FROM pg_attribute a 
            JOIN pg_class c ON a.attrelid = c.oid 
            JOIN pg_namespace n ON c.relnamespace = n.oid 
            WHERE n.nspname = '{schemaName}'
            AND c.relname = '{tableName}'
            AND c.relkind IN ('r', 'm') 
            AND a.attnum > 0 
            AND NOT a.attisdropped";

            var results = await RunQueryPostgresAsync(sql, dataSource);
            return results.Rows.Select(row => (string)row["column_name"]).ToList();
        }

        public static async Task<List<SchemaColumnInfo>> GetSchemaColumnInfoPostgresAsync(
            NpgsqlDataSource dataSource,
            string schemaName,
            List<TableInfo> tableNames)
        {
            var allColumns = new List<SchemaColumnInfo>();

            foreach (var table in tableNames)
            {
                var query = $@"
                SELECT column_name as ""columnName"", udt_name as ""fieldType"", ordinal_position as ""sortNumber""
                FROM information_schema.columns
                WHERE table_schema = '{table.SchemaName}' 
                    AND table_name = '{table.TableName}'
                
                UNION

                SELECT a.attname as ""columnName"", t.typname as ""fieldType"", a.attnum as ""sortNumber""
                FROM pg_attribute a
                JOIN pg_class c ON a.attrelid = c.oid
                JOIN pg_namespace n ON c.relnamespace = n.oid
                JOIN pg_type t ON a.atttypid = t.oid
                WHERE n.nspname = '{table.SchemaName}'
                    AND c.relname = '{table.TableName}'
                    AND c.relkind = 'm'
                    AND a.attnum > 0
                    AND NOT a.attisdropped
                ORDER BY ""sortNumber""";

                var results = await RunQueryPostgresAsync(query, dataSource);

                allColumns.Add(new SchemaColumnInfo
                {
                    TableName = $"{table.SchemaName}.{table.TableName}",
                    DisplayName = $"{table.SchemaName}.{table.TableName}",
                    Columns = results.Rows.Select(row => new ColumnInfo
                    {
                        ColumnName = (string)row["columnName"],
                        DisplayName = (string)row["columnName"],
                        DataTypeID = GetPgTypeOid((string)row["fieldType"]),
                        FieldType = (string)row["fieldType"]
                    }).ToList()
                });
            }

            return allColumns;
        }

        private static int GetPgTypeOid(string typeName)
        {
            var pgType = PgTypes.PG_TYPES.FirstOrDefault(pt => pt.Typname.Equals(typeName, StringComparison.OrdinalIgnoreCase));

            return pgType?.Oid ?? 1043;
        }

        public static string ConvertTypeToPostgres(int dataTypeId)
        {
            var pgType = PgTypes.PG_TYPES.FirstOrDefault(type => type.Oid == dataTypeId);

            if (pgType == null)
            {
                return "varchar";
            }

            return pgType.Typname;
        }

        public class PostgresDatabaseConnection : IDatabaseConnection
        {
            private readonly NpgsqlDataSource _dataSource;

            public PostgresDatabaseConnection(NpgsqlDataSource dataSource)
            {
                _dataSource = dataSource;
            }

            public dynamic DataSource => _dataSource;
        }

        public static PostgresConnectionConfig FormatPostgresConfig(string connectionString, bool isPooled = false)
        {
            Uri uri = new Uri(connectionString);

            // Extract components from the URI
            var username = uri.UserInfo.Split(':')[0];  // Extract the username
            var password = uri.UserInfo.Split(':')[1];  // Extract the password
            var host = uri.Host;                       // Extract the host
            var port = uri.Port.ToString();            // Extract the port
            var database = uri.AbsolutePath.TrimStart('/'); // Extract the database (e.g., postgres)

            // Format the connection string into the new format
            string formattedConnectionString = $"Host={host};Username={username};Password={password};Port={port};Database={database}";

            return new PostgresConnectionConfig
            {
                ConnectionString = formattedConnectionString,
                Ssl = new PostgresSslConfig { RejectUnauthorized = false },
                IsPooled = isPooled,
            };
        }
    }
}