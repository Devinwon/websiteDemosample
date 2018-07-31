using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
   public class ResumeInbox
    {
       public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable> GetResumeInboxTable(string EID, int start, int length) 
       {

           int TotalRows = 0;
           List<HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable> list = new List<HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable>();
           HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable> resumeIndexList = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable>();
           resumeIndexList.data = new List<HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable>();

           list = HPStudent.Student.Data.ResumeInbox.GetResumeInboxTable(start, length, EID, out TotalRows);

           resumeIndexList.recordsFiltered = TotalRows;
           resumeIndexList.recordsTotal = TotalRows;

           foreach (HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable item in list)
           {
               
               HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable table = new HPStudent.Student.ViewModel.ResumeInbox.ResumeInboxTable();
               table.RealName = item.RealName;
               table.Sex = item.Sex;
               table.Age = item.Age;
               table.Education = item.Education;
               table.SendDate = item.SendDate;
               table.Name = item.Name;
               table.Operation = "<button class=\"btn btn-primary btn-sm\" data-id=\"" + item.SenderID + "\" data-action=\"seeResume\">"
                + "<span class=\"fa fa-pencil\"></span>查看简历</button> "
                + "<button class=\"btn btn-primary btn-sm \" type=\"button\" data-id=\"" + item.SenderID + "\" data-name=\"" + item.RealName + "\" data-action=\"senderInvite\">"
                + "<span class=\"fa fa-pencil\"></span> 发送面试邀请</button> ";
               resumeIndexList.data.Add(table);
            
           }

           return resumeIndexList;

       }

    }
}
