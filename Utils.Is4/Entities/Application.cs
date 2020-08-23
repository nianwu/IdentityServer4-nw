using System;
using System.Collections.Generic;
using System.Text;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;

namespace Utils.Is4.Entities
{
    public class Application
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public List<Client> Clients { get; set; }
        public List<ApiResource> ApiResources { get; set; }
        public List<ApiScope> ApiScopes { get; set; }
        public List<IdentityResource> IdentityResources { get; set; }
    }
}
