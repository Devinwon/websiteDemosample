using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.PostBarInfo
{
   public class PostBarInfoController : Controller
    {
       public ActionResult Index() 
       {
           int PBCID = Convert.ToInt32(Request.QueryString["PBCID"]);
           ViewBag.Title = HPStudent.Student.Business.PostBarInfo.GetPTCName(PBCID);
           return View();
       }

       /// <summary>
       /// 绑定贴吧
       /// </summary>
       /// <param name="PBCID">贴吧分类ID</param>
       /// <returns>贴吧集合</returns>
       public JsonResult BindPostBar(int PBCID)
       {
           List<HPStudent.Entity.PostBarInfo> ProjectList = HPStudent.Student.Business.PostBarInfo.BindPostBar(PBCID);
           return Json(ProjectList);
       }

      
    }
}
