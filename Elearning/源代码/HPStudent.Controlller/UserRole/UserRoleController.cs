using System;
using System.Web.Mvc;
using System.Collections.Generic;
using HPStudent.Core;
using HPStudent.Business;
using HPStudent.Entity;


namespace HPStudent.Logic.UserRole
{
    public class UserRoleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //加载用户角色数据列表
        public ActionResult GetUserRoleList(int draw, int start, int length, HPStudent.Entity.UserRole KeyWords)
        {
            ViewModel.Common.Datatable<HPStudent.Entity.UserRole> Res = HPStudent.Business.Admin.UserRole.GetUserRoleList(start, length, KeyWords);
            Res.draw = draw;
            return Json(Res);
        }
        //权限编辑页面
        public ActionResult EditRole()
        {
            HPStudent.Entity.UserRole userrol = new Entity.UserRole();
            if (Request.QueryString["RID"] != null)
            {
                //如果是修改过来需要查询实体
                userrol = HPStudent.Business.Admin.UserRole.GetUserRoleByID(Convert.ToInt32(Request.QueryString["RID"]));
            }
            else
            {
                userrol = new Entity.UserRole();
            }
            return View(userrol);
        }
        //菜单权限
        public ActionResult UserMenuRole()
        {
            int RID = 0;
            if (Request.QueryString["RID"] != null)
            {
                RID = Convert.ToInt32(Request.QueryString["RID"]);
            }
            ViewBag.RID = RID;
            //加载权限菜单已绑定菜单
            ViewBag.UserRoleNavPowrer = HPStudent.Business.Admin.UserRole.GetUserRoleNavPowrerByID(RID).Navigation;
            //加载菜单数据
            HPStudent.ViewModel.Sys_Menu.SysMenuViewModel keywords = new ViewModel.Sys_Menu.SysMenuViewModel();
            keywords.Category = 0;
            List<HPStudent.Entity.Sys_Menu> menu = HPStudent.Business.Admin.SysMenu.GetNavationMenuListNotByPage(keywords);
            //找到所有父菜单
            ViewBag.MenuFList = menu.FindAll(x => x.PID == 0);
            //找到所有子菜单
            ViewBag.MenuSList = menu.FindAll(x => x.PID != 0);
            return View();
        }
        //新增用户角色表单提交事件
        public ActionResult UserRoleAdd(HPStudent.Entity.UserRole ur)
        {
            HPStudent.ViewModel.Common.RequestResult res = HPStudent.Business.Admin.UserRole.UserRoleAdd(ur);
            return Json(res);
        }
        //修改用户角色表单提交事件
        public ActionResult UserRoleUpdate(HPStudent.Entity.UserRole ur)
        {
            HPStudent.ViewModel.Common.RequestResult res = HPStudent.Business.Admin.UserRole.UserRoleUpdate(ur);
            return Json(res);
        }
        //用户导航权限编辑事件
        public ActionResult UserMenuRoleEdit(HPStudent.Entity.UserRole_NavPowrer roleNavPowrer)
        {
            if (!string.IsNullOrEmpty(roleNavPowrer.Navigation))
            {
                roleNavPowrer.Navigation = roleNavPowrer.Navigation.TrimEnd(',');
            }
            else
            {
                roleNavPowrer.Navigation = "";
            }
            HPStudent.ViewModel.Common.RequestResult res = HPStudent.Business.Admin.UserRole.UserMenuRoleEdit(roleNavPowrer);
            return Json(res);
        }
    }
}
