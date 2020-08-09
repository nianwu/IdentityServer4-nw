using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Utils.Is4.IEntities;

namespace Utils.Is4.Mssql
{
    public class Is4MssqlContext : ConfigurationDbContext<Is4MssqlContext>
    {
        public Is4MssqlContext(DbContextOptions<Is4MssqlContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}