using System.Threading.Tasks;
using Newtonsoft.Json;

namespace utils.Redis
{
    public class RedisSet<T>
    {
        private string _key;
        private RedisContext _redis;

        public RedisSet(string key, RedisContext redis)
        {
            _key = key;
            _redis = redis;
        }

        public async Task<bool> AddAsync(T entity)
        {
            var result = await _redis.SAddAsync(_key, JsonConvert.SerializeObject(entity));

            return result == 1;
        }

        public bool Add(T entity)
        {
            var result = _redis.SAdd(_key, JsonConvert.SerializeObject(entity));

            return result == 1;
        }
    }
}