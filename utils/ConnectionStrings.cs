using utils.ElasticSearch;

namespace utils
{
    public class ConnectionStrings
    {
        public string Mongo { get; set; }
        public string Redis { get; set; }
        public string Mssql { get; set; }

        public ElasticConnection ElasticSearch { get; set; }
    }
}