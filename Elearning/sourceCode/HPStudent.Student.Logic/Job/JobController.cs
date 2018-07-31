using System;
using System.Web.Mvc;
using HPStudent.Core;

namespace HPStudent.Student.Logic.Job
{
    public class JobController : Controller
    {
        public ActionResult Index()
        {
            //加载数据
            //薪资范围
            ViewBag.SalaryRange = HPStudent.Student.Business.Common.StudentCommon.GetOptionListByCode("SalaryRange");
            return View();
        }
        [HttpPost]
        public ActionResult QuerySimilarJobJson(int draw, int start, int length, HPStudent.Student.ViewModel.Job.JobListParameter keyWord)
        {
            ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Job.JobList> Res = HPStudent.Student.Business.Job.QueryJobList(keyWord, length, start);
            Res.draw = draw;
            return Json(Res);
        }
        public ActionResult QueryJobDetails(int JId)
        {
            HPStudent.Student.ViewModel.Job.JobList model = HPStudent.Student.Business.Job.GetJobInfoByID(JId);
            return View(model);
        }
        //发送简历申请
        public ActionResult SendResume(HPStudent.Student.ViewModel.Job.JobSendViewModel jobsend)
        {
            //获取当前学生ID
            jobsend.SenderID=Convert.ToInt16(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            HPStudent.Student.ViewModel.Common.RequestResult res = HPStudent.Student.Business.Job.SendResume(jobsend);
            return Json(res);
        }

        public ActionResult SeeSenderCompany() 
        {
            ViewBag.SalaryRange = HPStudent.Student.Business.Common.StudentCommon.GetOptionListByCode("SalaryRange");
            return View();
        }
    }
}
