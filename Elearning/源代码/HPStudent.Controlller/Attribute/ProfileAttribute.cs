using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;
using System.Web;
using System.Diagnostics;

namespace HPStudent.Logic.Attribute
{
    public class ProfileAttribute : ActionFilterAttribute
    {
        private Stopwatch timer;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();
            //filterContext.HttpContext.Response.Write("Action method elapsed time: " + timer.Elapsed.Milliseconds.ToString());
            filterContext.HttpContext.Response.Headers.Add("times", string.Format("{0}ms", timer.Elapsed.Milliseconds.ToString()));
        }
    }
}
