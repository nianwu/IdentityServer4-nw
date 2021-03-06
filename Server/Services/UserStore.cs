using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using IdentityModel;
using Server.Entities;
using Server.Models;

namespace Server.Services
{
    public class EfUserStore : IUserStore
    {
        private UserConfigurationDbContext _db;
        private IMapper _mapper;

        public EfUserStore(
            UserConfigurationDbContext db
            , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        private DbSet<Entities.User> _users => _db.Users;

        /// <summary>
        /// Validates the credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);

            if (user != null)
            {
                if (string.IsNullOrWhiteSpace(user.PasswordSaltMd5) && string.IsNullOrWhiteSpace(password))
                {
                    return true;
                }

                return user.PasswordSaltMd5.Equals(new Models.User { Username = username, Password = password }.PasswordSaltMd5);
            }

            return false;
        }

        /// <summary>
        /// Finds the user by subject identifier.
        /// </summary>
        /// <param name="subjectId">The subject identifier.</param>
        /// <returns></returns>
        public IIdentityUser FindBySubjectId(string subjectId)
        {
            var entity = _users.FirstOrDefault(x => x.SubjectId == subjectId);
            var result = _mapper.Map<Models.User>(entity);
            return result;
        }

        /// <summary>
        /// Finds the user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public IIdentityUser FindByUsername(string username)
        {
            var entity = _users.FirstOrDefault(x => x.Username == username);
            var result = _mapper.Map<Models.User>(entity);
            return result;
        }

        /// <summary>
        /// Finds the user by external provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IIdentityUser FindByExternalProvider(string provider, string userId)
        {
            var entity = _users.FirstOrDefault(x =>
               x.ProviderName == provider &&
               x.ProviderSubjectId == userId);
            var result = _mapper.Map<Models.User>(entity);
            return result;
        }

        /// <summary>
        /// Automatically provisions a user.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="claims">The claims.</param>
        /// <returns></returns>
        public IIdentityUser AutoProvisionUser(string provider, string userId, List<Claim> claims)
        {
            // create a list of claims that we want to transfer into our store
            var filtered = new List<Claim>();

            foreach (var claim in claims)
            {
                // if the external system sends a display name - translate that to the standard OIDC name claim
                if (claim.Type == ClaimTypes.Name)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, claim.Value));
                }
                // if the JWT handler has an outbound mapping to an OIDC claim use that
                else if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(claim.Type))
                {
                    filtered.Add(new Claim(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[claim.Type], claim.Value));
                }
                // copy the claim as-is
                else
                {
                    filtered.Add(claim);
                }
            }

            // if no display name was provided, try to construct by first and/or last name
            if (!filtered.Any(x => x.Type == JwtClaimTypes.Name))
            {
                var first = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value;
                var last = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value;
                if (first != null && last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
                }
                else if (first != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first));
                }
                else if (last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, last));
                }
            }

            // create a new unique subject id
            var sub = CryptoRandom.CreateUniqueId(format: CryptoRandom.OutputFormat.Hex);

            // check if a display name is available, otherwise fallback to subject id
            var name = filtered.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)?.Value ?? sub;

            // create new user
            var user = new Models.User
            {
                SubjectId = sub,
                Username = name,
                ProviderName = provider,
                ProviderSubjectId = userId,
                Claims = filtered
            };

            // add user to in-memory store
            _users.Add(_mapper.Map<Entities.User>(user));

            _db.SaveChanges();

            return user;
        }
    }
}