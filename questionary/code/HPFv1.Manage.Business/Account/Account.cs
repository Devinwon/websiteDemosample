using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPFv1.Entity;
using HPFv1.Manage.ViewModel.Account;

namespace HPFv1.Manage.Business.Account
{
   public class Account
    {
       public static Manager IsAdminLogin(Login login)
       {
           Manager manager = new Manager();
           manager.NickName = login.NickName;
           manager.Account = login.Account;
           manager.Password = login.Password;
           return HPFv1.Manage.Data.Account.Account.IsAdminLogin(manager);
       }
    }
}
