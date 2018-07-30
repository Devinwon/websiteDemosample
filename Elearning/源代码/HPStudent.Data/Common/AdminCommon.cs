using System;
using System.Collections.Generic;
using HPStudent.Entity;
using HPStudent.ViewModel;
using HPStudent.Data;
using HPStudent.Core;

namespace HPStudent.Business.Common
{
    public class AdminCommon
    {
        public static List<Sys_Menu> GetSideBar()
        {
            int MID = Convert.ToInt32(Core.CookieHelper.GetCookieValue("MID"));
            List<Sys_Menu> li = Data.Common.AdminCommon.GetSideBar(MID);
            List<Sys_Menu> liTemp = li.FindAll(x => x.PID == 0);
            //移除没有子菜单权限的母菜单(不包括母菜单的子菜单数为0的)
            foreach (var item in liTemp)
            {
                if (li.FindAll(x => x.PID == item.MID).Count <= 0 && item.ChildNum != 0)
                {
                    li.Remove(li.Find(x => x.MID == item.MID));
                }
            }
            return li;
        }
        public static string getAdminInfo()
        {
            string adminname = CookieHelper.GetCookieValue("AdminName");
            return adminname;

        }
    }
}
