using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPFv1.ViewModel.Question;
using HPFv1.ViewModel.Common;
using Newtonsoft.Json;
using ThoughtWorks;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;


namespace HPFv1.Business.Question
{
    public class Question
    {
        public static Datatable<QuestionTable> GetQuestionTable(int start, int length, int UID, string title, string startTime, string endTime)
        {
            int TotalRows = 0;
            List<HPFv1.ViewModel.Question.QuestionTable> list = new List<HPFv1.ViewModel.Question.QuestionTable>();
            Datatable<HPFv1.ViewModel.Question.QuestionTable> questionList = new Datatable<HPFv1.ViewModel.Question.QuestionTable>();
            questionList.data = new List<QuestionTable>();

            list = HPFv1.Data.DAL_Question.GetQuestionTable(start, length, UID, title, startTime, endTime, out TotalRows);

            questionList.recordsFiltered = TotalRows;
            questionList.recordsTotal = TotalRows;

            foreach (HPFv1.ViewModel.Question.QuestionTable item in list) 
            {
                StringBuilder sb = new StringBuilder();
                HPFv1.ViewModel.Question.QuestionTable table = new HPFv1.ViewModel.Question.QuestionTable();
                table.QID = item.QID;
                table.TemplateID = item.TemplateID;
                table.Title = item.Title;
                table.Description = item.Description;
                table.QuestionHtml = item.QuestionHtml;
                table.QuestionResult = item.QuestionResult;
                table.StartDate = item.StartDate;
                table.EndDate = item.EndDate;
                table.QNum = item.QNum;
                table.IsCraete = item.IsCraete;
                table.IsResult = item.IsResult;
                table.QuestionCode = item.QuestionCode;
                table.TemplateName = item.TemplateName;
                sb.AppendFormat("<button class=\"btn btn-primary btn-sm\" data-id=\"" + item.QID + "\" data-action=\"edit\">"
                 + "<span class=\"fa fa-pencil\"></span>编辑</button> "
                 + "&nbsp;<button class=\"btn btn-primary btn-sm \" type=\"button\" data-id=\"" + item.QID + "\" data-action=\"delete\">"
                 + "<span class=\"fa fa-times\"></span> 删除</button> "
                + "&nbsp;<button class=\"btn btn-primary btn-sm \" type=\"button\" data-id=\"" + item.QID + "\" data-action=\"getData\">"
                 + "<span class=\"fa fa-truck\"></span>收集数据</button>"
               + "&nbsp;<button class=\"btn btn-primary btn-sm \" type=\"button\" data-id=\"" + item.QID + "\" data-action=\"getHistory\">"
                 + "<span class=\"fa fa-calendar\"></span>查看历史</button>");
                if (item.IsCraete == 0 && item.IsResult == 0)
                {
                    sb.AppendFormat("&nbsp;<button class=\"btn btn-danger \" type=\"button\" data-id=\"" + item.QID + "\" data-action=\"createQuestion\">"
                     + "<span class=\"fa fa-bars\"></span> 生成问卷</button>");
                }
                else if (item.IsCraete == 1 && item.IsResult == 0)
                {
                    sb.AppendFormat("&nbsp;<button class=\"btn btn-info \" type=\"button\" data-id=\"" + item.QID + "\" data-action=\"createResult\">"
                   + "<span class=\"fa fa-check-circle\"></span> 生成结果</button>");
                }
                else if (item.IsCraete == 1 && item.IsResult == 1)
                {
                    sb.AppendFormat("&nbsp;<button class=\"btn btn-success \" type=\"button\" data-id=\"" + item.QID + "\" data-action=\"ShowResult\">"
                          + "<span class=\"fa fa-comments\"></span> 查看结果</button>");
                }
                table.Operation = sb.ToString();
                questionList.data.Add(table);
            }           
            return questionList;

        }




        /// <summary>
        /// 生成结果保存
        /// </summary>
        /// <param name="QID">问卷ID</param>
        /// <returns></returns>
        public static int GetCreateResult(int QID)
        {
            int QNum = 0;  //题目总数
            List<HPFv1.Entity.QuestionDetail> list= HPFv1.Data.DAL_QuestionDetail.GetList("QID=" + QID + "");



            if (list.Count != 0)
            {
               
                QNum = list.Count;
                int addHisResult = HPFv1.Data.DAL_QuestionDetailHistory.HisAdd(list);

                int delQdResult = HPFv1.Data.DAL_QuestionDetail.GetQIDDelete(QID);

                List<HPFv1.Entity.JsonAnswer> questionResultList = new List<Entity.JsonAnswer>();//最后返回的结果

                List<HPFv1.Entity.QuestionAnswer> allQAList = new List<Entity.QuestionAnswer>();

                foreach (HPFv1.Entity.QuestionDetail item in list) //循环所有问卷答案，生成QuestionAnswer结果集
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
                                        var checkbox = obj.SelAnswer.Where(p => p.SelectItem == caitem).FirstOrDefault();
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
                            HPFv1.Entity.SelAnswer sa = new Entity.SelAnswer();
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
                string questionResultJson = JsonConvert.SerializeObject(questionResultList);//将生成结果转化为Json
                HPFv1.Entity.Question entity = HPFv1.Data.DAL_Question.GetModel(QID);
                entity.QuestionResult = questionResultJson;
                entity.IsResult = 1;
                entity.QNum = QNum;
                int result = HPFv1.Data.DAL_Question.Update(entity);
                if (addHisResult > 0 && result > 0 && delQdResult > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else 
            {
                return 0;
            }

           
           
        }



    }


}
    

