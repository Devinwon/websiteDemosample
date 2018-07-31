using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;


namespace HPStudent.Student.Data
{
    public class Exercises
    {
        /// <summary>
        /// 根据专业ID获取课程列表
        /// </summary>
        /// <param name="MajorID"></param>
        /// <returns></returns>
        public static List<QA_Category> GetCategoryListByMajorID(string MajorID)
        {
            List<QA_Category> CateList;
            string sql = @"select Cate.CID,Cate .CategoryName from MajorToCategory Major
                            inner join QA_Category  Cate on Major .CID =Cate .CID 
                            where MID=@MajorID order by Cate .CID asc";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MajorID", MajorID)
            };
            DataTable dt = Student.Data.SqlHelper.ExecuteDataset(CommandType.Text, sql, paras).Tables[0];
            if (dt.Rows.Count > 0)
            {
                CateList = new List<QA_Category>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    QA_Category cate = new QA_Category();
                    cate.CID = Convert.ToInt32(dt.Rows[i]["CID"].ToString());
                    cate.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    CateList.Add(cate);
                }
                return CateList;
            }
            CateList = new List<QA_Category>();
            return CateList;
        }
        /// <summary>
        /// 根据专业ID获取课程列表
        /// </summary>
        /// <param name="MajorID"></param>
        /// <returns></returns>
        public static List<QA_Category> GetCategoryListByMajorIDNotNone(string MajorID)
        {
            List<QA_Category> CateList;
            string sql = @"select Cate.CID,Cate .CategoryName from MajorToCategory Major
                            inner join QA_Category  Cate on Major .CID =Cate .CID 
                            where MID=@MajorID AND QuestionNums >10  order by Cate .CID asc";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MajorID", MajorID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras).Tables[0];
            if (dt.Rows.Count > 0)
            {
                CateList = new List<QA_Category>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    QA_Category cate = new QA_Category();
                    cate.CID = Convert.ToInt32(dt.Rows[i]["CID"].ToString());
                    cate.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    CateList.Add(cate);
                }
                return CateList;
            }
            CateList = new List<QA_Category>();
            return CateList;
        }

        /// <summary>
        /// 通过分类CID获得题量
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static int GetQA_SelectNum(int cid)
        {
            string strSql = string.Format("select count(QID) from QA_Select where CID={0} ", cid);
            int result = (int)SqlHelper.ExecuteScalar(CommandType.Text, strSql);
            return result;
        }
        /// <summary>
        /// 获得10个区间的题目
        /// </summary>
        /// <param name="ViewModel"></param>
        /// <returns></returns>
        public static List<Student.ViewModel.Exercises.QA_SelectResultModel> GetQA_Select(Student.ViewModel.Exercises.QA_SelectViewModel ViewModel)
        {
            List<Student.ViewModel.Exercises.QA_SelectResultModel> result = new List<Student.ViewModel.Exercises.QA_SelectResultModel>();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select T2.ROWID, T1.QID,CID,Title,A,B,C,D,LEN(Answer) AS AnswerType,AnswerAnalysis
                        FROM QA_Select AS  T1
	                    INNER JOIN 
	                    (
	                      select  ROW_NUMBER() OVER (ORDER BY T3.QID ASC) AS ROWID,T3.QID
	                    FROM QA_Select AS  T3
	                     where T3.CID=@CID 
	                    ) AS T2
	                    ON T1.QID=T2.QID
	                    where   T2.ROWID between @Rowid and @RowidMax  ORDER BY QID ASC ");
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@CID", ViewModel.CID),
                new SqlParameter("@Rowid", ViewModel.RowID),
                new SqlParameter("@RowidMax", ViewModel.RowID+9)
            };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sb.ToString(), paras);
            if (ds.Tables.Count > 0)
            {
                HPStudent.Core.TBToList<Student.ViewModel.Exercises.QA_SelectResultModel> tbtolist = new Core.TBToList<Student.ViewModel.Exercises.QA_SelectResultModel>();
                result = (List<Student.ViewModel.Exercises.QA_SelectResultModel>)tbtolist.ToList(ds.Tables[0]);
            }
            return result;
        }
        /// <summary>
        /// 查询已答的题目
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAnswerInfo(ViewModel.Exercises.Answer_Json Answer_JsonModel, ViewModel.Exercises.StatisticsQuestion_SelectViewModel StatisticsModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(@"select ID,CID,StudentID,TestNum from StatisticsQuestion_Select
                                      where CID={0} and StudentID={1};", StatisticsModel.CID, StatisticsModel.StudentID));
            sb.Append(string.Format(@"select ID,StudentID,CID,Answer from AlreadyAnswer_Select
                                      where CID={0} and StudentID={1};", StatisticsModel.CID, StatisticsModel.StudentID));
            sb.Append(string.Format(@"select QID,CID,Title,A,B,C,D,Answer,AnswerAnalysis from QA_Select
                                      where QID={0};", Answer_JsonModel.QID));
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sb.ToString());
            return ds;
        }
        /// <summary>
        /// 保存已答题目
        /// </summary>
        /// <returns></returns>
        public static int SaveAnswerInfo(string sqlstr)
        {
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sqlstr);
            return iResult;
        }
    }
}
