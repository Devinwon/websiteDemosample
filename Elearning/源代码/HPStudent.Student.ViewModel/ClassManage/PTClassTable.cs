using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.ClassManage
{
   public class PTClassTable
    {
        public int PTCID { get; set; }// 
        public string PTCName { get; set; }// 
        public string StudentID { get; set; }// 
        public string TeacherID { get; set; }// 
        public string StudentName { get; set; }

        public List<HPStudent.Entity.StudentInfo> StudentInfoList { get; set; }
    }
}
