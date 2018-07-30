using HPStudent.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.PostReply
{
   public class PostReplyController : Controller
    {


       public ActionResult HtmlPage1() 
       {
           return View();
       
       }
       public ActionResult Index() 
       {
           int PTID = Convert.ToInt32(Request.QueryString["PTID"]);
           HPStudent.Student.ViewModel.PostTheme.PostBarInfoTable entity = HPStudent.Student.Business.PostReply.GetModel(PTID);
           return View(entity);;
       
       }

       /// <summary>
       /// 绑定贴吧回复
       /// </summary>
       /// <param name="start">开始页数</param>
       /// <param name="length">每页显示行数</param>
       /// <param name="PTID">贴吧ID</param>
       /// <returns></returns>
       public JsonResult BindPostReplyTable(int start, int length, int PTID, int showType)
       {
           int StudentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
           HPStudent.Student.ViewModel.PostReply.PostReplyDataTable<HPStudent.Student.ViewModel.PostReply.PostReplyTable> table = HPStudent.Student.Business.PostReply.BindPostReplyTable(start, length, PTID, showType, StudentID);
           table.LoginStudentID = StudentID;
           return Json(table);
       }

       /// <summary>
       /// 收藏帖子
       /// </summary>
       /// <param name="PTID">帖子ID</param>
       /// <returns>0:失败 1：成功</returns>
       public int Collect(int PTID) 
       {
           int StudentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
           int result = HPStudent.Student.Business.PostReply.Collect(PTID, StudentID);
           return result;
       }

       /// <summary>
       /// 添加楼层回复
       /// </summary>
       /// <param name="PRID">帖子ID</param>
       /// <param name="ReplyContent">回复内容</param>
       /// <param name="ByReplyManID">被回复人ID</param>
       /// <returns>0:失败 1：成功</returns>
       public int FloorReplyAdd(int PRID, string FloorReplyContent, int ByReplyManID) 
       {
           int StudentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
           int result = HPStudent.Student.Business.PostReply.FloorReplyAdd(PRID, FloorReplyContent, StudentID, ByReplyManID);
           return result;
       
       }

       /// <summary>
       /// 获取帖子回复内容
       /// </summary>
       /// <param name="PRID">帖子ID</param>
       /// <returns>获取一个帖子内容</returns>
       public JsonResult GetFloorReplyTable(int PRID) 
       {
           HPStudent.Student.ViewModel.PostReply.PostReplyTable entity = HPStudent.Student.Business.PostReply.GetFloorReplyTable(PRID);
           return Json(entity);
       
       }

       public JsonResult FloorReplyDel(int FRID, int PRID) 
       {
           HPStudent.Student.ViewModel.PostReply.PostReplyTable entity = HPStudent.Student.Business.PostReply.FloorReplyDel(FRID, PRID);
           entity.LoginStudentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
           return Json(entity);
       }
       /// <summary>
       /// 楼层内容翻页
       /// </summary>
       /// <param name="start">开始页数</param>
       /// <param name="length">每页显示多少行</param>
       /// <param name="PRID">楼层ID</param>
       /// <returns>翻页结果集</returns>
       public JsonResult ReplyPagerChangeGetTable(int start, int length, int PRID) 
       {
           HPStudent.Student.ViewModel.PostReply.PostReplyTable entity = HPStudent.Student.Business.PostReply.ReplyPagerChangeGetTable(start, length, PRID);
           entity.LoginStudentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
           return Json(entity);
       }

       /// <summary>
       /// 主题回复添加
       /// </summary>
       /// <param name="PTID">主题ID</param>
       /// <param name="PRContent">回复内容</param>
       /// <returns>0或1</returns>
       [HttpPost]
       [ValidateInput(false)]
       public int PostReplyAdd(int PTID, string PRContent)
       {
           int StudentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
           string RealName = Convert.ToString(CookieHelper.GetCookieValue("RealName"));
           PRContent = HttpUtility.UrlDecode(PRContent).ToString();
           int result = HPStudent.Student.Business.PostReply.PostReplyAdd(PTID, PRContent, StudentID, RealName);
           return result;
       }



       /// <summary>
       /// 回复内容图片上传
       /// </summary>
       /// <param name="file">图片信息</param>
       /// <returns>图片路径</returns>
       public ActionResult UploadProductDescriptionImage(HttpPostedFileBase file)
       {
           var imageUrl = "";

           if (file != null && file.ContentLength > 0)
           {
               string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
               string filePath = Path.Combine(Server.MapPath("~/PostBarFile/Image"), fileName);
               file.SaveAs(filePath);
               imageUrl = "~/PostBarFile/Image/" + fileName;
               imageUrl = Url.Content(imageUrl);
           }

           return Json(imageUrl);
       }

       public ActionResult MessageReport() 
       {
           return View();
       
       }

       /// <summary>
       /// 添加举报信息
       /// </summary>
       /// <param name="Tid">举报</param>
       /// <param name="BeReportUID">被举报ID</param>
       /// <param name="BeReporter">被举报人名称</param>
       /// <param name="Body">被举报内容</param>
       /// <param name="Type">举报类型</param>
       /// <returns>0失败，1成功 </returns>
       public int MessageReportAdd(int Tid,int BeReportUID,string BeReporter,string Body,int Type) 
       {
           string hostname = Dns.GetHostName();//得到本机名
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
           HPStudent.Entity.MessageReport entity = new Entity.MessageReport();
           entity.Tid = Tid;
           entity.BeReportUID = BeReportUID;
           entity.BeReporter = BeReporter;
           entity.Body = Body;
           entity.ReportUID =  Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));;
           entity.Reporter =Convert.ToString(CookieHelper.GetCookieValue("RealName"));
           entity.IsDo = 0;
           entity.ReportDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
           entity.IP = localhost.AddressList[0].ToString();
           entity.Category = Type;
           int result = HPStudent.Student.Business.PostReply.MessageReportAdd(entity);
           return result;  
       }

    }
}
