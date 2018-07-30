using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Project
{
    public class DetailProject :HPStudent.Entity.ProjectBook
    {
        public string TeacherName { get; set; }
        public List<HPStudent.Entity.ProjectItem> ProjectItemList { get; set; }

    }
}
