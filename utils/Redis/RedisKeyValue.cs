using System.Threading.Tasks;
using Newtonsoft.Json;

namespace utils.Redis
{
    public class RedisKeyValue<T>
    {
        private string _key;
        private RedisContext _redis;

        public RedisKeyValue(string key, RedisContext redis)
        {
            _key = key;
            _redis = redis;
        }

        public async Task<T> GetAsync()
        {
            var result = await _redis.GetAsync<string>(_key);

            if (string.IsNullOrEmpty(result))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<bool> SetAsync(T value)
        {
            var result = await _redis.SetAsync(_key, JsonConvert.SerializeObject(value));
            return result;
        }

        public T Get()
        {
            var result = _redis.Get<string>(_key);

            if (string.IsNullOrEmpty(result))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        public bool Set(T value)
        {
            var result = _redis.Set(_key, JsonConvert.SerializeObject(value));
            return result;
        }
    }

    public class RedisKeyValue
    {
        private string _key;
        private RedisContext _redis;

        public RedisKeyValue(string key, RedisContext redis)
        {
            _key = key;
            _redis = redis;
        }

        public async Task<string> GetAsync()
        {
            var result = await _redis.GetAsync<string>(_key);
            return result;
        }

        public async Task<bool> SetAsync(string value)
        {
            var result = await _redis.SetAsync(_key, value);
            return result;
        }

        public string Get()
        {
            var result = _redis.Get<string>(_key);

            if (string.IsNullOrEmpty(result))
            {
                return default;
            }

            return result;
        }

        public bool Set(string value)
        {
            var result = _redis.Set(_key, value);
            return result;
        }
    }
}