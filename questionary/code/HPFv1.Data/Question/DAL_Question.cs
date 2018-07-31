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
public  class DAL_Question
    {



    /// <summary>
    /// 获取所有个人添加问卷
    /// </summary>
    /// <param name="CID"></param>
    /// <param name="start">起始条数</param>
    /// <param name="length">显示数据长度</param>
    /// <param name="TotalRows">返回行数</param>
    /// <returns></returns>
    public static List<HPFv1.ViewModel.Question.QuestionTable> GetQuestionTable(int start, int length, int UID, string title, string endTime, string startTime, out int TotalRows)
    {
            StringBuilder sb = new StringBuilder();
            if (title != "")
            {
                sb.AppendFormat(" and a.title like '%" + title + "%'");
            }

            if (startTime != "" || endTime != "")
            {
                sb.AppendFormat(" and a.StartDate < '" + startTime + "' and a.StartDate >'" + endTime + "'");

            }
            string countSql = "select count(*) from Question a, QuestionTemplate b  where a.TemplateID = b.TID and a.UID=" + UID + " " + sb.ToString() + " ";

            string sql = string.Format("select top {0} a.*,b.Title as TemplateName from Question a , QuestionTemplate b   where a.UID={1} {2} and a.TemplateID = b.TID and QID not in (select top {3} QID from Question   order by qid desc)  order by qid desc", length, UID, sb.ToString(), start);

            TotalRows = (int)SqlHelper.ExecuteNonQuery(CommandType.Text, countSql);
            List<HPFv1.ViewModel.Question.QuestionTable> list = null;
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dt != null)
            {
                list = new List<HPFv1.ViewModel.Question.QuestionTable>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HPFv1.ViewModel.Question.QuestionTable question = new HPFv1.ViewModel.Question.QuestionTable();
                    question.QID = Convert.ToInt32(dt.Rows[i]["QID"]);
                    question.TemplateID = Convert.ToInt32(dt.Rows[i]["TemplateID"]);
                    question.TemplateName = dt.Rows[i]["TemplateName"].ToString();
                    question.TemplateName = dt.Rows[i]["Title"].ToString();
                    question.Title = dt.Rows[i]["Title"].ToString();
                    question.Description = dt.Rows[i]["Description"].ToString();
                    question.QuestionHtml = dt.Rows[i]["QuestionHtml"].ToString();
                    question.QuestionResult = dt.Rows[i]["QuestionResult"].ToString();
                    question.StartDate = Convert.ToDateTime(dt.Rows[i]["StartDate"]);
                    question.EndDate = Convert.ToDateTime(dt.Rows[i]["EndDate"]);
                    question.QNum = Convert.ToInt32(dt.Rows[i]["QNum"]);
                    question.IsCraete = Convert.ToByte(dt.Rows[i]["IsCraete"]);
                    question.IsResult = Convert.ToByte(dt.Rows[i]["IsResult"]);
                    question.QuestionCode = dt.Rows[i]["QuestionCode"].ToString();
                    list.Add(question);
                }

                return list;
            }
            list = new List<HPFv1.ViewModel.Question.QuestionTable>();
            return list;


        }
    


        public static int GetQID() 
        {
            string sql = "select ident_current('Question')";
            int UID = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text,sql));
            return UID;

        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(long QID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Question");
            strSql.Append(" where QID=@QID ");
            SqlParameter[] parameters = {
        			new SqlParameter("@QID",QID)			
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
        public static int Add( Question model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Question(");                  
            strSql.Append("UID,TemplateID,Title,Description,QuestionHtml,QuestionResult,StartDate,EndDate,QNum,IsCraete,IsResult,QuestionCode)");
            strSql.Append(" values (");
            strSql.Append("@UID,@TemplateID,@Title,@Description,@QuestionHtml,@QuestionResult,@StartDate,@EndDate,@QNum,@IsCraete,@IsResult,@QuestionCode)");
            SqlParameter[] parameters = {
                 
                                new SqlParameter("@UID", model.UID),
                                                        
                                new SqlParameter("@TemplateID", model.TemplateID),
                                                        
                                new SqlParameter("@Title", model.Title),
                                                        
                                new SqlParameter("@Description", model.Description),
                                                        
                                new SqlParameter("@QuestionHtml", model.QuestionHtml),
                                                        
                                new SqlParameter("@QuestionResult", model.QuestionResult),
                                                        
                                new SqlParameter("@StartDate", model.StartDate),
                                                        
                                new SqlParameter("@EndDate", model.EndDate),
                                                        
                                new SqlParameter("@QNum", model.QNum),
                                                          
                                new SqlParameter("@IsCraete", model.IsCraete),
                                                        
                                new SqlParameter("@IsResult", model.IsResult),

                                new SqlParameter("@QuestionCode",model.QuestionCode)
                                            
            };
                int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters));
                return rows;
        
        }

                
            /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(Question model)
        {
            StringBuilder strSql = new StringBuilder();
                strSql.Append("update Question set ");
                     
                    strSql.Append("   UID=@UID");                    
                     
                    strSql.Append(" ,  TemplateID=@TemplateID");                    
                     
                    strSql.Append(" ,  Title=@Title");                    
                     
                    strSql.Append(" ,  Description=@Description");                    
                     
                    strSql.Append(" ,  QuestionHtml=@QuestionHtml");                    
                     
                    strSql.Append(" ,  QuestionResult=@QuestionResult");                    
                     
                    strSql.Append(" ,  StartDate=@StartDate");                    
                     
                    strSql.Append(" ,  EndDate=@EndDate");                    
                     
                    strSql.Append(" ,  QNum=@QNum");                    
                     
                    strSql.Append(" ,  IsCraete=@IsCraete");                    
                     
                    strSql.Append(" ,  IsResult=@IsResult");

                    strSql.Append(",   QuestionCode=@QuestionCode");
                strSql.Append(" where QID=@QID ");
                    SqlParameter[] parameters = {
        
                                                                
                                        new SqlParameter("@QID", model.QID),
                                                                
                                        new SqlParameter("@UID", model.UID),
                                                                
                                        new SqlParameter("@TemplateID", model.TemplateID),
                                                                
                                        new SqlParameter("@Title", model.Title),
                                                                
                                        new SqlParameter("@Description", model.Description),
                                                                
                                        new SqlParameter("@QuestionHtml", model.QuestionHtml),
                                                                
                                        new SqlParameter("@QuestionResult", model.QuestionResult),
                                                                
                                        new SqlParameter("@StartDate", model.StartDate),
                                                                
                                        new SqlParameter("@EndDate", model.EndDate),
                                                                
                                        new SqlParameter("@QNum", model.QNum),
                                                                
                                        new SqlParameter("@IsCraete", model.IsCraete),
                                                                
                                        new SqlParameter("@IsResult", model.IsResult),

                                        new SqlParameter("@QuestionCode",model.QuestionCode)
                                                    
                    };
                int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters));
                return rows;
        }


            /// <summary>
            /// 删除一条数据
            /// </summary>
            public static int Delete(long QID)
            {
                 
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from Question ");
                strSql.Append(" where QID=@QID ");
                SqlParameter[] parameters = {
            	  	new SqlParameter("@QID", QID)			};
                   
                int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters));
                return rows;
            }


        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Question  ");
            strSql.Append(" where QID in (" + IDlist + ")  ");
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
        public static Question GetModel(long QID)
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 QID,UID,TemplateID,Title,Description,QuestionHtml,QuestionResult,StartDate,EndDate,QNum,IsCraete,IsResult,QuestionCode from Question ");
                    strSql.Append(" where QID=@QID ");
                    SqlParameter[] parameters = {
	                		new SqlParameter("@QID", QID)			};
                        
                    Question model = new Question();
                    DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text,strSql.ToString(), parameters);
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
        /// 通过QCode得到一个对象实体
        /// </summary>
        public static Question GetCodeByModel(string QuestionCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 QID,UID,TemplateID,Title,Description,QuestionHtml,QuestionResult,StartDate,EndDate,QNum,IsCraete,IsResult,QuestionCode from Question ");
            strSql.Append(" where QuestionCode=@QuestionCode ");
            SqlParameter[] parameters = {
	                		new SqlParameter("@QuestionCode", QuestionCode)			};

            Question model = new Question();
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
        public static Question DataRowToModel(DataRow row)
                {
                    Question model = new Question();
                    if (row != null)
                    {
                        
                        if (row["QID"] != null && row["QID"].ToString() != "")
                        {
                            
                                model.QID = long.Parse(row["QID"].ToString());
                            
                        }
                        
                        if (row["UID"] != null && row["UID"].ToString() != "")
                        {
                            
                                model.UID = long.Parse(row["UID"].ToString());
                            
                        }
                        
                        if (row["TemplateID"] != null && row["TemplateID"].ToString() != "")
                        {
                            
                                model.TemplateID = long.Parse(row["TemplateID"].ToString());
                            
                        }
                        
                        if (row["Title"] != null && row["Title"].ToString() != "")
                        {
                            
                                model.Title = row["Title"].ToString();

                            
                        }
                        
                        if (row["Description"] != null && row["Description"].ToString() != "")
                        {
                            
                                model.Description = row["Description"].ToString();

                            
                        }
                        
                        if (row["QuestionHtml"] != null && row["QuestionHtml"].ToString() != "")
                        {
                            
                                model.QuestionHtml = row["QuestionHtml"].ToString();

                            
                        }
                        
                        if (row["QuestionResult"] != null && row["QuestionResult"].ToString() != "")
                        {
                            
                                model.QuestionResult = row["QuestionResult"].ToString();

                            
                        }
                        
                        if (row["StartDate"] != null && row["StartDate"].ToString() != "")
                        {
                            
                                model.StartDate = DateTime.Parse(row["StartDate"].ToString());
                            
                        }
                        
                        if (row["EndDate"] != null && row["EndDate"].ToString() != "")
                        {
                            
                                model.EndDate = DateTime.Parse(row["EndDate"].ToString());
                            
                        }
                        
                        if (row["QNum"] != null && row["QNum"].ToString() != "")
                        {
                            
                                model.QNum = int.Parse(row["QNum"].ToString());
                            
                        }
                        
                        if (row["IsCraete"] != null && row["IsCraete"].ToString() != "")
                        {
                            
                                model.IsCraete = byte.Parse(row["IsCraete"].ToString());
                            
                        }
                        
                        if (row["IsResult"] != null && row["IsResult"].ToString() != "")
                        {
                            
                                model.IsResult = byte.Parse(row["IsResult"].ToString());
                            
                        }
                        if (row["QuestionCode"] != null && row["QuestionCode"].ToString() != "")
                        {

                            model.QuestionCode = row["QuestionCode"].ToString();

                        }
                    }
                    return model;
                }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<Question> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select QID,UID,TemplateID,Title,Description,QuestionHtml,QuestionResult,StartDate,EndDate,QNum,IsCraete,IsResult,QuestionCode from Question ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                    
                List<Question> list = new List<Question>();
                    
                foreach (DataRow row in tab.Rows)
                {
                    list.Add(DataRowToModel(row));
                }
                return list;
                        
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Question> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.QID desc");
            }
            strSql.Append(")AS Row, T.*  from Question T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                
            List<Question> list = new List<Question>();
                
            foreach (DataRow row in tab.Rows)
            {
                list.Add(DataRowToModel(row));
            }
                
                
                
            return list;
        }




    }
}




