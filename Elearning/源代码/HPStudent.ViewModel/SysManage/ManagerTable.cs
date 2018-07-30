using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.ViewModel.SysManage
{
    public class ManagerTable
    {
        public int MID { get; set; }

        public string ManagerName { get; set; }

        public string Level { get; set; }

        public string LastLoginTime { get; set; }

        public string Operation { get; set; }
    }
}
