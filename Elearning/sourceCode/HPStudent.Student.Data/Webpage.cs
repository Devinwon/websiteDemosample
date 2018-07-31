using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;

namespace HPStudent.Student.Data
{
    public class Webpage
    {
        /// <summary>
        /// 记录短训学生信息到数据库
        /// </summary>
        /// <param name="manager">学生联系信息</param>
        /// <returns>
        /// 0：添加成功
        /// 1：添加失败
        /// </returns>
        public static int Register(string realname, string mobile, string email)
        {
            string sql = "INSERT INTO api_StudentInfo (RealName, Mobile, Email, PostTime, IsExport) VALUES (@realname, @mobile, @email, getdate(),0)";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@realname", realname),
                new SqlParameter("@mobile", mobile),
                new SqlParameter("@email", email),
            };

            return (int)SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
        }
    }
}
