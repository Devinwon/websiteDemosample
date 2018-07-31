using HPFv1.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPFv1.Logic.QuestionDetail
{
   public class QuestionDetailController : Controller
    {
       public ActionResult Index() 
       {
           return View();  
       }


       [HttpPost] 
       public JsonResult QuestionDetailLogin(string qcode, string password)
       {
           CookieHelper.SetCookie("QuestionPassword", password);
           HPFv1.ViewModel.QuestionDetail.QuestionDetailTable entity = HPFv1.Data.DAL_QuestionDetail.QuestionDetailLogin(qcode, password);

           return Json(entity); 
       }

       [HttpPost]
       public int AddQuestionDetail(int qid,int gid,string answer) 
       {

           HPFv1.Entity.QuestionDetail entity = new HPFv1.Entity.QuestionDetail();
           entity.QID = qid;
           entity.GID = gid;
           entity.Answer = answer;
           entity.PostDate = DateTime.Now;
           entity.Password = CookieHelper.GetCookieValue("QuestionPassword");
           entity.PostIP = HPFv1.Core.HttpHelper.GetIPAddress();
           int i = HPFv1.Data.DAL_QuestionDetail.Add(entity);
           return i;


          
       }

    }
}
