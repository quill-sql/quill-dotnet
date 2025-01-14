using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using System.Linq;
using System.Text.Json.Serialization;
using Quill.Connection;
using Quill.Postgres;
using Quill.Mssql;

namespace Quill.Database
{
    public class DatabaseHelper
    {
        public static IDatabaseConfig GetDatabaseCredentials(string databaseType, string connectionString, bool isPooled)
        {
            switch (databaseType.ToLower())
            {
                case "postgres":
                case "postgresql":
                    return PostgresUtils.FormatPostgresConfig(connectionString, isPooled);
                case "mssql":
                case "sqlserver":
                case "transactsql":
                    return MssqlUtils.FormatMssqlConfig(connectionString, isPooled);
                case "snowflake":
                case "bigquery":
                case "mysql":
                case "clickhouse":
                    throw new NotImplementedException($"Database type {databaseType} not implemented");
                default:
                    throw new ArgumentException("Invalid database type");
            }
        }

        public static IDatabaseConnection ConnectToDatabase(string databaseType, IDatabaseConfig config)
        {
            switch (databaseType.ToLower())
            {
                case "postgres":
                case "postgresql":
                    return PostgresUtils.ConnectToPostgres((PostgresConnectionConfig)config);
                case "mssql":
                case "sqlserver":
                case "transactsql":
                    return MssqlUtils.ConnectToMssql((MssqlConnectionConfig)config);
                case "snowflake":
                case "bigquery":
                case "mysql":
                case "clickhouse":
                    throw new NotImplementedException($"Database type {databaseType} not implemented");
                default:
                    throw new ArgumentException("Invalid database type");
            }
        }

        public static async Task<QueryResult> RunQueryByDatabaseAsync(
            string databaseType,
            IDatabaseConnection connection,
            string sql)
        {
            switch (databaseType.ToLower())
            {
                case "postgres":
                case "postgresql":
                    return await PostgresUtils.RunQueryPostgresAsync(sql, connection.DataSource);
                case "mssql":
                case "sqlserver":
                case "transactsql":
                    return await MssqlUtils.RunQueryMssqlAsync(sql, connection.DataSource);
                case "snowflake":
                case "bigquery":
                case "mysql":
                case "clickhouse":
                    throw new NotImplementedException($"Database type {databaseType} not implemented");
                default:
                    throw new ArgumentException("Invalid database type");
            }
        }

        public static async Task DisconnectFromDatabaseAsync(
            string databaseType,
            IDatabaseConnection connection)
        {
            switch (databaseType.ToLower())
            {
                case "postgres":
                case "postgresql":
                    await PostgresUtils.DisconnectFromPostgresAsync(connection.DataSource);
                    break;
                case "mssql":
                case "sqlserver":
                case "transactsql":
                    await MssqlUtils.DisconnectFromMssqlAsync(connection.DataSource);
                    break;
                case "snowflake":
                case "bigquery":
                case "mysql":
                case "clickhouse":
                    throw new NotImplementedException($"Database type {databaseType} not implemented");
                default:
                    throw new ArgumentException("Invalid database type");
            }
        }

        public static async Task<IEnumerable<string>> GetSchemasByDatabaseAsync(
            string databaseType,
            IDatabaseConnection connection)
        {
            switch (databaseType.ToLower())
            {
                case "postgres":
                case "postgresql":
                    return await PostgresUtils.GetSchemasPostgresAsync(connection.DataSource);
                case "mssql":
                case "sqlserver":
                case "transactsql":
                    return await MssqlUtils.GetSchemasMssqlAsync(connection.DataSource);
                case "snowflake":
                case "bigquery":
                case "mysql":
                case "clickhouse":
                    throw new NotImplementedException($"Database type {databaseType} not implemented");
                default:
                    throw new ArgumentException("Invalid database type");
            }
        }

        public static async Task<IEnumerable<TableInfo>> GetTablesBySchemaAsync(
            string databaseType,
            IDatabaseConnection connection,
            IEnumerable<string> schemaNames)
        {
            switch (databaseType.ToLower())
            {
                case "postgres":
                case "postgresql":
                    return await PostgresUtils.GetTablesBySchemaPostgresAsync(
                        connection.DataSource,
                        schemaNames.ToList());
                case "mssql":
                case "sqlserver":
                case "transactsql":
                    return await MssqlUtils.GetTablesBySchemaMssqlAsync(
                        connection.DataSource,
                        schemaNames.ToList());
                case "snowflake":
                case "bigquery":
                case "mysql":
                case "clickhouse":
                    throw new NotImplementedException($"Database type {databaseType} not implemented");
                default:
                    throw new ArgumentException("Invalid database type");
            }
        }

        public static async Task<IEnumerable<SchemaColumnInfo>> GetColumnInfoBySchemaAsync(
            string databaseType,
            IDatabaseConnection connection,
            string schemaName,
            IEnumerable<TableInfo> tables)
        {
            switch (databaseType.ToLower())
            {
                case "postgres":
                case "postgresql":
                    return await PostgresUtils.GetSchemaColumnInfoPostgresAsync(
                        connection.DataSource,
                        schemaName,
                        tables.ToList());
                case "mssql":
                case "sqlserver":
                case "transactsql":
                    return await MssqlUtils.GetSchemaColumnInfoMssqlAsync(
                        connection.DataSource,
                        schemaName,
                        tables.ToList());
                case "snowflake":
                case "bigquery":
                case "mysql":
                case "clickhouse":
                    throw new NotImplementedException($"Database type {databaseType} not implemented");
                default:
                    throw new ArgumentException("Invalid database type");
            }
        }
    }

    public interface IDatabaseConfig { }
    public class TableInfo
    {
        [JsonPropertyName("schemaName")]
        public string? SchemaName { get; set; }

        [JsonPropertyName("tableName")]
        public string? TableName { get; set; }
    }
    public class SchemaColumnInfo
    {
        [JsonPropertyName("tableName")]
        public string? TableName { get; set; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("columns")]
        public List<ColumnInfo>? Columns { get; set; }
    }

    public class ColumnInfo
    {
        [JsonPropertyName("columnName")]
        public string? ColumnName { get; set; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("dataTypeID")]
        public int? DataTypeID { get; set; }

        [JsonPropertyName("fieldType")]
        public string? FieldType { get; set; }
    }

    public class QueryResult
    {
        [JsonPropertyName("rows")]
        public IEnumerable<IDictionary<string, object>> Rows { get; set; } = new List<IDictionary<string, object>>();

        [JsonPropertyName("fields")]
        public IEnumerable<Field> Fields { get; set; } = new List<Field>();
    }

    public class Field
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("dataTypeID")]
        public int DataTypeID { get; set; } = 0;
    }
}