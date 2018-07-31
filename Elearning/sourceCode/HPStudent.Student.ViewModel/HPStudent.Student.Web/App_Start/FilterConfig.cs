using System.Web;
using System.Web.Mvc;

namespace HPStudent.Student.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HPStudent.Logic.Attribute.CustomExceptionAttribute(), 1);     //自定义的异常特性
            filters.Add(new HPStudent.Student.Logic.Attribute.AuthorizationAttribute());          //自定义的验证特性
            filters.Add(new HandleErrorAttribute());
        }
    }
}
