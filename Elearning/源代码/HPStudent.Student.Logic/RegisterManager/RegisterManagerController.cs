using HPStudent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.RegisterManager
{
    public class RegisterManagerController : Controller
    {
        public ActionResult EnterpriseReview()
        {
            return View();
        }
        //企业审核信息列表
        [HttpPost]
        public ActionResult QuerySimilarEnterpriseReviewJson(int draw, int start, int length, HPStudent.Student.ViewModel.Account.UserRegister KeyWords)
        {
            ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Account.RegisterCheckInfo> Res = HPStudent.Student.Business.RegisterManager.QueryEnterpriseReviewList(length, start, KeyWords);
            Res.draw = draw;
            return Json(Res);
        }
        public ActionResult StudentReview()
        {
            return View();
        }
        //学生审核信息列表
        [HttpPost]
        public ActionResult QuerySimilarStudentReviewJson(int draw, int start, int length, HPStudent.Student.ViewModel.Account.UserRegister KeyWords)
        {
            ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Account.RegisterCheckInfo> Res = HPStudent.Student.Business.RegisterManager.QueryStudentReviewList(length, start, KeyWords);
            Res.draw = draw;
            return Json(Res);
        } 
        //审核通过
        public ActionResult CheckPass(int SID)
        {
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.RegisterManager.CheckPass(SID);
            return Json(res);
        }
    }
}
