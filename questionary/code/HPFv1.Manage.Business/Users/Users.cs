using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPFv1.Manage.Business.Users
{
   public class Users
    {
       public static HPFv1.Manage.ViewModel.Common.Datatable<HPFv1.Manage.ViewModel.Users.UserTable> GetUsersTable(int start,int length)
       {
           int TotalRows = 0;
           HPFv1.Manage.ViewModel.Common.Datatable<HPFv1.Manage.ViewModel.Users.UserTable> usersTalbe = new ViewModel.Common.Datatable<HPFv1.Manage.ViewModel.Users.UserTable>();
           List<HPFv1.Manage.ViewModel.Users.UserTable> list = new List<HPFv1.Manage.ViewModel.Users.UserTable>();
           list = HPFv1.Manage.Data.DAL_Users.GetUsersTable(start, length,out TotalRows);
           usersTalbe.data = new List<HPFv1.Manage.ViewModel.Users.UserTable>();
           usersTalbe.recordsFiltered = TotalRows;
           usersTalbe.recordsTotal = TotalRows;

           foreach (HPFv1.Manage.ViewModel.Users.UserTable item in list)
           {
               HPFv1.Manage.ViewModel.Users.UserTable table = new ViewModel.Users.UserTable();
               table.NickName = item.NickName;
               table.Email = item.Email;
               table.Password = item.Password;
               table.CreateDate = item.CreateDate;
               table.LastLogin = item.LastLogin;
               table.LastIP = item.LastIP;
               table.Operation = "<button class=\"btn btn-primary btn-sm\" data-id=\"" + item.UID + "\" data-action=\"edit\">"
                 + "<span class=\"fa fa-pencil\"></span>编辑</button> "
                 + "<button class=\"btn btn-primary btn-sm \" type=\"button\" data-id=\"" + item.UID + "\" data-action=\"delete\">"
                 + "<span class=\"fa fa-times\"></span> 删除</button> ";

               usersTalbe.data.Add(table);

           }


           return usersTalbe;
       
       }

    }
}
