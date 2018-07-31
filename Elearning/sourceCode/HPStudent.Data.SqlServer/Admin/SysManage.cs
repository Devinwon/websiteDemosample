using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using HPStudent.Entity;
using HPStudent.ViewModel;
using HPStudent.Data;

namespace HPStudent.Data.Admin
{
    public class SysManage
    {
        /// <summary>
        /// 查询所有的管理员信息
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static List<ManagerInfo> GetManagerList(int start, int length, out int TotalRows)
        {
            //获取符合要求记录的总行数
            string countSql = "select count(MID) from ManagerInfo";
            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql);

            //获取记录明细
            List<ManagerInfo> ManagerList = new List<ManagerInfo>();
            string sql = string.Format(@"SELECT TOP {0} * FROM ManagerInfo WHERE MID NOT IN 
                    (SELECT TOP {1} MID FROM ManagerInfo ORDER BY MID DESC) ORDER BY MID DESC",length,start);
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ManagerInfo manager = new ManagerInfo();
                    manager.MID = Convert.ToInt32(dt.Rows[i]["MID"].ToString());
                    manager.Password = dt.Rows[i]["Password"].ToString();
                    manager.ManagerName = dt.Rows[i]["ManagerName"].ToString();
                    manager.LastLoginIP = dt.Rows[i]["LastLoginIP"].ToString() == null ? "" : dt.Rows[i]["LastLoginIP"].ToString();
                    manager.LastLoginTime = dt.Rows[i]["LastLoginTime"] == null ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[i]["LastLoginTime"]);
                    manager.Level = Convert.ToInt32(dt.Rows[i]["Level"].ToString());
                    manager.RndCheckCode = dt.Rows[i]["RndCheckCode"].ToString();

                    ManagerList.Add(manager);
                }
                return ManagerList;
            }
            return ManagerList;

        }

        /// <summary>
        /// 添加管理员信息
        /// </summary>
        /// <param name="manager">管理员信息</param>
        /// <returns>
        /// 0:添加成功
        /// 1：管理员名称已存在，添加失败
        /// </returns>
        public static int AddManager(ManagerInfo manager)
        {
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@return",SqlDbType.Int),
                new SqlParameter("@ManagerName", manager.ManagerName),
                new SqlParameter("@Password", manager.Password),
                new SqlParameter("@Level", manager.Level),
            };
            paras[0].Direction = ParameterDirection.ReturnValue;    //声明此参数是一个返回类型

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Admin_AddManager", paras);

            return Convert.ToInt32(paras[0].Value.ToString());
        }

        public static int EditManager(ManagerInfo manager)
        {

            string sql = "UPDATE ManagerInfo SET Password = @Password , Level=@Level WHERE MID = @MID ";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MID", manager.MID),
                new SqlParameter("@Password", manager.Password),
                new SqlParameter("@Level", manager.Level),
            };

            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
        }

        public static ManagerInfo GetManagerByMid(int MID)
        {
            ManagerInfo manager = new ManagerInfo();
            string sql = "SELECT TOP 1 * FROM ManagerInfo WHERE MID = @MID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MID", MID),
            };
            SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, paras);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    manager.ManagerName = dr["ManagerName"].ToString();
                    manager.MID = Convert.ToInt32(dr["MID"].ToString());
                    manager.Level = Convert.ToInt32(dr["Level"].ToString());
                    manager.LastLoginIP = dr["LastLoginIP"].ToString();
                    manager.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"].ToString());
                    manager.Password = dr["Password"].ToString();
                }
            }

            return manager;
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public static int DelManager(int MID)
        {
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MID", MID),
            };

            //如果是最后一个超级管理员不允许删除
            string selectSql = "SELECT COUNT(MID) FROM ManagerInfo WHERE Level = 0 AND MID<>@MID";
            if(0 == Convert.ToInt32( SqlHelper.ExecuteScalar(CommandType.Text,selectSql,paras))){
                return -1;
            }


            //删除操作
            string sql = "DELETE FROM ManagerInfo WHERE MID = @MID";        
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);

        }
    }
}
