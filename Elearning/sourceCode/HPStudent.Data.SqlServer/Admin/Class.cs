using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HPStudent.Entity;

namespace HPStudent.Data.Common
{
    public class StudentClass
    {
        /// <summary>
        /// 根据校区和年份，搜索班级
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static List<HPStudent.Entity.StudentClass> GetStudentClassBySchoolID(int SchoolID, int year)
        {
            string sql = "select * from StudentClass where SchoolID=@SchoolID and [year]=@year ";
            SqlParameter[] param ={
                                    new SqlParameter("@SchoolID",SchoolID),
                                    new SqlParameter("@year",year),
                                  };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            List<Entity.StudentClass> StuClassList = new List<Entity.StudentClass>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    HPStudent.Entity.StudentClass stuClass = new Entity.StudentClass();
                    stuClass.SchoolID = Convert.ToInt32(ds.Tables[0].Rows[i]["SchoolID"].ToString());
                    stuClass.CID = Convert.ToInt32(ds.Tables[0].Rows[i]["CID"].ToString());
                    stuClass.CName = ds.Tables[0].Rows[i]["CName"].ToString();
                    stuClass.CCode = ds.Tables[0].Rows[i]["CCode"].ToString();
                    stuClass.Year = Convert.ToInt32(ds.Tables[0].Rows[i]["Year"].ToString());
                    StuClassList.Add(stuClass);
                }
            }
            return StuClassList;
        }


        public static DataSet GetStudentClassListBySchoolID(int SchoolID, int year, int start, int length, out int TotalRows)
        {

            string countSql = @"select count(1)  from  Common_School a
                            inner join StudentClass b on a.SchoolID =b.SchoolID 
                            where a.SchoolID=@SchoolID and b.[year]=@year";
            SqlParameter[] param ={
                                    new SqlParameter("@SchoolID",SchoolID),
                                    new SqlParameter("@year",year),
                                  };
            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql, param);

            string sql = string.Format(@"select top {0} * from Common_School a
                            inner join StudentClass b on a.SchoolID =b.SchoolID 
where a.SchoolID=@SchoolID and b.[year]=@year and
  b.CID not in(
 select top {1} B.CID from  Common_School a
                            inner join StudentClass b on a.SchoolID =b.SchoolID 
                            where a.SchoolID=@SchoolID and b.[year]=@year
                            order by b.CID desc
                            ) order by b.CID desc ", length, start);


            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);

            return ds;
        }
        public static int AddStudentClass(Entity.StudentClass stuClass)
        {
            string sql = @"insert into StudentClass (CCode,CName ,SchoolID ,[Year] )
                            values(@CCode,@CName ,@SchoolID ,@Year)";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@CCode", stuClass.CCode ),
                new SqlParameter("@CName", stuClass.CName ),
                new SqlParameter("@SchoolID", stuClass.SchoolID ),
                new SqlParameter("@Year", stuClass.Year ),                
            };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return i;
        }

        public static int EditStudentClass(Entity.StudentClass stuClass)
        {
            string sql = "update StudentClass set CCode=@CCode,CName =@CName where CID=@CID ";

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@CCode", stuClass.CCode ),
                new SqlParameter("@CName", stuClass.CName ),
                new SqlParameter("@CID",stuClass.CID ),
            };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return i;
        }

        public static int DeleteStudentClass(string CID)
        {
            string sql = "delete from StudentClass where CID=@CID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@CID", CID)
            };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return i;
        }

        public static Entity.StudentClass GetStudentClassByCID(string CID)
        {
            string sql = "select * from StudentClass where CID=@CID  ";
            SqlParameter[] param ={
                                    new SqlParameter("@CID",CID),                                   
                                  };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            HPStudent.Entity.StudentClass stuClass = new Entity.StudentClass();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                stuClass.SchoolID = Convert.ToInt32(ds.Tables[0].Rows[0]["SchoolID"].ToString());
                stuClass.CID = Convert.ToInt32(ds.Tables[0].Rows[0]["CID"].ToString());
                stuClass.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                stuClass.CCode = ds.Tables[0].Rows[0]["CCode"].ToString();
                stuClass.Year = Convert.ToInt32(ds.Tables[0].Rows[0]["Year"].ToString());
            }
            return stuClass;
        }

    }
}
