using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HPStudent.Entity;

namespace HPStudent.Data.Admin
{
    public class Account
    {
        /// <summary>
        /// 管理员登录验证
        /// </summary>
        /// <param name="manager">管理员信息</param>
        /// <returns></returns>
        public static ManagerInfo IsAdminLogin(ManagerInfo manager)
        {
            string sql = "SELECT * FROM ManagerInfo WHERE ManagerName=@ManagerName AND Password=@Password";

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@ManagerName", manager.ManagerName), 
                new SqlParameter("@Password", manager.Password)
            };

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras);
            HPStudent.Entity.ManagerInfo managerInfo;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                managerInfo = new HPStudent.Entity.ManagerInfo();
                managerInfo.MID = Convert.ToInt32(ds.Tables[0].Rows[0]["MID"].ToString());
                managerInfo.ManagerName = ds.Tables[0].Rows[0]["ManagerName"].ToString();
                managerInfo.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                managerInfo.Level = Convert.ToInt32(ds.Tables[0].Rows[0]["Level"].ToString());
               // managerInfo.RoleID = Convert.ToInt32(string.IsNullOrEmpty(ds.Tables[0].Rows[0]["RoleID"].ToString()) == true ? "0" : ds.Tables[0].Rows[0]["RoleID"].ToString());
                return managerInfo;
            }
            else
            {
                return null;
            }
        }

        public static bool CheckAdmin(string AdminName, string AdminPassword)
        {
            string sql = "SELECT COUNT(1) FROM ManagerInfo WHERE ManagerName = @ManagerName AND Password = @Password";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@ManagerName", AdminName), 
                new SqlParameter("@Password", AdminPassword)
            };

            return 1 <= Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql, paras)) ? true : false;
        }
    }
}
