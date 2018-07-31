using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
    public class UserOnline
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(Entity.UserOnline model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserOnline(");
            strSql.Append("UserID,UserName,RoleID,LastRequestTime,ClientIP,LastRequestPath)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserName,@RoleID,@LastRequestTime,@ClientIP,@LastRequestPath)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.VarChar,100),
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@LastRequestTime", SqlDbType.DateTime),
					new SqlParameter("@ClientIP", SqlDbType.VarChar,250),
					new SqlParameter("@LastRequestPath", SqlDbType.VarChar,250)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.RoleID;
            parameters[3].Value = model.LastRequestTime;
            parameters[4].Value = model.ClientIP;
            parameters[5].Value = model.LastRequestPath;

            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(Entity.UserOnline model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserOnline set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("RoleID=@RoleID,");
            strSql.Append("LastRequestTime=@LastRequestTime,");
            strSql.Append("ClientIP=@ClientIP,");
            strSql.Append("LastRequestPath=@LastRequestPath");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,100),
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@LastRequestTime", SqlDbType.DateTime),
					new SqlParameter("@ClientIP", SqlDbType.VarChar,250),
					new SqlParameter("@LastRequestPath", SqlDbType.VarChar,250),
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.RoleID;
            parameters[2].Value = model.LastRequestTime;
            parameters[3].Value = model.ClientIP;
            parameters[4].Value = model.LastRequestPath;
            parameters[5].Value = model.UserID;

            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        #region 用户在线列表（非分页）
        /// <summary>
        /// 用户在线列表（非分页）
        /// </summary>
        public static List<HPStudent.Entity.UserOnline> UserOnlineListNotPage(HPStudent.Entity.UserOnline keyWord)
        {
            //获取条件
            string condition = "";
            if (keyWord.UserID != 0)
            {
                condition += string.Format("AND  UserID={0}", keyWord.UserID);
            }
            string sql = string.Format(@" select UserID,UserName,RoleID,LastRequestTime,ClientIP,LastRequestPath
                                          FROM UserOnline  WHERE 1=1 {0}", condition);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.UserOnline> lstRes = new List<HPStudent.Entity.UserOnline>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.UserOnline objTemp = new HPStudent.Entity.UserOnline();
                    objTemp.UserID = Convert.ToInt32(item["UserID"]);
                    objTemp.LastRequestTime = DateTime.Parse(item["LastRequestTime"].ToString());
                    objTemp.ClientIP = Convert.ToString(item["ClientIP"] == DBNull.Value ? "" : (object)item["ClientIP"]);
                    objTemp.LastRequestPath = Convert.ToString(item["LastRequestPath"] == DBNull.Value ? "" : (object)item["LastRequestPath"]);
                    objTemp.RoleID = Convert.ToInt32(item["RoleID"]);
                    objTemp.UserName = Convert.ToString(item["UserName"] == DBNull.Value ? "" : (object)item["UserName"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Entity.UserOnline>();
        }
        #endregion
        /// <summary>
        /// 修改学生信息表在线时长字段
        /// </summary>
        public static int UpdateOnlineTime(int studentid, int OnlineTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StudentInfo set ");
            strSql.Append("OnlineTime=@OnlineTime");
            strSql.Append(" where StudentID=@StudentID");
            SqlParameter[] parameters = {
					new SqlParameter("@OnlineTime", SqlDbType.BigInt,8),
					new SqlParameter("@StudentID", SqlDbType.BigInt,8)};
            parameters[0].Value = OnlineTime;
            parameters[1].Value = studentid;

            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
    }
}
