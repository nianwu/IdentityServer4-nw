using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace utils.Redis
{
    public class RedisQueue<T> : IEnumerable<T>
    {
        private string _key;
        private RedisContext _redis;

        public RedisQueue(string key, RedisContext redis)
        {
            _key = key;
            _redis = redis;
        }

        public async Task EnqueueAsync(params T[] entities)
        {
            await _redis.RPushAsync(_key, entities);
        }

        public async Task<T> DequeueAsync()
        {
            var result = await _redis.LPopAsync<T>(_key);
            return result;
        }

        public void Enqueue(params T[] entities)
        {
            _redis.RPush(_key, entities);
        }

        public T Dequeue()
        {
            var result = _redis.LPop<T>(_key);
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

        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                var result = Dequeue();

                if (result == null || result.Equals(default(T)))
                {
                    break;
                }

                yield return result;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class RedisQueue : RedisQueue<string>
    {
        public RedisQueue(string key, RedisContext redis) : base(key, redis)
        {
        }
    }
}