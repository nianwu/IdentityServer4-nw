using System.Collections.Generic;
using System.Security.Claims;

namespace Server.Models
{
    public class User : IIdentityUser
    {
        public string SubjectId { get; set; }
        public string Username { get; set; }
        public string PasswordSaltMd5 { get; set; }
        public string ProviderName { get; set; }
        public string ProviderSubjectId { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Claim> Claims { get; set; }
        public string Account { get; set; }
    }
}