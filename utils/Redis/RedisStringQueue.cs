using System.Collections;
using System.Threading.Tasks;

namespace utils.Redis
{
    public class RedisStringQueue
    {
        private string _key;
        private RedisContext _redis;

        public RedisStringQueue(string key, RedisContext redis)
        {
            _key = key;
            _redis = redis;
        }

        public async Task EnqueueAsync(params string[] entities)
        {
            await _redis.RPushAsync(_key, entities);
        }

        public void Enqueue(params string[] entities)
        {
            _redis.RPush(_key, entities);
        }

        public async Task EnStackAsync(params string[] entities)
        {
            await _redis.LPushAsync(_key, entities);
        }

        public void EnStack(params string[] entities)
        {
            _redis.LPush(_key, entities);
        }

        public async Task<string> DequeueAsync()
        {
            var result = await _redis.LPopAsync<string>(_key);
            return result;
        }

        public string Dequeue()
        {
            var result = _redis.LPop<string>(_key);
            return result;
        }

        public async Task<int> LengthAsync()
        {
            var result = await _redis.LLenAsync(_key);
            return (int)result;
        }

        public int Length()
        {
            var result = _redis.LLen(_key);
            return (int)result;
        }
    }
}