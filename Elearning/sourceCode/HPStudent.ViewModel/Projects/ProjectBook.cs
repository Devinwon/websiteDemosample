using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.ViewModel.Projects
{
    public class ProjectBook
    {
        public int PID { get; set; }
        public int MID { get; set; }
        public string ProjectName { get; set; }
        public string TeacherName { get; set; }
        public int TeacherID { get; set; }
        public List<Entity.TeacherInfo> TeacherList { get; set; }
        public int selTeacherID { get; set; }

        public string UseTechnology { get; set; }
        public string ProjectPic { get; set; }
        public string ProjectDesc { get;set; }
        public int ClassHour { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }

        public int ShowPart { get; set; }
    }
}
