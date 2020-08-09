using System.Threading.Tasks;

namespace utils.Redis
{
    public class RedisInt
    {
        private string _key;
        private RedisContext _redis;

        public RedisInt(string key, RedisContext redis)
        {
            _key = key;
            _redis = redis;
        }

        public async Task<int> GetAsync()
        {
            var result = await _redis.GetAsync<int>(_key);
            return result;
        }

        public async Task<bool> SetAsync(int value)
        {
            var result = await _redis.SetAsync(_key, value);
            return result;
        }

        public int Get()
        {
            var result = _redis.Get<int>(_key);

            return result;
        }

        public bool Set(int value)
        {
            var result = _redis.Set(_key, value);
            return result;
        }

        public int IncrBy(int value = 1)
        {
            var result = _redis.IncrBy(_key, value);
            return (int)result;
        }

        public async Task<int> IncrByAsync(int value = 1)
        {
            var result = await _redis.IncrByAsync(_key, value);
            return (int)result;
        }
    }
}