using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Elasticsearch.Net;
using Nest;
using utils.ElasticSearch;

namespace utils
{
    public static class Extend
    {
        static IServiceCollection AddUtilsServer(this IServiceCollection services)
        {
            return services
                .AddSingleton<MbPosSegmenter>()
                .AddSingleton<StaticConnectionPool, MbStaticConnectionPoolService>()
                .AddSingleton<ConnectionSettings, MbConnectionSettingsService>()
                .AddSingleton<EsContext>()
                .AddSingleton(MD5.Create())
                .AddSingleton<RegexDic>()
                .AddSingleton<Functions>()
                ;
        }

        public static IServiceCollection AddUtils(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddUtils()
                .Configure<Options>(configuration);
        }

        public static IServiceCollection AddUtils(this IServiceCollection services, Action<Options> configuration)
        {
            return services
                .AddUtils()
                .Configure(configuration);
        }

        public static IServiceCollection AddUtils(this IServiceCollection services)
        {
            return services
                .AddUtilsServer();
        }
    }
}