using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;
using ProjectVModel = HPStudent.ViewModel.Projects;

namespace HPStudent.Data.Admin
{
    public class SysMenu
    {
        //获得菜单列表(分页)
        public static List<HPStudent.Entity.Sys_Menu> GetNavationMenuList(int start, int length, out int totalRows, HPStudent.ViewModel.Sys_Menu.SysMenuViewModel KeyWords)
        {
            string condition = "";
            if (KeyWords.SearchType == 0)
            {
                condition += "and pid=0";
                if (!string.IsNullOrEmpty(KeyWords.MenuName))
                {
                    condition += string.Format("and MenuName like'%{0}%'", KeyWords.MenuName);
                }
                condition += string.Format("and Category={0}", KeyWords.Category);
            }
            else if (KeyWords.SearchType == 1)
            {
                condition += string.Format("and pid={0}", KeyWords.PID);
            }
            string strCount = string.Format(@" select count(MID) FROM Sys_Menu where 1=1 {0}", condition);
            totalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strCount));
            if (totalRows <= 0)
            {
                return new List<HPStudent.Entity.Sys_Menu>();
            }
            string sql = string.Format(@" select TOP {0} MID,PID,MenuName,SortCode,Controller,Action,ChildNum,Icon,Category from Sys_Menu as s
                                          WHERE s.MID not in (select top {1} s1.mid from Sys_Menu as s1 where 1=1 {2} order by [SortCode]) {2} order by [SortCode]", length, start, condition);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.Sys_Menu> lstRes = new List<HPStudent.Entity.Sys_Menu>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.Sys_Menu objTemp = new HPStudent.Entity.Sys_Menu();
                    objTemp.Action = item["Action"].ToString();
                    objTemp.ChildNum = Convert.ToInt32(item["ChildNum"]);
                    objTemp.Controller = item["Controller"].ToString();
                    objTemp.Icon = item["Icon"].ToString();
                    objTemp.MenuName = item["MenuName"].ToString();
                    objTemp.MID = Convert.ToInt32(item["MID"]);
                    objTemp.PID = Convert.ToInt32(item["PID"]);
                    objTemp.SortCode = Convert.ToInt32(item["SortCode"]);
                    objTemp.Category = Convert.ToInt32(string.IsNullOrEmpty(item["Category"].ToString()) == true ? 0 : item["Category"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Entity.Sys_Menu>();
        }
        //获得菜单列表
        public static List<HPStudent.Entity.Sys_Menu> GetNavationMenuListNotByPage(HPStudent.ViewModel.Sys_Menu.SysMenuViewModel KeyWords)
        {
            string condition = "";
            if (KeyWords.Category == 0 || KeyWords.Category == 1)
            {
                condition += "and Category=" + KeyWords.Category;
            }
            string sql = string.Format(@" select MID,PID,MenuName,SortCode,Controller,Action,ChildNum,Icon,Category FROM Sys_Menu where 1=1 {0}", condition);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.Sys_Menu> lstRes = new List<HPStudent.Entity.Sys_Menu>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.Sys_Menu objTemp = new HPStudent.Entity.Sys_Menu();
                    objTemp.Action = item["Action"].ToString();
                    objTemp.ChildNum = Convert.ToInt32(item["ChildNum"]);
                    objTemp.Controller = item["Controller"].ToString();
                    objTemp.Icon = item["Icon"].ToString();
                    objTemp.MenuName = item["MenuName"].ToString();
                    objTemp.MID = Convert.ToInt32(item["MID"]);
                    objTemp.PID = Convert.ToInt32(item["PID"]);
                    objTemp.SortCode = Convert.ToInt32(item["SortCode"]);
                    objTemp.Category = Convert.ToInt32(string.IsNullOrEmpty(item["Category"].ToString()) == true ? 0 : item["Category"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Entity.Sys_Menu>();
        }
        //菜单向上（向下）移动
        public static int MenuMove(HPStudent.ViewModel.Sys_Menu.SysMenuViewModel KeyWords)
        {
            string sql = @"SP_MenuDownUpMove ";
            //需求确定之后，注意区分确定相应的用户账号角色RID
            SqlParameter[] param = { 
                                       new SqlParameter("@ReturnInfo",SqlDbType.Int),
                                       new SqlParameter("@MID",KeyWords.MID),
                                       new SqlParameter("@MoveType",KeyWords.MoveType),
                                       new SqlParameter("@PID", KeyWords.PID)                                    
                                   };
            param[0].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, param);
            return Convert.ToInt32(param[0].Value);
        }
        //得到最大的ID和排序号
        public static HPStudent.Entity.Sys_Menu GetMaxIDAndSort()
        {
            HPStudent.Entity.Sys_Menu m = new HPStudent.Entity.Sys_Menu();
            string sql = string.Format(@"select MAX(MID) as mid,MAX(SortCode) as sortcode from Sys_Menu");
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    m.MID = Convert.ToInt32(reader["mid"]);
                    m.SortCode = Convert.ToInt32(reader["SortCode"]);
                }
            }
            return m;
        }
        //添加菜单
        public static int AddMenuItem(HPStudent.Entity.Sys_Menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sys_Menu(");
            strSql.Append("MID,PID,MenuName,SortCode,Controller,Action,ChildNum,Icon,Category)");
            strSql.Append(" values (");
            strSql.Append("@MID,@PID,@MenuName,@SortCode,@Controller,@Action,@ChildNum,@Icon,@Category)");
            SqlParameter[] parameters = {
					new SqlParameter("@MID", SqlDbType.BigInt,8),
					new SqlParameter("@PID", SqlDbType.BigInt,8),
					new SqlParameter("@MenuName", SqlDbType.VarChar,255),
					new SqlParameter("@SortCode", SqlDbType.Int,4),
					new SqlParameter("@Controller", SqlDbType.VarChar,50),
					new SqlParameter("@Action", SqlDbType.VarChar,50),
					new SqlParameter("@ChildNum", SqlDbType.Int,4),
					new SqlParameter("@Icon", SqlDbType.VarChar,50),
                    new SqlParameter("@Category", SqlDbType.Int,4)};
            parameters[0].Value = model.MID;
            parameters[1].Value = model.PID;
            parameters[2].Value = model.MenuName;
            parameters[3].Value = model.SortCode;
            parameters[4].Value = model.Controller;
            parameters[5].Value = model.Action;
            parameters[6].Value = model.ChildNum;
            parameters[7].Value = model.Icon;
            parameters[8].Value = model.Category;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        //得到单个实体
        public static HPStudent.Entity.Sys_Menu GetInfoByID(int id)
        {
            HPStudent.Entity.Sys_Menu model = new HPStudent.Entity.Sys_Menu();
            string sql = string.Format(@"select [MID],[PID],[MenuName],[SortCode],[Controller],[Action]
                                        ,[ChildNum],[Icon],[Category]  from Sys_Menu where MID=@MID");
            SqlParameter[] parameters = {
					new SqlParameter("@MID", SqlDbType.BigInt,8)};
            parameters[0].Value = id;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, parameters))
            {
                while (reader.Read())
                {
                    if (reader["MID"] != null && reader["MID"].ToString() != "")
                    {
                        model.MID = Convert.ToInt32(Convert.ToInt32(string.IsNullOrEmpty(reader["MID"].ToString()) ? "0" : reader["MID"].ToString()));
                    }
                    if (reader["PID"] != null && reader["PID"].ToString() != "")
                    {
                        model.PID = Convert.ToInt32(reader["PID"].ToString());
                    }
                    if (reader["MenuName"] != null)
                    {
                        model.MenuName = reader["MenuName"].ToString();
                    }
                    if (reader["SortCode"] != null && reader["SortCode"].ToString() != "")
                    {
                        model.SortCode = int.Parse(reader["SortCode"].ToString());
                    }
                    if (reader["Controller"] != null)
                    {
                        model.Controller = reader["Controller"].ToString();
                    }
                    if (reader["Action"] != null)
                    {
                        model.Action = reader["Action"].ToString();
                    }
                    if (reader["ChildNum"] != null && reader["ChildNum"].ToString() != "")
                    {
                        model.ChildNum = int.Parse(reader["ChildNum"].ToString());
                    }
                    if (reader["Icon"] != null)
                    {
                        model.Icon = reader["Icon"].ToString();
                    }
                    if (reader["Category"] != null && reader["Category"].ToString() != "")
                    {
                        model.Category = int.Parse(reader["Category"].ToString());
                    }
                }
            }
            return model;
        }
        //修改菜单
        public static int UpdateMenuItem(HPStudent.Entity.Sys_Menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_Menu set ");
            strSql.Append("PID=@PID,");
            strSql.Append("MenuName=@MenuName,");
            strSql.Append("Controller=@Controller,");
            strSql.Append("Action=@Action,");
            strSql.Append("ChildNum=@ChildNum,");
            strSql.Append("Icon=@Icon,");
            strSql.Append("Category=@Category");
            strSql.Append(" where MID=@MID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PID", SqlDbType.BigInt,8),
					new SqlParameter("@MenuName", SqlDbType.VarChar,255),
					new SqlParameter("@Controller", SqlDbType.VarChar,50),
					new SqlParameter("@Action", SqlDbType.VarChar,50),
					new SqlParameter("@ChildNum", SqlDbType.Int,4),
					new SqlParameter("@Icon", SqlDbType.VarChar,50),
                    new SqlParameter("@Category", SqlDbType.Int,4),
					new SqlParameter("@MID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.PID;
            parameters[1].Value = model.MenuName;
            parameters[2].Value = model.Controller;
            parameters[3].Value = model.Action;
            parameters[4].Value = model.ChildNum;
            parameters[5].Value = model.Icon;
            parameters[6].Value = model.Category;
            parameters[7].Value = model.MID;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        //变更子项的类别
        public static int UpdateChildCategory(HPStudent.Entity.Sys_Menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_Menu set ");
            strSql.Append("Category=@Category");
            strSql.Append(" where PID=@PID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Category", SqlDbType.Int,4),
					new SqlParameter("@PID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.Category;
            parameters[1].Value = model.MID;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
    }
}
