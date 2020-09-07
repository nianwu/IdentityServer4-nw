using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Server.Entities
{
    public class User
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Key]
        public string SubjectId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码的密文
        /// </summary>
        public string PasswordSaltMd5 { get; set; }

        /// <summary>
        /// 第三方名称
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 第三方id
        /// </summary>
        public string ProviderSubjectId { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 用户的属性
        /// </summary>
        public ICollection<UserClaim> Claims { get; set; }

        /// <summary>
        /// 用户角色关系
        /// </summary>
        public List<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public IEnumerable<Role> Roles => UserRoles?.Select(x => x.Role) ?? new Role[] { };
    }

    public class UserClaim
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User UserEntity { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}