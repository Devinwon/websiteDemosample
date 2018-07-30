using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HPStudent.Logic
{
    public class UtilityController:Controller
    {
        public ActionResult PageNotFound()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Error500()
        {
            return View();
        }
        public ActionResult Demo()
        {
            return View();
        }
    }
}
