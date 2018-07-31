using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Logic.Resume
{
    public class ResumeController : Controller
    {
        public ActionResult Index()
        {
            int StudentID = Convert.ToInt16(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            HPStudent.Student.ViewModel.Resume.ResumeMain model = HPStudent.Student.Business.Resume.GetStudentResumeMainInfoByStudentID(StudentID);
            ViewBag.Province = HPStudent.Student.Business.Common.SchoolCommon.GetComAreaByParentAID("0");
            ViewBag.IsCheckProvince = HPStudent.Student.Business.Common.SchoolCommon.GetComAreaByAreaName(model.ResumeBasic.CityName).AreaID;
            int y = DateTime.Now.Year;
            int a = 2009;
            ViewData["year"] = y - a;
            return View(model);
        }

        [HttpPost]
        public JsonResult GetCityListByParentAID(string ParentAID)
        {
            return Json(Business.Common.SchoolCommon.GetComAreaByParentAID(ParentAID));
        }
        [HttpGet]
        public ActionResult PreviewResume(int SID)
        {
            HPStudent.Student.ViewModel.Resume.ResumeMain model = HPStudent.Student.Business.Resume.GetStudentResumeMainInfoByStudentID(SID);
            return View(model);
        }

        public ActionResult CreateOrUpdateResumeBasic(HPStudent.Entity.StudentResume model)
        {
            HPStudent.Student.ViewModel.Common.RequestResult Res = HPStudent.Student.Business.Resume.UpdateOrCreateResumeBaisic(model);
            return Json(Res);
        }

        public ActionResult CreateEducationRecord(HPStudent.Entity.SchoolDetail model)
        {
            HPStudent.Student.ViewModel.Common.RequestResult Res = HPStudent.Student.Business.Resume.AddEducationExp(model);
            return Json(Res);
        }

        public ActionResult UpdateEducationRecordByID(HPStudent.Entity.SchoolDetail model)
        {
            HPStudent.Student.ViewModel.Common.RequestResult Res = HPStudent.Student.Business.Resume.UpdateEducationExp(model);
            return Json(Res);
        }

        public ActionResult DeleteEducationRecordByID(int id)
        {
            HPStudent.Student.ViewModel.Common.RequestResult Res = HPStudent.Student.Business.Resume.DeleteEducationExpByID(id);
            return Json(Res);
        }

        public ActionResult CreateWorkExperience(HPStudent.Entity.StudentWorkExp model)
        {
            HPStudent.Student.ViewModel.Common.RequestResult Res = HPStudent.Student.Business.Resume.AddWorkExp(model);
            return Json(Res);
        }

        public ActionResult UpdateWorkExperienceByID(HPStudent.Entity.StudentWorkExp model)
        {
            HPStudent.Student.ViewModel.Common.RequestResult Res = HPStudent.Student.Business.Resume.UpdateWorkExp(model);
            return Json(Res);
        }

        public ActionResult DeletWorkExperienceByID(int id)
        {
            HPStudent.Student.ViewModel.Common.RequestResult Res = HPStudent.Student.Business.Resume.DeleteWorkExp(id);
            return Json(Res);
        }

        public ActionResult CreateProjectExperience(HPStudent.Entity.StudentProjectExp model)
        {
            HPStudent.Student.ViewModel.Common.RequestResult Res = HPStudent.Student.Business.Resume.AddProjectExp(model);
            return Json(Res);
        }

        public ActionResult UpdateProjectExperienceByID(HPStudent.Entity.StudentProjectExp model)
        {
            HPStudent.Student.ViewModel.Common.RequestResult Res = HPStudent.Student.Business.Resume.UpdateProject(model);
            return Json(Res);
        }

        public ActionResult DeleteProjectExperienceByID(int id)
        {
            HPStudent.Student.ViewModel.Common.RequestResult Res = HPStudent.Student.Business.Resume.DeleteProjectExp(id);
            return Json(Res);
        }

    }
}
