using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.ResumeInbox
{
   public class ResumeInboxTable
    {

       public int ID { get; set; }


       /// <summary>
       /// 发送的ID
       /// </summary>
       public int SenderID { get; set; }
       /// <summary>
       /// 姓名
       /// </summary>
       public string RealName { get; set; }


       /// <summary>
       /// 应聘职位
       /// </summary>
       public string Name { get; set; }
       /// <summary>
       /// 性别
       /// </summary>
       public string Sex { get; set; }

       /// <summary>
       /// 年龄
       /// </summary>
       public int Age { get; set; }

       /// <summary>
       /// 学历
       /// </summary>
       public string Education { get; set; }

       /// <summary>
       /// 提交时间
       /// </summary>
       public DateTime SendDate { get; set; }

       /// <summary>
       /// 操作
       /// </summary>
       public string Operation { get; set; }
    }
}
