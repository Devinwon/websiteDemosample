using HPStudent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.InterviewInvitation
{
    public class InterviewInvitationController : Controller
    {
        public ActionResult Index() 
        {
            return View();
        }

        public JsonResult GetInterviewInvitationTable(int draw, int start, int length) 
        {

            string SID = CookieHelper.GetCookieValue("StudentID");
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> talbe = HPStudent.Student.Business.InterviewInvitation.GetInterviewInvitationTable(SID, start, length);
            talbe.draw = draw;

            return Json(talbe);
        
        }

        public ActionResult EnterpriseDetail(string IID)
        {
            string SID = CookieHelper.GetCookieValue("StudentID");
            HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable talbe = HPStudent.Student.Business.InterviewInvitation.GetEnterpriseDetailBind(SID, IID).data[0];

            return View(talbe);
        }

        //public JsonResult GetEnterpriseDetailBind(int draw, int start, int length,string IID) 
        //{
        //    string SID = CookieHelper.GetCookieValue("StudentID");
        //    HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> talbe = HPStudent.Student.Business.InterviewInvitation.GetEnterpriseDetailBind(SID, IID, start, length);
        //    talbe.draw = draw;



        //    return null;
        //}


    }
}