using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.PostTheme
{
   public class PostThemeTable : HPStudent.Entity.PostTheme
    {
       public string PostManName { get; set; }//发帖人
       public int PRCount { get; set; }//回帖数量

       public int BarLV { get; set; }//发帖人等级

       public int ReplyManID { get; set; }//最后回复人ID

       public string ReplyManName { get; set; }//最后回复人

       public string LastReplyDate { get; set; } //最后回复时间

       

    }
}
