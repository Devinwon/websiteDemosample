using System;
using HPStudent.Entity;
using HPStudent.Student.ViewModel;
using HPStudent.Student.Data;
using System.Collections.Generic;
using HPStudent.Core;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace HPStudent.Student.Business
{
    public class Exercises
    {
        public static List<HPStudent.Student.ViewModel.Exercises.QA_Category> GetCategoryListByMajorID(string MajorID)
        {
            List<HPStudent.Student.ViewModel.Exercises.QA_Category> itemList = new List<HPStudent.Student.ViewModel.Exercises.QA_Category>();
            List<HPStudent.Entity.QA_Category> CategoryList = new List<QA_Category>();
            CategoryList = Data.Exercises.GetCategoryListByMajorID(MajorID);
            foreach (QA_Category item in CategoryList)
            {
                HPStudent.Student.ViewModel.Exercises.QA_Category view = new HPStudent.Student.ViewModel.Exercises.QA_Category();
                view.CID = item.CID;
                view.CategoryName = item.CategoryName;
                itemList.Add(view);
            }
            return itemList;
        }
        public static List<ViewModel.Exercises.QA_Category> GetCategoryListByMajorIDNotNone(string MajorID)
        {
            List<ViewModel.Exercises.QA_Category> itemList = new List<ViewModel.Exercises.QA_Category>();
            List<HPStudent.Entity.QA_Category> CategoryList = new List<QA_Category>();
            CategoryList = Data.Exercises.GetCategoryListByMajorIDNotNone(MajorID);
            foreach (QA_Category item in CategoryList)
            {
                ViewModel.Exercises.QA_Category view = new ViewModel.Exercises.QA_Category();
                view.CID = item.CID;
                view.CategoryName = item.CategoryName;

                itemList.Add(view);
            }
            return itemList;
        }

        /// <summary>
        /// 获得10个区间的题目
        /// </summary>
        /// <param name="ViewModel"></param>
        /// <returns></returns>
        public static List<HPStudent.Student.ViewModel.Exercises.QA_SelectResultModel> GetQA_Select(HPStudent.Student.ViewModel.Exercises.QA_SelectViewModel ViewModel)
        {
            return HPStudent.Student.Data.Exercises.GetQA_Select(ViewModel);
        }
        /// <summary>
        /// 通过分类CID获得题量
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static int GetQA_SelectNum(int cid)
        {
            return HPStudent.Student.Data.Exercises.GetQA_SelectNum(cid);
        }
        /// <summary>
        /// 获得已答题记录
        /// </summary>
        /// <returns></returns>
        public static List<Entity.AlreadyAnswer_Select> GetAlreadyAnswerList(Entity.AlreadyAnswer_Select model)
        {
            return HPStudent.Student.Data.AlreadyAnswer_Select.GetList(model);
        }
        /// <summary>
        /// 保存答题记录
        /// </summary>
        /// <param name="Answer_JsonModel"></param>
        /// <param name="StatisticsModel"></param>
        /// <returns></returns>
        public static ViewModel.Exercises.Answer_Json SaveAnswer(ViewModel.Exercises.Answer_Json Answer_JsonModel, ViewModel.Exercises.StatisticsQuestion_SelectViewModel StatisticsModel)
        {
            ViewModel.Exercises.Answer_Json ajson = new ViewModel.Exercises.Answer_Json();
            if (Answer_JsonModel.YourAnswer != "")
            {
                StringBuilder sqlStr = new StringBuilder();
                //通过题目查询数据库中原有记录
                DataSet ds = Data.Exercises.GetAnswerInfo(Answer_JsonModel, StatisticsModel);
                if (ds.Tables.Count > 0)
                {
                    Core.TBToList<Entity.StatisticsQuestion_Select> tbtolist = new Core.TBToList<Entity.StatisticsQuestion_Select>();
                    List<Entity.StatisticsQuestion_Select> liStatisticsQuestion_Select = (List<Entity.StatisticsQuestion_Select>)tbtolist.ToList(ds.Tables[0]);
                    Core.TBToList<Entity.AlreadyAnswer_Select> tbASelect = new Core.TBToList<Entity.AlreadyAnswer_Select>();
                    List<Entity.AlreadyAnswer_Select> liASelect = (List<Entity.AlreadyAnswer_Select>)tbASelect.ToList(ds.Tables[1]);
                    Core.TBToList<HPStudent.Student.ViewModel.Exercises.QA_SelectResultNotRowidModel> tbQSelect = new Core.TBToList<HPStudent.Student.ViewModel.Exercises.QA_SelectResultNotRowidModel>();
                    List<HPStudent.Student.ViewModel.Exercises.QA_SelectResultNotRowidModel> liQSelect = (List<HPStudent.Student.ViewModel.Exercises.QA_SelectResultNotRowidModel>)tbQSelect.ToList(ds.Tables[2]);
                    if (liQSelect != null)
                    {
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        //对比题目答案
                        string asnwerStr = Answer_JsonModel.YourAnswer.TrimEnd(',');
                        string[] answerArr = asnwerStr.Split(',');
                        if (answerArr.Length != liQSelect[0].Answer.Length)
                        {
                            Answer_JsonModel.IsTrue = false;
                        }
                        else
                        {
                            int trunLength = 0;
                            for (int i = 0; i < answerArr.Length; i++)
                            {
                                if (liQSelect[0].Answer.IndexOf(answerArr[i]) > -1)
                                {
                                    trunLength++;
                                }
                            }
                            if (trunLength == liQSelect[0].Answer.Length)
                            {
                                Answer_JsonModel.IsTrue = true;
                            }
                            else
                            {
                                Answer_JsonModel.IsTrue = false;
                            }
                        }
                        Answer_JsonModel.AnswerAnalysis = liQSelect[0].AnswerAnalysis;
                        Answer_JsonModel.TrueAnswer = liQSelect[0].Answer;
                        string NewJson = "";
                        ajson = Answer_JsonModel;
                        //保存答题表
                        if (liASelect == null)
                        {
                            NewJson = "[" + jss.Serialize(Answer_JsonModel) + "]";
                            sqlStr.Append(string.Format(@"insert into AlreadyAnswer_Select(CID,StudentID,Answer) 
                                                values ({0},{1},'{2}');", StatisticsModel.CID, StatisticsModel.StudentID, NewJson));
                        }
                        else
                        {
                            List<ViewModel.Exercises.Answer_Json> listAnswer_Json = jss.Deserialize<List<ViewModel.Exercises.Answer_Json>>(liASelect[0].Answer);
                            //如果已经作答则不需要重复统计了
                            if (listAnswer_Json.Find(x => x.QID == Answer_JsonModel.QID) != null)
                            {
                                return listAnswer_Json.Find(x => x.QID == Answer_JsonModel.QID);
                            }
                            listAnswer_Json.Add(Answer_JsonModel);
                            NewJson = jss.Serialize(listAnswer_Json);
                            sqlStr.Append(string.Format(@"update AlreadyAnswer_Select set Answer='{0}'
                                                      where  CID={1} and  StudentID={2};", NewJson, StatisticsModel.CID, StatisticsModel.StudentID));
                        }

                        //保存答题统计表
                        if (liStatisticsQuestion_Select == null)
                        {
                            sqlStr.Append(string.Format(@"insert into StatisticsQuestion_Select(CID,StudentID,TestNum)
                                                values ({0},{1},{2});", StatisticsModel.CID, StatisticsModel.StudentID, 1));
                        }
                        else
                        {
                            int answernum = liStatisticsQuestion_Select[0].TestNum;
                            answernum++;
                            sqlStr.Append(string.Format(@"update StatisticsQuestion_Select set TestNum={0}
                                                      where  CID={1} and  StudentID={2};", answernum, StatisticsModel.CID, StatisticsModel.StudentID));
                        }

                        //保存作答信息
                        Data.Exercises.SaveAnswerInfo(sqlStr.ToString());
                    }
                }
            }
            return ajson;
        }
    }
}
