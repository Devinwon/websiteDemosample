using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HPStudent.Entity;
using HPStudent.ViewModel;

namespace HPStudent.Data.Common
{
    public class SchoolCommon
    {
        /// <summary>
        /// 获取所有校区
        /// </summary>
        /// <returns></returns>
        public static List<Common_School> GetAllSchool()
        {
            string sql = "select * from Common_School  ORDER BY DisplayOrder";
            List<Common_School> SchoolList = new List<Common_School>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    SchoolList.Add(
                       new Common_School()
                        {
                            SchoolID = Convert.ToInt32(reader["SchoolID"].ToString()),
                            AreaID = Convert.ToInt32(reader["AreaID"].ToString()),
                            SchoolName = reader["SchoolName"].ToString(),
                            DisplayOrder = Convert.ToInt32(reader["DisplayOrder"].ToString()),
                        }
                    );
                }
            }
            return SchoolList;
        }

        /// <summary>
        /// 根据区域获取校区
        /// </summary>
        /// <param name="AreaID"></param>
        /// <returns></returns>
        public static List<HPStudent.ViewModel.Student.StudentSchool> GetSchoolByAreaID(string ParentAID, string AreaID, int start, int length, out int TotalRows)
        {

            string sqlCount = @"select  count(1)  from Common_Area a
                            inner join Common_Area b on a.AreaID =b.ParentAID 
                            inner join Common_School C on c.AreaID =b.AreaID 
                            where b.ParentAID =@ParentAID and (b.AreaID =@AreaID or @AreaID='')";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@ParentAID",ParentAID),
                new SqlParameter("@AreaID",AreaID),
            };
            TotalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlCount, paras));

            string sql = string.Format(@"select top {0}  a.AreaName ,b.AreaName as CityName,C.SchoolName,C.SchoolID,C.DisplayOrder ,C.AreaID  from Common_Area a
                            inner join Common_Area b on a.AreaID =b.ParentAID 
                            inner join Common_School C on c.AreaID =b.AreaID 
                            where b.ParentAID =@ParentAID and (b.AreaID =@AreaID or @AreaID='') and C.SchoolID not in
                            (
                            select top {1}   C.SchoolID from Common_Area a
                            inner join Common_Area b on a.AreaID =b.ParentAID 
                            inner join Common_School C on c.AreaID =b.AreaID 
                            where b.ParentAID =@ParentAID and (b.AreaID =@AreaID or @AreaID='')
                            order by C.DisplayOrder asc
                            )
                            order by C.DisplayOrder asc",length,start);

//            string sql = @"select  a.AreaName ,b.AreaName as CityName,C.SchoolName,C.SchoolID,C.DisplayOrder ,C.AreaID  from Common_Area a
//                            inner join Common_Area b on a.AreaID =b.ParentAID 
//                            inner join Common_School C on c.AreaID =b.AreaID 
//                            where b.ParentAID =@ParentAID and (b.AreaID =@AreaID or @AreaID='')";

            List<HPStudent.ViewModel.Student.StudentSchool> ViewSchoolList = new List<HPStudent.ViewModel.Student.StudentSchool>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                while (reader.Read())
                {

                    ViewSchoolList.Add(
                       new HPStudent.ViewModel.Student.StudentSchool()
                       {
                           SchoolID = reader["SchoolID"].ToString(),
                           AreaID = reader["AreaID"].ToString(),
                           SchoolName = reader["SchoolName"].ToString(),
                           DisplayOrder = reader["DisplayOrder"].ToString(),
                           CityName = reader["CityName"].ToString(),
                           AreaName = reader["AreaName"].ToString(),
                       }
                    );
                }
            }
            return ViewSchoolList;
        }

        public static Common_School GetSchoolBySchoolID(int SchoolID)
        {
            string sql = "select * from Common_School where SchoolID=@SchoolID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@SchoolID",DbType.Int32),
            };
            paras[0].Value = SchoolID;

            Common_School EntitySchool = new Common_School();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                if (reader.Read())
                {
                    EntitySchool.SchoolID = Convert.ToInt32(reader["SchoolID"].ToString());
                    EntitySchool.AreaID = Convert.ToInt32(reader["AreaID"].ToString());
                    EntitySchool.SchoolName = reader["SchoolName"].ToString();
                    EntitySchool.DisplayOrder = Convert.ToInt32(reader["DisplayOrder"].ToString());
                }
            }
            return EntitySchool;
        }

        public static int AddtCommon_School(Entity.Common_School comSchool)
        {
            string sql = @"insert into Common_School(AreaID,SchoolName,DisplayOrder) select @AreaID,@SchoolName,max(DisplayOrder)+1 from Common_School";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@AreaID", comSchool.AreaID ),
                new SqlParameter("@SchoolName", comSchool.SchoolName ),                
            };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return i;
        }

        public static int EditCommon_School(Entity.Common_School comSchool)
        {
            string sql = "update Common_School set SchoolName=@SchoolName where SchoolID=@SchoolID ";

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@SchoolName", comSchool.SchoolName ),              
                new SqlParameter("@SchoolID",comSchool.SchoolID ),
            };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return i;
        }

        public static int DeleteCommon_School(string SchoolID)
        {
            string sql = "delete from Common_School where SchoolID=@SchoolID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@SchoolID", SchoolID)
            };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return i;
        }

        public static List<Entity.Common_Area> GetComAreaByParentAID(string ParentAID)
        {
            string sql = @"select * from Common_Area where ParentAID =@ParentAID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@ParentAID", ParentAID),                       
            };

            List<Entity.Common_Area> EntityComAreaList = new List<Common_Area>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                while (reader.Read())
                {
                    EntityComAreaList.Add(
                       new HPStudent.Entity.Common_Area()
                       {
                           ParentAID = Convert.ToInt32(reader["ParentAID"].ToString()),
                           AreaID = Convert.ToInt32(reader["AreaID"].ToString()),
                           AreaName = reader["AreaName"].ToString(),
                           DisplayOrder = Convert.ToInt32(reader["DisplayOrder"].ToString()),
                           Level = Convert.ToInt32(reader["Level"].ToString()),
                       }
                    );
                }
            }
            return EntityComAreaList;
        }

    }
}
