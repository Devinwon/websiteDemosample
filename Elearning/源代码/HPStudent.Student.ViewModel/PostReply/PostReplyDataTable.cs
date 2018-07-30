using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.PostReply
{
   public class PostReplyDataTable<T>
    {
       public int PTID { get; set; }
       public int PostManID { get; set; }

       public string PostManName { get; set; }
       public string PostThemeContent { get; set; }

       public string PostContent { get; set; }

       public DateTime PostDate { get; set; }

       public int recordsTotal { get; set; }

       public int IsCollect { get; set; }

       public int LoginStudentID { get; set; }

       public List<T> data { get; set; }
    }
}
