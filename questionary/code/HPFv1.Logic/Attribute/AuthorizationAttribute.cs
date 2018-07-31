using System;
using System.Web.Mvc;
using HPFv1.Core;

namespace HPFv1.Logic.Attribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        private String _AuthUrl = "~/Account/Login";
        private String _AuthSaveKey = "AdminName";
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
                    string AdminName = CookieHelper.GetCookieValue("AdminName");
                    string AdminPassword = CookieHelper.GetCookieValue("AdminPassword");
                    //if (!Business.Account.CheckAdmin(AdminName, AdminPassword))
                    //{
                    //    filterContext.Result = new RedirectResult(_AuthUrl);
                    //}
                    
                }
            }
        }
    }


}
