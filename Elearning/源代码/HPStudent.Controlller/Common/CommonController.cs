using System;
using System.Web.Mvc;
using HPStudent.Core;
using System.Collections.Generic;
using HPStudent.Entity;

namespace HPStudent.Logic.Common
{
    public class CommonController : Controller
    {
        /// <summary>
        /// 管理员后台共用导航栏
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult SideBar(string ParentAction, string ParentController)
        {
            //获得当前登入角色

            List<Sys_Menu> sidebar = HPStudent.Business.Common.AdminCommon.GetSideBar();
            ViewBag.ParentAction = ParentAction;
            ViewBag.ParentController = ParentController;
            return View(sidebar);

        }

        /// <summary>
        /// 管理员后台共用Body头部
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult BodyHeader()
        {
            //每次刷新时取数据库中用户信息 
            ViewBag.AdminName = Core.CookieHelper.GetCookieValue("AdminName");
            //每次刷新时取数据库中用户信息 
            return View();

        }
    }
}
