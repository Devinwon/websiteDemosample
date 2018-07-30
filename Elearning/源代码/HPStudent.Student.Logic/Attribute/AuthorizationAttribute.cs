using System;
using System.Web.Mvc;
using HPStudent.Core;
using System.Web;
using System.Web.SessionState;

namespace HPStudent.Student.Logic.Attribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        private String _AuthUrl = "~/Account/Login";
        private String _AuthSaveKey = "UserName";
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AuthorizationAttribute()
        {

        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext == null)
            {
                throw new Exception("此特性只适合于Web应用程序使用！");
            }
            else
            {
                if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                {
                    //如果验证不通过，则跳转到登录界面
                    string UserName = CookieHelper.GetCookieValue("UserName");
                    string UserPassword = CookieHelper.GetCookieValue("UserPwd");
                    string Action = (filterContext.RouteData.Values["action"]).ToString().ToLower();
                    string Controller = (filterContext.RouteData.Values["controller"]).ToString().ToLower();
                    int result = HPStudent.Student.Business.Account.CheckUserRight(UserName, UserPassword, Controller, Action);
                    switch (result)
                    {
                        case 0:
                            if (string.IsNullOrEmpty(CookieHelper.GetCookieValue("LastLoginTime")))
                            {
                                HPStudent.Student.ViewModel.Account.UserLogin student = new ViewModel.Account.UserLogin();
                                student.Password = UserPassword;
                                student.Email = UserName;
                                HPStudent.Student.ViewModel.Account.UserLogin UserInfo = HPStudent.Student.Business.Account.IsUserLogin(student);
                                CookieHelper.SetCookie("LastLoginTime", UserInfo.LastLoginTime.ToString());
                                CookieHelper.SetCookie("OnlineTime", UserInfo.OnlineTime.ToString());
                            }
                            break;
                        case 1:
                            filterContext.Result = new RedirectResult(_AuthUrl);
                            break;
                        case 2:
                            filterContext.Result = new RedirectResult("~/Utility/PageNotFound");
                            break;
                    }
                }
            }
        }
    }


}
