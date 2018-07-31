using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
   public class InterviewInvitation
    {
       public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> GetInterviewInvitationTable(string SID, int start, int length)
       {

           int TotalRows = 0;
           List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> list = new List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable>();
           HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> resumeIndexList = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable>();
           resumeIndexList.data = new List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable>();

           list = HPStudent.Student.Data.InterviewInvitation.GetInterviewInvitationTable(start, length, SID, out TotalRows);

           resumeIndexList.recordsFiltered = TotalRows;
           resumeIndexList.recordsTotal = TotalRows;

           foreach (HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable item in list)
           {

               HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable table = new HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable();
               table.CompanyName = item.CompanyName;
               table.Address = item.Address;
               table.TelPhone = item.TelPhone;
               table.Email = item.Email;
               table.WebSite = item.WebSite;
               table.Content = item.Content;
               table.SendDate = item.SendDate;
               table.Name = item.Name;
               table.City = item.City;
               table.SalaryRange = item.SalaryRange;
               table.WorkType = item.WorkType;
               table.DegreeRequired = item.DegreeRequired;
               table.ExperienceRequired = item.ExperienceRequired;
               table.JobDescription = item.JobDescription;
               table.Operation = "<button class=\"btn btn-primary btn-sm\" data-id=\"" + item.ID + "\" data-action=\"seeResume\">"
                + "<span class=\"fa fa-pencil\"></span>查看详细</button> ";
                
               resumeIndexList.data.Add(table);

           }

           return resumeIndexList;

       }
       public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> GetEnterpriseDetailBind(string SID,string IID)
       {

           List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> list = new List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable>();
           HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable> resumeIndexList = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable>();
           resumeIndexList.data = new List<HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable>();

           list = HPStudent.Student.Data.InterviewInvitation.GetEnterpriseDetailBind(SID, IID);

           foreach (HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable item in list)
           {

               HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable table = new HPStudent.Student.ViewModel.InterviewInvitation.InterviewInvitationTable();
               table.CompanyName = item.CompanyName;
               table.Address = item.Address;
               table.TelPhone = item.TelPhone;
               table.Email = item.Email;
               table.WebSite = item.WebSite;
               table.Content = item.Content;
               table.SendDate = item.SendDate;
               table.Name = item.Name;
               table.City = item.City;
               table.SalaryRange = item.SalaryRange;
               table.WorkType = item.WorkType;
               table.DegreeRequired = item.DegreeRequired;
               table.ExperienceRequired = item.ExperienceRequired;
               table.JobDescription = item.JobDescription;
               resumeIndexList.data.Add(table);

           }

           return resumeIndexList;

       }
       #region 获取企业发布简历信息
       public static List<HPStudent.Entity.InterviewInvitation> getInvitationList(int SenderID, int ReceiverID)
       {
           return HPStudent.Student.Data.InterviewInvitation.GetList("SenderID='" + SenderID + "' and SID='" + ReceiverID + "' order by ID desc ");

       }


       #endregion
    }
}
