using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace HPFv1.ViewModel
{
   public class Login
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

        public string NickName { get; set; }


    }
}
