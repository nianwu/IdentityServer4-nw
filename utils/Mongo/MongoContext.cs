using System;
using System.Runtime.CompilerServices;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace utils.Mongo
{
    public class MongoContext : MongoClient
    {
        private string _connectionString;

        public MongoContext(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

        private ConnectionString ConnectionString => new ConnectionString(_connectionString);
        private IMongoDatabase Db => GetDatabase(ConnectionString.DatabaseName);
        protected IMongoCollection<T> GetCollection<T>([CallerMemberName] string callerMemberName = null) =>
            Db.GetCollection<T>(callerMemberName ?? throw new ArgumentNullException(nameof(callerMemberName)));
    }
}