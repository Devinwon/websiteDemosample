using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
   public class PostBarClassify
    {
       public static List<HPStudent.Entity.PostBarClassify> BindPostBarClassify(out int TotalRecords) 
       {
           string countSql = @"SELECT  COUNT(*) FROM PostBarClassify";

           string sql = string.Format(@"SELECT * FROM PostBarClassify ");

           TotalRecords = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql);
           List<HPStudent.Entity.PostBarClassify> list = null;
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           if (dt != null)
           {
               list = new List<HPStudent.Entity.PostBarClassify>();
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   HPStudent.Entity.PostBarClassify table = new HPStudent.Entity.PostBarClassify();
                   table.PBCID = Convert.ToInt32(dt.Rows[i]["PBCID"]);
                   table.PBCName = Convert.ToString(dt.Rows[i]["PBCName"]);
                   table.PBCPic = Convert.ToString(dt.Rows[i]["PBCPic"]);
                   list.Add(table);
               }

               return list;
           }
           list = new List<HPStudent.Entity.PostBarClassify>();
           return list;
       }

       public static List<HPStudent.Entity.PostBarInfo> BindHotPostBar() 
       {
           string sql = string.Format(@"SELECT TOP 8 * FROM PostBarInfo ORDER BY Attention DESC");
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
