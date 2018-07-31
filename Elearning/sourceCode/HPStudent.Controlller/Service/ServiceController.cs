using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web;
using HPStudent.Core;
using HPStudent.Business;
using HPStudent.Entity;
using HV = HPStudent.ViewModel;

namespace HPStudent.Logic.Service
{
    public class ServiceController : Controller
    {
        public int ManagerID = Convert.ToInt32(CookieHelper.GetCookieValue("MID"));
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Suggest()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetSuggestList(int draw, int start, int length)
        {

            HPStudent.ViewModel.Common.Datatable<HV.Service.Suggest> suggestList = Business.Admin.Service.GetSuggestList(start, length);
            suggestList.draw = draw;
            return Json(suggestList);
        }

        public JsonResult ReplySuggest(HPStudent.Entity.SuggestItem suggestItem)
        {
            suggestItem.IsStudent = 0;
            suggestItem.TeacherID = ManagerID;
            HPStudent.ViewModel.Common.RequestResult result = Business.Admin.Service.ReplySuggest(suggestItem);
            return Json(result);
        }

        public ActionResult SuggestDetail(int SID)
        {
            HV.Service.SuggestDetailList myDetail = Business.Admin.Service.GetSuggestDetail(SID);
            ViewBag.SID = SID;
            ViewBag.State = myDetail.Status;
            return View(myDetail);
        }
        //事件完结
        [HttpPost]
        public ActionResult SuggestEndEvent(HPStudent.Entity.SuggestItem suggestItem)
        {
            HPStudent.ViewModel.Common.RequestResult res = HPStudent.Business.Admin.Service.SuggestEndEvent(suggestItem.SID);
            return Json(res);
        }
    }
}
