using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;

namespace HPStudent.Student.Data
{
    public class TestPaper
    {
        public static HPStudent.Entity.TestPaper GetTestPaperByCID(int TopCount, string CID, string StudentID)
        {
//            string sql = string.Format(@"select  top {0} ROW_NUMBER ()over(order by NEWID ()) as RowIndex,  a.*,b.CategoryName  from 
//QA_Select a
//inner join QA_Category b on a.CID =b.CID 
// where a.CID =@CID 
//order by QID", TopCount);

            string sql = string.Format(@"select * from
                                        (select  top {0} a.*,b.CategoryName from 
                                            (select * ,ROW_NUMBER ()over(order by NEWID ()) as RowIndex from QA_Select where CID =@CID ) a
                                        inner join QA_Category b on a.CID =b.CID 
                                        where a.CID =@CID
                                        order by RowIndex) a
                                        order by a.qid
            ", TopCount);
            SqlParameter[] param = { 
                                   new SqlParameter ("@CID",CID),
                                   };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            StringBuilder QIDS = new StringBuilder();
            StringBuilder SelectAnswer = new StringBuilder();
            string Range = "";

            if (ds != null && ds.Tables[0].Rows.Count == TopCount)
            {
                Range = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    QIDS.Append(item["QID"] + ",");
                    SelectAnswer.Append(item["Answer"] + "|");
                }
            }
            else
            {
                return new HPStudent.Entity.TestPaper(); ;
            }
            string strSql = @"declare @TID int

INSERT INTO TestPaper (
            StudentID
           ,SID
           ,CreateDate
           ,SelectIDS
           ,SelectRightItem
           ,SelectAnswer
           ,FillIDS
           ,QuestionIDS
           ,IsComplete
           ,Score
           ,Range
           ,EndDate
) values(
@StudentID,@SID,@CreateDate,@SelectIDS,@SelectRightItem,@SelectAnswer,@FillIDS,@QuestionIDS,@IsComplete,@Score,@Range,@EndDate
)
   select @TID=@@IDENTITY
select * from TestPaper where TID= @TID
";
            string QIDList = QIDS.ToString().Substring(0, QIDS.Length - 1);
            string AnswerList = SelectAnswer.ToString().Substring(0, SelectAnswer.Length - 1);
            SqlParameter[] StrParam = { 
                                   new SqlParameter ("@StudentID",StudentID),
                                   new SqlParameter ("@SID",StudentID),
                                   new SqlParameter ("@CreateDate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                                   new SqlParameter ("@SelectIDS",QIDList),
                                   new SqlParameter ("@SelectRightItem",AnswerList),
                                   new SqlParameter ("@SelectAnswer",""),
                                   new SqlParameter ("@FillIDS",""),
                                   new SqlParameter ("@QuestionIDS",""),
                                   new SqlParameter ("@IsComplete","0"),//0未完成1完成
                                   new SqlParameter ("@Score","0"),
                                   new SqlParameter ("@Range",Range),
                                   new SqlParameter ("@EndDate",""),
                                   };
            try
            {
                DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, strSql, StrParam).Tables[0];
                HPStudent.Entity.TestPaper TestPaper = new HPStudent.Entity.TestPaper();
                TestPaper.TID = Convert.ToInt32(dt.Rows[0]["TID"].ToString());
                TestPaper.StudentID = Convert.ToInt32(dt.Rows[0]["StudentID"].ToString());
                return TestPaper;
            }
            catch
            {
                return new HPStudent.Entity.TestPaper();
            }
            //int i = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, StrParam);

            //return i;
        }

        public static List<HPStudent.Entity.TestPaper> GetTestPaperListBySID(string StudentID, int start, int length, out int TotalRows)
        {
            string sqlCount = "select count(1) from TestPaper  where StudentID =@StudentID";

            string sql = string.Format(@"select top {0} * from TestPaper  
                            where StudentID =@StudentID and TID not in(
                            select top {1} TID from TestPaper  where StudentID =@StudentID
                            order by IsComplete asc,CreateDate desc
                            )
                            order by IsComplete asc,CreateDate desc", length, start);
            SqlParameter[] param = { 
                            new SqlParameter ("@StudentID",StudentID),                                   
                                   };
            TotalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlCount, param));
            if (TotalRows == 0)
            {
                return new List<Entity.TestPaper>();
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            List<HPStudent.Entity.TestPaper> TestPaperList = new List<Entity.TestPaper>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.TestPaper test = new Entity.TestPaper();
                    test.TID = Convert.ToInt32(item["TID"].ToString());
                    test.CreateDate = DateTime.Parse(item["CreateDate"].ToString());
                    test.EndDate = DateTime.Parse(item["EndDate"].ToString());
                    test.Range = item["Range"].ToString();
                    test.Score = float.Parse(item["Score"].ToString());
                    test.IsComplete = Convert.ToInt32(item["IsComplete"].ToString()) == 1 ? 1 : 0;
                    test.StudentID = Convert.ToInt32(item["StudentID"].ToString());
                    test.SID = Convert.ToInt32(item["SID"].ToString());
                    TestPaperList.Add(test);
                }
                return TestPaperList;
            }
            else
                return new List<Entity.TestPaper>();
        }

        public static List<QA_Select> GetTestPaperQA_SelectByTID(string TID)
        {
            string sql = @"declare @QIDS varchar(1000)
                        select @QIDS = SelectIDS from TestPaper where TID =@TID
                        select * from QA_Select a
                            inner join   dbo.f_splitSTR((select SelectIDS from TestPaper where TID =@TID),',') b 
                            on a.QID =b.col  
                        order by charindex(','+rtrim(cast(QID as varchar(10)))+',',','+@QIDS+',')";
            SqlParameter[] param = { 
                                   new SqlParameter ("@TID",TID ),

                                   };
            List<QA_Select> SelectList = new List<QA_Select>();
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Entity.QA_Select select = new QA_Select();
                    select.A = StringHelper.EnterToBR(item["A"].ToString());
                    select.Answer = item["Answer"].ToString();
                    select.B = StringHelper.EnterToBR(item["B"].ToString());
                    select.C = StringHelper.EnterToBR(item["C"].ToString());
                    select.CID = Convert.ToInt32(item["CID"].ToString());
                    select.CreateDate = DateTime.Parse(item["CreateDate"].ToString());
                    select.Creater = Convert.ToInt32(item["Creater"].ToString());
                    select.D = StringHelper.EnterToBR(item["D"].ToString());
                    select.Level = Convert.ToInt32(item["Level"].ToString());
                    select.QID = Convert.ToInt32(item["QID"].ToString());
                    select.Title = StringHelper.EnterToBR(item["Title"].ToString());
                    SelectList.Add(select);
                }
            }
            else
            {
                SelectList = new List<QA_Select>();
            }
            return SelectList;
        }
        public static Entity.TestPaper GetTestPaperByTID(string TID)
        {
            string sql = @"select  *  from TestPaper where TID =@TID ";
            SqlParameter[] param = { 
                                   new SqlParameter ("@TID",TID),
                                   };

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            Entity.TestPaper testPaper;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow item = ds.Tables[0].Rows[0];
                testPaper = new Entity.TestPaper();
                testPaper.TID = Convert.ToInt32(item["TID"].ToString());
                testPaper.CreateDate = DateTime.Parse(item["CreateDate"].ToString());
                testPaper.EndDate = DateTime.Parse(item["EndDate"].ToString());
                testPaper.Range = item["Range"].ToString();
                testPaper.Score = float.Parse(item["Score"].ToString());
                testPaper.IsComplete = Convert.ToInt32(item["IsComplete"].ToString()) == 1 ? 1 : 0;
                testPaper.StudentID = Convert.ToInt32(item["StudentID"].ToString());
                testPaper.SID = Convert.ToInt32(item["SID"].ToString());
                testPaper.SelectAnswer = item["SelectAnswer"].ToString();
                testPaper.SelectIDS = item["SelectIDS"].ToString();
                testPaper.SelectRightItem = item["SelectRightItem"].ToString();
                testPaper.QuestionIDS = item["QuestionIDS"].ToString();
                testPaper.FillIDS = item["FillIDS"].ToString();

                return testPaper;
            }
            return new Entity.TestPaper();
        }
        public static int SubmitDoTest(string TID, float Score, string Answer)
        {
            string sql = @"update TestPaper set SelectAnswer =@SelectAnswer,IsComplete =1,Score =@Score ,EndDate =GETDATE ()  where TID =@TID";
            SqlParameter[] param ={
                                       new SqlParameter ("@SelectAnswer",Answer ),
                                       new SqlParameter ("@Score",Score),
                                       new SqlParameter ("@TID",TID )
                                  };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            return i;
        }

    }
}
