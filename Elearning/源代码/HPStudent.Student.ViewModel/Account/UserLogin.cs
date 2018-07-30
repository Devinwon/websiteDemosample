using System;
using System.ComponentModel.DataAnnotations;

namespace HPStudent.Student.ViewModel.Account
{
    /// <summary>
    /// 用户登录模型
    /// </summary>
    public class UserLogin
    {
        [Display(Name = "用户名", Description = "不要超过255个字符")]
        [Required(ErrorMessage = "用户名不可为空！")]
        public string Email { get; set; }

        [Display(Name = "密码", Description = "不要超过255个字符")]
        [Required(ErrorMessage = "密码不可以为空！")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "验证码", Description = "请输入图片中的验证码")]
        [Required(ErrorMessage = "验证码不可以为空！")]
        public String VerificationCode { get; set; }

        public int MID { get; set; }

        public string RealName { get; set; }

        public string StudentID { get; set; }
        public int IsActivated { get; set; }//账号状态 0：未激活，1：已激活，2：已审核
        public int RoleID { get; set; }//角色类别
        public DateTime LastLoginTime { get; set; }//上次一最后登入时间
        public int OnlineTime { get; set; }//在线总时长
    }
}