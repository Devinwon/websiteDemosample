using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
   public class PostTheme
    {
      
       public static HPStudent.Student.ViewModel.PostTheme.PostBarInfoTable GetModel(int PBID, int StudentID) 
       {
           return HPStudent.Student.Data.PostTheme.GetModel(PBID, StudentID);
       
       }

       public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.PostTheme.PostThemeTable> BindPostTheme(int start, int length, int PBID) 
       {
           return HPStudent.Student.Data.PostTheme.BindPostTheme(start, length, PBID);
       
       }

       public static int Attention(int PBID,int StudentID) 
       {
           return HPStudent.Student.Data.PostTheme.Attention(PBID, StudentID);
       
       }

       public static int CancelAttention(int PBID, int StudentID) 
       {
           return HPStudent.Student.Data.PostTheme.CancelAttention(PBID, StudentID);
       
       }

       public static int PostReplyAdd(int PBID, string PostThemeContent, string PostContent, int StudentID) 
       {
           return HPStudent.Student.Data.PostTheme.PostThemeAdd(PBID, PostThemeContent, PostContent, StudentID);
       }
    }
}
