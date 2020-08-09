using Microsoft.Extensions.Options;
using utils.Redis;

namespace utils
{
    public class DcRedisContextServer : RedisContext
    {
        public DcRedisContextServer(IOptionsMonitor<Options> options) : base(options.CurrentValue.ConnectionStrings.Redis)
        {
        }

        public RedisQueue<string> ParagraphIdQueue => GetQueue<string>();

        public RedisHash<string> ErrorParagraphIdQueue => GetHash<string>();

        public RedisKeyValue<string> ProductorCursor => GetKeyValue<string>();

        public RedisKeyValue<string> ReLabelCursor => GetKeyValue<string>();

        public RedisSet<string> RelabelDataIdSet => GetSet<string>();

        public RedisInt RelabeDataIdlHandledCount => GetInt();
        
        public RedisInt RelabeParagraphslHandledCount => GetInt();

        public RedisInt ProductorCount => GetInt();
    }
}