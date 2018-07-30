using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.PostTheme
{
   public class PostBarInfoTable : HPStudent.Entity.PostBarInfo
    {
       /// <summary>
       /// 是否关注 0：否 1：是
       /// </summary>
        public int AttentionStatus { get; set; }//是否关注
    }
}
