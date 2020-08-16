using System.Collections.Generic;

namespace Api.Models
{
    public class LimitResponse<T>
    {
        public List<T> List { get; set; }
        public int Total { get; set; }
    }
}