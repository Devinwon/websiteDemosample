using HPStudent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.Utility
{
    public class UtilityController : Controller
    {
        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}
