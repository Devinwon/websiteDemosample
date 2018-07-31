using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPFv1.Entity;
using HPFv1.Core;


namespace HPFv1.Business.Common
{
   public class Common
    {
       public static List<UsersMenu> GetSideBar() 
       {
           return HPFv1.Data.Common.DAL_Common.GetSideBar();
       }

       public static string GetAdminInfo()
       {
           string adminname = CookieHelper.GetCookieValue("AdminName");
           return adminname;
       
       }

    }
}
