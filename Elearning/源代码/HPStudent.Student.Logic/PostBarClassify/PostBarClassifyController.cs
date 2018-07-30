using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.PostBarClassify
{
   public class PostBarClassifyController:Controller
    {
       public ActionResult Index() 
       {
           return View();
       
       }

       /// <summary>
       ///  贴吧分类绑定
       /// </summary>
       /// <returns></returns>
       public JsonResult BindPostBarClassify(int start, int length) 
       {
           HPStudent.Student.ViewModel.Common.GridView<HPStudent.Student.ViewModel.Project.MainProject> ProjectList = HPStudent.Student.Business.PostBarClassify.BindPostBarClassify();
           return Json(ProjectList);
       }

       /// <summary>
       /// 热门贴吧绑定
       /// </summary>
       /// <returns></returns>
       public JsonResult BindHotPostBar() 
       {
           List<HPStudent.Entity.PostBarInfo> ProjectList = HPStudent.Student.Business.PostBarClassify.BindHotPostBar();
           return Json(ProjectList);
       }

    }
}
