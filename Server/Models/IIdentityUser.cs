using System.Collections.Generic;
using System.Security.Claims;

namespace Server.Models
{
    public interface IIdentityUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        string SubjectId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// 密码的明文
        /// </summary>
        string PasswordSaltMd5 { get; set; }

        /// <summary>
        /// 第三方名称
        /// </summary>
        string ProviderName { get; set; }

        /// <summary>
        /// 第三方id
        /// </summary>
        string ProviderSubjectId { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// 用户的属性
        /// </summary>
        ICollection<Claim> Claims { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        string Account { get; set; }
    }
}