using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Data.Admin
{
    public class AdminRole_NavPowrer
    {
        //通过RID获得菜单权限
        public static HPStudent.Entity.AdminRole_NavPowrer GetAdminRoleNavPowrerByID(int id)
        {
            HPStudent.Entity.AdminRole_NavPowrer model = new HPStudent.Entity.AdminRole_NavPowrer();
            string sql = string.Format(@"select RID,Navigation,SortCode from AdminRole_NavPowrer where RID=@RID");
            SqlParameter[] parameters = {
					new SqlParameter("@RID", SqlDbType.BigInt,8)};
            parameters[0].Value = id;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, parameters))
            {
                while (reader.Read())
                {
                    if (reader["RID"] != null && reader["RID"].ToString() != "")
                    {
                        model.RID = Convert.ToInt32(reader["RID"].ToString());
                    }
                    if (reader["Navigation"] != null)
                    {
                        model.Navigation = reader["Navigation"].ToString();
                    }
                    if (reader["SortCode"] != null && reader["SortCode"].ToString() != "")
                    {
                        model.SortCode = int.Parse(reader["SortCode"].ToString());
                    }
                }
            }
            return model;
        }
        //菜单权限新增
        public static int AddAdminRoleNavPowrerItem(HPStudent.Entity.AdminRole_NavPowrer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AdminRole_NavPowrer(");
            strSql.Append("RID,Navigation,SortCode)");
            strSql.Append(" values (");
            strSql.Append("@RID,@Navigation,@SortCode)");
            SqlParameter[] parameters = {
					new SqlParameter("@RID", SqlDbType.BigInt,8),
					new SqlParameter("@Navigation", SqlDbType.VarChar,500),
					new SqlParameter("@SortCode", SqlDbType.Int,4)};
            parameters[0].Value = model.RID;
            parameters[1].Value = model.Navigation;
            parameters[2].Value = model.SortCode;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        //菜单权限修改
        public static int UpdateAdminRoleNavPowrerItem(HPStudent.Entity.AdminRole_NavPowrer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AdminRole_NavPowrer set ");
            strSql.Append("Navigation=@Navigation");
            strSql.Append(" where RID=@RID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Navigation", SqlDbType.VarChar,500),
					new SqlParameter("@RID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.Navigation;
            parameters[1].Value = model.RID;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
    }
}
