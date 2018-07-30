using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Student
{
    public class StudentSchool
    {
        public string AreaName { get; set; } 
        public string CityName { get; set; }
        public string SchoolName { get; set; }
        public string SchoolID { get; set; }
        public string DisplayOrder { get; set; }
        public string AreaID { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }
    }
}
