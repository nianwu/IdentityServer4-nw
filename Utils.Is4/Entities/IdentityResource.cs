namespace Utils.Is4.Entities
{
    public class IdentityResource : IdentityServer4.EntityFramework.Entities.IdentityResource, IApplicationResource
    {
        public string ApplicationName { get; set; }
        public Application Application { get; set; }
    }
}