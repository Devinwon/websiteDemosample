using System;
using System.Web.Mvc;
using System.Collections.Generic;
using HPStudent.Core;
using HPStudent.Business;
using HPStudent.Entity;

namespace HPStudent.Logic.AdminRole
{
    public class AdminRoleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //加载管理员角色数据列表
        public ActionResult GetAdminRoleList(int draw, int start, int length, HPStudent.Entity.AdminRole KeyWords)
        {
            ViewModel.Common.Datatable<HPStudent.Entity.AdminRole> Res = HPStudent.Business.Admin.AdminRole.GetAdminRoleList(start, length, KeyWords);
            Res.draw = draw;
            return Json(Res);
        }
        //权限编辑页面
        public ActionResult EditRole()
        {
            HPStudent.Entity.AdminRole adminrol = new Entity.AdminRole();
            if (Request.QueryString["RID"] != null)
            {
                //如果是修改过来需要查询实体
                adminrol = HPStudent.Business.Admin.AdminRole.GetAdminRoleByID(Convert.ToInt32(Request.QueryString["RID"]));
            }
            else
            {
                adminrol = new Entity.AdminRole();
            }
            return View(adminrol);
        }
        //新增用户角色表单提交事件
        public ActionResult AdminRoleAdd(HPStudent.Entity.AdminRole ur)
        {
            HPStudent.ViewModel.Common.RequestResult res = HPStudent.Business.Admin.AdminRole.AdminRoleAdd(ur);
            return Json(res);
        }
        //修改用户角色表单提交事件
        public ActionResult AdminRoleUpdate(HPStudent.Entity.AdminRole ur)
        {
            HPStudent.ViewModel.Common.RequestResult res = HPStudent.Business.Admin.AdminRole.AdminRoleUpdate(ur);
            return Json(res);
        }
        //菜单权限
        public ActionResult AdminMenuRole()
        {
            int RID = 0;
            if (Request.QueryString["RID"] != null)
            {
                RID = Convert.ToInt32(Request.QueryString["RID"]);
            }
            ViewBag.RID = RID;
            //加载权限菜单已绑定菜单
            ViewBag.UserRoleNavPowrer = HPStudent.Business.Admin.AdminRole.GetAdminRoleNavPowrerByID(RID).Navigation;
            //加载菜单数据
            HPStudent.ViewModel.Sys_Menu.SysMenuViewModel keywords = new ViewModel.Sys_Menu.SysMenuViewModel();
            keywords.Category = 1;
            List<HPStudent.Entity.Sys_Menu> menu = HPStudent.Business.Admin.SysMenu.GetNavationMenuListNotByPage(keywords);
            //找到所有父菜单
            ViewBag.MenuFList = menu.FindAll(x => x.PID == 0);
            //找到所有子菜单
            ViewBag.MenuSList = menu.FindAll(x => x.PID != 0);
            return View();
        }
        //管理员导航权限编辑事件
        public ActionResult AdminMenuRoleEdit(HPStudent.Entity.AdminRole_NavPowrer roleNavPowrer)
        {
            if (!string.IsNullOrEmpty(roleNavPowrer.Navigation))
            {
                roleNavPowrer.Navigation = roleNavPowrer.Navigation.TrimEnd(',');
            }
            else
            {
                roleNavPowrer.Navigation = "";
            }
            HPStudent.ViewModel.Common.RequestResult res = HPStudent.Business.Admin.AdminRole.AdminMenuRoleEdit(roleNavPowrer);
            return Json(res);
        }
    }
}
