using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.MyBarHomePage
{
   public class BarInfoTable
    {
       public int ID { get; set; }

       public string Name { get; set; }

       /// <summary>
       /// 0:爱逛的吧 1：关注的吧 2 :关注的帖子 
       /// </summary>
       public int Type { get; set; }
    }
}
