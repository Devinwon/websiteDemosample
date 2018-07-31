using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.ViewModel.Service
{
    public class Suggest:Entity.Suggestions
    {
        public string SchoolName { get; set; }

        public string StudentName { get; set; }

        public string ClassName { get; set; }
    }
}
