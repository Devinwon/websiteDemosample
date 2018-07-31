using System;
using System.Web.Mvc;
using System.Collections.Generic;
using HPStudent.Core;
using HPStudent.Student.Business;
using StuVModel = HPStudent.Student.ViewModel;


namespace HPStudent.Student.Logic
{
    public class WebpageController : Controller
    {
        /// <summary>
        /// 阿里支付页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Alipay()
        {
            return View();
        }

        /// <summary>
        /// 短训推荐也页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult VideoList()
        {
            return View();
        }

        /// <summary>
        /// 短训推荐视频详细页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult VideoDetail(int id)
        {
            StuVModel.Project.DetailProject myDetailProject = HPStudent.Student.Business.Projects.GetDetailProjectByPID(id);
            return View(myDetailProject);
        }

        /// <summary>
        /// 记录短训学生的信息
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Register(string realname, string mobile, string email)
        {
            int iResult = HPStudent.Student.Business.Webpage.Register(realname, mobile, email);
            return Json(new { iResult = iResult });
        }
    }
}
