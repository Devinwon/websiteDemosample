using System;
using System.Web.Mvc;
using System.Collections.Generic;
using HPStudent.Core;
using HPStudent.Business;
using HPStudent.Entity;

namespace HPStudent.Logic.SysManage
{
    public class SysManageController : Controller
    {
        #region 管理员管理
        [HttpGet]
        public ActionResult Administrator()
        {
            return View();
        }

        public ActionResult Pop_Admin_Add()
        {
            return View();
        }

        public ActionResult Pop_Admin_Edit(int MID)
        {
            //获得管理员角色
            ViewBag.AdminRole = HPStudent.Business.Admin.AdminRole.GetAdminRoleListNotPage();
            //获得已经绑定的角色
            AdminRoleRelation ar = new AdminRoleRelation();
            ar.MID = MID;
            ViewBag.AdminRoleRelation = HPStudent.Business.Admin.AdminRole.GetAdminRoleRelationList(ar);
            ManagerInfo manager = Business.Admin.SysManage.GetManagerByMid(MID);
            return View(manager);
        }

        /// <summary>
        /// 获得管理员列表
        /// </summary>
        /// <param name="draw">请求次数</param>
        /// <param name="start">开始行</param>
        /// <param name="length">页记录数</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetManagerList(int draw, int start, int length)
        {
            HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.SysManage.ManagerTable> ManagerList = Business.Admin.SysManage.GetManagerDatatable(start, length);
            ManagerList.draw = draw;

            return Json(ManagerList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        public JsonResult AddManager(ManagerInfo manager)
        {
            ViewModel.Common.RequestResult result = Business.Admin.SysManage.AddManager(manager);

            return Json(result);

        }

        [HttpPost]
        public JsonResult EditManager(HPStudent.ViewModel.SysManage.EditManagerInfo manager)
        {
            ViewModel.Common.RequestResult result = Business.Admin.SysManage.EditManager(manager);
            //重置角色关系
            HPStudent.Business.Admin.AdminRole.ResetdminRoleRelation(manager);
            return Json(result);

        }

        public JsonResult DelManager(int MID)
        {
            ViewModel.Common.RequestResult result = Business.Admin.SysManage.DelManager(MID);

            return Json(result);

        }
    }
}
