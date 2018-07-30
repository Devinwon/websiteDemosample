using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
   public class MyBarHomePage
    {
       public static HPStudent.Student.ViewModel.MyBarHomePage.MyBarHomePageTable GetMyBarHomePageTable(int StudentID) 
       {
           string sql = "SELECT A.*,(SELECT RealName FROM StudentInfo WHERE StudentID = a.StudentID) AS RealName FROM MyBarHomePage A WHERE A.StudentID = " + StudentID + "";
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           HPStudent.Student.ViewModel.MyBarHomePage.MyBarHomePageTable entity=  new ViewModel.MyBarHomePage.MyBarHomePageTable();
           if(dt.Rows.Count>0)
           {
               entity.StudentID = Convert.ToInt32(dt.Rows[0]["StudentID"]);
               entity.RealName = Convert.ToString(dt.Rows[0]["RealName"]);
               entity.Signature = Convert.ToString(dt.Rows[0]["Signature"]);
               entity.BarAge = Convert.ToInt32(dt.Rows[0]["BarAge"]);
               entity.BarLV = Convert.ToInt32(dt.Rows[0]["BarLV"]);
               entity.LoveStrollBar = Convert.ToString(dt.Rows[0]["LoveStrollBar"]).ToString();
               entity.AttentionBar = Convert.ToString(dt.Rows[0]["AttentionBar"]).ToString();
               entity.AttentionPost = Convert.ToString(dt.Rows[0]["AttentionPost"]).ToString();
           }
           List<HPStudent.Student.ViewModel.MyBarHomePage.BarInfoTable> list = new List<ViewModel.MyBarHomePage.BarInfoTable>();
           sql = string.Format(@"DECLARE  @LOVEID NVARCHAR(1000) ,  @BARID NVARCHAR(1000) , @POSTID NVARCHAR(1000)
                                SET @LOVEID  = (SELECT TOP 1 [VALUES]=STUFF((SELECT ','+[LoveStrollBar] FROM MyBarHomePage  t  FOR XML PATH('')), 1, 1, '')  FROM MyBarHomePage WHERE StudentID={0}) 
                                SET @BARID  = (SELECT TOP 1 [VALUES]=STUFF((SELECT ','+[AttentionBar] FROM MyBarHomePage  t  FOR XML PATH('')), 1, 1, '')  FROM MyBarHomePage WHERE StudentID={0}) 
                                SET @POSTID  = (SELECT TOP 1 [VALUES]=STUFF((SELECT ','+[AttentionPost] FROM MyBarHomePage  t  FOR XML PATH('')), 1, 1, '')  FROM MyBarHomePage WHERE StudentID={0})  
                                SELECT PBID AS ID,PBNAME AS NAME,0 AS TYPE FROM PostBarInfo WHERE PBID IN (SELECT * FROM f_splitSTR(@LOVEID,','))
                                UNION ALL SELECT PBID AS ID ,PBNAME AS NAME ,1 AS  TYPE FROM PostBarInfo WHERE PBID IN (SELECT * FROM f_splitSTR(@BARID,','))
                                UNION ALL SELECT PTID AS ID,PostThemeContent AS NAME,2 AS TYPE FROM PostTheme WHERE PTID IN (SELECT * FROM f_splitSTR(@POSTID,','))", StudentID);
           dt = null;
           dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           if (dt.Rows.Count > 0)
           {
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                    ViewModel.MyBarHomePage.BarInfoTable model = new ViewModel.MyBarHomePage.BarInfoTable();
                    model.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    model.Name = Convert.ToString(dt.Rows[i]["NAME"]);
                    model.Type = Convert.ToInt32(dt.Rows[i]["TYPE"]);
                    list.Add(model);
               }
           }
           entity.List = list;

           return entity;
       }
       

    }
}
