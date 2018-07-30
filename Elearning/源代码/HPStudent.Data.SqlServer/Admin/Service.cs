using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;
using PVM = HPStudent.ViewModel;

namespace HPStudent.Data.Admin
{
    public class Service
    {
        public static List<PVM.Service.Suggest> GetSuggestList(int start, int length, out int TotalRows)
        {
            //获得符合要求的记录总数
            string countSql = "select count(sid) from Suggestions";

            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql);

            //            List<HPStudent.ViewModel.Projects.ProjectBook> ProjectBookList = new List<ViewModel.Projects.ProjectBook>();
            //            string sql = string.Format(@"SELECT TOP {0} P.PID , P.ProjectName , T.TeacherName , ClassHour ,CreateDate , EditDate  FROM ProjectBook P
            //                                        LEFT JOIN TeacherInfo T ON P.TeacherID = T.TID 
            //                                        WHERE P.PID NOT IN (SELECT TOP {1} PID FROM ProjectBook WHERE MID = @MID AND Status =0 ORDER BY PID DESC)
            //                                        AND MID = @MID AND Status =0 ORDER BY PID DESC", length, start);



            List<PVM.Service.Suggest> mySuggestlist = new List<PVM.Service.Suggest>();
            string sql = string.Format(@"SELECT TOP {0} [SID],school.SchoolName ,student.RealName StudentName,class.CName ClassName,s.[SchoolID] ,s.[StudentID] 
                        ,[Status] ,[Category] ,[IsSuggest] ,[PostDate] ,[Title] ,[Content] ,[SuggestImg] ,[sResult] ,[TeacherID] ,[LastReply]
                        FROM Suggestions s
                        LEFT JOIN Common_School school on s.SchoolID = school.SchoolID
                        LEFT JOIN StudentInfo student on s.StudentID = student.StudentID
                        LEFT JOIN StudentClass class on student.CID = class.CID
                        WHERE S.SID NOT IN (SELECT TOP {1} SID FROM Suggestions ORDER BY SID DESC)
                        ORDER BY SID DESC", length, start); ;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    mySuggestlist.Add(
                        new PVM.Service.Suggest()
                        {
                            SID = Convert.ToInt32(reader["SID"].ToString()),
                            SchoolID = Convert.ToInt32(reader["SchoolID"].ToString()),
                            StudentID = Convert.ToInt32(reader["StudentID"].ToString()),
                            Status = Convert.ToInt32(reader["Status"].ToString()),
                            Category = Convert.ToInt32(reader["Category"].ToString()),
                            IsSuggest = Convert.ToInt32(reader["IsSuggest"].ToString()),
                            PostDate = Core.DateHelper.ToDate(reader["PostDate"].ToString()),
                            Title = reader["Title"].ToString(),
                            Content = reader["Content"].ToString(),
                            SuggestImg = reader["SuggestImg"].ToString(),
                            sResult = reader["sResult"].ToString(),
                            TeacherID = Convert.ToInt32(reader["TeacherID"].ToString()),
                            SchoolName = reader["SchoolName"].ToString(),
                            StudentName = reader["StudentName"].ToString(),
                            ClassName = reader["ClassName"].ToString(),
                            LastReply = Convert.ToInt32(reader["LastReply"].ToString()),
                            //PID = Convert.ToInt32(reader["PID"].ToString()),
                            //ProjectName = reader["ProjectName"].ToString(),
                            //TeacherName = reader["TeacherName"].ToString(),
                            //ClassHour = Convert.ToInt32(reader["ClassHour"].ToString()),
                            //CreateDate = Core.DateHelper.ToDate(reader["CreateDate"].ToString()),
                            //EditDate = Core.DateHelper.ToDate(reader["EditDate"].ToString()),
                        }
                    );
                }
            }
            return mySuggestlist;
        }

        public static int ReplySuggest(HPStudent.Entity.SuggestItem SuggestItem)
        {
            string sql = @"IF EXISTS (SELECT SID FROM Suggestions WHERE Status = 0 AND SID = @SID)
                    BEGIN
	                    UPDATE Suggestions SET Status = 1 , LastReply=1 WHERE SID = @SID
                    END
                    ELSE                    
                    BEGIN
	                    UPDATE Suggestions SET LastReply=1 WHERE SID = @SID
                    END
                    INSERT INTO [SuggestItem] ( [SID] ,[Content]  ,[CreateDate]  ,[StudentID]  ,[IsStudent] ,[TeacherID]) VALUES
                    (@SID, @Content , GETDATE() , 0 , @IsStudent , @TeacherID)";

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@SID", SuggestItem.SID),
                new SqlParameter("@Content", SuggestItem.Content),
                new SqlParameter("@IsStudent", SuggestItem.IsStudent),
                new SqlParameter("@TeacherID", SuggestItem.TeacherID),
            };

            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
        }

        public static HPStudent.Entity.Suggestions GetSuggestBySID(int SID)
        {
            HPStudent.Entity.Suggestions mySuggest = new Entity.Suggestions();

            string sql = @"SELECT TOP 1 [SID] ,[SchoolID]      ,[StudentID]      ,[Status]      ,[Category]      ,[IsSuggest]      
                    ,[PostDate]      ,[Title]      ,[Content]      ,[SuggestImg]      ,[sResult]      ,[TeacherID] FROM Suggestions WHERE SID=@SID";

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
        //事件完结方法
        public static int SuggestEndEvent(int PID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Suggestions set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where SID=@SID");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.TinyInt,1),
					new SqlParameter("@SID", SqlDbType.BigInt,8)};
            parameters[0].Value = 2;
            parameters[1].Value = PID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(),parameters) > 0 ? 0 : 1;
        }
    }
}
