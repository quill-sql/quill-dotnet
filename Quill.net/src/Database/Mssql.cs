using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using Quill.Database;
using Quill.Connection;
using System.Text.Json;

namespace Quill.Mssql
{
    public class MssqlConnectionConfig : IDatabaseConfig
    {
        public string ConnectionString { get; set; } = string.Empty;
        public bool IsPooled { get; set; } = false;
    }

    public static class MssqlUtils
    {
        public static IDatabaseConnection ConnectToMssql(MssqlConnectionConfig config)
        {
            if (config == null || string.IsNullOrEmpty(config.ConnectionString))
            {
                throw new ArgumentException("Invalid configuration for MSSQL connection");
            }

            var builder = new SqlConnectionStringBuilder(config.ConnectionString)
            {
                Pooling = true,
                MultipleActiveResultSets = config.IsPooled,
            };

            return new MssqlDatabaseConnection(builder.ConnectionString);
        }

        public static async Task DisconnectFromMssqlAsync(SqlConnection connection)
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                await connection.CloseAsync();
                await Task.Run(() => connection.Dispose());
            }
        }

        public static async Task<QueryResult> RunQueryMssqlAsync(string sql, string connectionString)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(sql, connection);
                using var reader = await command.ExecuteReaderAsync();

                var fields = new List<Field>();
                var rows = new List<Dictionary<string, object>>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    fields.Add(new Field
                    {
                        Name = reader.GetName(i),
                        DataTypeID = GetSqlTypeId(reader.GetFieldType(i).Name)
                    });
                }

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.IsDBNull(i) ? null : reader.GetValue(i);

                        // Handle JSON data types
                        if (value != null && (
                            reader.GetDataTypeName(i).Equals("nvarchar(max)", StringComparison.OrdinalIgnoreCase) ||
                            reader.GetDataTypeName(i).Equals("varchar(max)", StringComparison.OrdinalIgnoreCase)))
                        {
                            try
                            {
                                var root = JsonDocument.Parse(value.ToString() ?? string.Empty).RootElement;

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
                            catch
                            {
                                // If JSON parsing fails, keep the original value
                            }
                        }

                        row[reader.GetName(i)] = value!;
                    }
                    rows.Add(row);
                }

                return new QueryResult { Fields = fields, Rows = rows };
            }
            catch (SqlException sqlEx)
            {
                string errorMessage = sqlEx.Message;
                throw new Exception(errorMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<string>> GetSchemasMssqlAsync(string connectionString)
        {
            const string sql = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME != 'INFORMATION_SCHEMA';";
            var results = await RunQueryMssqlAsync(sql, connectionString);
            return results.Rows.Select(row => (string)row["SCHEMA_NAME"]).ToList();
        }

        public static async Task<List<TableInfo>> GetTablesBySchemaMssqlAsync(
            string connectionString,
            List<string> schemaNames)
        {
            var allTables = new List<TableInfo>();

            foreach (var schema in schemaNames)
            {
                var sql = $@"
                SELECT TABLE_NAME, TABLE_SCHEMA
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_SCHEMA = '{schema}'
                AND TABLE_TYPE = 'BASE TABLE'";

                var results = await RunQueryMssqlAsync(sql, connectionString);
                allTables.AddRange(results.Rows.Select(row => new TableInfo
                {
                    TableName = (string)row["TABLE_NAME"],
                    SchemaName = (string)row["TABLE_SCHEMA"]
                }));
            }

            return allTables;
        }

        public static async Task<List<string>> GetColumnsByTableMssqlAsync(
            string connectionString,
            string schemaName,
            string tableName)
        {
            var sql = $@"
            SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = '{schemaName}'
            AND TABLE_NAME = '{tableName}'
            ORDER BY ORDINAL_POSITION";

            var results = await RunQueryMssqlAsync(sql, connectionString);
            return results.Rows.Select(row => (string)row["COLUMN_NAME"]).ToList();
        }

        public static async Task<List<SchemaColumnInfo>> GetSchemaColumnInfoMssqlAsync(
            string connectionString,
            string schemaName,
            List<TableInfo> tableNames)
        {
            var allColumns = new List<SchemaColumnInfo>();

            foreach (var table in tableNames)
            {
                var query = $@"
                SELECT 
                    COLUMN_NAME as columnName,
                    DATA_TYPE as fieldType,
                    ORDINAL_POSITION as sortNumber
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = '{table.SchemaName}'
                AND TABLE_NAME = '{table.TableName}'
                ORDER BY ORDINAL_POSITION";

                var results = await RunQueryMssqlAsync(query, connectionString);

                allColumns.Add(new SchemaColumnInfo
                {
                    TableName = $"{table.SchemaName}.{table.TableName}",
                    DisplayName = $"{table.SchemaName}.{table.TableName}",
                    Columns = results.Rows.Select(row => new ColumnInfo
                    {
                        ColumnName = (string)row["columnName"],
                        DisplayName = (string)row["columnName"],
                        DataTypeID = GetSqlTypeId((string)row["fieldType"]),
                        FieldType = (string)row["fieldType"]
                    }).ToList()
                });
            }

            return allColumns;
        }
        private static int GetSqlTypeId(string typeName)
        {
            switch (typeName.ToLower())
            {
                // Numeric Types
                case "bigint": return 20;      // maps to pg BIGINT
                case "int": return 23;         // maps to pg INTEGER
                case "smallint": return 21;    // maps to pg SMALLINT
                case "tinyint": return 21;     // maps to pg SMALLINT
                case "decimal": return 1700;   // maps to pg NUMERIC
                case "numeric": return 1700;   // maps to pg NUMERIC
                case "money": return 790;      // maps to pg MONEY
                case "smallmoney": return 790; // maps to pg MONEY
                case "float": return 701;      // maps to pg DOUBLE PRECISION
                case "real": return 700;       // maps to pg REAL
                case "bit": return 16;         // maps to pg BOOLEAN

                // Character Types
                case "char": return 1042;      // maps to pg CHAR
                case "varchar": return 1043;   // maps to pg VARCHAR
                case "text": return 25;        // maps to pg TEXT
                case "nchar": return 1042;     // maps to pg CHAR
                case "nvarchar": return 1043;  // maps to pg VARCHAR
                case "ntext": return 25;       // maps to pg TEXT

                // Binary Types
                case "binary": return 17;      // maps to pg BYTEA
                case "varbinary": return 17;   // maps to pg BYTEA
                case "image": return 17;       // maps to pg BYTEA

                // Date/Time Types
                case "date": return 1082;      // maps to pg DATE
                case "datetime": return 1114;  // maps to pg TIMESTAMP
                case "datetime2": return 1114; // maps to pg TIMESTAMP
                case "smalldatetime": return 1114; // maps to pg TIMESTAMP
                case "time": return 1083;      // maps to pg TIME
                case "datetimeoffset": return 1184; // maps to pg TIMESTAMPTZ

                // Other Types
                case "uniqueidentifier": return 2950; // maps to pg UUID
                case "xml": return 142;        // maps to pg XML
                case "json": return 114;       // maps to pg JSON
                case "sql_variant": return 1043; // maps to pg VARCHAR as fallback

                // Spatial Types
                case "geometry": return 17;    // maps to pg BYTEA
                case "geography": return 17;   // maps to pg BYTEA

                default: return 1043;          // Default to VARCHAR
            }
        }

        public static string ConvertTypeToMssql(int pgTypeOid)
        {
            switch (pgTypeOid)
            {
                // Numeric Types
                case 20: return "bigint";      // pg BIGINT
                case 23: return "int";         // pg INTEGER
                case 21: return "smallint";    // pg SMALLINT
                case 1700: return "decimal";   // pg NUMERIC
                case 790: return "money";      // pg MONEY
                case 701: return "float";      // pg DOUBLE PRECISION
                case 700: return "real";       // pg REAL
                case 16: return "bit";         // pg BOOLEAN

                // Character Types
                case 1042: return "char";      // pg CHAR
                case 1043: return "varchar";   // pg VARCHAR
                case 25: return "text";        // pg TEXT

                // Binary Types
                case 17: return "varbinary";   // pg BYTEA

                // Date/Time Types
                case 1082: return "date";      // pg DATE
                case 1114: return "datetime2"; // pg TIMESTAMP
                case 1083: return "time";      // pg TIME
                case 1184: return "datetimeoffset"; // pg TIMESTAMPTZ

                // Other Types
                case 2950: return "uniqueidentifier"; // pg UUID
                case 142: return "xml";        // pg XML
                case 114: return "json";       // pg JSON

                // Special Types
                case 3802: return "json";      // pg JSONB
                case 18: return "char";        // pg CHAR
                case 19: return "varchar";     // pg NAME
                case 22: return "int";         // pg INT2VECTOR
                case 24: return "varchar";     // pg REGPROC
                case 26: return "int";         // pg OID
                case 27: return "int";         // pg TID
                case 28: return "int";         // pg XID
                case 29: return "int";         // pg CID

                default: return "varchar";     // Default to VARCHAR
            }
        }

        public class MssqlDatabaseConnection : IDatabaseConnection
        {
            private readonly string _connectionString;

            public MssqlDatabaseConnection(string connectionString)
            {
                _connectionString = connectionString;
            }

            public dynamic DataSource => _connectionString;
        }

        public static MssqlConnectionConfig FormatMssqlConfig(string connectionString, bool isPooled = false)
        {
            // Assuming the connection string is already in the correct format for MSSQL
            return new MssqlConnectionConfig
            {
                ConnectionString = connectionString,
                IsPooled = isPooled,
            };
        }
    }
}