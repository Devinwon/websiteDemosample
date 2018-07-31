using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HPFv1.Core;
using HPFv1.Manage.Business.Account;
using HPFv1.Manage.ViewModel.Account;
using HPFv1.Entity;


namespace HPFv1.Manage.Logic
{
    public class AccountController : Controller
    {

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
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
            Manager manager = HPFv1.Manage.Business.Account.Account.IsAdminLogin(login);
            if (manager != null)
            {
                CookieHelper.SetCookie("MID", manager.MID.ToString());
                CookieHelper.SetCookie("NickName", manager.NickName);
                CookieHelper.SetCookie("Account", manager.Account);
                CookieHelper.SetCookie("PassWord", manager.Password);
                return RedirectToAction("Index", "Users");
            }
            else
            {
                ModelState.AddModelError("PassWord", "登录帐号或密码不正确");

            }
            return View();
        }


        public ActionResult Logout()
        {
            CookieHelper.ClearCookie("NickName");
            CookieHelper.ClearCookie("Account");
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
            byte[] bytes = validatecode.CreateGraphic();
            return File(bytes, @"image/jpeg");

        }



    }
}
