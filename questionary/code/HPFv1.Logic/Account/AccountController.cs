using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HPFv1.ViewModel;
using HPFv1.Business;
using HPFv1.Entity;
using HPFv1.Core;
namespace HPFv1.Logic
{
    public class AccountController : Controller  
    {
        [AllowAnonymous]
        public ActionResult Login() 
        {
            if (CookieHelper.GetCookieValue("UID").ToString() != "")
            {
                return RedirectToAction("Index", "Question");
            }
            else 
            {
                return View();
            }
           
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Login login) 
        {
            ViewBag.Title = "登录";
            
            if (string.IsNullOrEmpty(login.VerificationCode) || Session["ValidateCode"] == null || Session["ValidateCode"].ToString().ToUpper() != login.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "验证码不正确");
                return View();
            }
            Users user =  HPFv1.Business.Admin.Account.IsAdminLogin(login);
            if (user != null)
            {
                CookieHelper.SetCookie("UID", user.UID.ToString(), 40320);
                CookieHelper.SetCookie("NickName", user.NickName, 40320);
                CookieHelper.SetCookie("UserName", user.Email);
                CookieHelper.SetCookie("PassWord", user.Password);
                return RedirectToAction("Index", "Question");
            }
            else 
            {
                ModelState.AddModelError("PassWord", "登录帐号或密码不正确");
            
            }
            return View();
        }


        public ActionResult Logout() 
        {
            CookieHelper.ClearCookie("UID");
            CookieHelper.ClearCookie("NickName");
            CookieHelper.ClearCookie("UserName");
            CookieHelper.ClearCookie("PassWord");
            return RedirectToAction("Login");
        
        }




        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public FileResult GetValidateCode() 
        {
            string code = RandCode.Str(4);
            Session["ValidateCode"] = code;
            ValidateCode validatecode = new ValidateCode(code);
            byte [] bytes = validatecode.CreateGraphic();
            return File(bytes, @"image/jpeg");
        
        }

    }
}
