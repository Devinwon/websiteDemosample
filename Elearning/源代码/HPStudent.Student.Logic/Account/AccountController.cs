using System;
using System.Web.Mvc;
using HPStudent.Core;

namespace HPStudent.Student.Logic
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            //如果验证不通过，则跳转到登录界面
            //string UserName = CookieHelper.GetCookieValue("UserName");
            //string UserPassword = CookieHelper.GetCookieValue("UserPwd");
            //if (HPStudent.Student.Business.Account.CheckUserLogin(UserName, UserPassword))
            //{
            //    return RedirectToAction("Index", "Exercises");
            //}
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(HPStudent.Student.ViewModel.Account.UserLogin student)
        {
            if (string.IsNullOrEmpty(student.VerificationCode) || Session["ValidateCode"] == null || Session["ValidateCode"].ToString().ToUpper() != student.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "验证码不正确");
                return View();
            }
            HPStudent.Student.ViewModel.Account.UserLogin UserInfo = HPStudent.Student.Business.Account.IsUserLogin(student);

            if (UserInfo != null)
            {
                if (UserInfo.IsActivated == 0)
                {
                    ModelState.AddModelError("Password", "您的账号尚未激活，请联系工作人员！");
                    return View();
                }
                //登录验证成功
                CookieHelper.SetCookie("UserName", UserInfo.Email, 43200);
                CookieHelper.SetCookie("UserPwd", UserInfo.Password, 43200);
                CookieHelper.SetCookie("RealName", UserInfo.RealName, 43200);

                CookieHelper.SetCookie("StudentID", UserInfo.StudentID, 43200);
                CookieHelper.SetCookie("RoleID", UserInfo.RoleID.ToString(), 43200);
                CookieHelper.SetCookie("LastLoginTime", UserInfo.LastLoginTime.ToString());
                CookieHelper.SetCookie("OnlineTime", UserInfo.OnlineTime.ToString());
                return RedirectToAction("Index", "Exercises");
            }
            else
            {
                ModelState.AddModelError("Password", "用户名或密码无效");
                return View();

            }
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            CookieHelper.ClearCookie("UserName");
            CookieHelper.ClearCookie("UserPwd");
            CookieHelper.ClearCookie("RealName");
            CookieHelper.ClearCookie("StudentID");
            CookieHelper.ClearCookie("LastLoginTime");
            //清除Session
            Session.Abandon();

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
            return File(bytes, @"image/png");

        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        //企业注册
        [AllowAnonymous]
        public ActionResult EnterpriseRegister()
        {
            HPStudent.Student.ViewModel.Account.UserRegister studentRegist = new ViewModel.Account.UserRegister();
            studentRegist.Email = Request.Params["EnterpriseEmailName"];
            studentRegist.Password = Request.Params["EnterprisePassword"];
            studentRegist.RealName = Request.Params["EnterpriseRealName"];
            studentRegist.CompanyName = Request.Params["EnterpriseName"];
            studentRegist.Phone = Request.Params["EnterprisePhone"];
            studentRegist.RegisterType = 0;
            studentRegist.RoleId = 0;
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.Account.RegistInfo(studentRegist);
            //如果注册成功则开始写入Cook
            //if (res.ResultState.Equals(HPStudent.ViewModel.Common.RequestResult.StateCode.success))
            //{
            //    WriteAccountCook(studentRegist);
            //}
            return Json(res);
        }
        //学生注册
        [AllowAnonymous]
        public ActionResult StudentRegister()
        {
            HPStudent.Student.ViewModel.Account.UserRegister studentRegist = new ViewModel.Account.UserRegister();
            studentRegist.Email = Request.Params["StudentEmailName"];
            studentRegist.Password = Request.Params["StudentPassword"];
            studentRegist.RealName = Request.Params["StudentRealName"];
            studentRegist.Phone = Request.Params["StudentPhone"];
            studentRegist.RegisterType = 1;
            studentRegist.RoleId = 0;
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.Account.RegistInfo(studentRegist);
            //如果注册成功则开始写入Cook
            //if (res.ResultState.Equals(HPStudent.ViewModel.Common.RequestResult.StateCode.success))
            //{
            //    WriteAccountCook(studentRegist);
            //}
            return Json(res);
        }
        //注册成功开始写入Cookie
        public void WriteAccountCook(HPStudent.Student.ViewModel.Account.UserRegister studentRegist)
        {
            HPStudent.Student.ViewModel.Account.UserLogin student = new ViewModel.Account.UserLogin();
            student.Password = studentRegist.Password;
            student.Email = studentRegist.Email;
            HPStudent.Student.ViewModel.Account.UserLogin UserInfo = HPStudent.Student.Business.Account.IsUserLogin(student);
            CookieHelper.SetCookie("UserName", UserInfo.Email);
            CookieHelper.SetCookie("UserPwd", UserInfo.Password);
            CookieHelper.SetCookie("RealName", UserInfo.RealName);
            CookieHelper.SetCookie("StudentID", UserInfo.StudentID);
        }
    }
}
