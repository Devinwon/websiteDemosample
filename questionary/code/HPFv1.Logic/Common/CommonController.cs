using HPFv1.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPFv1.Logic.Common
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
            List<UsersMenu> sidebar = HPFv1.Business.Common.Common.GetSideBar();
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
            ViewBag.AdminName = Core.CookieHelper.GetCookieValue("NickName");
            //每次刷新时取数据库中用户信息 
            return View();

        }
    }
}
