using HPStudent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.MyBarHomePage
{
  public class MyBarHomePageController : Controller
    {
      public ActionResult Index() 
      {
          int StudentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
          HPStudent.Student.ViewModel.MyBarHomePage.MyBarHomePageTable entity = HPStudent.Student.Business.MyBarHomePage.GetMyBarHomePageTable(StudentID);
          return View(entity);
      
      }

    }
}
