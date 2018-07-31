using HPFv1.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPFv1.Entity;

namespace HPFv1.Manage.Business.Common
{
   public class Common
    {

        public static List<UsersMenu> GetSideBar()
        {
            return HPFv1.Manage.Data.Common.DAL_Common.GetSideBar();
        }

        public static string GetAdminInfo()
        {
            string adminname = CookieHelper.GetCookieValue("AdminName");
            return adminname;

        }

    }
}
