using System;
using System.Web.Mvc;
using System.Collections.Generic;
using HPStudent.Core;
using HPStudent.Student.Business;


namespace HPStudent.Student.Logic
{
    public class PlayerController : Controller
    {
        public ActionResult Pdf()
        {
            return View();
        }
        public ActionResult Video()
        {
            return View();
        }
        /// <summary>
        /// 对短训宣传开放的PPT查看页面，无需验证用户信息
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult OutPdf()
        {
            return View();
        }

        /// <summary>
        /// 对短训宣传开放的视频播放页面，观看视频需要填写个人信息（前端实现）
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult OutVideo()
        {
            return View();
        }

        /// <summary>
        /// 对短训宣传开放的文件下载页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult OutDownload(string f)
        {
            string fpath = Server.MapPath("~/Library/Download" + f);
            string fname = System.IO.Path.GetFileName(fpath);
            return File(fpath,"application/octet-stream",fname);

        }

        public ActionResult Download(string f)
        {
            string fpath = Server.MapPath("~/Library/Download" + f);
            string fname = System.IO.Path.GetFileName(fpath);
            return File(fpath, "application/octet-stream", fname);

        }
    }
}
