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
public  class DAL_Manager
    { 
             
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long MID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Manager");
            strSql.Append(" where MID=@MID ");
            SqlParameter[] parameters = {
        			new SqlParameter("@MID",MID)			
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
        public bool Add( Manager model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Manager(");                  
            strSql.Append("MID,Account,Password,LastIP,LastDate,NickName)");
            strSql.Append(" values (");
            strSql.Append("@MID,@Account,@Password,@LastIP,@LastDate,@NickName)");
            SqlParameter[] parameters = {

                                                        
                                new SqlParameter("@MID", model.MID),
                                                        
                                new SqlParameter("@Account", model.Account),
                                                        
                                new SqlParameter("@Password", model.Password),
                                                        
                                new SqlParameter("@LastIP", model.LastIP),
                                                        
                                new SqlParameter("@LastDate", model.LastDate),
                                                        
                                new SqlParameter("@NickName", model.NickName),
                                            
            };
            int rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters));
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
        public bool Update(Manager model)
        {
            StringBuilder strSql = new StringBuilder();
                strSql.Append("update Manager set ");
                     
                    strSql.Append("   Account=@Account");                    
                     
                    strSql.Append(" ,  Password=@Password");                    
                     
                    strSql.Append(" ,  LastIP=@LastIP");                    
                     
                    strSql.Append(" ,  LastDate=@LastDate");                    
                     
                    strSql.Append(" ,  NickName=@NickName");                    
                        
                strSql.Append(" where ID=@ID ");
                    SqlParameter[] parameters = {
        
                                                                
                                        new SqlParameter("@MID", model.MID),
                                                                
                                        new SqlParameter("@Account", model.Account),
                                                                
                                        new SqlParameter("@Password", model.Password),
                                                                
                                        new SqlParameter("@LastIP", model.LastIP),
                                                                
                                        new SqlParameter("@LastDate", model.LastDate),
                                                                
                                        new SqlParameter("@NickName", model.NickName),
                                                    
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
            public bool Delete(long MID)
            {
                 
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from Manager ");
                strSql.Append(" where MID=@MID ");
                SqlParameter[] parameters = {
            	  	new SqlParameter("@MID", MID)			};
                   
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
            strSql.Append("delete from Manager  ");
            strSql.Append(" where MID in (" + IDlist + ")  ");
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
        public Manager GetModel(long MID)
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 MID,Account,Password,LastIP,LastDate,NickName from Manager ");
                    strSql.Append(" where MID=@MID ");
                    SqlParameter[] parameters = {
	                		new SqlParameter("@MID", MID)			};
                        
                    Manager model = new Manager();
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
        public Manager DataRowToModel(DataRow row)
                {
                    Manager model = new Manager();
                    if (row != null)
                    {
                        
                        if (row["MID"] != null && row["MID"].ToString() != "")
                        {
                            
                                model.MID = long.Parse(row["MID"].ToString());
                            
                        }
                        
                        if (row["Account"] != null && row["Account"].ToString() != "")
                        {
                            
                                model.Account = row["Account"].ToString();

                            
                        }
                        
                        if (row["Password"] != null && row["Password"].ToString() != "")
                        {
                            
                                model.Password = row["Password"].ToString();

                            
                        }
                        
                        if (row["LastIP"] != null && row["LastIP"].ToString() != "")
                        {
                            
                                model.LastIP = row["LastIP"].ToString();

                            
                        }
                        
                        if (row["LastDate"] != null && row["LastDate"].ToString() != "")
                        {
                            
                                model.LastDate = DateTime.Parse(row["LastDate"].ToString());
                            
                        }
                        
                        if (row["NickName"] != null && row["NickName"].ToString() != "")
                        {
                            
                                model.NickName = row["NickName"].ToString();

                            
                        }
                        
                    }
                    return model;
                }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Manager> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MID,Account,Password,LastIP,LastDate,NickName from Manager ");
            strSql.Append(" FROM Manager ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                    
                List<Manager> list = new List<Manager>();
                    
                foreach (DataRow row in tab.Rows)
                {
                    list.Add(DataRowToModel(row));
                }
                return list;
                        
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Manager> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.MID desc");
            }
            strSql.Append(")AS Row, T.*  from Manager T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                
            List<Manager> list = new List<Manager>();
                
            foreach (DataRow row in tab.Rows)
            {
                list.Add(DataRowToModel(row));
            }
                
                
                
            return list;
        }




    }
}




