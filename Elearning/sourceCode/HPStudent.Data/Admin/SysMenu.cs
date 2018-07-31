using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Business.Admin
{
    public class SysMenu
    {
        //获得菜单列表(分页)
        public static HPStudent.ViewModel.Common.Datatable<HPStudent.Entity.Sys_Menu> GetNavationMenuList(int start, int length, HPStudent.ViewModel.Sys_Menu.SysMenuViewModel KeyWords)
        {
            int TotalRows = 0;
            HPStudent.ViewModel.Common.Datatable<HPStudent.Entity.Sys_Menu> ProjectTable = new ViewModel.Common.Datatable<HPStudent.Entity.Sys_Menu>();
            //初始化返回Datatable行数
            ProjectTable.data = Data.Admin.SysMenu.GetNavationMenuList(start, length, out TotalRows, KeyWords);
            ProjectTable.recordsTotal = TotalRows;
            ProjectTable.recordsFiltered = TotalRows;
            return ProjectTable;
        }
        //获得菜单列表
        public static List<HPStudent.Entity.Sys_Menu> GetNavationMenuListNotByPage(HPStudent.ViewModel.Sys_Menu.SysMenuViewModel KeyWords)
        {
            return Data.Admin.SysMenu.GetNavationMenuListNotByPage(KeyWords);
        }
        //菜单向上（向下）移动
        public static HPStudent.ViewModel.Common.RequestResult MenuMove(HPStudent.ViewModel.Sys_Menu.SysMenuViewModel KeyWords)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            int iResult = HPStudent.Data.Admin.SysMenu.MenuMove(KeyWords);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    if (KeyWords.MoveType == 0)
                    {
                        result.ResultMsg = string.Format("上移成功！");
                    }
                    else
                    {
                        result.ResultMsg = string.Format("下移成功！");
                    }
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    if (KeyWords.MoveType == 0)
                    {
                        result.ResultMsg = string.Format("已经置顶！");
                    }
                    else
                    {
                        result.ResultMsg = string.Format("已经置底！");
                    }
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，移动操作处理失败！");
                    break;
            }
            return result;
        }
        //菜单添加事件
        public static HPStudent.ViewModel.Common.RequestResult MenuAdd(HPStudent.Entity.Sys_Menu menu)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            //获得最大序号和ID
            HPStudent.Entity.Sys_Menu maxMenu = HPStudent.Data.Admin.SysMenu.GetMaxIDAndSort();
            menu.MID = maxMenu.MID + 1;
            menu.SortCode = maxMenu.SortCode + 1;
            int iResult = HPStudent.Data.Admin.SysMenu.AddMenuItem(menu);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("添加成功！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加操作处理失败！");
                    break;
            }
            return result;
        }
        //菜单修改事件
        public static HPStudent.ViewModel.Common.RequestResult MenuUpdate(HPStudent.Entity.Sys_Menu menu)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            int iResult = HPStudent.Data.Admin.SysMenu.UpdateMenuItem(menu);
            //如果是母项的情况下修改子类别
            if (iResult == 0 && menu.PID == 0)
            {
                HPStudent.Data.Admin.SysMenu.UpdateChildCategory(menu);
            }
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("修改成功！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加操作处理失败！");
                    break;
            }
            return result;
        }
        //得到单个实体
        public static HPStudent.Entity.Sys_Menu GetInfoByID(int id)
        {
            return HPStudent.Data.Admin.SysMenu.GetInfoByID(id);
        }
    }
}
