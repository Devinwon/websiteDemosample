using System;
using System.Web.Mvc;
using System.Collections.Generic;
using HPStudent.Core;
using HPStudent.Student.Business;
using HPStudent.Entity;
using HSV = HPStudent.Student.ViewModel;

namespace HPStudent.Student.Logic
{

    public class ServiceController : Controller
    {
        public string StudentID = CookieHelper.GetCookieValue("StudentID");
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSuggest()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddSuggest(HPStudent.Entity.Suggestions SuggestItem)
        {
            SuggestItem.StudentID = Convert.ToInt32(StudentID);
            HPStudent.Student.ViewModel.Common.RequestResult result = HPStudent.Student.Business.Suggestions.AddSuggest(SuggestItem);

            return Json(result);
        }

        public JsonResult ReplySuggest(HPStudent.Entity.SuggestItem suggestItem)
        {
            suggestItem.StudentID = Convert.ToInt32(StudentID);
            suggestItem.IsStudent = 1;
            HPStudent.Student.ViewModel.Common.RequestResult result = HPStudent.Student.Business.Suggestions.ReplySuggest(suggestItem);
            return Json(result);
        }
        public ActionResult Suggest()
        {
            return View();
        }

        public ActionResult Pop_AddSuggest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSuggestList(int draw, int start, int length)
        {
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Entity.Suggestions> pbList = HPStudent.Student.Business.Suggestions.GetSuggestList(StudentID, start, length);
            pbList.draw = draw;
            return Json(pbList);

        }

        public ActionResult SuggestDetail(int SID)
        {
            HSV.Service.SuggestDetailList myDetail = HPStudent.Student.Business.Suggestions.GetSuggestDetail(SID);
            ViewBag.State = myDetail.Status;
            return View(myDetail);
        }
        [HttpPost]
        public ActionResult SuggestScore(int SID)
        {
            HPStudent.Entity.Suggestions suggestions = HPStudent.Student.Business.Suggestions.GetSuggestBySID(SID);
            return View(suggestions);
        }
        public ActionResult SuggestScoreSave(HPStudent.Entity.Suggestions suggestions)
        {
            HPStudent.Student.ViewModel.Common.RequestResult result = HPStudent.Student.Business.Suggestions.SuggestScoreSave(suggestions);
            return Json(result);
        }
    }
}
