using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
   public class PostTheme
    {
       public static HPStudent.Student.ViewModel.PostTheme.PostBarInfoTable GetModel(int PBID, int StudentID) 
       {
           string sql = @"SELECT * FROM PostBarInfo WHERE PBID = " + PBID + "";
           string sqlStatus = string.Format(@"SELECT Count(*) FROM PostBarStudentRelation WHERE StudentID ={0} and PBID = {1}", StudentID, PBID);
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           int Status =Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStatus));
           HPStudent.Student.ViewModel.PostTheme.PostBarInfoTable entity = new ViewModel.PostTheme.PostBarInfoTable();
           if (dt.Rows.Count > 0)
           {
               entity.PBName = Convert.ToString(dt.Rows[0]["PBName"]);
               entity.PBID = Convert.ToInt32(dt.Rows[0]["PBID"].ToString());
               entity.PBSignature = Convert.ToString(dt.Rows[0]["PBSignature"]);
               entity.Attention = Convert.ToInt32(dt.Rows[0]["Attention"]);
               entity.PostNumber = Convert.ToInt32(dt.Rows[0]["PostNumber"]);
           }
           else 
           {
               return null;
           }
           entity.AttentionStatus = Status;

           sql = string.Format(@"DECLARE  @PBID NVARCHAR(1000)
                                SET  @PBID  = (SELECT  TOP 1 [values]=STUFF((SELECT  ','+[LoveStrollBar] FROM MyBarHomePage t  FOR XML PATH('')), 1, 1, '')  FROM MyBarHomePage WHERE StudentID=646)
                                IF(@PBID is null)
	                                 BEGIN 
		                                SET @PBID = '' 
	                                 END 
                                SET @PBID = stuff(@PBID,1,1,'')
                                PRINT @PBID
                                CREATE TABLE #LoveStrollBarList(PBID varchar(2000))
                                INSERT INTO #LoveStrollBarList SELECT  col FROM f_splitSTR(@PBID,',')
                                IF NOT EXISTS(select * from #LoveStrollBarList WHERE PBID = 16 ) 
                                BEGIN
                                INSERT INTO #LoveStrollBarList VALUES (16)
	                                IF((select COUNT(*) from #LoveStrollBarList )>10)
	                                BEGIN
	                                DELETE FROM #LoveStrollBarList WHERE PBID = (SELECT TOP 1 PBID FROM #LoveStrollBarList)
	                                END 
                                END
                                DECLARE @LoveStrollBar VARCHAR(2000)
                                SELECT @LoveStrollBar = CASE isnull(@LoveStrollBar, '') WHEN '' THEN '' ELSE @LoveStrollBar + ',' END + PBID FROM #LoveStrollBarList WHERE PBID !=''
                                UPDATE MyBarHomePage SET LoveStrollBar = ','+@LoveStrollBar+',' WHERE StudentID = 646
                                DROP TABLE  #LoveStrollBarList", StudentID, PBID);
           Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql));

           return entity;
       }

       public static HPStudent.Student.ViewModel.Common.Datatable< HPStudent.Student.ViewModel.PostTheme.PostThemeTable> BindPostTheme(int start,int length,int PBID) 
       {
           HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.PostTheme.PostThemeTable> table = new ViewModel.Common.Datatable<ViewModel.PostTheme.PostThemeTable>(); 
           table.data = new List<ViewModel.PostTheme.PostThemeTable>();
           string sql = string.Format( @"
                        SELECT TOP {0} A.PBID, A.PTID,A.PostThemeContent,A.PostContent,A.PostDate,
                        (SELECT RealName FROM StudentInfo WHERE StudentID = A.StudentID) AS PostManName,
                        (SELECT BarLV FROM MyBarHomePage WHERE StudentID  = A.StudentID) AS BarLV,
                        (SELECT COUNT(*) FROM PostReply WHERE PTID = A.PTID) AS PRCount
                        FROM PostTheme A 
                         WHERE A.PBID = {1}  AND A.PTID NOT IN (
                         SELECT TOP {2} PTID FROM PostTheme WHERE PBID = {1} ORDER BY PTID DESC
                           ) ORDER BY A.PTID DESC", length, PBID, start);
           string countSql = "SELECT COUNT(*) FROM PostTheme WHERE PBID = " + PBID + "";
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           table.recordsTotal = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, countSql));
            List<HPStudent.Student.ViewModel.PostTheme.PostThemeTable> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<HPStudent.Student.ViewModel.PostTheme.PostThemeTable>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HPStudent.Student.ViewModel.PostTheme.PostThemeTable entity = new HPStudent.Student.ViewModel.PostTheme.PostThemeTable();
                    entity.PTID = Convert.ToInt32(dt.Rows[i]["PTID"]);
                    entity.PBID = Convert.ToInt32(dt.Rows[i]["PBID"]);
                    entity.PostThemeContent = Convert.ToString(dt.Rows[i]["PostThemeContent"]);
                    entity.PostContent = Convert.ToString(dt.Rows[i]["PostContent"]);
                    entity.PostDate = Convert.ToDateTime(dt.Rows[i]["PostDate"]);
                    entity.PRCount = Convert.ToInt32(dt.Rows[i]["PRCount"]);
                    entity.PostManName = Convert.ToString(Convert.ToString(dt.Rows[i]["PostManName"]));
                    list.Add(entity);
                }
                table.data = list;
            }
            else 
            {
                list = new List<HPStudent.Student.ViewModel.PostTheme.PostThemeTable>();
                table.data = list;
            }
            return table;
       
       }

       public static int Attention(int PBID,int StudentID) 
       {
           string sql = string.Format("INSERT INTO PostBarStudentRelation VALUES('{0}','{1}')",StudentID,PBID);
           int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
           return result;
       
       }

       public static int CancelAttention(int PBID, int StudentID) 
       {
           string sql = string.Format("DELETE FROM PostBarStudentRelation WHERE PBID = '{0}' and StudentID = '{1}'", PBID, StudentID);
           int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
           return result;
       
       }

       public static int PostThemeAdd(int PBID, string PostThemeContent, string PostContent, int StudentID) 
       {
           string sql = string.Format("INSERT INTO PostTheme VALUES('{0}','{1}','{2}','{3}','{4}')", PBID, PostThemeContent, PostContent, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), StudentID);
           int result = SqlHelper.ExecuteNonQuery(CommandType.Text,sql);
           return result;
       }

    }
}
