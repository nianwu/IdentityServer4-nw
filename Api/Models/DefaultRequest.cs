using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Models
{
    public class DefaultRequest
    {
        public virtual string KeyWord { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public virtual int Page { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public virtual int Limit { get; set; }

        [BindNever]
        [JsonIgnore]
        public int Skip => (Page - 1) * Limit;
    }
}