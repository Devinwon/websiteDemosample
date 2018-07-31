using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using HPFv1.Core;

namespace HPFv1.Logic
{
    public class HomeController:Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "HP框架";
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            string sourceStr = f["tbString"];
            ViewBag.Result = Core.Security.SEncryptString(sourceStr)    ;
            ViewBag.Source = sourceStr;
            return View();
        }



    }
}
