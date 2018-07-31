using System;
using System.Web.Mvc;
using HPStudent.Core;


namespace HPStudent.Logic
{
    public class AccountController:Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(HPStudent.ViewModel .Account .ManagerLogin  manager)
        {
            if (string.IsNullOrEmpty(manager.VerificationCode) || Session["ValidateCode"] == null || Session["ValidateCode"].ToString().ToUpper() != manager.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "验证码不正确");
                return View();
            }
            HPStudent.Entity.ManagerInfo managerInfo = HPStudent.Business.Admin.Account.IsAdminLogin(manager);

            if (managerInfo!=null)
            {
                //登录验证成功
                CookieHelper.SetCookie("MID", managerInfo.MID.ToString());
                CookieHelper.SetCookie("AdminName", managerInfo.ManagerName);
                CookieHelper.SetCookie("AdminPassword", managerInfo.Password);
                //CookieHelper.SetCookie("RoleID", managerInfo.RoleID.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", "登录密码不正确");
                return View();

            }
            
        }


        [AllowAnonymous]
        public ActionResult Logout()
        {
            CookieHelper.ClearCookie("MID");
            CookieHelper.ClearCookie("AdminName");
            CookieHelper.ClearCookie("AdminPassword");

            return RedirectToAction("Login");
        }


        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public FileResult GetValidateCode()
        {
            string code = HPStudent.Core.RandCode.Str(4);
            Session["ValidateCode"] = code;
            HPStudent.Core.ValidateCode validatecode = new Core.ValidateCode(code);
            byte[] bytes = validatecode.CreateGraphic();
            return File(bytes, @"image/jpeg");
            
        }

        /// <summary>
        /// 数据库连接加密字符串
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Encrypt()
        {
            string plainstr = "Data Source=.;Initial Catalog=HPStudent;User ID=sa;Password=svse;";
            string encryptstr = HPStudent.Core.Security.GetConnectionStr(plainstr);
            ViewBag.Plaintext = plainstr;
            ViewBag.Encrypttext = encryptstr;
            ViewBag.Decrypttext = HPStudent.Core.Security.SDecryptString(encryptstr);
            return View();
        }
    }
}
