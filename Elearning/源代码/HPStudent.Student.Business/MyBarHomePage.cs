using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
   public class MyBarHomePage
    {
       public static HPStudent.Student.ViewModel.MyBarHomePage.MyBarHomePageTable GetMyBarHomePageTable(int StudentID) 
       {
           return HPStudent.Student.Data.MyBarHomePage.GetMyBarHomePageTable(StudentID);
       }


    }
}
