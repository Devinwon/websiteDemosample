using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
   public class PostBarInfo
    {

       public static string GetPTCName(int PBCID) 
       {
           string sql = "SELECT PBCName FROM  PostBarClassify WHERE PBCID=" + PBCID + "";
           return Convert.ToString(SqlHelper.ExecuteScalar(CommandType.Text,sql));
       
       }

       public static List<HPStudent.Entity.PostBarInfo> BindPostBar(int PBCID) 
       {
        string sql = @"SELECT * FROM PostBarInfo WHERE PBCID="+PBCID+" ORDER BY Attention DESC";
           List<HPStudent.Entity.PostBarInfo> list = null;
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           if (dt != null)
           {
               list = new List<HPStudent.Entity.PostBarInfo>();
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   HPStudent.Entity.PostBarInfo table = new HPStudent.Entity.PostBarInfo();
                   table.PBID = Convert.ToInt32(dt.Rows[i]["PBID"]);
                   table.PBName = Convert.ToString(dt.Rows[i]["PBName"]);
                   table.PBHeadPortrait = Convert.ToString(dt.Rows[i]["PBHeadPortrait"]);
                   table.Attention = Convert.ToInt32(dt.Rows[i]["Attention"]);
                   table.PostNumber = Convert.ToInt32(dt.Rows[i]["PostNumber"]);
                   table.DiscussTopic = Convert.ToString(dt.Rows[i]["DiscussTopic"]);
                   list.Add(table);
               }

               return list;
           }
           list = new List<HPStudent.Entity.PostBarInfo>();
           return list;
       
       }

    }
}
