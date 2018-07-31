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
public partial class DAL_Users
    { 
             
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Users");
            strSql.Append(" where UID=@UID ");
            SqlParameter[] parameters = {
        			new SqlParameter("@UID",UID)			
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
        public static int Add( Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Users(");                  
            strSql.Append("NickName,Email,Password,CreateDate,LastLogin,LastIP)");
            strSql.Append(" values (");
            strSql.Append("@NickName,@Email,@Password,@CreateDate,@LastLogin,@LastIP)");
            SqlParameter[] parameters = {

                                                        
                                new SqlParameter("@NickName", model.NickName),
                                                        
                                new SqlParameter("@Email", model.Email),
                                                        
                                new SqlParameter("@Password", model.Password),
                                                        
                                new SqlParameter("@CreateDate", model.CreateDate),
                                                        
                                new SqlParameter("@LastLogin", model.LastLogin),
                                                        
                                new SqlParameter("@LastIP", model.LastIP),
                                            
            };
                int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text,strSql.ToString(), parameters));
                return rows;
        
        }

                
            /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(Users model)
        {
            StringBuilder strSql = new StringBuilder();
                strSql.Append("update Users set ");
                     
                    strSql.Append("   NickName=@NickName");                    
                     
                    strSql.Append(" ,  Email=@Email");                    
                     
                    strSql.Append(" ,  Password=@Password");                    
                     
                    strSql.Append(" ,  CreateDate=@CreateDate");                    
                     
                    strSql.Append(" ,  LastLogin=@LastLogin");                    
                     
                    strSql.Append(" ,  LastIP=@LastIP");                    
                        
                strSql.Append(" where UID=@UID ");
                    SqlParameter[] parameters = {
        
                                                                
                                        new SqlParameter("@UID", model.UID),
                                                                
                                        new SqlParameter("@NickName", model.NickName),
                                                                
                                        new SqlParameter("@Email", model.Email),
                                                                
                                        new SqlParameter("@Password", model.Password),
                                                                
                                        new SqlParameter("@CreateDate", model.CreateDate),
                                                                
                                        new SqlParameter("@LastLogin", model.LastLogin),
                                                                
                                        new SqlParameter("@LastIP", model.LastIP),
                                                    
                    };
                int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters));
                return rows;
        }


            /// <summary>
            /// 删除一条数据
            /// </summary>
            public static int Delete(long UID)
            {
                 
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from Users ");
                strSql.Append(" where UID=@UID ");
                SqlParameter[] parameters = {
            	  	new SqlParameter("@UID", UID)			};

                int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters));
                return rows;
            }


        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Users  ");
            strSql.Append(" where UID in (" + IDlist + ")  ");
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
        public static Users GetModel(long UID)
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 UID,NickName,Email,Password,CreateDate,LastLogin,LastIP from Users ");
                    strSql.Append(" where UID=@UID ");
                    SqlParameter[] parameters = {
	                		new SqlParameter("@UID", UID)			};
                        
                    Users model = new Users();
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
        public static Users DataRowToModel(DataRow row)
                {
                    Users model = new Users();
                    if (row != null)
                    {
                        
                        if (row["UID"] != null && row["UID"].ToString() != "")
                        {
                            
                                model.UID = long.Parse(row["UID"].ToString());
                            
                        }
                        
                        if (row["NickName"] != null && row["NickName"].ToString() != "")
                        {
                            
                                model.NickName = row["NickName"].ToString();

                            
                        }
                        
                        if (row["Email"] != null && row["Email"].ToString() != "")
                        {
                            
                                model.Email = row["Email"].ToString();

                            
                        }
                        
                        if (row["Password"] != null && row["Password"].ToString() != "")
                        {
                            
                                model.Password = row["Password"].ToString();

                            
                        }
                        
                        if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                        {
                            
                                model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                            
                        }
                        
                        if (row["LastLogin"] != null && row["LastLogin"].ToString() != "")
                        {
                            
                                model.LastLogin = DateTime.Parse(row["LastLogin"].ToString());
                            
                        }
                        
                        if (row["LastIP"] != null && row["LastIP"].ToString() != "")
                        {
                            
                                model.LastIP = row["LastIP"].ToString();

                            
                        }
                        
                    }
                    return model;
                }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<Users> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UID,NickName,Email,Password,CreateDate,LastLogin,LastIP from Users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                    
                List<Users> list = new List<Users>();
                    
                foreach (DataRow row in tab.Rows)
                {
                    list.Add(DataRowToModel(row));
                }
                return list;
                        
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Users> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.UID desc");
            }
            strSql.Append(")AS Row, T.*  from Users T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                
            List<Users> list = new List<Users>();
                
            foreach (DataRow row in tab.Rows)
            {
                list.Add(DataRowToModel(row));
            }
                
                
                
            return list;
        }




    }
}




