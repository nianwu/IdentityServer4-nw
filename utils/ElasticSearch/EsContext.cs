using System.Linq;
using System.Xml.Linq;
using System.Security.Principal;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using Microsoft.Extensions.Hosting;

namespace utils.ElasticSearch
{
    public class EsContext : ElasticClient, IDisposable
    {
        public EsContext(ConnectionSettings settings) : base(settings)
        {
        }

        public void Dispose()
        {
        }

        // public ISearchResponse<T> GetIndex<T>([CallerMemberName] string callerMemberName = null) where T : IIdEntity
        // {

        // }
    }

    // public class EsQueue<T> where T : IIdentity
    // {

    // }

    public class MbConnectionSettings : ConnectionSettings, IDisposable
    {
        public MbConnectionSettings(StaticConnectionPool pool, string account, string password) : base(pool)
        {
            BasicAuthentication(account, password);
        }

        public void Dispose()
        {
        }
    }

    public class MbConnectionSettingsService : MbConnectionSettings
    {
        public MbConnectionSettingsService(
            StaticConnectionPool pool
            , IOptionsMonitor<Options> options
            , IHostEnvironment environment
        )
            : base(pool,
                options.CurrentValue.ConnectionStrings.ElasticSearch.Account,
                options.CurrentValue.ConnectionStrings.ElasticSearch.Password)
        {
            if (environment.IsDevelopment())
            {
                DisableDirectStreaming();
            }
        }
    }

    public class MbStaticConnectionPool : StaticConnectionPool, IDisposable
    {
        public MbStaticConnectionPool(IEnumerable<Uri> connectionStrings) : base(connectionStrings)
        {
            if (connectionStrings is null || !connectionStrings.Any())
            {
                throw new ArgumentNullException(nameof(connectionStrings));
            }
        }
    }

    public class MbStaticConnectionPoolService : MbStaticConnectionPool
    {
        public MbStaticConnectionPoolService(IOptionsMonitor<Options> options) : base(options.CurrentValue.ConnectionStrings.ElasticSearch.Urls)
        {

        }
    }
}
