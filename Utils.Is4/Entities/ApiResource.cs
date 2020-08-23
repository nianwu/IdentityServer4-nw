using System.Security.Cryptography.X509Certificates;
namespace Utils.Is4.Entities
{
    public class ApiResource : IdentityServer4.EntityFramework.Entities.ApiResource, IApplicationResource
    {
        public string ApplicationName { get; set; }
        public Application Application { get; set; }
    }
}