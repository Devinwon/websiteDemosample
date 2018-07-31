using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HPStudent.Entity;

namespace HPStudent.Data.Common
{
    public class AdminCommon
    {
        /// <summary>
        /// 获得左导航栏的所有节点
        /// </summary>
        /// <returns></returns>
        public static List<Sys_Menu> GetSideBar(int MID)
        {
            List<Sys_Menu> SysMenuList = new List<Sys_Menu>();
            string sql = string.Format(@"declare @Navigation varchar(100);
                                         select @Navigation=stuff((
                                         select   Navigation+','    from AdminRole as u inner join  dbo.AdminRole_NavPowrer as up
                                          on u.RID=up.RID 
                                          inner join  dbo.AdminRoleRelation as ar
                                         on up.RID=ar.RoleID
                                         where ar.MID={0}  FOR XML PATH('')
                                         ),1,0,'');
                                         select [MID],[PID],[MenuName],[SortCode],[Controller],[Action],[ChildNum],[Icon] 
                                         from dbo.Sys_Menu as m where (exists(select col from dbo.f_splitSTR(@Navigation,',')
                                         where col=m.MID) or m.PID=0 ) and Category=1 order by SortCode;", MID);
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql))
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
    }
}
