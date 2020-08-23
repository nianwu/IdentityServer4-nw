namespace Utils.Is4.Entities
{
    public class ApiScope : IdentityServer4.EntityFramework.Entities.ApiScope, IApplicationResource
    {
        public string ApplicationName { get; set; }
        public Application Application { get; set; }
    }
}