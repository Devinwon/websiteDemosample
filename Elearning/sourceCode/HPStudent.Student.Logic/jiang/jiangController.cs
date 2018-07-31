using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.jiang
{
   public class jiangController : Controller
    {
       [AllowAnonymous]
       public ViewResult index() 
       {

           return View();
       }
       [AllowAnonymous]
       public ViewResult Champion() 
       {
           return View();
       }
        [AllowAnonymous]
       public ViewResult RunnerUp() 
       {
           return View();
       }
        [AllowAnonymous]
        public ViewResult Third() 
        {
            return View();
        }

    }
}
