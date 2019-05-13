using System;
using System.ComponentModel.DataAnnotations;

namespace WS.Music.Entities
{
    /// <summary>
    /// 用户信息，用于登陆，UserName和PassWord是基础数据
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID，主键
        /// </summary>
        [Key]
        [MaxLength(36, ErrorMessage = "GUID最长不超过36")]
        public string Id { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(63)]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(127)]
        public string PassWord { get; set; }

        /// <summary>
        /// 头像 URL
        /// </summary>
        [MaxLength(511)]
        public string Avatar { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [MaxLength(320, ErrorMessage = "邮箱地址不能超过最长320个字符限制")]
        public string Email { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(255)]
        public string Address { get; set; }

        /// <summary>
        /// 介绍，可空（Empty=Blank>Null）
        /// </summary>
        [MaxLength(511)]
        public string Description { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthTime { get; set; }

        /// <summary>
        /// 性别，可空（null：未知，true：男，false：女）
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public class SexEnum
        {
            public static readonly bool Male = true;
            public static readonly bool Female = false;
        }

        /// <summary>
        /// 用户唯一码，用以标识自然人、组织等
        /// </summary>
        [MaxLength(36)]
        public string UserCode { get; set; }
    }
}
