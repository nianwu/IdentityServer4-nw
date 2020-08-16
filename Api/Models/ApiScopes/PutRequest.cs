using System.ComponentModel.DataAnnotations;

namespace Api.Models.ApiScopes
{
    public class ApiScopeDefaultConstructorRequest
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string DisplayName { get; set; }
    }
}