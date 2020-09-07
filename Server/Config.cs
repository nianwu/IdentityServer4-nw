using System.Linq;
// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using Server.Entities;
using Server.Models;

namespace Server
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResources.Phone(),
            new IdentityResources.Address()
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
        };

        public static IEnumerable<ApiResource> Apis => new ApiResource[]
        {
        };

        public static IEnumerable<Role> Roles => new[]
        {
            new Role
            {
                Name = "admin",
                DisplayName = "管理员"
            }
        };

        public static IEnumerable<User> Users => new[]
        {
            new User
            {
                SubjectId = "admin",
                Account = "admin",
                Password = "admin"
            }
        };

        public static IEnumerable<UserRole> UserRoles => new[]
        {
            new UserRole
            {
                UserAccount = Users.First().Account,
                RoleName = Roles.First().Name
            }
        };
    }
}
