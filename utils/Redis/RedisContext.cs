using System.Runtime.CompilerServices;
using CSRedis;

namespace utils.Redis
{
    public class RedisContext : CSRedisClient
    {
        private string _connectionString;

        public RedisContext(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
            RedisHelper.Initialization(this);
        }

        public RedisQueue<T> GetQueue<T>([CallerMemberName] string key = null)
        {
            return new RedisQueue<T>(key, this);
        }


        public RedisQueue GetQueue([CallerMemberName] string key = null)
        {
            return new RedisQueue(key, this);
        }

        public RedisStringQueue GetStringQueue([CallerMemberName] string key = null)
        {
            return new RedisStringQueue(key, this);
        }

        public RedisKeyValue<T> GetKeyValue<T>([CallerMemberName] string key = null)
        {
            return new RedisKeyValue<T>(key, this);
        }

        public RedisInt GetInt([CallerMemberName] string key = null)
        {
            return new RedisInt(key, this);
        }

        public RedisSet<T> GetSet<T>([CallerMemberName] string key = null)
        {
            return new RedisSet<T>(key, this);
        }

        public RedisHash<T> GetHash<T>([CallerMemberName] string key = null)
        {
            return new RedisHash<T>(key, this);
        }
    }
}