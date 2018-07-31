using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HPFv1.Entity;
using HPFv1.Core;

namespace HPFv1.Logic.QuestionTemplate
{
   public class QuestionTemplateController : Controller
    {
       public ActionResult Index() 
       {
           return View();
       }

       public JsonResult GetQuestionTemplateTable(int draw, int start, int length)
       {

           int UID = Convert.ToInt32(CookieHelper.GetCookieValue("UID"));
           HPFv1.ViewModel.Common.Datatable<HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable> qtt = HPFv1.Business.QuestionTemplate.QuestionTemplate.GetQuestionTemplateTable(start, length, UID);
           qtt.draw = draw;
           return Json(qtt);
       }

       public ActionResult QuestionTemplateEdit() 
       {
               return View();
       }

       [HttpPost]
       [ValidateInput(false)]
       public int AddQuestionTemplate(string title, string content, int topicCount) 
       {
           HPFv1.Entity.QuestionTemplate temp = new Entity.QuestionTemplate();
           temp.UID = Convert.ToInt32(CookieHelper.GetCookieValue("UID"));
           temp.Title = title;
           temp.Content = content;
           temp.CreateDate = DateTime.Now;
           temp.EditDate = DateTime.Now;
           temp.TopicCount = topicCount;
           int i = HPFv1.Business.QuestionTemplate.QuestionTemplate.AddTemplate(temp);
           return i ;
       }


      



       public JsonResult GetIdByQuestionTemplate(int TID) 
       {
           HPFv1.Entity.QuestionTemplate model = HPFv1.Data.DAL_QuestionTemplate.GetModel(TID);

           return Json(model);
       
       }

       [HttpPost]
       [ValidateInput(false)]
       public int UpdateQuestionTemplate(int tid,string title,string content,int topicCount) 
       {

           HPFv1.Entity.QuestionTemplate model = new HPFv1.Entity.QuestionTemplate();
           model.TID = tid;
           model.Title = title;
           model.Content = content;
           model.EditDate = DateTime.Now;
           model.TopicCount = topicCount;
           model.EditID = Convert.ToInt32(CookieHelper.GetCookieValue("UID"));
           int i = HPFv1.Data.DAL_QuestionTemplate.Update(model);
           return i;
       
       }


       [HttpPost]
       public int DeleteQuestionTemplate(int TID) 
       {

           int i = HPFv1.Data.DAL_QuestionTemplate.Delete(TID); ;
           return i;
       }

       public ActionResult panal() 
       {
           return View();
       }


     

    }
}
