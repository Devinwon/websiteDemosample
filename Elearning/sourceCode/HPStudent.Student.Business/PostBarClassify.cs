using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
   public class PostBarClassify
    {
       public static HPStudent.Student.ViewModel.Common.GridView<HPStudent.Student.ViewModel.Project.MainProject> BindPostBarClassify() 
       {
           int TotalRecords = 0;
           List<HPStudent.Entity.PostBarClassify> list = new List<HPStudent.Entity.PostBarClassify>();

           HPStudent.Student.ViewModel.Common.GridView<HPStudent.Student.ViewModel.Project.MainProject> ProjectGridview = new HPStudent.Student.ViewModel.Common.GridView<HPStudent.Student.ViewModel.Project.MainProject>();
           ProjectGridview.data = new List<HPStudent.Student.ViewModel.Project.MainProject>();
           list = HPStudent.Student.Data.PostBarClassify.BindPostBarClassify(out TotalRecords);
           //初始化返回Datatable行数
           ProjectGridview.recordsTotal = TotalRecords;
           if (list == null)
           {
               return null;
           }
           foreach (HPStudent.Entity.PostBarClassify item in list)
           {
               HPStudent.Student.ViewModel.Project.MainProject table = new HPStudent.Student.ViewModel.Project.MainProject();
               table.yoyotui_url = "PostBarInfo/Index?PBCID=" + item.PBCID.ToString();
               table.yoyotui_img = item.PBCPic;
               table.yoyotui_title = item.PBCName;

               ProjectGridview.data.Add(table);
           }
           return ProjectGridview;
       
       }


       public static List<HPStudent.Entity.PostBarInfo> BindHotPostBar()
       {
           return HPStudent.Student.Data.PostBarClassify.BindHotPostBar();
       }


    }
}
