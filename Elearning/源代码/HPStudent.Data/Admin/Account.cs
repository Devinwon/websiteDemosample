using System;
using System.Collections.Generic;
using HPStudent.Entity;
using HPStudent.ViewModel;
using HPStudent.Data;

namespace HPStudent.Business.Admin
{
    public class Account
    {
        /// <summary>tp
        /// 管理员登录验证
        /// </summary>
        /// <param name="managerlogin"></param>
        /// <returns></returns>
        public static HPStudent.Entity .ManagerInfo IsAdminLogin(ViewModel.Account.ManagerLogin managerlogin)
        {
            ManagerInfo manager = new ManagerInfo();
            manager.MID = managerlogin.MID;
            manager.ManagerName = managerlogin.UserName;
            manager.Password = managerlogin.Password;
            return HPStudent.Data.Admin.Account.IsAdminLogin(manager);
        }

        public static bool CheckAdmin(string AdminName, string AdminPassword)
        {
            return HPStudent.Data.Admin.Account.CheckAdmin(AdminName, AdminPassword);
        }
    }
}
