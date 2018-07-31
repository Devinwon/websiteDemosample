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
public partial class DAL_QuestionDetailHistory
    {



    public static List<HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable> GetQuestionDetailHistoryTable(int start, int length, int QID,int GID, string startTime, string endTime, out int TotalRows) 
    {
        StringBuilder strWhere = new StringBuilder();
        if(GID!=0)
        {
            strWhere.Append(" and  c.GID="+GID+""); 
        }
        if(startTime!=""||endTime!="")
        {
            strWhere.Append(" and  a.PostDate>'" + startTime + "' and a.PostDate<'" + endTime + "'");
        }

        string countSql = string.Format("select Count(*) from QuestionDetailHistory a inner join Question b on a.QID = b.QID inner join QuestionGroup c on a.GID = c.GID  where a.QID={0}  {1} ",QID,strWhere.ToString());

        string sql = string.Format("  select top {0} a.*,b.Title,c.GroupName from QuestionDetailHistory a inner join Question b on a.QID = b.QID inner join QuestionGroup c on a.GID = c.GID  where a.QID={1} "+strWhere.ToString()+" and  DID not in (select top {2} DID from QuestionDetailHistory   order by DID desc)  order by DID desc", length,QID, start);

        TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql);
        List<HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable> list = null;
        DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        if (dt != null)
        {
            list = new List<HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable questionDH = new HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable();
                questionDH.GID = Convert.ToInt32(dt.Rows[i]["GID"]);
                questionDH.DID = Convert.ToInt32(dt.Rows[i]["DID"]);
                questionDH.QID = Convert.ToInt32(dt.Rows[i]["QID"]);
                questionDH.Title = dt.Rows[i]["Title"].ToString();
                questionDH.GroupName = dt.Rows[i]["GroupName"].ToString();
                questionDH.Answer = dt.Rows[i]["Answer"].ToString();
                questionDH.PostDate = Convert.ToDateTime(dt.Rows[i]["PostDate"]);
                questionDH.PostIP = dt.Rows[i]["PostIP"].ToString();
                list.Add(questionDH);
            }

            return list;
        }
        list = new List<HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable>();
        return list;
    
    
    }



    /// <summary>
    /// 添加生成结果集历史数据
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static int HisAdd(List<HPFv1.Entity.QuestionDetail> list)
        {
            StringBuilder strbuf = new StringBuilder();
            foreach (var insItem in list)
            {
                strbuf.Append("insert into QuestionDetailHistory values(" + insItem.DID + "," + insItem.QID + "," + insItem.GID + ",'" + insItem.Answer + "','" + insItem.PostDate.ToString("yyyy-MM-dd") + "','" + insItem.PostIP + "')");
            }

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strbuf.ToString());
        
        }

  


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long DID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QuestionDetailHistory");
            strSql.Append(" where DID=@DID ");
            SqlParameter[] parameters = {
        			new SqlParameter("@DID",DID)			
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
        public bool Add( QuestionDetailHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QuestionDetailHistory(");                  
            strSql.Append("DID,QID,GID,Answer,PostDate,PostIP)");
            strSql.Append(" values (");
            strSql.Append("@DID,@QID,@GID,@Answer,@PostDate,@PostIP)");
            SqlParameter[] parameters = {

                                                        
                                new SqlParameter("@DID", model.DID),
                                                        
                                new SqlParameter("@QID", model.QID),
                                                        
                                new SqlParameter("@GID", model.GID),
                                                        
                                new SqlParameter("@Answer", model.Answer),
                                                        
                                new SqlParameter("@PostDate", model.PostDate),
                                                        
                                new SqlParameter("@PostIP", model.PostIP),
                                            
            };
            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
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
        public bool Update(QuestionDetailHistory model)
        {
            StringBuilder strSql = new StringBuilder();
                strSql.Append("update QuestionDetailHistory set ");
                     
                    strSql.Append("   QID=@QID");                    
                     
                    strSql.Append(" ,  GID=@GID");                    
                     
                    strSql.Append(" ,  Answer=@Answer");                    
                     
                    strSql.Append(" ,  PostDate=@PostDate");                    
                     
                    strSql.Append(" ,  PostIP=@PostIP");                    
                        
                strSql.Append(" where ID=@ID ");
                    SqlParameter[] parameters = {
        
                                                                
                                        new SqlParameter("@DID", model.DID),
                                                                
                                        new SqlParameter("@QID", model.QID),
                                                                
                                        new SqlParameter("@GID", model.GID),
                                                                
                                        new SqlParameter("@Answer", model.Answer),
                                                                
                                        new SqlParameter("@PostDate", model.PostDate),
                                                                
                                        new SqlParameter("@PostIP", model.PostIP),
                                                    
                    };
                    int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
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
            public bool Delete(long DID)
            {
                 
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from QuestionDetailHistory ");
                strSql.Append(" where DID=@DID ");
                SqlParameter[] parameters = {
            	  	new SqlParameter("@DID", DID)			};

                int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
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
            strSql.Append("delete from QuestionDetailHistory  ");
            strSql.Append(" where DID in (" + IDlist + ")  ");
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
        public static QuestionDetailHistory GetModel(long DID)
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 DID,QID,GID,Answer,PostDate,PostIP from QuestionDetailHistory ");
                    strSql.Append(" where DID=@DID ");
                    SqlParameter[] parameters = {
	                		new SqlParameter("@DID", DID)			};
                        
                    QuestionDetailHistory model = new QuestionDetailHistory();
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
        public static QuestionDetailHistory DataRowToModel(DataRow row)
                {
                    QuestionDetailHistory model = new QuestionDetailHistory();
                    if (row != null)
                    {
                        
                        if (row["DID"] != null && row["DID"].ToString() != "")
                        {
                            
                                model.DID = long.Parse(row["DID"].ToString());
                            
                        }
                        
                        if (row["QID"] != null && row["QID"].ToString() != "")
                        {
                            
                                model.QID = long.Parse(row["QID"].ToString());
                            
                        }
                        
                        if (row["GID"] != null && row["GID"].ToString() != "")
                        {
                            
                                model.GID = long.Parse(row["GID"].ToString());
                            
                        }
                        
                        if (row["Answer"] != null && row["Answer"].ToString() != "")
                        {
                            
                                model.Answer = row["Answer"].ToString();

                            
                        }
                        
                        if (row["PostDate"] != null && row["PostDate"].ToString() != "")
                        {
                            
                                model.PostDate = DateTime.Parse(row["PostDate"].ToString());
                            
                        }
                        
                        if (row["PostIP"] != null && row["PostIP"].ToString() != "")
                        {
                            
                                model.PostIP = row["PostIP"].ToString();

                            
                        }
                        
                    }
                    return model;
                }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<QuestionDetailHistory> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DID,QID,GID,Answer,PostDate,PostIP from QuestionDetailHistory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                    
                List<QuestionDetailHistory> list = new List<QuestionDetailHistory>();
                    
                foreach (DataRow row in tab.Rows)
                {
                    list.Add(DataRowToModel(row));
                }
                return list;
                        
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<QuestionDetailHistory> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.DID desc");
            }
            strSql.Append(")AS Row, T.*  from QuestionDetailHistory T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                
            List<QuestionDetailHistory> list = new List<QuestionDetailHistory>();
                
            foreach (DataRow row in tab.Rows)
            {
                list.Add(DataRowToModel(row));
            }
                
                
                
            return list;
        }




    }
}




