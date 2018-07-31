using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HPStudent.Business.Admin
{
    public class VisitorLog 
    {
        public static DataTable GetVisitorList()
        {
           return Data.Admin.VisitorLog.GetVisitorList();
        }

    }
}
