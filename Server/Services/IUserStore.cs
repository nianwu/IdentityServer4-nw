using System.Collections.Generic;
using System.Security.Claims;
using Server.Models;

namespace Server.Services
{
    public interface IUserStore
    {
        IIdentityUser AutoProvisionUser(string provider, string userId, List<Claim> claims);
        IIdentityUser FindByExternalProvider(string provider, string userId);
        IIdentityUser FindBySubjectId(string subjectId);
        IIdentityUser FindByUsername(string username);
        bool ValidateCredentials(string username, string password);
    }
}