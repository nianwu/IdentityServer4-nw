using System.Collections.Generic;
using System.Security.Claims;
using Server.Entities;

namespace Server.Services
{
    public interface IUserStore
    {
        IUserEntity AutoProvisionUser(string provider, string userId, List<Claim> claims);
        IUserEntity FindByExternalProvider(string provider, string userId);
        IUserEntity FindBySubjectId(string subjectId);
        IUserEntity FindByUsername(string username);
        bool ValidateCredentials(string username, string password);
    }
}