using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web;


namespace HPFv1.Business.QuetionGroup
{
   public static class QuestionGroup
    {
       public static HPFv1.ViewModel.Common.Datatable<HPFv1.ViewModel.QuestionGroup.QuestionGroupTable> GetQuestionGroupTable(int start, int length, int QID) 
       {
           int TotalRows = 0;
           HPFv1.ViewModel.Common.Datatable<HPFv1.ViewModel.QuestionGroup.QuestionGroupTable> qg = new ViewModel.Common.Datatable<ViewModel.QuestionGroup.QuestionGroupTable>();
           qg.data = new List<ViewModel.QuestionGroup.QuestionGroupTable>();
           List<HPFv1.ViewModel.QuestionGroup.QuestionGroupTable> list = new List<ViewModel.QuestionGroup.QuestionGroupTable>();
           list = HPFv1.Data.DAL_QuestionGroup.GetQuestionGroupTable(start,length,QID,out TotalRows);
           qg.recordsFiltered = TotalRows;
           qg.recordsTotal = TotalRows;
           foreach (ViewModel.QuestionGroup.QuestionGroupTable item in list)
           {
               HPFv1.ViewModel.QuestionGroup.QuestionGroupTable table = new ViewModel.QuestionGroup.QuestionGroupTable();
               table.GID = item.GID;
               table.QID = item.QID;
               table.UID = item.UID;
               table.Title = item.Title;
               table.UName = item.UName;
               table.GroupName = item.GroupName;
               table.Password = item.Password;
               table.CreateDate = item.CreateDate;  
               table.Operation = "<button class=\"btn btn-primary btn-sm\" data-id=\"" + item.GID + "\" data-action=\"edit\">"
               + "<span class=\"fa fa-pencil\"></span>编辑</button> "
               + "<button class=\"btn btn-primary btn-sm \"  type=\"button\" data-id=\"" + item.GID + "\" data-action=\"delete\">"
               + "<span class=\"fa fa-times\"></span> 删除</button>"
               + "&nbsp;<button class=\"btn btn-primary btn-sm \"  type=\"button\" data-id=\"" + item.GID + "\">"
               + "<span class=\"fa fa-comments\"></span> 查看详情</button>";

               qg.data.Add(table);

           }

           return qg;
       }


       public static string GetGroupResult(int GID)
       {
           List<HPFv1.Entity.QuestionDetailHistory> list = HPFv1.Data.DAL_QuestionDetailHistory.GetList("GID=" + GID + "");



           if (list.Count != 0)
           {
               //int addHisResult = HPFv1.Data.DAL_QuestionDetailHistory.HisAdd(list);

               //int delQdResult = HPFv1.Data.DAL_QuestionDetail.GetQIDDelete(QID);

               List<HPFv1.Entity.JsonAnswer> questionResultList = new List<Entity.JsonAnswer>();//最后返回的结果

               List<HPFv1.Entity.QuestionAnswer> allQAList = new List<Entity.QuestionAnswer>();



               foreach (HPFv1.Entity.QuestionDetailHistory  item in list) //循环所有问卷答案，生成QuestionAnswer结果集
               {
                   List<HPFv1.Entity.QuestionAnswer> jsonList = new List<HPFv1.Entity.QuestionAnswer>();
                   jsonList = JsonConvert.DeserializeObject<List<HPFv1.Entity.QuestionAnswer>>(item.Answer);//将循环答案转为QuestionAnswer集合
                   foreach (HPFv1.Entity.QuestionAnswer jsonItem in jsonList)
                   {
                       allQAList.Add(jsonItem);
                   }
               }
               //遍刚刚生成的QuestionAnswer结果集
               foreach (HPFv1.Entity.QuestionAnswer QAItem in allQAList)//循环所有QuestionAnswer集合，取出QuestionAnswer对象
               {
                   //在返回的结果集中判断是否存在当前题目编号
                   //CreateQuesetion(ref toJsonList,QAItem)

                   //

                   if (questionResultList.Count > 0)//如果toJsonList为空,则添加
                   {

                       if (questionResultList.Any(p => p.Number.ToString() == QAItem.Number.ToString()))//判断toListJson中是否有此标题
                       {
                           var obj = questionResultList.Where(p => p.Number == QAItem.Number).FirstOrDefault();
                           switch (obj.Type.ToLower())
                           {
                               case "radio":
                                   var radio = obj.SelAnswer.Where(p => p.SelectItem == QAItem.Choice).FirstOrDefault();
                                   if (radio != null)
                                   {
                                       obj.SelAnswer.Where(q => q.SelectItem == QAItem.Choice).FirstOrDefault().SelectCount++;
                                   }
                                   else
                                   {
                                       HPFv1.Entity.SelAnswer sa = new Entity.SelAnswer();
                                       sa.SelectItem = QAItem.Choice;
                                       sa.SelectCount = 1;
                                       sa.SelectContent = QAItem.Content;
                                       obj.SelAnswer.Add(sa);
                                   }

                                   break;
                               case "checkbox":
                                   string[] chiarr = QAItem.Choice.Split(',');
                                   Dictionary<string, int> choice = new Dictionary<string, int>();
                                   foreach (string caitem in chiarr)
                                   {
                                       var checkbox = obj.SelAnswer.Where(p => p.SelectItem ==caitem).FirstOrDefault();
                                       if (checkbox != null)
                                       {
                                           obj.SelAnswer.Where(q => q.SelectItem == caitem).FirstOrDefault().SelectCount++;
                                       }
                                       else
                                       {
                                           HPFv1.Entity.SelAnswer sa = new Entity.SelAnswer();
                                           sa.SelectItem = caitem;
                                           sa.SelectCount = 1;
                                           sa.SelectContent = QAItem.CheckContent[caitem];
                                           obj.SelAnswer.Add(sa);
                                       }
                                   }
                                   break;
                               case "text":
                                   HPFv1.Entity.TextAnswer ta = new Entity.TextAnswer();
                                   ta.Content = QAItem.Content;
                                   obj.TxtAnswer.Add(ta);

                                   break;
                               case "textarea":
                                   HPFv1.Entity.TextAnswer taarea = new Entity.TextAnswer();
                                   taarea.Content = QAItem.Content;
                                   obj.TxtAnswer.Add(taarea);
                                   break;
                           }
                       }
                       else
                       {
                           HPFv1.Entity.JsonAnswer ja = new Entity.JsonAnswer();//临时保存QuestionAnswerItem(每个题目对应一个对象）
                           ja.Number = QAItem.Number;
                           ja.Type = QAItem.Type;
                           ja.Title = QAItem.Title;
                           List<HPFv1.Entity.SelAnswer> selList = new List<Entity.SelAnswer>();//新建选择题答案集合                          
                           List<HPFv1.Entity.TextAnswer> txtList = new List<Entity.TextAnswer>();//新建问答题答案集合
                           HPFv1.Entity.TextAnswer ta = new Entity.TextAnswer();
                           switch (QAItem.Type)
                           {
                               case "radio":
                                   HPFv1.Entity.SelAnswer radioSA = new Entity.SelAnswer();
                                   radioSA.SelectItem = QAItem.Choice;
                                   radioSA.SelectCount = 1;
                                   radioSA.SelectContent = QAItem.Content;
                                   selList.Add(radioSA);
                                   break;
                               case "checkbox":
                                   string[] chiarr = QAItem.Choice.Split(',');
                                   foreach (string caitem in chiarr)
                                   {
                                       HPFv1.Entity.SelAnswer checkSA = new Entity.SelAnswer();
                                       checkSA.SelectItem = caitem;
                                       checkSA.SelectCount = 1;
                                       checkSA.SelectContent = QAItem.CheckContent[caitem];
                                       selList.Add(checkSA);
                                   }
                                   
                                   break;
                               case "text":  
                                   ta.Content = QAItem.Content;
                                   txtList.Add(ta);
                                   break;
                               case "textarea":
                                   ta.Content = QAItem.Content;
                                   txtList.Add(ta);
                                   break;

                           }
                           ja.SelAnswer = selList;
                           ja.TxtAnswer = txtList;
                           questionResultList.Add(ja);

                       }

                   }
                   else
                   {
                       HPFv1.Entity.JsonAnswer ja = new Entity.JsonAnswer();//临时保存QuestionAnswerItem(每个题目对应一个对象）
                       ja.Number = QAItem.Number;
                       ja.Type = QAItem.Type;
                       ja.Title = QAItem.Title;
                       List<HPFv1.Entity.SelAnswer> selList = new List<Entity.SelAnswer>();//新建选择题答案集合
                       List<HPFv1.Entity.TextAnswer> txtList = new List<Entity.TextAnswer>();//新建问答题答案集合
                       HPFv1.Entity.TextAnswer ta = new Entity.TextAnswer();
                       switch (QAItem.Type)
                       {
                           case "radio":
                                HPFv1.Entity.SelAnswer radioSA = new Entity.SelAnswer();
                                radioSA.SelectItem = QAItem.Choice;
                                radioSA.SelectCount = 1;
                                radioSA.SelectContent = QAItem.Content;
                                selList.Add(radioSA);
                               break;
                           case "checkbox":
                               string[] chiarr = QAItem.Choice.Split(',');
                               foreach (string caitem in chiarr)
                               {
                                   HPFv1.Entity.SelAnswer checkSA = new Entity.SelAnswer();
                                   checkSA.SelectItem = caitem;
                                   checkSA.SelectCount = 1;
                                   checkSA.SelectContent = QAItem.CheckContent[caitem];
                                   selList.Add(checkSA);
                               }
                               break;
                           case "text":
                               ta.Content = QAItem.Content;
                               txtList.Add(ta);
                               break;
                           case "textarea":
                               ta.Content = QAItem.Content;
                               txtList.Add(ta);
                               break;
                       }
                       ja.SelAnswer = selList;
                       ja.TxtAnswer = txtList;
                       questionResultList.Add(ja);
                   }
               }

               foreach (HPFv1.Entity.JsonAnswer item in questionResultList)
               {
                   if (item.SelAnswer != null)
                   {
                       decimal SelCount = 0;
                       foreach (var selItem in item.SelAnswer)
                       {
                           SelCount += (int)selItem.SelectCount;//计算总和
                       }
                       foreach (var newSelItem in item.SelAnswer)
                       {
                           newSelItem.SelectCount = Math.Round(newSelItem.SelectCount / SelCount, 1) * 100;//计算百分比

                       }
                   }
               }

               string questionResultJson = JsonConvert.SerializeObject(questionResultList);//将生成结果转化为Json
               
               return questionResultJson;

           }
           else
           {
               return "";
           }

       }

       public static void DataTableToExcel(System.Data.DataTable dtData, string printaction, string fileName)
       {
           System.Web.UI.WebControls.GridView gvExport = null;
           // 当前对话 
           System.Web.HttpContext curContext = System.Web.HttpContext.Current;
           // IO用于导出并返回excel文件 
           System.IO.StringWriter strWriter = null;
           System.Web.UI.HtmlTextWriter htmlWriter = null;

           if (dtData != null)
           {

               // 设置编码和附件格式 
               curContext.Response.ContentType = "application/vnd.ms-excel";
               curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
               curContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName + ".XLS"));
               curContext.Response.Charset = "utf-8";

               // 导出excel文件 
               strWriter = new System.IO.StringWriter();
               htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);
               // 为了解决gvData中可能进行了分页的情况，需要重新定义一个无分页的GridView 
               gvExport = new System.Web.UI.WebControls.GridView();
               gvExport.DataSource = dtData.DefaultView;
               gvExport.AllowPaging = false;
               gvExport.DataBind();

               if (printaction == "contactprint")
               {
                   foreach (GridViewRow dg in gvExport.Rows)
                   {
                       dg.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat: @");
                   }
               }

               gvExport.RenderControl(htmlWriter);
               curContext.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />" + strWriter.ToString());
               curContext.Response.End();
           }
       }



    }
}
