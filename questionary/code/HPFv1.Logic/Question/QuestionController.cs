using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HPFv1.ViewModel.Question;
using HPFv1.ViewModel.Common;
using HPFv1.Business.Question;
using Newtonsoft.Json;
using HPFv1.Core;
using System.IO;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;


namespace HPFv1.Logic.Question
{
    public class QuestionController : Controller
    {
        public ActionResult Index() 
        {

            return View();
        }


        public JsonResult GetQuestionTable(int draw, int start, int length, string title, string startTime, string endTime) 
        {
            int UID = Convert.ToInt32( CookieHelper.GetCookieValue("UID"));
            Datatable<QuestionTable> questionList = HPFv1.Business.Question.Question.GetQuestionTable(start, length, UID, title, startTime, endTime);
            questionList.draw = draw;
            return Json(questionList);
        }

        public ActionResult QuestionTemplateEdit(long QID) 
        {
           
            return View();
        }


        [HttpPost]
        public int DeleteQuestion(int QID) 
        {

            int i = HPFv1.Data.DAL_Question.Delete(QID);
            return i;
        }

        [HttpPost]
        public JsonResult GetCreateQuestion(int QID) 
        {
            HPFv1.Entity.Question question = HPFv1.Data.DAL_Question.GetModel(QID);
            HPFv1.Entity.QuestionTemplate questionTemplate = HPFv1.Data.DAL_QuestionTemplate.GetModel(question.TemplateID);
            return Json(questionTemplate);
        }

        [ValidateInput(false)]
        public int CreateQuestion(int QID, string questionHtml) 
        {
        HPFv1.Entity.Question question = HPFv1.Data.DAL_Question.GetModel(QID);
        question.IsCraete = 1;
        question.QuestionHtml = questionHtml;
        int i = HPFv1.Data.DAL_Question.Update(question);
        return i;
        }

        
        public int GetCreateResult(int QID) 
        {
           int result = HPFv1.Business.Question.Question.GetCreateResult(QID);
           return result;
        }

        public JsonResult GetShowResult(int QID) 
        {
          HPFv1.Entity.Question entity =  HPFv1.Data.DAL_Question.GetModel(QID);
          List<HPFv1.Entity.JsonAnswer> list = new List<HPFv1.Entity.JsonAnswer>();
          list = JsonConvert.DeserializeObject<List<HPFv1.Entity.JsonAnswer>>(entity.QuestionResult);//将循环答案转为QuestionAnswer集合
          HPFv1.ViewModel.Question.QuestionCountTable qct = new QuestionCountTable();
          qct.Title = entity.Title;
          qct.JsonAnswer = list;
          return Json(qct);
        }

        public ActionResult ShowQuestionResult() 
        {
            return View();
        
        }

        public ActionResult QuestionEdit() 
        {
            return View();
        
        }

        public JsonResult QuestionBind(int QID) 
        {
            int UID = Convert.ToInt32(CookieHelper.GetCookieValue("UID"));
            HPFv1.ViewModel.Question.QuestionTable table = new ViewModel.Question.QuestionTable();
            HPFv1.Entity.Question entity = HPFv1.Data.DAL_Question.GetModel(QID);
            List<HPFv1.Entity.QuestionTemplate> qtList =  HPFv1.Data.DAL_QuestionTemplate.GetList("UID="+UID+"");
            if (QID != 0)
            {
                table.QID = entity.QID;
                table.Title = entity.Title;
                table.QuestionCode = entity.QuestionCode;
                table.StartDate = entity.StartDate;
                table.EndDate = entity.EndDate;
                table.TemplateID = entity.TemplateID;
                table.Description = entity.Description;
                table.QuestionTemplateList = qtList;
            }
            else
            {
                table.QuestionTemplateList = qtList;
            }

            return Json(table);
        
        
        }


        [HttpPost]
        [ValidateInput(false)]
        public int UpdateQuestion(int qid, string title, string description, string startTime, string endTime, int tid)
        {


            HPFv1.Entity.Question entity = HPFv1.Data.DAL_Question.GetModel(qid);
            entity.QID = qid;
            entity.Title = title;
            entity.TemplateID = tid;
            entity.StartDate = Convert.ToDateTime(startTime);
            entity.EndDate = Convert.ToDateTime(endTime);
            entity.Description = description;
            entity.UID = Convert.ToInt32( CookieHelper.GetCookieValue("UID"));
            int i = HPFv1.Data.DAL_Question.Update(entity);
            return i;
        }

        [HttpPost]
        [ValidateInput(false)]
        public int AddQuestion(string title,string description,string startTime ,string endTime ,int tid)
        {

            HPFv1.Entity.Question entity = new Entity.Question();
            entity.Title = title;
            entity.TemplateID = tid;
            entity.QuestionCode = "";
            entity.Description = description;
            entity.UID = Convert.ToInt32(CookieHelper.GetCookieValue("UID"));
            entity.StartDate = Convert.ToDateTime(startTime);
            entity.EndDate = Convert.ToDateTime(endTime);
            entity.QuestionHtml = "";
            entity.QuestionResult = "";
            entity.QNum = 0;
            entity.IsCraete = 0;
            entity.IsResult = 0;
            int i = HPFv1.Data.DAL_Question.Add(entity);


            entity.QID =  HPFv1.Data.DAL_Question.GetQID();
            entity.QuestionCode = ShortUrl(entity.QID.ToString())[0];
            int j = HPFv1.Data.DAL_Question.Update(entity);

            if (i > 0 && j > 0)
            {
                return 1;
            }
            else 
            {
                return 0;
            }

            
        }


        private static char[] constant = 
        { 
        '0','1','2','3','4','5','6','7','8','9', 
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z', 
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' 
        };

        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }

        public string GetGenerateRandom()
        {
            string CreateCode = GenerateRandom(6);
            string UID = CookieHelper.GetCookieValue("UID");
            List<HPFv1.Entity.Question> list = HPFv1.Data.DAL_Question.GetList("UID="+UID+"");
            if (list.Count(p => p.QuestionCode == CreateCode) != 0)
            {
                return GetGenerateRandom();
            }
            else 
            {
                return CreateCode;
            }

            
        }

           public static string[] ShortUrl(string url)
           {
               //可以自定义生成MD5加密字符传前的混合KEY   
               string key = "Leejor";
               //要使用生成URL的字符   
               string[] chars = new string[]{   
                "a","b","c","d","e","f","g","h",   
                "i","j","k","l","m","n","o","p",   
                "q","r","s","t","u","v","w","x",   
                "y","z","0","1","2","3","4","5",   
                "6","7","8","9","A","B","C","D",   
                "E","F","G","H","I","J","K","L",   
                "M","N","O","P","Q","R","S","T",   
                "U","V","W","X","Y","Z"   
   
              };
               //对传入网址进行MD5加密   
               string hex = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key + url, "md5");

               string[] resUrl = new string[4];

               for (int i = 0; i < 4; i++)
               {
                   //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算   
                   int hexint = 0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i * 8, 8), 16);
                   string outChars = string.Empty;
                   for (int j = 0; j < 6; j++)
                   {
                       //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引   
                       int index = 0x0000003D & hexint;
                       //把取得的字符相加   
                       outChars += chars[index];
                       //每次循环按位右移5位   
                       hexint = hexint >> 5;
                   }
                   //把字符串存入对应索引的输出数组   
                   resUrl[i] = outChars;
               }

               return resUrl;
           }  




           public static string MD5Encrypt(string strText)
           {
               byte[] result = Encoding.Default.GetBytes(strText);
               MD5 md5 = new MD5CryptoServiceProvider();
               byte[] output = md5.ComputeHash(result);
               return BitConverter.ToString(output).Replace("-", "");
           }

    }
}
