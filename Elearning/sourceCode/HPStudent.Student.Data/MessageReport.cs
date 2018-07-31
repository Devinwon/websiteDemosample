using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
    public class MessageReport
    {
        #region 消息举报添加
        /// <summary>
        /// 消息举报添加
        /// </summary>
        public static int MessagesAdd(HPStudent.Entity.MessageReport model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MessageReport(");
            strSql.Append("Tid,ReportUID,Reporter,BeReportUID,BeReporter,Body,IsDo,ReportDate,IP,Category)");
            strSql.Append(" values (");
            strSql.Append("@Tid,@ReportUID,@Reporter,@BeReportUID,@BeReporter,@Body,@IsDo,@ReportDate,@IP,@Category);");
            SqlParameter[] parameters = {
					new SqlParameter("@Tid", SqlDbType.BigInt,8),
					new SqlParameter("@ReportUID", SqlDbType.BigInt,8),
					new SqlParameter("@Reporter", SqlDbType.VarChar,64),
					new SqlParameter("@BeReportUID", SqlDbType.BigInt,8),
					new SqlParameter("@BeReporter", SqlDbType.VarChar,64),
					new SqlParameter("@Body", SqlDbType.VarChar,2000),
					new SqlParameter("@IsDo", SqlDbType.TinyInt,1),
					new SqlParameter("@ReportDate", SqlDbType.DateTime),
					new SqlParameter("@IP", SqlDbType.VarChar,64),
					new SqlParameter("@Category", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.Tid;
            parameters[1].Value = model.ReportUID;
            parameters[2].Value = model.Reporter;
            parameters[3].Value = model.BeReportUID;
            parameters[4].Value = model.BeReporter;
            parameters[5].Value = model.Body;
            parameters[6].Value = model.IsDo;
            parameters[7].Value = model.ReportDate;
            parameters[8].Value = model.IP;
            parameters[9].Value = model.Category;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        #endregion
        #region 消息举报列表
        /// <summary>
        /// 消息举报列表
        /// </summary>
        public static List<HPStudent.Entity.MessageReport> MessagesReportList(HPStudent.Entity.MessageReport keyWord, int start, int length, out int totalRows)
        {
            //获取条件
            string condition = "";
            string strCount = string.Format(@" select Count(RID) FROM MessageReport where 1=1 {0} ", condition);
            totalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strCount));
            if (totalRows <= 0)
            {
                return new List<HPStudent.Entity.MessageReport>();
            }
            string sql = string.Format(@" select TOP {0} RID,Tid,ReportUID,Reporter,BeReportUID,BeReporter,Body,IsDo,ReportDate,IP,Category
                                          FROM MessageReport  WHERE RID not in (select top {1}  RID from MessageReport where 1=1 {2} ORDER BY ReportDate DESC) {2}
                                          ORDER BY ReportDate DESC", length, start, condition);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.MessageReport> lstRes = new List<HPStudent.Entity.MessageReport>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.MessageReport objTemp = new HPStudent.Entity.MessageReport();
                    objTemp.BeReporter = Convert.ToString(item["BeReporter"] == DBNull.Value ? "" : (object)item["BeReporter"]);
                    objTemp.BeReportUID = Convert.ToInt32(item["BeReportUID"]);
                    objTemp.ReportDate = Convert.ToDateTime(item["ReportDate"] == DBNull.Value ? "" : (object)Convert.ToDateTime(item["ReportDate"]).ToString("yyyy-MM-dd hh:mm:ss"));
                    objTemp.Body = Convert.ToString(item["Body"] == DBNull.Value ? "" : (object)item["Body"]);
                    objTemp.Category = Convert.ToInt32(item["Category"]);
                    objTemp.IP = Convert.ToString(item["IP"] == DBNull.Value ? "" : (object)item["IP"]);
                    objTemp.IsDo = Convert.ToInt32(item["IsDo"]);
                    objTemp.Reporter = Convert.ToString(item["Reporter"] == DBNull.Value ? "" : (object)item["Reporter"]);
                    objTemp.ReportUID = Convert.ToInt32(item["ReportUID"]);
                    objTemp.RID = Convert.ToInt32(item["RID"]);
                    objTemp.Tid = Convert.ToInt32(item["Tid"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Entity.MessageReport>();
        }
        #endregion
    }
}
