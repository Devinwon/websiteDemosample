using System;
using System.ComponentModel.DataAnnotations;

namespace HPStudent.Web.Models.Account
{
    /// <summary>
    /// 用户登录模型
    /// </summary>
    public class UserLogin
    {
        [Display(Name="用户名",Description="不要超过255个字符")]
        [Required(ErrorMessage="*")]
        public string UserName { get; set; }

        [Display(Name = "密码", Description = "不要超过255个字符")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "验证码", Description = "请输入图片中的验证码")]
        [Required(ErrorMessage = "*")]
        public String VerificationCode { get; set; }
    }
}