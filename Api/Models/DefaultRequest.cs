using System.Text.Json.Serialization;

namespace Api.Models
{
    public class DefaultRequest
    {
        public virtual string KeyWord { get; set; }
        public virtual int Page { get; set; }
        public virtual int Limit { get; set; }
        
        [JsonIgnore]
        public int Skip => (Page - 1) * Limit;
    }
}