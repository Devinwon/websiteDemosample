using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
   public class PostBarInfo
    {
       public static string GetPTCName(int PTCID) 
       {

           return HPStudent.Student.Data.PostBarInfo.GetPTCName(PTCID);
       
       }


       public static List<HPStudent.Entity.PostBarInfo> BindPostBar(int PBCID) 
       {

           return HPStudent.Student.Data.PostBarInfo.BindPostBar(PBCID);
       }

    }
}
