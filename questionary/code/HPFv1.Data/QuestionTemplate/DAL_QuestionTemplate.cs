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
public  class DAL_QuestionTemplate
    {

    public static List<HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable> GetQuestionTemplateTable(int start, int length, int UID, out int TotalRows) 
    {
        string countSql = "select count(*) from QuestionTemplate ";

        string sql = string.Format("select top {0} a.* ,b.NickName as UName ,(select NickName from Users where UID = a.EditID) as EditName from  QuestionTemplate a inner join Users b on a.UID = b.UID   where a.UID= {1} and TID not in (select top {2} TID from QuestionTemplate   order by TID desc)  order by TID desc", length, UID,start);

        TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql);
        List<HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable> list = null;
        DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        if (dt != null)
        {
            list = new List<HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable qt = new ViewModel.QuestionTemplate.QuestionTemplateTable();
                qt.TID = Convert.ToInt32(dt.Rows[i]["TID"]);
                qt.UID = Convert.ToInt32(dt.Rows[i]["UID"]);
                qt.UName = dt.Rows[i]["UName"].ToString();
                qt.EditName = dt.Rows[i]["EditName"].ToString();
                qt.Title = dt.Rows[i]["Title"].ToString();
                qt.Content = dt.Rows[i]["Content"].ToString();
                qt.CreateDate = Convert.ToDateTime(dt.Rows[i]["CreateDate"]);
                qt.EditDate = Convert.ToDateTime(dt.Rows[i]["EditDate"]);
                list.Add(qt);
            }

            return list;
        }
        list = new List<HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable>();
        return list;
    }



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long TID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QuestionTemplate");
            strSql.Append(" where TID=@TID ");
            SqlParameter[] parameters = {
        			new SqlParameter("@TID",TID)			
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
        public static int Add( QuestionTemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QuestionTemplate(");
            strSql.Append("UID,Title,Content,CreateDate,EditDate,TopicCount,EditID)");
            strSql.Append(" values (");
            strSql.Append("@UID,@Title,@Content,@CreateDate,@EditDate,@TopicCount,@EditID)");
            SqlParameter[] parameters = {

                                                        
                               
                                                        
                                new SqlParameter("@UID", model.UID),
                                                        
                                new SqlParameter("@Title", model.Title),
                                                        
                                new SqlParameter("@Content", model.Content),
                                                        
                                new SqlParameter("@CreateDate", model.CreateDate),
                                                        
                                new SqlParameter("@EditDate", model.EditDate),

                                new SqlParameter("@TopicCount", model.TopicCount),

                                  new SqlParameter("@EditID", model.EditID),
                                            
            };
            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return rows;
                }
                else
                {
                    return rows;
                }
        
        }

                
            /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(QuestionTemplate model)
        {
            StringBuilder strSql = new StringBuilder();
                strSql.Append("update QuestionTemplate set ");
                     
                    strSql.Append("   Title=@Title");                    
                     
                    strSql.Append(" ,  Content=@Content");                    
                     
                    strSql.Append(" ,  EditDate=@EditDate");

                    strSql.Append(" ,  TopicCount=@TopicCount");

                    strSql.Append(" ,  EditID = @EditID ");
                strSql.Append(" where TID=@TID ");
                    SqlParameter[] parameters = {
        
                                                                
                                        new SqlParameter("@TID", model.TID),
                                                                            
                                        new SqlParameter("@Title", model.Title),
                                                                
                                        new SqlParameter("@Content", model.Content),
                                                                               
                                        new SqlParameter("@EditDate", model.EditDate),

                                        new SqlParameter("@TopicCount", model.TopicCount),

                                        new SqlParameter("@EditID", model.EditID),
                                                    
                    };
                    int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
                return rows;
        }


            /// <summary>
            /// 删除一条数据
            /// </summary>
            public static int Delete(long TID)
            {
                 
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from QuestionTemplate ");
                strSql.Append(" where TID=@TID ");
                SqlParameter[] parameters = {
            	  	new SqlParameter("@TID", TID)			};

                int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
                return rows;
            }


        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from QuestionTemplate  ");
            strSql.Append(" where TID in (" + IDlist + ")  ");
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
        public static QuestionTemplate GetModel(long TID)
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 TID,UID,Title,Content,CreateDate,EditDate,TopicCount,EditID from QuestionTemplate ");
                    strSql.Append(" where TID=@TID ");
                    SqlParameter[] parameters = {
	                		new SqlParameter("@TID", TID)			};
                        
                    QuestionTemplate model = new QuestionTemplate();
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
        public static QuestionTemplate DataRowToModel(DataRow row)
                {
                    QuestionTemplate model = new QuestionTemplate();
                    if (row != null)
                    {
                        
                        if (row["TID"] != null && row["TID"].ToString() != "")
                        {
                            
                                model.TID = long.Parse(row["TID"].ToString());
                            
                        }
                        
                        if (row["UID"] != null && row["UID"].ToString() != "")
                        {
                            
                                model.UID = long.Parse(row["UID"].ToString());
                            
                        }
                        
                        if (row["Title"] != null && row["Title"].ToString() != "")
                        {
                            
                                model.Title = row["Title"].ToString();

                            
                        }
                        
                        if (row["Content"] != null && row["Content"].ToString() != "")
                        {
                            
                                model.Content = row["Content"].ToString();

                            
                        }
                        
                        if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                        {
                            
                                model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                            
                        }
                        
                        if (row["EditDate"] != null && row["EditDate"].ToString() != "")
                        {
                            
                                model.EditDate = DateTime.Parse(row["EditDate"].ToString());
                            
                        }
                        if (row["TopicCount"] != null && row["TopicCount"].ToString() != "")
                        {

                            model.TopicCount = int.Parse(row["TopicCount"].ToString());
                            
                        }

                        if (row["EditID"] != null && row["EditID"].ToString() != "")
                        {

                            model.EditID = int.Parse(row["EditID"].ToString());

                        }
                        
                    }
                    return model;
                }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<QuestionTemplate> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TID,UID,Title,Content,CreateDate,EditDate,TopicCount,EditID from QuestionTemplate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

                DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text,strSql.ToString()).Tables[0];
                    
                List<QuestionTemplate> list = new List<QuestionTemplate>();
                    
                foreach (DataRow row in tab.Rows)
                {
                    list.Add(DataRowToModel(row));
                }
                return list;
                        
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<QuestionTemplate> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.TID desc");
            }
            strSql.Append(")AS Row, T.*  from QuestionTemplate T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                
            List<QuestionTemplate> list = new List<QuestionTemplate>();
                
            foreach (DataRow row in tab.Rows)
            {
                list.Add(DataRowToModel(row));
            }
                
                
                
            return list;
        }




    }
}




