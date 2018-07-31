using HPStudent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.PostTheme
{
   public class PostThemeController : Controller
    {

       /// <summary>
       /// 绑定贴吧信息及登录用户是否关注贴吧
       /// </summary>
       /// <returns>贴吧信息</returns>
       public ActionResult Index() 
       {
           int PBID = Convert.ToInt32(Request.QueryString["PBID"]);
           int StudentID = Convert.ToInt32( CookieHelper.GetCookieValue("StudentID"));
           HPStudent.Student.ViewModel.PostTheme.PostBarInfoTable entity = HPStudent.Student.Business.PostTheme.GetModel(PBID, StudentID);
           return View(entity);
       }

       /// <summary>
       /// 绑定贴吧主题
       /// </summary>
       /// <param name="start">开始页数</param>
       /// <param name="length">每页显示行数</param>
       /// <param name="PBID">贴吧ID</param>
       /// <returns></returns>
       public JsonResult BindPostTheme(int start, int length, int PBID) 
       {
           HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.PostTheme.PostThemeTable> table = HPStudent.Student.Business.PostTheme.BindPostTheme(start, length, PBID);
           return Json(table);
       }

       /// <summary>
       /// 关注贴吧
       /// </summary>
       /// <param name="PBID">贴吧ID</param>
       /// <returns>1或者0</returns>
       public int Attention(int PBID)
       {
           int StudentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
           int result = HPStudent.Student.Business.PostTheme.Attention(PBID, StudentID);
           return result;
       
       }

       /// <summary>
       /// 取消关注贴吧
       /// </summary>
       /// <param name="PBID">贴吧ID</param>
       /// <returns>1或者0</returns>
       public int CancelAttention(int PBID)
       {
           int StudentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
           int result = HPStudent.Student.Business.PostTheme.CancelAttention(PBID, StudentID);
           return result;

       }

       /// <summary>
       /// 帖子添加
       /// </summary>
       /// <returns></returns>
       public ActionResult PostThemeEdit() 
       {
           return View();
       }

       /// <summary>
       /// 主题回复添加
       /// </summary>
       /// <param name="PTID">主题ID</param>
       /// <param name="PRContent">回复内容</param>
       /// <returns>0或1</returns>
       [HttpPost]
       [ValidateInput(false)]
       public int PostThemeAdd(int PBID,  string PostThemeContent,string PostContent)
       {
           int StudentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
           PostContent = HttpUtility.UrlDecode(PostContent).ToString();
           int result = HPStudent.Student.Business.PostTheme.PostReplyAdd(PBID, PostThemeContent, PostContent, StudentID);
           return result;
       }

    }
}
