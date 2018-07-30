using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
    public class Messages
    {
        #region 站内消息添加
        /// <summary>
        /// 站内消息添加
        /// </summary>
        public static int MessagesAdd(HPStudent.Entity.Messages model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Messages(");
            strSql.Append("SenderID,Sender,ReceiverID,Receiver,Title,Body,IsRead,DateCreated,IP,Type)");
            strSql.Append(" values (");
            strSql.Append("@SenderID,@Sender,@ReceiverID,@Receiver,@Title,@Body,@IsRead,@DateCreated,@IP,@Type);");
            SqlParameter[] parameters = {
					new SqlParameter("@SenderID", SqlDbType.BigInt,8),
					new SqlParameter("@Sender", SqlDbType.VarChar,64),
					new SqlParameter("@ReceiverID", SqlDbType.BigInt,8),
					new SqlParameter("@Receiver", SqlDbType.VarChar,64),
					new SqlParameter("@Title", SqlDbType.VarChar,1000),
					new SqlParameter("@Body", SqlDbType.VarChar,2000),
					new SqlParameter("@IsRead", SqlDbType.TinyInt,1),
					new SqlParameter("@DateCreated", SqlDbType.DateTime),
					new SqlParameter("@IP", SqlDbType.VarChar,64),
					new SqlParameter("@Type", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.SenderID;
            parameters[1].Value = model.Sender;
            parameters[2].Value = model.ReceiverID;
            parameters[3].Value = model.Receiver;
            parameters[4].Value = model.Title;
            parameters[5].Value = model.Body;
            parameters[6].Value = model.IsRead;
            parameters[7].Value = model.DateCreated;
            parameters[8].Value = model.IP;
            parameters[9].Value = model.Type;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        #endregion

        #region 站内消息列表
        /// <summary>
        /// 站内消息列表
        /// </summary>
        public static List<HPStudent.Entity.Messages> MessagesList(HPStudent.Entity.Messages keyWord, int start, int length, out int totalRows)
        {
            //获取条件
            string condition = "";
            if (keyWord.ReceiverID != 0)
            {
                condition += string.Format("AND  ReceiverID={0}", keyWord.ReceiverID);
            }
            if (keyWord.SenderID != 0)
            {
                condition += string.Format("AND  SenderID={0}", keyWord.SenderID);
            }
            string strCount = string.Format(@" select Count(MID) FROM Messages where 1=1 {0} ", condition);
            totalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strCount));
            if (totalRows <= 0)
            {
                return new List<HPStudent.Entity.Messages>();
            }
            string sql = string.Format(@" select TOP {0} MID,SenderID,Sender,ReceiverID,Receiver,Title,Body,IsRead,DateCreated,IP,Type
                                          FROM Messages  WHERE MID not in (select top {1}  MID from Messages where 1=1 {2} ORDER BY DateCreated DESC) {2}
                                          ORDER BY DateCreated DESC", length, start, condition);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.Messages> lstRes = new List<HPStudent.Entity.Messages>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.Messages objTemp = new HPStudent.Entity.Messages();
                    objTemp.Body = Convert.ToString(item["Body"] == DBNull.Value ? "" : (object)item["Body"]);
                    objTemp.DateCreated = Convert.ToDateTime(item["DateCreated"] == DBNull.Value ? "" : (object)Convert.ToDateTime(item["DateCreated"]).ToString("yyyy-MM-dd hh:mm:ss"));
                    objTemp.IP = Convert.ToString(item["IP"] == DBNull.Value ? "" : (object)item["IP"]);
                    objTemp.IsRead = Convert.ToInt32(item["IsRead"]);
                    objTemp.MID = Convert.ToInt32(item["MID"]);
                    objTemp.Receiver = Convert.ToString(item["Receiver"] == DBNull.Value ? "" : (object)item["Receiver"]);
                    objTemp.ReceiverID = Convert.ToInt32(item["ReceiverID"]);
                    objTemp.Sender = Convert.ToString(item["Sender"] == DBNull.Value ? "" : (object)item["Sender"]);
                    objTemp.SenderID = Convert.ToInt32(item["SenderID"]);
                    objTemp.Title = Convert.ToString(item["Title"] == DBNull.Value ? "" : (object)item["Title"]);
                    objTemp.Type = Convert.ToInt32(item["Type"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Entity.Messages>();
        }
        #endregion

        #region 站内消息列表（非分页）
        /// <summary>
        /// 站内消息列表（非分页）
        /// </summary>
        public static List<HPStudent.Entity.Messages> MessagesListNotPage(HPStudent.Entity.Messages keyWord)
        {
            //获取条件
            string condition = "";
            if (keyWord.ReceiverID != 0)
            {
                condition += string.Format("AND  ReceiverID={0}", keyWord.ReceiverID);
            }
            if (keyWord.SenderID != 0)
            {
                condition += string.Format("AND  SenderID={0}", keyWord.SenderID);
            }
            if (keyWord.MID != 0)
            {
                condition += string.Format("AND  MID={0}", keyWord.MID);
            }
            string sql = string.Format(@" select MID,SenderID,Sender,ReceiverID,Receiver,Title,Body,IsRead,DateCreated,IP,Type
                                          FROM Messages  WHERE 1=1 {0}", condition);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.Messages> lstRes = new List<HPStudent.Entity.Messages>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.Messages objTemp = new HPStudent.Entity.Messages();
                    objTemp.Body = Convert.ToString(item["Body"] == DBNull.Value ? "" : (object)item["Body"]);
                    objTemp.DateCreated = Convert.ToDateTime(item["DateCreated"] == DBNull.Value ? "" : (object)Convert.ToDateTime(item["DateCreated"]).ToString("yyyy-MM-dd hh:mm:ss"));
                    objTemp.IP = Convert.ToString(item["IP"] == DBNull.Value ? "" : (object)item["IP"]);
                    objTemp.IsRead = Convert.ToInt32(item["IsRead"]);
                    objTemp.MID = Convert.ToInt32(item["MID"]);
                    objTemp.Receiver = Convert.ToString(item["Receiver"] == DBNull.Value ? "" : (object)item["Receiver"]);
                    objTemp.ReceiverID = Convert.ToInt32(item["ReceiverID"]);
                    objTemp.Sender = Convert.ToString(item["Sender"] == DBNull.Value ? "" : (object)item["Sender"]);
                    objTemp.SenderID = Convert.ToInt32(item["SenderID"]);
                    objTemp.Title = Convert.ToString(item["Title"] == DBNull.Value ? "" : (object)item["Title"]);
                    objTemp.Type = Convert.ToInt32(item["Type"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Entity.Messages>();
        }
        #endregion

        #region 获取未读的邮件数量
        /// <summary>
        /// 获取未读的邮件数量
        /// </summary>
        public static int GetNotReaderMessage(int ReceiverID)
        {
            //获取条件
            string condition = "";
            if (ReceiverID != 0)
            {
                condition += string.Format("AND  ReceiverID={0}", ReceiverID);
            }
            string sql = string.Format(@"select count(MID) from Messages where IsRead=0 {0};", condition);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql));
        }
        #endregion

        #region 站内消息设置已读
        /// <summary>
        /// 站内消息设置已读
        /// </summary>
        public static int SetMessagesIsReader(HPStudent.Entity.Messages model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Messages set ");
            strSql.Append("IsRead=@IsRead");
            strSql.Append(" where MID=@MID");
            strSql.Append(" AND ReceiverID=@ReceiverID");
            SqlParameter[] parameters = {
					new SqlParameter("@IsRead", SqlDbType.TinyInt,1),
					new SqlParameter("@MID", SqlDbType.BigInt,8),
                    new SqlParameter("@ReceiverID", SqlDbType.BigInt,8)};
            parameters[0].Value = 1;
            parameters[1].Value = model.MID;
            parameters[2].Value = model.ReceiverID;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        #endregion

        #region 获取企业发送的信息集合
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<HPStudent.Entity.Messages> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Messages ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString());

            List<HPStudent.Entity.Messages> list = new List<HPStudent.Entity.Messages>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.Messages objTemp = new HPStudent.Entity.Messages();
                    objTemp.Body = Convert.ToString(item["Body"] == DBNull.Value ? "" : (object)item["Body"]);
                    objTemp.DateCreated = Convert.ToDateTime(item["DateCreated"] == DBNull.Value ? "" : (object)Convert.ToDateTime(item["DateCreated"]).ToString("yyyy-MM-dd hh:mm:ss"));
                    objTemp.IP = Convert.ToString(item["IP"] == DBNull.Value ? "" : (object)item["IP"]);
                    objTemp.IsRead = Convert.ToInt32(item["IsRead"]);
                    objTemp.MID = Convert.ToInt32(item["MID"]);
                    objTemp.Receiver = Convert.ToString(item["Receiver"] == DBNull.Value ? "" : (object)item["Receiver"]);
                    objTemp.ReceiverID = Convert.ToInt32(item["ReceiverID"]);
                    objTemp.Sender = Convert.ToString(item["Sender"] == DBNull.Value ? "" : (object)item["Sender"]);
                    objTemp.SenderID = Convert.ToInt32(item["SenderID"]);
                    objTemp.Title = Convert.ToString(item["Title"] == DBNull.Value ? "" : (object)item["Title"]);
                    objTemp.Type = Convert.ToInt32(item["Type"]);
                    list.Add(objTemp);
                }
            
            }
            return list;
            

        }

        #endregion

      

    }
}
