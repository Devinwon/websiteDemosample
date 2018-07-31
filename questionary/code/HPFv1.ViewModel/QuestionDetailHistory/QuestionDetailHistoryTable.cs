using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPFv1.ViewModel.QuestionDetailHistory
{
   public class QuestionDetailHistoryTable
    {

       /// <summary>
       /// 详情ID
       /// </summary>
       public int DID { get; set; }
       /// <summary>
       /// 分组ID
       /// </summary>
       public int GID { get; set; }

       /// <summary>
       /// 问卷ID
       /// </summary>
       public int QID { get; set; }
       /// <summary>
       /// 问卷标题
       /// </summary>
       public string Title { get; set; }

       /// <summary>
       /// 分组名称
       /// </summary>
       public string GroupName { get; set; }

       /// <summary>
       /// 问卷答案（Json字符串）
       /// </summary>
       public string Answer { get; set; }

       /// <summary>
       /// 问卷提交日期
       /// </summary>
       public DateTime PostDate { get; set; }

       /// <summary>
       /// 提交IP地址
       /// </summary>
       public string PostIP { get; set; }


       /// <summary>
       /// 操作
       /// </summary>
       public string Operation { get; set; }
    }
}
