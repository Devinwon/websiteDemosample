using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;

namespace HPStudent.Data.Admin
{
    public class Resource
    {
        // <summary>
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

        public static List<Entity.ResourceBook> GetResourceBookList(int mid, int start, int length, out int TotalRows)
        {
            //获得符合要求的记录总数
            string countSql = "select count(rid) from ResourceBook where MID = @MID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MID", mid)
            };
            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql, paras);

            List<Entity.ResourceBook> ResourceBookList = new List<Entity.ResourceBook>();

            string sql = string.Format(@"SELECT TOP {0} RID,MID,ResourceName,TeacherName,CourceHour,ResourceFrom,CreateDate,ResourcePic, ResourceDesc
FROM ResourceBook WHERE RID NOT IN (SELECT TOP {1} RID FROM ResourceBook WHERE MID = @MID ORDER BY RID DESC)
AND MID = @MID ORDER BY RID DESC", length, start);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                while (reader.Read())
                {
                    ResourceBookList.Add(
                        new Entity.ResourceBook()
                        {
                            MID = Convert.ToInt32(reader["MID"].ToString()),
                            RID = Convert.ToInt32(reader["RID"].ToString()),
                            ResourceName = reader["ResourceName"].ToString(),
                            TeacherName = reader["TeacherName"].ToString(),
                            CourceHour = Convert.ToInt32(reader["CourceHour"].ToString()),
                            ResourceFrom = reader["ResourceFrom"].ToString(),
                            ResourcePic = reader["ResourcePic"].ToString(),
                            ResourceDesc = reader["ResourceDesc"].ToString(),
                            CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString())
                        }
                    );
                }
            }
            return ResourceBookList;
        }

        public static int AddResource(Entity.ResourceBook resource)
        {
            string sql = @"INSERT INTO [ResourceBook] ( [MID] ,[ResourceName] ,[TeacherName] ,[CourceHour]  ,[ResourceFrom]  ,[CreateDate]
                   ,[EditDate]  ,[ResourcePic]  ,[ResourceDesc])
                   VALUES (@MID , @ResourceName , @TeacherName , @CourceHour , @ResourceFrom , GETDATE() , GETDATE() , @ResourcePic , @ResourceDesc )";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MID", resource.MID),
                new SqlParameter("@ResourceName", resource.ResourceName),
                new SqlParameter("@TeacherName", resource.TeacherName),
                new SqlParameter("@CourceHour", resource.CourceHour),
                new SqlParameter("@ResourceFrom", resource.ResourceFrom),
                new SqlParameter("@ResourcePic", resource.ResourcePic),
                new SqlParameter("@ResourceDesc", resource.ResourceDesc),
            };

            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;
        }

        public static Entity.ResourceBook GetResourceBookByRID(int RID)
        {
            Entity.ResourceBook myResourceBook = new Entity.ResourceBook();
            string sql = @"SELECT TOP 1 [RID] , [MID] , [ResourceName] , [TeacherName] , [CourceHour]  , [ResourceFrom]  ,[CreateDate]
                   ,[EditDate]  ,[ResourcePic]  ,[ResourceDesc] FROM ResourceBook WHERE RID =@RID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@RID", RID),
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                while (reader.Read())
                {
                    myResourceBook.CourceHour = Convert.ToInt32(reader["CourceHour"].ToString());
                    myResourceBook.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                    myResourceBook.EditDate = Convert.ToDateTime(reader["EditDate"].ToString());
                    myResourceBook.MID = Convert.ToInt32(reader["MID"].ToString());
                    myResourceBook.ResourceDesc = reader["ResourceDesc"].ToString();
                    myResourceBook.ResourceFrom = reader["ResourceFrom"].ToString();
                    myResourceBook.ResourceName = reader["ResourceName"].ToString();
                    myResourceBook.ResourcePic = reader["ResourcePic"].ToString();
                    myResourceBook.TeacherName = reader["TeacherName"].ToString();
                    myResourceBook.RID = Convert.ToInt32(reader["RID"].ToString());

                }
            }
            return myResourceBook;
        }
        public static List<Entity.ResourceItem> GetResourceItemListByRID (int RID)
        {
            List<Entity.ResourceItem> ResourceItemList = new List<Entity.ResourceItem>();
            string sql = @"SELECT ID , RID , CourseName , URL , CreateDate , EditDate FROM ResourceItem WHERE RID =@RID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@RID", RID),
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                while (reader.Read())
                {
                    ResourceItemList.Add(
                        new Entity.ResourceItem()
                        {
                            ID = Convert.ToInt32(reader["ID"].ToString()),
                            RID = Convert.ToInt32(reader["RID"].ToString()),
                            CourseName = reader["CourseName"].ToString(),
                            URL = reader["URL"].ToString(),
                            CreateDate =  Convert.ToDateTime(reader["CreateDate"].ToString()),
                            EditDate =  Convert.ToDateTime(reader["EditDate"].ToString())                            
                        }
                    );
                }
            }
            return ResourceItemList;
        }
    }
}
