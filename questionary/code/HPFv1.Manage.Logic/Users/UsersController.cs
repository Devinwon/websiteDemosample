using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HPFv1.Manage.ViewModel;

namespace HPFv1.Manage.Logic.Users
{
    public class UsersController : Controller
    {

        public ActionResult Index() 
        {
            return View();
        
        }


        public JsonResult GetUsersTable(int draw ,int start,int length) 
        {
            HPFv1.Manage.ViewModel.Common.Datatable<HPFv1.Manage.ViewModel.Users.UserTable> userTable = HPFv1.Manage.Business.Users.Users.GetUsersTable(start,length);
            userTable.draw = draw;
            return Json(userTable);
        
        
        }

        public ActionResult UsersEdit() 
        {
            return View();        
        }


        public JsonResult UsersBind(string UID) 
        {
            HPFv1.Entity.Users entity = HPFv1.Manage.Data.DAL_Users.GetModel(Convert.ToInt32(UID));
            return Json(entity);
        }


        public int UpdateUsers(string UID, string nickName, string email, string password) 
        {
            HPFv1.Entity.Users entity = new HPFv1.Entity.Users();
            entity = HPFv1.Manage.Data.DAL_Users.GetModel(Convert.ToInt32(UID));
            entity.NickName = nickName;
            entity.Email = email;
            entity.Password = password;
            int result = HPFv1.Manage.Data.DAL_Users.Update(entity);
            return result;
        
        }

        public int AddUsers(string nickName, string email, string password) 
        {
            HPFv1.Entity.Users entity = new HPFv1.Entity.Users();
            entity.NickName = nickName;
            entity.Email = email;
            entity.Password = password;
            entity.CreateDate = DateTime.Now;
            entity.LastLogin = DateTime.Now;
            entity.LastIP = HttpContext.Request.UserHostAddress ;
            int result = HPFv1.Manage.Data.DAL_Users.Add(entity);
            return result;
        
        }

        public int DeleteUsers(string UID) 
        {
            int result = HPFv1.Manage.Data.DAL_Users.Delete(Convert.ToInt32(UID));
            return result;
        }

        public int UsersEmailValidation(string email) 
        {

            List<HPFv1.Entity.Users> list = HPFv1.Manage.Data.DAL_Users.GetList("email='" + email + "'");
            return list.Count;
            
        }

    }
}
