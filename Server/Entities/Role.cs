using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Entities
{
    public class Role
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Key]
        public string Name { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}