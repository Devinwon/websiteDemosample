using HPStudent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.SendResume
{
    public class ResumeInboxController : Controller
    {
        public ActionResult Index() 
        {
            return View();
        }

        public JsonResult GetResumeInboxTable(int draw, int start, int length) 
        {
            string EID = CookieHelper.GetCookieValue("StudentID");
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable> talbe = HPStudent.Student.Business.ResumeInbox.GetResumeInboxTable(EID,start,length) ;
            talbe.draw = draw;

            return Json(talbe);
        }

    }
}
