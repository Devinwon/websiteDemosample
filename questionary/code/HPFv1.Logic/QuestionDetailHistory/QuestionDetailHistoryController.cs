using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace HPFv1.Logic.QuestionDetailHistory
{
    public class QuestionDetailHistoryController : Controller
    {
        public ActionResult Index() 
        {
           
           
            return View();


        }

        public JsonResult GetGroupList(string QID) 
        {
            List<HPFv1.Entity.QuestionGroup> list = HPFv1.Data.DAL_QuestionGroup.GetList("QID =" + QID);
            return Json(list);
        
        }


        public JsonResult GetQuestionDetailHistoryTable(int draw, int start, int length, string QID, string GID, string startTime, string endTime) 
        {
            HPFv1.ViewModel.Common.Datatable<HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable> qdh = HPFv1.Business.QuestionDetailHistory.QuestionDetailHistory.GetQuestionDetailHistoryTable(start, length, Convert.ToInt32(QID),  Convert.ToInt32(GID), startTime, endTime);
            qdh.draw = draw;
            return Json(qdh);
           
        }

        public ActionResult ShowQuestionDetailHistory() 
        {
            return View();
        }


        public JsonResult GetQuestionTemplateBind(int QID) 
        {
            HPFv1.Entity.Question entity = HPFv1.Data.DAL_Question.GetModel(QID);
            return Json(entity);
        
        }

        public JsonResult QuestionDetailHistoryBind(int DID) 
        {
            HPFv1.Entity.QuestionDetailHistory entity = HPFv1.Data.DAL_QuestionDetailHistory.GetModel(DID);
            return Json(entity);
        
        }
    }
}
