using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;

namespace HPStudent.Data.Admin
{
    public class Teacher
    {
        public static List<Entity.TeacherInfo> GetTeacherList()
        {
            List<Entity.TeacherInfo> myTeacherList = new List<TeacherInfo>();
            string sql = @"SELECT [TID] ,[TeacherName] ,[Password] ,[Level] ,[LastLoginTime] ,[LastLoginIP]
  FROM [dbo].[TeacherInfo]";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    myTeacherList.Add(
                        new Entity.TeacherInfo()
                        {
                            TID = Convert.ToInt32(reader["TID"].ToString()),
                            TeacherName = reader["TeacherName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Level = Convert.ToInt32(reader["Level"].ToString()),
                            LastLoginTime =Convert.ToDateTime( reader["LastLoginTime"].ToString()),
                            LastLoginIP = reader["LastLoginIP"].ToString()
                        }
                    );
                }
            }
            return myTeacherList;
        }
    }
}
