using System;
using System.Web.Mvc;
using System.Collections.Generic;
using HPStudent.Core;
using HPStudent.Business;
using HPStudent.Entity;

namespace HPStudent.Logic.SysManage
{
    public class SysMenuController : Controller
    {
        #region 菜单管理
        [HttpGet]
        public ActionResult NavigationMenu()
        {
            return View();
        }
        //菜单（父）列表
        public ActionResult GetNavationMenuFList(int draw, int start, int length, HPStudent.ViewModel.Sys_Menu.SysMenuViewModel KeyWords)
        {
            KeyWords.SearchType = 0;
            ViewModel.Common.Datatable<HPStudent.Entity.Sys_Menu> Res = HPStudent.Business.Admin.SysMenu.GetNavationMenuList(start, length, KeyWords);
            Res.draw = draw;
            return Json(Res);
        }
        //菜单菜单向上(向下)移动
        public ActionResult MenuMove(HPStudent.ViewModel.Sys_Menu.SysMenuViewModel KeyWords)
        {
            KeyWords.SearchType = 0;
            HPStudent.ViewModel.Common.RequestResult res = HPStudent.Business.Admin.SysMenu.MenuMove(KeyWords);
            return Json(res);
        }
        public ActionResult EditMenu()
        {
            HPStudent.Entity.Sys_Menu menu;
            if (Request.QueryString["MID"] != null)
            {
                //如果是修改过来需要查询实体
                int mid = Convert.ToInt32(Request.QueryString["MID"]);
                menu = HPStudent.Business.Admin.SysMenu.GetInfoByID(mid);
            }
            else
            {
                menu = new HPStudent.Entity.Sys_Menu();
                //如果是子菜单进来绑定PID
                if (Request.QueryString["PID"] != null)
                {
                    menu.PID = Convert.ToInt32(Request.QueryString["PID"]);
                    HPStudent.Entity.Sys_Menu menuTemp = HPStudent.Business.Admin.SysMenu.GetInfoByID(menu.PID);
                    menu.Category = menuTemp.Category;
                }
            }
            return View(menu);
        }
        //新增菜单表单提交事件
        public ActionResult MenuAdd(HPStudent.Entity.Sys_Menu menu)
        {
            HPStudent.ViewModel.Common.RequestResult res = HPStudent.Business.Admin.SysMenu.MenuAdd(menu);
            return Json(res);
        }
        //修改菜单
        public ActionResult MenuUpdate(HPStudent.Entity.Sys_Menu menu)
        {
            HPStudent.ViewModel.Common.RequestResult res = HPStudent.Business.Admin.SysMenu.MenuUpdate(menu);
            return Json(res);
        }
        public ActionResult ShowChildMenu(int PID, int Category)
        {
            //通过PID查询母菜单
            HPStudent.Entity.Sys_Menu menu = new HPStudent.Entity.Sys_Menu();
            menu = HPStudent.Business.Admin.SysMenu.GetInfoByID(PID);
            if (Request.QueryString["Category"] != null)
            {
                ViewBag.PCategory = Category;
            }
            return View(menu);
        }
        //菜单（子）列表
        public ActionResult GetNavationMenuchildList(int draw, int start, int length, HPStudent.ViewModel.Sys_Menu.SysMenuViewModel KeyWords)
        {
            if (Request.QueryString["PID"] != null)
            {
                KeyWords.PID = Convert.ToInt32(Request.QueryString["PID"]);
            }
            KeyWords.SearchType = 1;
            ViewModel.Common.Datatable<HPStudent.Entity.Sys_Menu> Res = HPStudent.Business.Admin.SysMenu.GetNavationMenuList(start, length, KeyWords);
            Res.draw = draw;
            return Json(Res);
        }
        //设置菜单图标
        public ActionResult SetIcons()
        {
            return View();
        }
        #endregion
    }
}
