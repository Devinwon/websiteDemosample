using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;
using HSVC = HPStudent.Student.ViewModel.Common;
using HSV = HPStudent.Student.ViewModel;

namespace HPStudent.Student.Data
{
    public class Suggestions
    {
        /// <summary>
        /// 得到所有专业列表
        /// </summary>
        /// <returns></returns>
        public static List<HPStudent.Entity.Major> GetMajorList()
        {
            List<HPStudent.Entity.Major> MajorList = new List<Major>();
            string sql = "SELECT * FROM Major";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    MajorList.Add(
                        new Major()
                        {
                            MID = Convert.ToInt32(reader["MID"].ToString()),
                            MajorName = reader["MajorName"].ToString()
                        }
                    );
                }
            }
            return MajorList;

        }

        /// <summary>
        /// 根据MajorID获得该专业下的项目（分页）
        /// </summary>
        /// <returns></returns>
        public static List<HPStudent.Entity.Suggestions> GetSuggestList(string StudentID, int start, int length, out int TotalRows)
        {
            //获得符合要求的记录总数
            string countSql = "select count(SID) from Suggestions where StudentID = @StudentID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@StudentID", StudentID)
            };
            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql, paras);


            //获取记录明细
            List<HPStudent.Entity.Suggestions> SuggestionList = new List<HPStudent.Entity.Suggestions>();
            string sql = string.Format(@"select top {0} * from Suggestions where SID not in 
                                    (select top {1} SID from Suggestions where StudentID=@StudentID order by SID desc)
                                    and StudentID =@StudentID order by SID desc", length, start);
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime _Date;
                    HPStudent.Entity.Suggestions Suggest = new Entity.Suggestions();
                    Suggest.Content = dt.Rows[i]["Content"].ToString();
                    Suggest.IsSuggest = Convert.ToInt32(dt.Rows[i]["IsSuggest"].ToString());
                    DateTime.TryParse(dt.Rows[i]["PostDate"].ToString(), out _Date);
                    Suggest.PostDate = _Date;
                    Suggest.SID = Convert.ToInt32(dt.Rows[i]["SID"].ToString());
                    Suggest.Title = dt.Rows[i]["Title"].ToString();
                    Suggest.Status = Convert.ToInt32(dt.Rows[i]["Status"].ToString());
                    Suggest.LastReply = Convert.ToInt32(dt.Rows[i]["LastReply"].ToString());


                    SuggestionList.Add(Suggest);
                }
            }
            return SuggestionList;

        }

        public static int AddSuggest(HPStudent.Entity.Suggestions SuggestItem)
        {
            string sql = @"INSERT INTO Suggestions  ([SchoolID] , [StudentID] , [Status] , [Category] , [IsSuggest] , [PostDate] , 
            [Title] , [Content] , [SuggestImg] , [sResult] , [TeacherID] , [LastReply] ) VALUES (
            (SELECT TOP 1 SCHOOLID FROM StudentInfo WHERE StudentID=@StudentID),
            @StudentID , 0 , 0 , @IsSuggest , getdate() , @Title , @Content , '' , '' , 0 , 0
            )";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@StudentID", SuggestItem.StudentID),
                new SqlParameter("@IsSuggest", SuggestItem.IsSuggest),
                new SqlParameter("@Title", SuggestItem.Title),
                new SqlParameter("@Content", SuggestItem.Content),
            };

            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
        }

        public static int ReplySuggest(HPStudent.Entity.SuggestItem SuggestItem)
        {
            string sql = @"UPDATE Suggestions SET LastReply = 0 WHERE SID = @SID;
                    INSERT INTO [SuggestItem] ( [SID] ,[Content]  ,[CreateDate]  ,[StudentID]  ,[IsStudent] ,[TeacherID]) VALUES
                    (@SID, @Content , GETDATE() , @StudentID , @IsStudent , @TeacherID)";

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@SID", SuggestItem.SID),
                new SqlParameter("@Content", SuggestItem.Content),
                new SqlParameter("@StudentID", SuggestItem.StudentID),
                new SqlParameter("@IsStudent", SuggestItem.IsStudent),
                new SqlParameter("@TeacherID", SuggestItem.TeacherID),
            };

            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
        }

        public static HPStudent.Entity.Suggestions GetSuggestBySID(int SID)
        {
            HPStudent.Entity.Suggestions mySuggest = new Entity.Suggestions();

            string sql = @"SELECT TOP 1 [SID] ,[SchoolID]      ,[StudentID]      ,[Status]      ,[Category]      ,[IsSuggest]      
                    ,[PostDate]      ,[Title]      ,[Content]      ,[SuggestImg]      ,[sResult]      ,[TeacherID],[ScoreStar],[ScoreDetail] FROM Suggestions WHERE SID=@SID";

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@SID", SID),
            };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                while (reader.Read())
                {
                    mySuggest.Category = Convert.ToInt32(reader["Category"].ToString());
                    mySuggest.Content = reader["Content"].ToString();
                    mySuggest.IsSuggest = Convert.ToInt32(reader["IsSuggest"].ToString());
                    mySuggest.PostDate = Core.DateHelper.ToDate(reader["PostDate"].ToString());
                    mySuggest.SchoolID = Convert.ToInt32(reader["SchoolID"].ToString());
                    mySuggest.SID = SID;
                    mySuggest.sResult = reader["sResult"].ToString();
                    mySuggest.Status = Convert.ToInt32(reader["Status"].ToString());
                    mySuggest.StudentID = Convert.ToInt32(reader["StudentID"].ToString());
                    mySuggest.SuggestImg = reader["SuggestImg"].ToString();
                    mySuggest.TeacherID = Convert.ToInt32(reader["TeacherID"].ToString());
                    mySuggest.Title = reader["Title"].ToString();
                    mySuggest.ScoreStar = Convert.ToInt32(string.IsNullOrEmpty(reader["ScoreStar"].ToString()) == true ? "0" : reader["ScoreStar"].ToString());
                    mySuggest.ScoreDetail = reader["ScoreDetail"].ToString();
                }
            }
            return mySuggest;
        }

        public static List<HPStudent.Entity.SuggestItem> GetSuggestItemListBySID(int SID)
        {
            List<HPStudent.Entity.SuggestItem> mySuggestItemList = new List<SuggestItem>();
            string sql = "SELECT [ID] ,[SID] ,[Content] ,[CreateDate] ,[StudentID]  ,[IsStudent]  ,[TeacherID] FROM SuggestItem WHERE SID = @SID";

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@SID", SID),
            };

            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                while (reader.Read())
                {
                    mySuggestItemList.Add(
                        new SuggestItem()
                        {
                            ID = Convert.ToInt32(reader["ID"].ToString()),
                            SID = Convert.ToInt32(reader["SID"].ToString()),
                            Content = reader["Content"].ToString(),
                            CreateDate = Core.DateHelper.ToDate(reader["CreateDate"].ToString()),
                            StudentID = Convert.ToInt32(reader["StudentID"].ToString()),
                            IsStudent = Convert.ToInt32(reader["IsStudent"].ToString()),
                            TeacherID = Convert.ToInt32(reader["TeacherID"].ToString()),

                        }
                    );
                }
            }
            return mySuggestItemList;
        }
        //评分保存
        public static int SuggestScoreSave(HPStudent.Entity.Suggestions suggestions)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Suggestions set ");
            strSql.Append("ScoreDetail=@ScoreDetail,");
            strSql.Append("ScoreStar=@ScoreStar");
            strSql.Append(" where SID=@SID");
            SqlParameter[] parameters = {
					new SqlParameter("@ScoreDetail", SqlDbType.VarChar,400),
					new SqlParameter("@ScoreStar", SqlDbType.Int,4),
					new SqlParameter("@SID", SqlDbType.BigInt,8)};
            parameters[0].Value = suggestions.ScoreDetail;
            parameters[1].Value = suggestions.ScoreStar;
            parameters[2].Value = suggestions.SID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
    }
}
