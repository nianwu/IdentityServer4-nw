using System;
using System.Collections.Generic;
namespace utils.ElasticSearch
{
    public class ElasticConnection
    {
        public List<Uri> Urls { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}