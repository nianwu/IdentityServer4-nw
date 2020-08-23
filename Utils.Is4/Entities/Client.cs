namespace Utils.Is4.Entities
{
    public class Client : IdentityServer4.EntityFramework.Entities.Client, IApplicationResource
    {
        public string ApplicationName { get; set; }
        public Application Application { get; set; }
    }
}