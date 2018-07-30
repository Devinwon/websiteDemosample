using System;
using System.Web.Mvc;
using HPStudent.Core;

namespace HPStudent.Student.Logic
{
    public class HomeController:Controller
    {
        public ActionResult Index()
        {


            return RedirectToAction("Index", "Exercises");
        } 
        public ActionResult Test()
        {
            ViewBag.Showtime = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            return View();
        }
    }
}
