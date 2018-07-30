using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.MyBarHomePage
{
   public class MyBarHomePageTable : HPStudent.Entity.MyBarHomePage
    {
       /// <summary>
       /// 足迹，关注贴吧和帖子
       /// </summary>
      public List<HPStudent.Student.ViewModel.MyBarHomePage.BarInfoTable> List { get; set; }

      public string RealName { get; set; }
    }
}
