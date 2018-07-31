using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using HPFv1.Entity;

namespace HPFv1.Data
{   
    /// <summary>
    ///  
    /// </summary>
public partial class DAL_QuestionGroup
    {

        /// <summary>
        /// 获取所有个人添加问卷
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="start">起始条数</param>
        /// <param name="length">显示数据长度</param>
        /// <param name="TotalRows">返回行数</param>
        /// <returns></returns>
    public static List<HPFv1.ViewModel.QuestionGroup.QuestionGroupTable> GetQuestionGroupTable(int start, int length,int QID,out int TotalRows)
        {
            string countSql = "select count(*) from QuestionGroup where QID = " + QID + "";

            string sql = string.Format("select top {0} a.*,b.Title,c.NickName,b.QuestionCode from QuestionGroup a inner join Question b  on a.QID=b.QID inner join Users c on a.UID = c.UID  where a.QID = {1} and  GID not in (select top {2} GID from QuestionGroup   order by GID desc)  order by GID desc", length, QID, start);

            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql);
            List<HPFv1.ViewModel.QuestionGroup.QuestionGroupTable> list = null;
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dt!=null)
            {
                list = new List<HPFv1.ViewModel.QuestionGroup.QuestionGroupTable>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HPFv1.ViewModel.QuestionGroup.QuestionGroupTable question = new ViewModel.QuestionGroup.QuestionGroupTable();
                    question.GID = Convert.ToInt32(dt.Rows[i]["GID"]);
                    question.QID = Convert.ToInt32(dt.Rows[i]["QID"]);
                    question.UID = Convert.ToInt32(dt.Rows[i]["UID"]);
                    question.Title = dt.Rows[i]["Title"].ToString();
                    question.UName = dt.Rows[i]["NickName"].ToString();
                    question.GroupName = dt.Rows[i]["GroupName"].ToString();
                    question.CreateDate = Convert.ToDateTime(dt.Rows[i]["CreateDate"]);
                    question.Password = dt.Rows[i]["Password"].ToString();
                    question.QuestionCode = dt.Rows[i]["QuestionCode"].ToString();
                    list.Add(question);
                    
                }

                return list;
            }
            list = new List<ViewModel.QuestionGroup.QuestionGroupTable>();
            return list;


        }



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long GID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QuestionGroup");
            strSql.Append(" where GID=@GID ");
            SqlParameter[] parameters = {
        			new SqlParameter("@GID",GID)			
            };
            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public  static int Add( QuestionGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QuestionGroup(");                  
            strSql.Append("QID,UID,GroupName,Password,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@QID,@UID,@GroupName,@Password,@CreateDate)");
            SqlParameter[] parameters = {

                                                        
                                new SqlParameter("@QID", model.QID),
                                                        
                                new SqlParameter("@UID", model.UID),
                                                        
                                new SqlParameter("@GroupName", model.GroupName),
                                                        
                                new SqlParameter("@Password", model.Password),
                                                        
                                new SqlParameter("@CreateDate", model.CreateDate),
                                            
            };
            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
                return rows;
        
        }

                
            /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(QuestionGroup model)
        {
            StringBuilder strSql = new StringBuilder();
                strSql.Append("update QuestionGroup set ");
                     
                    strSql.Append("   QID=@QID");                    
                     
                    strSql.Append(" ,  UID=@UID");                    
                     
                    strSql.Append(" ,  GroupName=@GroupName");                    
                     
                    strSql.Append(" ,  Password=@Password");                    
                     
                    strSql.Append(" ,  CreateDate=@CreateDate");

                    strSql.Append(" where GID=@GID ");
                    SqlParameter[] parameters = {
        
                                                                
                                        new SqlParameter("@GID", model.GID),
                                                                
                                        new SqlParameter("@QID", model.QID),
                                                                
                                        new SqlParameter("@UID", model.UID),
                                                                
                                        new SqlParameter("@GroupName", model.GroupName),
                                                                
                                        new SqlParameter("@Password", model.Password),
                                                                
                                        new SqlParameter("@CreateDate", model.CreateDate),
                                                    
                    };
                int rows = SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters);
                return rows;
        }


            /// <summary>
            /// 删除一条数据
            /// </summary>
            public static int Delete(long GID)
            {
                 
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from QuestionGroup ");
                strSql.Append(" where GID=@GID ");
                SqlParameter[] parameters = {
            	  	new SqlParameter("@GID", GID)			};
                   
                int rows = SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters);
                return rows;
            }


        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from QuestionGroup  ");
            strSql.Append(" where GID in (" + IDlist + ")  ");
            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static QuestionGroup GetModel(long GID)
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 GID,QID,UID,GroupName,Password,CreateDate from QuestionGroup ");
                    strSql.Append(" where GID=@GID ");
                    SqlParameter[] parameters = {
	                		new SqlParameter("@GID", GID)			};
                        
                    QuestionGroup model = new QuestionGroup();
                    DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return DataRowToModel(ds.Tables[0].Rows[0]);
                    }
                    else
                    {
                        return null;
                    }
                }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static QuestionGroup DataRowToModel(DataRow row)
                {
                    QuestionGroup model = new QuestionGroup();
                    if (row != null)
                    {
                        
                        if (row["GID"] != null && row["GID"].ToString() != "")
                        {
                            
                                model.GID = long.Parse(row["GID"].ToString());
                            
                        }
                        
                        if (row["QID"] != null && row["QID"].ToString() != "")
                        {
                            
                                model.QID = long.Parse(row["QID"].ToString());
                            
                        }
                        
                        if (row["UID"] != null && row["UID"].ToString() != "")
                        {
                            
                                model.UID = long.Parse(row["UID"].ToString());
                            
                        }
                        
                        if (row["GroupName"] != null && row["GroupName"].ToString() != "")
                        {
                            
                                model.GroupName = row["GroupName"].ToString();

                            
                        }
                        
                        if (row["Password"] != null && row["Password"].ToString() != "")
                        {
                            
                                model.Password = row["Password"].ToString();

                            
                        }
                        
                        if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                        {
                            
                                model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                            
                        }
                        
                    }
                    return model;
                }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<QuestionGroup> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GID,QID,UID,GroupName,Password,CreateDate from QuestionGroup ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                    
                List<QuestionGroup> list = new List<QuestionGroup>();
                    
                foreach (DataRow row in tab.Rows)
                {
                    list.Add(DataRowToModel(row));
                }
                return list;
                        
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<QuestionGroup> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.GID desc");
            }
            strSql.Append(")AS Row, T.*  from QuestionGroup T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                
            List<QuestionGroup> list = new List<QuestionGroup>();
                
            foreach (DataRow row in tab.Rows)
            {
                list.Add(DataRowToModel(row));
            }
                
                
                
            return list;
        }




    }
}




