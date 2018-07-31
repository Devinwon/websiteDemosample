using System;
using System.Web.Mvc;
using HPStudent.Core;
using System.Collections.Generic;
using HPStudent.Entity;

namespace HPStudent.Student.Logic.Common
{
    public class CommonController : Controller
    {
        /// <summary>
        /// 管理员后台共用导航栏
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult SideBar(string ParentAction, string ParentController)
        {
            List<Sys_Menu> sidebar = HPStudent.Student.Business.Common.StudentCommon.GetSideBar();
            ViewBag.ParentAction = ParentAction;
            ViewBag.ParentController = ParentController;
            return View(sidebar);

        }

        /// <summary>
        /// 管理员后台共用Body头部
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult BodyHeader()
        {
            string RealName = Core.CookieHelper.GetCookieValue("RealName");
            //每次刷新时取数据库中用户信息 
            ViewBag.RealName = RealName;
            //在线用户统计
            Entity.UserOnline useronline = new UserOnline();
            useronline.UserName = RealName;
            useronline.UserID = Convert.ToInt32(Core.CookieHelper.GetCookieValue("StudentID"));
            useronline.RoleID = Convert.ToInt32(Core.CookieHelper.GetCookieValue("RoleID"));
            useronline.LastRequestTime = DateTime.Now;
            useronline.LastRequestPath = Request.Url.LocalPath.ToString();
            useronline.ClientIP = HttpHelper.IPAddress();
            int NewOnlineTime = HPStudent.Student.Business.UserOnline.EditUserOnline(useronline, Convert.ToInt32(Core.CookieHelper.GetCookieValue("OnlineTime")), DateTime.Parse(Core.CookieHelper.GetCookieValue("LastLoginTime")));
            //重新绑定Session时长
            CookieHelper.SetCookie("LastLoginTime", DateTime.Now.ToString());
            CookieHelper.SetCookie("OnlineTime", NewOnlineTime.ToString());
            return View();
        }
    }
}
