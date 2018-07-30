using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;


namespace HPStudent.Logic.Attribute
{
    public class CustomExceptionAttribute : FilterAttribute, IExceptionFilter  
    {
        
        public void OnException(ExceptionContext filterContext)
        {

            filterContext.Controller.ViewData["Exception"] = filterContext.Exception;

            //filterContext.Result = new ViewResult() { ViewName = filterContext.Controller.ControllerContext.RouteData.Values["Action"].ToString(), ViewData = filterContext.Controller.ViewData };
            
            
            Exception Error = filterContext.Exception;
            string Message = Error.Message;//错误信息
            string Url = HttpContext.Current.Request.RawUrl;//错误发生地址
            string controllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];
            //处理错误异常
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Debug(Url, Error);

            //定义跳转地址
            filterContext.Result = new ViewResult() {  ViewName = "Error500", ViewData = filterContext.Controller.ViewData };

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
 

        }
    }
}
