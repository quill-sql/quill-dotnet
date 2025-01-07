using System;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Collections.Generic;
using Quill.Database;

namespace Quill.Connection
{
    public interface IMappable
    {
        Task<string> GetAsync(string key);
        Task SetAsync(string key, string value, string flag, int expiry);
        Task ConnectAsync();
    }

    public class CachedConnection : IDisposable
    {
        private const int DEFAULT_CACHE_TTL = 24 * 60 * 60; // 24 hours in seconds
        private bool _isClosed = false;

        public string DatabaseType { get; }
        public IDatabaseConnection Pool { get; }
        public IEnumerable<object>? TenantIds { get; set; }
        public int Ttl { get; }
        public IMappable? Cache { get; }

        public bool IsClosed => _isClosed;

        public CachedConnection(
            string databaseType,
            IDatabaseConfig config,
            CacheCredentials? cacheConfig = null)
        {
            if (!IsValidDatabaseType(databaseType))
                throw new ArgumentException("Invalid database type", nameof(databaseType));

            DatabaseType = databaseType;
            Pool = DatabaseHelper.ConnectToDatabase(databaseType, config);
            Ttl = cacheConfig?.Ttl ?? DEFAULT_CACHE_TTL;
            Cache = cacheConfig != null ? GetCache(cacheConfig) : null;
        }

        public async Task<QueryResult> QueryAsync(string text)
        {
            try
            {
                if (IsClosed)
                    throw new InvalidOperationException("Connection is closed");

                if (Cache == null)
                    return await DatabaseHelper.RunQueryByDatabaseAsync(DatabaseType, Pool, text);

                string key = $"{string.Join(",", TenantIds ?? Array.Empty<object>())}:{text}";
                string cachedResult = await Cache.GetAsync(key);

                if (!string.IsNullOrEmpty(cachedResult))
                    return JsonSerializer.Deserialize<QueryResult>(cachedResult)!;

                var newResult = await DatabaseHelper.RunQueryByDatabaseAsync(DatabaseType, Pool, text);
                string newResultString = JsonSerializer.Serialize(newResult);
                await Cache.SetAsync(key, newResultString, "EX", DEFAULT_CACHE_TTL);
                return newResult;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        private IMappable? GetCache(CacheCredentials config)
        {
            if (config == null) return null;

            if (config.CacheType == "redis" || config.CacheType == "rediss")
            {
                return new RedisCache(config);
            }

            return null;
        }

        public IDatabaseConnection GetPool() => Pool;

        public async Task CloseAsync()
        {
            await DatabaseHelper.DisconnectFromDatabaseAsync(DatabaseType, Pool);
            _isClosed = true;
        }

        public void Dispose()
        {
            if (!_isClosed)
            {
                CloseAsync().GetAwaiter().GetResult();
            }
            Pool?.DataSource?.Dispose();
            (Cache as IDisposable)?.Dispose();
        }

        private bool IsValidDatabaseType(string type) => type switch
        {
            "postgresql" or "snowflake" or "bigquery" or "mysql" or "clickhouse" => true,
            _ => false
        };
    }

    public class RedisCache : IMappable, IDisposable
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisCache(CacheCredentials config)
        {
            var redisUrl = $"{config.CacheType}://{config.Username}:{config.Password}@{config.Host}:{config.Port}";
            _redis = ConnectionMultiplexer.Connect(redisUrl);
            _db = _redis.GetDatabase();
        }

        public async Task ConnectAsync()
        {
            // Connection is handled in constructor for Redis
            await Task.CompletedTask;
        }

        public async Task<string> GetAsync(string key)
        {
            var result = await _db.StringGetAsync(key);
            return result.HasValue ? result.ToString() : string.Empty;
        }

        public async Task SetAsync(string key, string value, string flag, int expiry)
        {
            await _db.StringSetAsync(key, value, TimeSpan.FromSeconds(expiry));
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }
    }

    public class CacheCredentials
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int? Port { get; set; }
        public string? CacheType { get; set; }
        public int? Ttl { get; set; }
    }

    public class DatabaseException : Exception
    {
        public string? Detail { get; }
        public string? Hint { get; }
        public string? Position { get; }

        public DatabaseException(string message) : base(message) { }

        public DatabaseException(string message, string detail, string hint, string position)
            : base(message)
        {
            Detail = detail;
            Hint = hint;
            Position = position;
        }
    }

    public interface IDatabaseConnection
    {
        public dynamic DataSource { get; }
    }

}