using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
   public class PostReply
    {
       public static HPStudent.Student.ViewModel.PostTheme.PostBarInfoTable GetModel(int PTID)
       {
           return HPStudent.Student.Data.PostReply.GetModel(PTID);

       }

       public static HPStudent.Student.ViewModel.PostReply.PostReplyDataTable<HPStudent.Student.ViewModel.PostReply.PostReplyTable> BindPostReplyTable(int start, int length, int PTID, int showType, int StudentID)
       {
           return HPStudent.Student.Data.PostReply.BindPostReplyTable(start, length, PTID, showType, StudentID);

       }

       public static int Collect(int PTID,int StudentID) 
       {
           return HPStudent.Student.Data.PostReply.Collect(PTID,StudentID);
       }

       public static int FloorReplyAdd(int PRID, string FloorReplyContent, int StudentID, int ByReplyManID) 
       {
           return HPStudent.Student.Data.PostReply.FloorReplyAdd(PRID, FloorReplyContent, StudentID, ByReplyManID);
       }

       public static HPStudent.Student.ViewModel.PostReply.PostReplyTable GetFloorReplyTable(int PRID) 
       {

           return HPStudent.Student.Data.PostReply.GetFloorReplyTable(PRID);

       }

       public static HPStudent.Student.ViewModel.PostReply.PostReplyTable FloorReplyDel(int FRID, int PRID) 
       {
           return HPStudent.Student.Data.PostReply.FloorReplyDel(FRID, PRID);
       
       }

       public static HPStudent.Student.ViewModel.PostReply.PostReplyTable ReplyPagerChangeGetTable(int start, int length, int PRID) 
       {
           return HPStudent.Student.Data.PostReply.ReplyPagerChangeGetTable(start, length, PRID);
       }


       public static int PostReplyAdd(int PTID, string FRContent, int StudentID, string RealName) 
       {
           return HPStudent.Student.Data.PostReply.PostReplyAdd(PTID, FRContent, StudentID, RealName);
       }

       public static int MessageReportAdd(HPStudent.Entity.MessageReport entity) 
       {
           return HPStudent.Student.Data.PostReply.MessageReportAdd(entity);
       
       
       }

    }
}
