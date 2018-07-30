using HPStudent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.SeeSenderCompany
{
   public class SeeSenderCompanyController : Controller
    {
       public ActionResult Index() 
       {
           //加载数据
           //薪资范围
           ViewBag.SalaryRange = HPStudent.Student.Business.Common.StudentCommon.GetOptionListByCode("SalaryRange");
           return View();
       }

       public JsonResult GetSeeSenderCompanyTable(int draw, int start, int length, HPStudent.Student.ViewModel.Job.JobListParameter keyWord) 
       {
           string SenderID = CookieHelper.GetCookieValue("StudentID");
           ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Job.JobList> Res = HPStudent.Student.Business.Job.GetSeeSenderCompanyTable(keyWord,SenderID , length, start);
           Res.draw = draw;
           return Json(Res);
       }

    }
}
