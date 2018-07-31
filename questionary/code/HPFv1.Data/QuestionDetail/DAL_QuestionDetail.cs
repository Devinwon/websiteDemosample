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
public partial class DAL_QuestionDetail
    {


        /// <summary>
        /// 调查问卷登录
        /// </summary>
        /// <returns></returns>
        public static HPFv1.ViewModel.QuestionDetail.QuestionDetailTable QuestionDetailLogin(string code,string password) 
        {

            string strSql = "select * from Question a inner join QuestionGroup b on a.QID = b.QID where a.QuestionCode = @QuestionCode and b.Password=@Password";
            SqlParameter[] parameters = 
            {
                new SqlParameter("@QuestionCode",code),
                new SqlParameter("@Password",password)
            };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
             
           if (ds.Tables[0].Rows.Count > 0)
           {
               return RowToModel(ds.Tables[0].Rows[0]);
           }
           else
           {
               return null;
           }

    
        }




    /// <summary>
    /// 调查问卷登录获得表格
    /// </summary>
    /// <returns></returns>
        public static HPFv1.ViewModel.QuestionDetail.QuestionDetailTable RowToModel(DataRow row)
          {
              HPFv1.ViewModel.QuestionDetail.QuestionDetailTable model = new HPFv1.ViewModel.QuestionDetail.QuestionDetailTable();
              if (row != null)
              {

                  if (row["QID"] != null && row["QID"].ToString() != "")
                  {

                      model.QID = long.Parse(row["QID"].ToString());

                  }

                  if (row["GID"] != null && row["GID"].ToString() != "")
                  {

                      model.GID = long.Parse(row["GID"].ToString());

                  }
                  if (row["Password"] != null && row["Password"].ToString() != "")
                  {

                      model.Password = row["Password"].ToString();
                  }

                  if (row["QuestionHtml"] != null && row["QuestionHtml"].ToString() != "")
                  {

                      model.QuestionHtml = row["QuestionHtml"].ToString();
                  }
                  if (row["IsResult"] != null && row["IsResult"].ToString() != "")
                  {

                      model.IsResult = int.Parse(row["IsResult"].ToString());
                  }
                  if (row["IsCraete"] != null && row["IsCraete"].ToString() != "")
                  {

                      model.IsCraete = int.Parse(row["IsCraete"].ToString());
                  }
                  

              }
              return model;
      
          }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long DID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QuestionDetail");
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
        public static int Add( QuestionDetail model)
        {



            StringBuilder strSql = new StringBuilder();
            strSql.Append("BEGIN tran ");
            strSql.Append(" BEGIN TRY");

            strSql.Append(" update question set Qnum = (select QNum from question where qid = " + model.QID + " )+1 where qid = " + model.QID + "  ");

            strSql.Append("insert into QuestionDetail(");                  
            strSql.Append("QID,GID,Answer,PostDate,PostIP,Password)");
            strSql.Append(" values (");
            strSql.Append("@QID,@GID,@Answer,@PostDate,@PostIP,@Password)");
            SqlParameter[] parameters = {               
                                new SqlParameter("@QID", model.QID),
                                                        
                                new SqlParameter("@GID", model.GID),
                                                        
                                new SqlParameter("@Answer", model.Answer),
                                                        
                                new SqlParameter("@PostDate", model.PostDate),
                                                        
                                new SqlParameter("@PostIP", model.PostIP),
                                                        
                                new SqlParameter("@Password", model.Password),
                                            
            };
            strSql.Append(" COMMIT Tran");
            strSql.Append(" END TRY");
            strSql.Append(" BEGIN catch");
            strSql.Append(" ROLLBACK Tran");
            strSql.Append(" end catch");
            strSql.Append(" select @@error");
               int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters));
                return rows;
        
        }

        
                
            /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(QuestionDetail model)
        {
            StringBuilder strSql = new StringBuilder();
                strSql.Append("update QuestionDetail set ");
                     
                    strSql.Append("   QID=@QID");                    
                     
                    strSql.Append(" ,  GID=@GID");                    
                     
                    strSql.Append(" ,  Answer=@Answer");                    
                     
                    strSql.Append(" ,  PostDate=@PostDate");                    
                     
                    strSql.Append(" ,  PostIP=@PostIP");                    
                     
                    strSql.Append(" ,  Password=@Password");                    
                        
                strSql.Append(" where ID=@ID ");
                    SqlParameter[] parameters = {
        
                                                                
                                        new SqlParameter("@DID", model.DID),
                                                                
                                        new SqlParameter("@QID", model.QID),
                                                                
                                        new SqlParameter("@GID", model.GID),
                                                                
                                        new SqlParameter("@Answer", model.Answer),
                                                                
                                        new SqlParameter("@PostDate", model.PostDate),
                                                                
                                        new SqlParameter("@PostIP", model.PostIP),
                                                                
                                        new SqlParameter("@Password", model.Password),
                                                    
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
            public static int Delete(long DID)
            {
                 
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from QuestionDetail ");
                strSql.Append(" where DID=@DID ");
                SqlParameter[] parameters = {
            	  	new SqlParameter("@DID", DID)			};
                   
               int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters));
                return rows;
            }

            /// <summary>
            /// 删除一条数据
            /// </summary>
            public static int GetQIDDelete(long QID)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from QuestionDetail ");
                strSql.Append(" where QID=@QID ");
                SqlParameter[] parameters = {
            	  	new SqlParameter("@QID", QID)			};

               int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters));
                return rows;
            }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from QuestionDetail  ");
            strSql.Append(" where DID in (" + IDlist + ")  ");
            int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString()));
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
        public QuestionDetail GetModel(long DID)
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 DID,QID,GID,Answer,PostDate,PostIP,Password from QuestionDetail ");
                    strSql.Append(" where DID=@DID ");
                    SqlParameter[] parameters = {
	                		new SqlParameter("@DID", DID)			};
                        
                    QuestionDetail model = new QuestionDetail();
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
        public static QuestionDetail DataRowToModel(DataRow row)
                {
                    QuestionDetail model = new QuestionDetail();
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
                        
                        if (row["Password"] != null && row["Password"].ToString() != "")
                        {
                            
                                model.Password = row["Password"].ToString();

                            
                        }
                        
                    }
                    return model;
                }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<QuestionDetail> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DID,QID,GID,Answer,PostDate,PostIP,Password from QuestionDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                    
                List<QuestionDetail> list = new List<QuestionDetail>();
                    
                foreach (DataRow row in tab.Rows)
                {
                    list.Add(DataRowToModel(row));
                }
                return list;
                        
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<QuestionDetail> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from QuestionDetail T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                
            List<QuestionDetail> list = new List<QuestionDetail>();
                
            foreach (DataRow row in tab.Rows)
            {
                list.Add(DataRowToModel(row));
            }
                
                
                
            return list;
        }




    }
}




