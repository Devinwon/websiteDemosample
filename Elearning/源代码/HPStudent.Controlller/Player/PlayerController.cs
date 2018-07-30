using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HPStudent.Logic.Attribute;

namespace HPStudent.Logic.Player
{
   public class PlayerController:Controller
    {
       /// <summary>
       /// 阅读PDF文件
       /// </summary>
       /// <returns></returns> 
       public ActionResult Pdf()
        {
            return View();
        }
       /// <summary>
       /// 观看视频
       /// </summary>
       /// <returns></returns>
        public ActionResult Video()
        {
            return View();
        }
       /// <summary>
       /// 下载文件
       /// </summary>
       /// <param name="f"></param>
       /// <returns></returns>
       public ActionResult Download(string f)
        {
            string fpath = Server.MapPath("~/Library/Download" + f);
            string fname = System.IO.Path.GetFileName(fpath);
            return File(fpath,"application/octet-stream",fname);

        }

    }
}
