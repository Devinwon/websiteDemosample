using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Data.Admin
{
    public class UserRole
    {
        //获得权限列表(分页)
        public static List<HPStudent.Entity.UserRole> GetUserRoleList(int start, int length, out int totalRows, HPStudent.Entity.UserRole KeyWords)
        {
            string condition = "";
            string strCount = string.Format(@" select count(RID) FROM UserRole where 1=1 {0}", condition);
            totalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strCount));
            if (totalRows <= 0)
            {
                return new List<HPStudent.Entity.UserRole>();
            }
            string sql = string.Format(@" select TOP {0} RID,RoleName,SortCode from UserRole as u
                                          WHERE u.RID not in (select top {1} u1.RID from UserRole as u1 where 1=1 {2}) {2}", length, start, condition);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.UserRole> lstRes = new List<HPStudent.Entity.UserRole>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.UserRole objTemp = new HPStudent.Entity.UserRole();
                    objTemp.RID = Convert.ToInt32(item["RID"]);
                    objTemp.RoleName = item["RoleName"].ToString();
                    objTemp.SortCode = Convert.ToInt32(item["SortCode"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Entity.UserRole>();
        }
        //得到最大的ID和排序号
        public static HPStudent.Entity.UserRole GetMaxIDAndSort()
        {
            HPStudent.Entity.UserRole ur = new HPStudent.Entity.UserRole();
            string sql = string.Format(@"select MAX(RID) as mid,MAX(SortCode) as sortcode from UserRole");
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    ur.RID = Convert.ToInt32(string.IsNullOrEmpty(reader["mid"].ToString()) ? "0" : reader["mid"].ToString());
                    ur.SortCode = Convert.ToInt32(string.IsNullOrEmpty(reader["SortCode"].ToString()) ? "0" : reader["SortCode"].ToString());
                }
            }
            return ur;
        }
        //添加用户角色
        public static int AddUserRoleItem(HPStudent.Entity.UserRole model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserRole(");
            strSql.Append("RID,RoleName,SortCode)");
            strSql.Append(" values (");
            strSql.Append("@RID,@RoleName,@SortCode)");
            SqlParameter[] parameters = {
					new SqlParameter("@RID", SqlDbType.BigInt,8),
					new SqlParameter("@RoleName", SqlDbType.VarChar,200),
					new SqlParameter("@SortCode", SqlDbType.Int,4)};
            parameters[0].Value = model.RID;
            parameters[1].Value = model.RoleName;
            parameters[2].Value = model.SortCode;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        //得到单个用户角色实体
        public static HPStudent.Entity.UserRole GetUserRoleByID(int id)
        {
            HPStudent.Entity.UserRole model = new HPStudent.Entity.UserRole();
            string sql = string.Format(@"select RID,RoleName,SortCode from UserRole where RID=@RID");
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
                    if (reader["RoleName"] != null)
                    {
                        model.RoleName = reader["RoleName"].ToString();
                    }
                    if (reader["SortCode"] != null && reader["SortCode"].ToString() != "")
                    {
                        model.SortCode = int.Parse(reader["SortCode"].ToString());
                    }
                }
            }
            return model;
        }
        //修改用户角色
        public static int UpdateUserRoleItem(HPStudent.Entity.UserRole model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserRole set ");
            strSql.Append("RoleName=@RoleName");
            strSql.Append(" where RID=@RID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleName", SqlDbType.VarChar,200),
					new SqlParameter("@RID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.RID;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        //获得权限列表
        public static List<HPStudent.Entity.UserRole> GetUserRoleListNotPage()
        {
            string sql = string.Format(@" select  RID,RoleName,SortCode from UserRole as u ");
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.UserRole> lstRes = new List<HPStudent.Entity.UserRole>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.UserRole objTemp = new HPStudent.Entity.UserRole();
                    objTemp.RID = Convert.ToInt32(item["RID"]);
                    objTemp.RoleName = item["RoleName"].ToString();
                    objTemp.SortCode = Convert.ToInt32(item["SortCode"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Entity.UserRole>();
        }
    }
}
