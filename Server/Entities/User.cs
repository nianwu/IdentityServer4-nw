using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Entities
{
    public class UserEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Key]
        public string Account { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码的明文
        /// </summary>
        public string Password { set => PasswordSaltMd5.SaltMd5(Account); }

        /// <summary>
        /// 密码的密文
        /// </summary>
        public string PasswordSaltMd5 { get; set; }

        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disabled { get; set; } = false;

        public List<UserRole> UserRoles { get; set; }
    }
}