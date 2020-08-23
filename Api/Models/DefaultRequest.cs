using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Models
{
    public class DefaultRequest
    {
        public virtual string KeyWord { get; set; }
        public virtual int Page { get; set; }
        public virtual int Limit { get; set; }
        
        [BindNever]
        [JsonIgnore]
        public int Skip => (Page - 1) * Limit;
    }
}