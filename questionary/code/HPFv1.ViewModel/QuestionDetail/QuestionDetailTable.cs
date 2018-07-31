using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPFv1.ViewModel.QuestionDetail
{
   public class QuestionDetailTable
    {
       


       /// <summary>
       /// 问卷ID
       /// </summary>
       public long QID { get; set; }

       /// <summary>
       /// 分组ID
       /// </summary>
       public long GID { get; set; }


       /// <summary>
       /// 问卷HTML
       /// </summary>
       public string QuestionHtml { get; set; }


       /// <summary>
       /// 分组密码
       /// </summary>
       public string Password { get; set; }

       /// <summary>
       /// 是否已生成结果
       /// </summary>
       public int IsResult { get; set; }

       /// <summary>
       /// 是否已生成问卷
       /// </summary>
       public int IsCraete { get; set; }
    }
}
