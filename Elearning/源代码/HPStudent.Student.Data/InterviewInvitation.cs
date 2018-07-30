using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
   public class InterviewInvitation
    {
       public static List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> GetInterviewInvitationTable(int start, int length, string SID, out int TotalRows)
       {
           
           string  countSql = "select count(*) from InterviewInvitation where SID=" + SID + "";
           string  sql = string.Format(" select top {0} a.ID,b.CompanyName,b.Address,b.TelPhone,b.Email,b.WebSite,a.Content,a.SendDate,c.Name,c.City,c.SalaryRange,WorkType,c.DegreeRequired,c.ExperienceRequired,c.JobDescription from InterviewInvitation a inner join CompanyInfo b on a.SenderID = b.SID inner join JobTittle c on a.JobTitleID = c.JID where a.SID = {1} and ID not in (select top {2} ID from SendResume   order by id desc)  order by id desc", length, SID, start);
           TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql);
           List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> list = null;
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           if (dt != null)
           {
               list = new List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable>();
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable table = new HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable();
                   table.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                   table.CompanyName = dt.Rows[i]["CompanyName"].ToString();
                   table.Address = dt.Rows[i]["Address"].ToString();
                   table.TelPhone = dt.Rows[i]["TelPhone"].ToString();
                   table.Email = dt.Rows[i]["Email"].ToString();
                   table.WebSite = dt.Rows[i]["WebSite"].ToString();
                   table.Content = dt.Rows[i]["Content"].ToString();
                   table.SendDate = Convert.ToDateTime(dt.Rows[i]["SendDate"]);
                   table.Name = dt.Rows[i]["Name"].ToString();
                   table.City = dt.Rows[i]["City"].ToString();
                   table.SalaryRange = dt.Rows[i]["SalaryRange"].ToString();
                   table.WorkType = dt.Rows[i]["WorkType"].ToString();
                   table.DegreeRequired = dt.Rows[i]["DegreeRequired"].ToString();
                   table.ExperienceRequired = dt.Rows[i]["ExperienceRequired"].ToString();
                   table.JobDescription = dt.Rows[i]["JobDescription"].ToString();
                   list.Add(table);
               }

               return list;
           }
           list = new List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable>();
           return list;
       }

       public static List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> GetEnterpriseDetailBind(string SID, string IID) 
       {
           string sql = string.Format(" select a.ID,b.CompanyName,b.Address,b.TelPhone,b.Email,b.WebSite,a.Content,a.SendDate,c.Name,c.City,c.SalaryRange,WorkType,c.DegreeRequired,c.ExperienceRequired,c.JobDescription from InterviewInvitation a inner join CompanyInfo b on a.SenderID = b.SID inner join JobTittle c on a.JobTitleID = c.JID where a.SID = {0} and ID = {1} order by id desc",SID,IID);

           List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> list = null;
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           if (dt != null)
           {
               list = new List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable>();
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable table = new HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable();
                   table.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                   table.CompanyName = dt.Rows[i]["CompanyName"].ToString();
                   table.Address = dt.Rows[i]["Address"].ToString();
                   table.TelPhone = dt.Rows[i]["TelPhone"].ToString();
                   table.Email = dt.Rows[i]["Email"].ToString();
                   table.WebSite = dt.Rows[i]["WebSite"].ToString();
                   table.Content = dt.Rows[i]["Content"].ToString();
                   table.SendDate = Convert.ToDateTime(dt.Rows[i]["SendDate"]);
                   table.Name = dt.Rows[i]["Name"].ToString();
                   table.City = dt.Rows[i]["City"].ToString();
                   table.SalaryRange = dt.Rows[i]["SalaryRange"].ToString();
                   table.WorkType = dt.Rows[i]["WorkType"].ToString();
                   table.DegreeRequired = dt.Rows[i]["DegreeRequired"].ToString();
                   table.ExperienceRequired = dt.Rows[i]["ExperienceRequired"].ToString();
                   table.JobDescription = dt.Rows[i]["JobDescription"].ToString();
                   list.Add(table);
               }

               return list;
           }
           list = new List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable>();
           return list;
       }


       #region 获取企业发送的信息集合
       /// <summary>
       /// 获得数据列表
       /// </summary>
       public static List<HPStudent.Entity.InterviewInvitation> GetList(string strWhere)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select * from InterviewInvitation ");
           if (strWhere.Trim() != "")
           {
               strSql.Append(" where " + strWhere);
           }

           DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString());

           List<HPStudent.Entity.InterviewInvitation> list = new List<HPStudent.Entity.InterviewInvitation>();
           if (ds != null && ds.Tables[0].Rows.Count > 0)
           {
               foreach (DataRow item in ds.Tables[0].Rows)
               {
                   HPStudent.Entity.InterviewInvitation objTemp = new HPStudent.Entity.InterviewInvitation();
                   objTemp.ID = Convert.ToInt32(item["ID"]);
                   objTemp.ID = Convert.ToInt32(item["SID"]);
                   objTemp.SenderID = Convert.ToInt32(item["SenderID"]);
                   objTemp.JobTitleID = Convert.ToInt32(item["JobTitleID"]);
                   objTemp.Content = item["Content"].ToString();
                   objTemp.IsRead = item["IsRead"].ToString();
                   objTemp.SendDate = Convert.ToDateTime(item["SendDate"]);
                   list.Add(objTemp);
               }

           }
           return list;


       }

       #endregion
    }
}
