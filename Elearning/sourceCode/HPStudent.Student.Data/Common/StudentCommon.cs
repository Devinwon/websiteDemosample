using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HPStudent.Entity;

namespace HPStudent.Student.Data.Common
{
    public class StudentCommon
    {
        /// <summary>
        /// 获得左导航栏的所有节点
        /// </summary>
        /// <returns></returns>
        public static List<Sys_Menu> GetSideBar(int RoleID)
        {
            List<Sys_Menu> SysMenuList = new List<Sys_Menu>();
            string sql = string.Format(@"declare @Navigation varchar(100);
                                         select @Navigation=up.Navigation from UserRole as u inner join  dbo.UserRole_NavPowrer as up
                                         on u.RID=up.RID where u.RID={0};
                                         select [MID],[PID],[MenuName],[SortCode],[Controller],[Action],[ChildNum],[Icon] 
                                         from dbo.Sys_Menu as m where (exists(select col from dbo.f_splitSTR(@Navigation,',')
                                         where col=m.MID) or m.PID=0 ) and Category=0;", RoleID);
            using (SqlDataReader reader = HPStudent.Student.Data.SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    SysMenuList.Add(
                        new Sys_Menu()
                        {
                            MID = Convert.ToInt32(reader["MID"].ToString()),
                            PID = Convert.ToInt32(reader["PID"].ToString()),
                            MenuName = reader["MenuName"].ToString(),
                            Controller = reader["controller"].ToString(),
                            Action = reader["Action"].ToString(),
                            SortCode = Convert.ToInt32(reader["SortCode"].ToString()),
                            ChildNum = Convert.ToInt32(reader["ChildNum"].ToString()),
                            Icon = reader["Icon"].ToString()
                        }
                    );
                }
            }
            return SysMenuList;

        }
        ///<summary>
        ///functionName(函数名)：GetCityNameByCityID
        ///通过城市ID获取城市地区名称
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-18 11:43:10
        ///Update Date(更新日期): 2016-03-18 11:45:10
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public static string GetCityNameByCityID(int CID)
        {
            string sql = @"  SELECT AREANAME FROM Common_Area WHERE AreaID = @id;";
            SqlParameter[] param = { new SqlParameter("@id", CID) };
            string CityName = HPStudent.Student.Data.SqlHelper.ExecuteScalar(CommandType.Text, sql, param) as string;
            return String.IsNullOrEmpty(CityName) ? "未选择城市" : CityName;
        }

        ///<summary>
        ///functionName(函数名)：GetCityListByParentID
        ///获取城市名称列表
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-18 13:57:10
        ///Update Date(更新日期): 2016-03-18 13:57:10
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public static List<string> GetCityListByParentID(int PID)
        {
            string sql = @"  SELECT AREANAME FROM Common_Area WHERE ParentAID = @id;";
            SqlParameter[] param = { new SqlParameter("@id", PID) };
            DataSet ds = HPStudent.Student.Data.SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<string> lstResult = new List<string>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string temp = Convert.ToString((item["AREANAME"] == DBNull.Value) ? string.Empty : item["AREANAME"].ToString());
                    lstResult.Add(temp);
                }
                return lstResult;
            }
            else
            {
                return null;
            }
        }

        ///<summary>
        ///functionName(函数名)：GetOptionListByCode
        ///通过列表代码来获取对应的列表项 参考 OptionList表
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-04-05 10:26:10
        ///Update Date(更新日期): 2016-04-05 10:26:10
        /// </summary>     
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<string> GetOptionListByCode(string code)
        {
            string sql = @"  SELECT OptionValue FROM OptionList WHERE ListTypeCode = @code ORDER BY SortCode ASC;";
            SqlParameter[] param = { new SqlParameter("@code", code) };
            DataSet ds = HPStudent.Student.Data.SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<string> lstResult = new List<string>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string temp = Convert.ToString((item["OptionValue"] == DBNull.Value) ? string.Empty : item["OptionValue"].ToString());
                    lstResult.Add(temp);
                }
                return lstResult;
            }
            else
            {
                return null;
            }
        }
        ///<summary>
        ///functionName(函数名)：GetOptionListInCode
        ///通过列表代码来获取对应的列表项 参考 OptionList表
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-04-05 10:26:10
        ///Update Date(更新日期): 2016-04-05 10:26:10
        /// </summary>     
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<OptionList> GetOptionListInCode(string code)
        {
            string sql = @"  SELECT ListTypeCode,OptionValue FROM OptionList WHERE ListTypeCode in(" + code + ") ORDER BY SortCode ASC;";
            DataSet ds = HPStudent.Student.Data.SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<OptionList> lstResult = new List<OptionList>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    OptionList temp = new OptionList();
                    temp.OptionValue = Convert.ToString((item["OptionValue"] == DBNull.Value) ? string.Empty : item["OptionValue"].ToString());
                    temp.ListTypeCode = Convert.ToString((item["ListTypeCode"] == DBNull.Value) ? string.Empty : item["ListTypeCode"].ToString());
                    lstResult.Add(temp);
                }
                return lstResult;
            }
            else
            {
                return null;
            }
        }
    }
}
