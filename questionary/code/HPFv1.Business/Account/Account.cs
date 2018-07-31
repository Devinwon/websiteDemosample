using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPFv1.ViewModel;
using HPFv1.Entity;
using HPFv1.Data;
namespace HPFv1.Business.Admin
{
    public class Account
    {
        public static Users IsAdminLogin(Login login)
        {
            Users user = new Users();
            user.NickName = login.NickName;
            user.Email = login.Email;
            user.Password = login.Password;
            return HPFv1.Data.Account.DAL_Account.IsAdminLogin(user);
        }


    }
}
