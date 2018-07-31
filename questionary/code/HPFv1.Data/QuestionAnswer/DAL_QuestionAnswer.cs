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
public partial class DAL_QuestionAnswer
    { 
             
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QuestionAnswer");
            strSql.Append(" where AID=@AID ");
            SqlParameter[] parameters = {
        			new SqlParameter("@AID",AID)			
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
        public bool Add( QuestionAnswer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QuestionAnswer(");                  
            strSql.Append("AID,Number,Type,Choice,Title,Content,PCode)");
            strSql.Append(" values (");
            strSql.Append("@AID,@Number,@Type,@Choice,@Title,@Content,@PCode)");
            SqlParameter[] parameters = {

                                                        
                                new SqlParameter("@AID", model.AID),
                                                        
                                new SqlParameter("@Number", model.Number),
                                                        
                                new SqlParameter("@Type", model.Type),
                                                        
                                new SqlParameter("@Choice", model.Choice),
                                                        
                                new SqlParameter("@Title", model.Title),
                                                        
                                new SqlParameter("@Content", model.Content),
                                                        
                                new SqlParameter("@PCode", model.PCode),
                                            
            };
                int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters));
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
        /// 更新一条数据
        /// </summary>
        public bool Update(QuestionAnswer model)
        {
            StringBuilder strSql = new StringBuilder();
                strSql.Append("update QuestionAnswer set ");
                     
                    strSql.Append("   Number=@Number");                    
                     
                    strSql.Append(" ,  Type=@Type");                    
                     
                    strSql.Append(" ,  Choice=@Choice");                    
                     
                    strSql.Append(" ,  Title=@Title");                    
                     
                    strSql.Append(" ,  Content=@Content");                    
                     
                    strSql.Append(" ,  PCode=@PCode");                    
                        
                strSql.Append(" where ID=@ID ");
                    SqlParameter[] parameters = {
        
                                                                
                                        new SqlParameter("@AID", model.AID),
                                                                
                                        new SqlParameter("@Number", model.Number),
                                                                
                                        new SqlParameter("@Type", model.Type),
                                                                
                                        new SqlParameter("@Choice", model.Choice),
                                                                
                                        new SqlParameter("@Title", model.Title),
                                                                
                                        new SqlParameter("@Content", model.Content),
                                                                
                                        new SqlParameter("@PCode", model.PCode),
                                                    
                    };
                int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters));
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
            /// 删除一条数据
            /// </summary>
            public bool Delete(long AID)
            {
                 
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from QuestionAnswer ");
                strSql.Append(" where AID=@AID ");
                SqlParameter[] parameters = {
            	  	new SqlParameter("@AID", AID)			};
                   
                int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters));
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from QuestionAnswer  ");
            strSql.Append(" where AID in (" + IDlist + ")  ");
            int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString()));
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
        public QuestionAnswer GetModel(long AID)
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 AID,Number,Type,Choice,Title,Content,PCode from QuestionAnswer ");
                    strSql.Append(" where AID=@AID ");
                    SqlParameter[] parameters = {
	                		new SqlParameter("@AID", AID)			};
                        
                    QuestionAnswer model = new QuestionAnswer();
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
        public QuestionAnswer DataRowToModel(DataRow row)
                {
                    QuestionAnswer model = new QuestionAnswer();
                    if (row != null)
                    {
                        
                        if (row["AID"] != null && row["AID"].ToString() != "")
                        {
                            
                                model.AID = long.Parse(row["AID"].ToString());
                            
                        }
                        
                        if (row["Number"] != null && row["Number"].ToString() != "")
                        {
                            
                                model.Number = row["Number"].ToString();

                            
                        }
                        
                        if (row["Type"] != null && row["Type"].ToString() != "")
                        {
                            
                                model.Type = row["Type"].ToString();

                            
                        }
                        
                        if (row["Choice"] != null && row["Choice"].ToString() != "")
                        {
                            
                                model.Choice = row["Choice"].ToString();

                            
                        }
                        
                        if (row["Title"] != null && row["Title"].ToString() != "")
                        {
                            
                                model.Title = row["Title"].ToString();

                            
                        }
                        
                        if (row["Content"] != null && row["Content"].ToString() != "")
                        {
                            
                                model.Content = row["Content"].ToString();

                            
                        }
                        
                        if (row["PCode"] != null && row["PCode"].ToString() != "")
                        {
                            
                                model.PCode = row["PCode"].ToString();

                            
                        }
                        
                    }
                    return model;
                }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<QuestionAnswer> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AID,Number,Type,Choice,Title,Content,PCode from QuestionAnswer ");
            strSql.Append(" FROM QuestionAnswer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                    
                List<QuestionAnswer> list = new List<QuestionAnswer>();
                    
                foreach (DataRow row in tab.Rows)
                {
                    list.Add(DataRowToModel(row));
                }
                return list;
                        
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<QuestionAnswer> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_Number() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.AID desc");
            }
            strSql.Append(")AS Row, T.*  from QuestionAnswer T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                
            List<QuestionAnswer> list = new List<QuestionAnswer>();
                
            foreach (DataRow row in tab.Rows)
            {
                list.Add(DataRowToModel(row));
            }
                
                
                
            return list;
        }




    }
}




