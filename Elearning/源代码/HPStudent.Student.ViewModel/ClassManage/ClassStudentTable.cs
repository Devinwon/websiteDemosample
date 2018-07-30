using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.ClassManage
{
   public class ClassStudentTable
    {
       public int StudentID { get; set; }

       public string RealName { get; set; }

       public int Sex { get; set; }

       public string LastLoginTime { get; set; }

       public int ResumeStatus { get; set; }


       public string Operation { get; set; }
    }
}
