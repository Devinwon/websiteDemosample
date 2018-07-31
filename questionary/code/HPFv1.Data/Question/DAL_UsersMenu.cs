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
public partial class DAL_UsersMenu
    { 
             
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int MID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UsersMenu");
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
        public bool Add( UsersMenu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UsersMenu(");                  
            strSql.Append("MID,PID,MenuName,ControllerName,ActionName,Sort,ChildNum,Icon)");
            strSql.Append(" values (");
            strSql.Append("@MID,@PID,@MenuName,@ControllerName,@ActionName,@Sort,@ChildNum,@Icon)");
            SqlParameter[] parameters = {

                                                        
                                new SqlParameter("@MID", model.MID),
                                                        
                                new SqlParameter("@PID", model.PID),
                                                        
                                new SqlParameter("@MenuName", model.MenuName),
                                                        
                                new SqlParameter("@ControllerName", model.ControllerName),
                                                        
                                new SqlParameter("@ActionName", model.ActionName),
                                                        
                                new SqlParameter("@Sort", model.Sort),
                                                        
                                new SqlParameter("@ChildNum", model.ChildNum),
                                                        
                                new SqlParameter("@Icon", model.Icon),
                                            
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
        public bool Update(UsersMenu model)
        {
            StringBuilder strSql = new StringBuilder();
                strSql.Append("update UsersMenu set ");
                     
                    strSql.Append("   PID=@PID");                    
                     
                    strSql.Append(" ,  MenuName=@MenuName");                    
                     
                    strSql.Append(" ,  ControllerName=@ControllerName");                    
                     
                    strSql.Append(" ,  ActionName=@ActionName");                    
                     
                    strSql.Append(" ,  Sort=@Sort");                    
                     
                    strSql.Append(" ,  ChildNum=@ChildNum");                    
                     
                    strSql.Append(" ,  Icon=@Icon");                    
                        
                strSql.Append(" where ID=@ID ");
                    SqlParameter[] parameters = {
        
                                                                
                                        new SqlParameter("@MID", model.MID),
                                                                
                                        new SqlParameter("@PID", model.PID),
                                                                
                                        new SqlParameter("@MenuName", model.MenuName),
                                                                
                                        new SqlParameter("@ControllerName", model.ControllerName),
                                                                
                                        new SqlParameter("@ActionName", model.ActionName),
                                                                
                                        new SqlParameter("@Sort", model.Sort),
                                                                
                                        new SqlParameter("@ChildNum", model.ChildNum),
                                                                
                                        new SqlParameter("@Icon", model.Icon),
                                                    
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
            public bool Delete(int MID)
            {
                 
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from UsersMenu ");
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
            strSql.Append("delete from UsersMenu  ");
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
        public UsersMenu GetModel(int MID)
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select  top 1 MID,PID,MenuName,ControllerName,ActionName,Sort,ChildNum,Icon from UsersMenu ");
                    strSql.Append(" where MID=@MID ");
                    SqlParameter[] parameters = {
	                		new SqlParameter("@MID", MID)			};
                        
                    UsersMenu model = new UsersMenu();
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
        public UsersMenu DataRowToModel(DataRow row)
                {
                    UsersMenu model = new UsersMenu();
                    if (row != null)
                    {
                        
                        if (row["MID"] != null && row["MID"].ToString() != "")
                        {
                            
                                model.MID = int.Parse(row["MID"].ToString());
                            
                        }
                        
                        if (row["PID"] != null && row["PID"].ToString() != "")
                        {
                            
                                model.PID = int.Parse(row["PID"].ToString());
                            
                        }
                        
                        if (row["MenuName"] != null && row["MenuName"].ToString() != "")
                        {
                            
                                model.MenuName = row["MenuName"].ToString();

                            
                        }
                        
                        if (row["ControllerName"] != null && row["ControllerName"].ToString() != "")
                        {
                            
                                model.ControllerName = row["ControllerName"].ToString();

                            
                        }
                        
                        if (row["ActionName"] != null && row["ActionName"].ToString() != "")
                        {
                            
                                model.ActionName = row["ActionName"].ToString();

                            
                        }
                        
                        if (row["Sort"] != null && row["Sort"].ToString() != "")
                        {
                            
                                model.Sort = int.Parse(row["Sort"].ToString());
                            
                        }
                        
                        if (row["ChildNum"] != null && row["ChildNum"].ToString() != "")
                        {
                            
                                model.ChildNum = int.Parse(row["ChildNum"].ToString());
                            
                        }
                        
                        if (row["Icon"] != null && row["Icon"].ToString() != "")
                        {
                            
                                model.Icon = row["Icon"].ToString();

                            
                        }
                        
                    }
                    return model;
                }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<UsersMenu> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MID,PID,MenuName,ControllerName,ActionName,Sort,ChildNum,Icon from UsersMenu ");
            strSql.Append(" FROM UsersMenu ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                    
                List<UsersMenu> list = new List<UsersMenu>();
                    
                foreach (DataRow row in tab.Rows)
                {
                    list.Add(DataRowToModel(row));
                }
                return list;
                        
        }
    

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<UsersMenu> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from UsersMenu T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataTable tab = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString()).Tables[0];
                
            List<UsersMenu> list = new List<UsersMenu>();
                
            foreach (DataRow row in tab.Rows)
            {
                list.Add(DataRowToModel(row));
            }
                
                
                
            return list;
        }




    }
}




