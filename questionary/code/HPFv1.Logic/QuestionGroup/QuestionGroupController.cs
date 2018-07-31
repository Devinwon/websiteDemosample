using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using HPFv1.Core;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web;


namespace HPFv1.Logic.QuestionGroup
{
  public  class QuestionGroupController : Controller
    {

      public ActionResult Index() 
       {
          int QID = Convert.ToInt32(Request.QueryString["QID"]);
          HPFv1.Entity.Question entity = HPFv1.Data.DAL_Question.GetModel(QID);
          int port = HttpContext.Request.Url.Port;
              if (port != 80)
              {
                  ViewBag.QuestionUrl = "http://" + HttpContext.Request.Url.Host + ":" + port.ToString() + "/QuestionDetail/Index?QuestionCode=" + entity.QuestionCode;
              }
              else 
              {
                  ViewBag.QuestionUrl ="http://"+ HttpContext.Request.Url.Host + "/QuestionDetail/Index?QuestionCode=" + entity.QuestionCode;
              }
          return View();
      }


      public JsonResult GetQuestionGroupTable(int draw, int start, int length, int QID) 
      {
          HPFv1.ViewModel.Common.Datatable<HPFv1.ViewModel.QuestionGroup.QuestionGroupTable> qg = HPFv1.Business.QuetionGroup.QuestionGroup.GetQuestionGroupTable(start, length, QID);
          qg.draw = draw;
          return Json(qg);
          
       
      
      }


      public int QuestionGroupDelete(int GID) 
      {
         int i = HPFv1.Data.DAL_QuestionGroup.Delete(GID);
         return i;
      }



      public ActionResult QuestionGroupEdit() 
      {
         
          return View();
      }

      public JsonResult QuestionGroupBind(int GID) 
      {
          //HPFv1.Entity.QuestionGroup entity = HPFv1.Data.DAL_QuestionGroup.GetModel(GID);
          HPFv1.ViewModel.QuestionGroup.QuestionGroupTable table = new ViewModel.QuestionGroup.QuestionGroupTable();
          HPFv1.Entity.QuestionGroup entity = HPFv1.Data.DAL_QuestionGroup.GetModel(GID);
          if (GID != 0)
          {
              table.GID = entity.GID;
              table.QID = entity.QID;
              table.UID = entity.UID;
              table.GroupName = entity.GroupName;
              table.Password = entity.Password;
              table.UsersList = HPFv1.Data.DAL_Users.GetList("");
              table.QuestionList = HPFv1.Data.DAL_Question.GetList("");
             
          }
          else 
          {
              table.UsersList = HPFv1.Data.DAL_Users.GetList("");
              table.QuestionList = HPFv1.Data.DAL_Question.GetList("");
          }

          return Json(table);
      
      }

      [HttpPost]
      [ValidateInput(false)]
      public int UpdateQuestionGroup(int gid,string groupname, string password,int qid) 
      {
        HPFv1.Entity.QuestionGroup entity = new Entity.QuestionGroup();
        entity.GID = gid;
        entity.GroupName = groupname;
        entity.Password = password;
        entity.UID = Convert.ToInt32( CookieHelper.GetCookieValue("UID"));
        entity.QID = qid;
        entity.CreateDate = DateTime.Now;
        int i = HPFv1.Data.DAL_QuestionGroup.Update(entity);
        return i;
      }

      [HttpPost]
      [ValidateInput(false)]
      public int AddQuestionGroup(string groupname, string password, int qid)
      {
          HPFv1.Entity.QuestionGroup entity = new Entity.QuestionGroup();
          entity.GroupName = groupname;
          entity.Password = password;
          entity.UID = Convert.ToInt32(CookieHelper.GetCookieValue("UID"));
          entity.QID = qid;
          entity.CreateDate = DateTime.Now;
          int i = HPFv1.Data.DAL_QuestionGroup.Add(entity);
          return i;
      }



      public bool GetQrCode(string strContent, MemoryStream ms)
      {
          ErrorCorrectionLevel Ecl = ErrorCorrectionLevel.M;
          string Content = strContent;
          QuietZoneModules QuietZones = QuietZoneModules.Two;


          int ModuleSize = 12;
          var encoder = new QrEncoder(Ecl);
          QrCode qr;
          if (encoder.TryEncode(Content, out qr))
          {
              var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones));
              render.WriteToStream(qr.Matrix, ImageFormat.Png, ms);
          }
          else
          {
              return false;

          }

          return true;
      }

      [HttpGet]
      public ActionResult GetTwoDimensionCode()
      {
          using (var ms = new MemoryStream())
          {
              string url = Request.Params["path"];
              GetQrCode(url, ms);
              Response.ContentType = "image/Png";
              Response.OutputStream.Write(ms.GetBuffer(), 0, (int)ms.Length);
              Response.End();

          }
          return View();
      }

      public ActionResult  ShowGroupResult() 
      {

          return View();
      }


      public JsonResult GetShowResult(int GID) 
      {


          HPFv1.Entity.Question entity = HPFv1.Data.DAL_Question.GetModel(HPFv1.Data.DAL_QuestionGroup.GetModel(GID).QID);
          List<HPFv1.Entity.JsonAnswer> list = new List<HPFv1.Entity.JsonAnswer>();
          string result = HPFv1.Business.QuetionGroup.QuestionGroup.GetGroupResult(GID);
          list = JsonConvert.DeserializeObject<List<HPFv1.Entity.JsonAnswer>>(result);//将循环答案转为QuestionAnswer集合
          HPFv1.ViewModel.Question.QuestionCountTable qct = new HPFv1.ViewModel.Question.QuestionCountTable();
          qct.Title = entity.Title;
          qct.JsonAnswer = list;
          return Json(qct);

      }

      public int passwordVerification(string password,string qid) 
      {
          List<HPFv1.Entity.QuestionGroup> list = HPFv1.Data.DAL_QuestionGroup.GetList("password='" + password + "' and QID = " + Convert.ToInt32(qid));
          int result = list.Count;
         return result;
      }

    }    
}  
