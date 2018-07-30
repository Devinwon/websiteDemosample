using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Student
{
    public class InterviewInvitationBase : HPStudent.Entity.InterviewInvitation
    {
        public string Receiver { get; set; }//接受者姓名
    }
}
