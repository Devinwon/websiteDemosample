using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;

namespace HPStudent.Data.Admin
{
    public class VisitorLog
    {
        /// <summary>
        /// 得到所有专业列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetVisitorList()
        {
            string sql = @"DECLARE @sql_str VARCHAR(8000)
                           DECLARE @sql_col VARCHAR(8000)
                           SELECT @sql_col = ISNULL(@sql_col + ',','') + QUOTENAME(RoleName) FROM (
                           select b.PostDate,b.UserNum,u.RoleName from Bigdate_VisitorByDay as b 
                           inner join UserRole as u 
                           on u.RID=b.RoleID  where DateDiff(dd,b.PostDate,getdate())<=7
                           ) as t group by RoleName
                           SET @sql_str = '
                           SELECT * FROM (
                               select CONVERT(varchar(100), b.PostDate, 23) as PostDate,b.UserNum,u.RoleName from Bigdate_VisitorByDay as b 
                           inner join UserRole as u 
                           on u.RID=b.RoleID where DateDiff(dd,b.PostDate,getdate())<=7) p PIVOT 
                               (SUM([UserNum]) FOR [RoleName] IN ( '+ @sql_col +') ) AS pvt 
                           ORDER BY pvt.[PostDate]'
                           EXEC (@sql_str)";
            DataTable dt = new DataTable();
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}
