using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
   public  class ResumeInbox
    {
       public static List<HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable> GetResumeInboxTable(int start,int length, string EID, out int TotalRows) 
       {
           string countSql = "select count(*) from SendResume where EID=" + EID + " ";

           string sql = string.Format(" select top {0} a.*, b.RealName, a.SendDate,IsRead, b.Sex,b.Brithday,d.Education,c.Name,c.SalaryRange from SendResume a inner join StudentInfo b on a.SenderID = b.StudentID inner join JobTittle c on a.JobTitleID = c.JID inner join StudentBaseInfo d on a.SenderID = d.StudentID  where  EID = {1} and ID not in (select top {2} ID from SendResume   order by id desc)  order by id desc", length, EID, start);

           TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql);
           List<HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable> list = null;
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           if (dt != null)
           {
               list = new List<HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable>();
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable table = new HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable();
                   table.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                   table.SenderID = Convert.ToInt32(dt.Rows[i]["SenderID"]);
                   table.RealName = dt.Rows[i]["RealName"].ToString();
                   table.Sex = Convert.ToInt32(dt.Rows[i]["Sex"])==1?"男":"女";
                   table.Age = DateTime.Now.Year - Convert.ToDateTime(dt.Rows[i]["Brithday"]).Year;
                   table.Education = Convert.ToInt32(dt.Rows[i]["Education"]) == 0 ? "其他" : Convert.ToInt32(dt.Rows[i]["Education"]) == 1?"大专":Convert.ToInt32(dt.Rows[i]["Education"]) == 2?"本科":Convert.ToInt32(dt.Rows[i]["Education"]) == 3?"研究生":"博士";
                   table.SendDate = Convert.ToDateTime(dt.Rows[i]["SendDate"]);
                   table.Name = dt.Rows[i]["Name"].ToString();
                   list.Add(table);
               }

               return list;
           }
           list = new List<HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable>();
           return list;
       }


    }
}
