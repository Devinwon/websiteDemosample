using System;
using System.Collections.Generic;
using HPStudent.Entity;
using HPStudent.Student.ViewModel;
using HPStudent.Student.Data;
using HPStudent.Core;

namespace HPStudent.Student.Business.Common
{
    public class StudentCommon
    {
        public static List<Sys_Menu> GetSideBar()
        {
            int RoleID = Convert.ToInt32(Core.CookieHelper.GetCookieValue("RoleID"));
            List<Sys_Menu> li = HPStudent.Student.Data.Common.StudentCommon.GetSideBar(RoleID);
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
        public static string getStudentInfo()
        {
            string adminname = CookieHelper.GetCookieValue("StudentName");
            return adminname;
        }

        public static List<string> GetOptionListByCode(string strCode)
        {
            return Data.Common.StudentCommon.GetOptionListByCode(strCode);
        }

        public static List<OptionList> GetOptionListInCode(string code)
        {
            return Data.Common.StudentCommon.GetOptionListInCode(code);
        }
    }
}
