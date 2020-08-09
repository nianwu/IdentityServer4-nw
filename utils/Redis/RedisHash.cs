using System.Threading.Tasks;
using Newtonsoft.Json;

namespace utils.Redis
{
    public class RedisHash<T>
    {
        private string _key;
        private RedisContext _redis;

        public RedisHash(string key, RedisContext redis)
        {
            _key = key;
            _redis = redis;
        }

        public async Task<bool> SetAsync(string key, T value)
        {
            var result = await _redis.HSetAsync(_key, key, JsonConvert.SerializeObject(value));
            return result;
        }
    }
}