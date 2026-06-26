using System.Threading.Tasks;
using IMDBAPI.Services.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace IMDBAPI.Services
{
    public class RedisService<T> where T : class
    {
        private readonly IDatabase _db;
        public RedisService(IConnectionMultiplexer connection) {
            _db = connection.GetDatabase();
        }

        public async Task SetAsync(string key, T value)
        {
            string stringValue = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key, stringValue);
        }

        public async Task<T?> GetValueAsync(string key)
        {
            string? stringValue = await _db.StringGetAsync(key);
            if (stringValue == null)
            {
                return null;
            }
            return JsonSerializer.Deserialize<T>(stringValue);
        }

        public async Task DeleteAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }
    }
}
