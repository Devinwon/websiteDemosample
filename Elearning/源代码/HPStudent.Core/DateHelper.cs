using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Core
{
    public class DateHelper
    {
        public static DateTime  ToDate(string strDate)
        { 
            DateTime myDate = new DateTime();
            DateTime.TryParse(strDate, out myDate);
            return myDate;
        }
    }
}
