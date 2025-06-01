using Comandas.Data.Interface;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Comandas.Data.Implementation
{


    public class RedisRepository : IRedisRepository
    {

        private readonly IDatabase _database;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _database = _connectionMultiplexer.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string Chave)
        {
            var dado = await _database.StringGetAsync(Chave);

            if (dado.IsNullOrEmpty)
            {
                return default;
            }

            var configJson = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include
            };

            return JsonConvert.DeserializeObject<T>(dado, configJson);

        }

        public async Task<bool> RemoveAsync(string Chave)
        {
            return await _database.KeyDeleteAsync(Chave);
        }

        public async Task<bool> SaveAsync<T>(string Chave, T dados, TimeSpan? TempoVida)
        {

            var configJson = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include
            };

            var serialize = JsonConvert.SerializeObject(dados, configJson);

            return await _database.StringSetAsync(Chave, serialize, TempoVida);
        }
    }

}