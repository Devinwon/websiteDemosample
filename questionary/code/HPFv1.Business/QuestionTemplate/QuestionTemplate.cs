using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPFv1.Business.QuestionTemplate
{
   public static class QuestionTemplate
    {
       public static HPFv1.ViewModel.Common.Datatable<HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable> GetQuestionTemplateTable(int start, int length,int UID)
       {
           int TotalRows = 0;
           HPFv1.ViewModel.Common.Datatable<HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable> qtt = new ViewModel.Common.Datatable<HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable>();
           List<HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable> list = new List<HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable>();
           qtt.data = new List<HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable>();
           list = HPFv1.Data.DAL_QuestionTemplate.GetQuestionTemplateTable(start, length, UID, out TotalRows);
           qtt.recordsFiltered = TotalRows;
           qtt.recordsTotal = TotalRows;
           foreach (HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable item in list)
           {
               HPFv1.ViewModel.QuestionTemplate.QuestionTemplateTable table = new ViewModel.QuestionTemplate.QuestionTemplateTable();
               table.TID = item.TID;
               table.UName = item.UName;
               table.EditName = item.EditName;
               table.Title = item.Title;
               table.Content = item.Content;
               table.CreateDate = item.CreateDate;
               table.EditDate = item.EditDate;
               table.Operation = "<button class=\"btn btn-primary btn-sm\" data-id=\"" + item.TID + "\" data-action=\"edit\">"
               + "<span class=\"fa fa-pencil\"></span>编辑</button> "
               + "<button class=\"btn btn-primary btn-sm \"  type=\"button\" data-id=\"" + item.TID + "\">"
               + "<span class=\"fa fa-times\"></span> 删除</button>";
               qtt.data.Add(table);

           }
           return qtt;
       }


       public static int AddTemplate(HPFv1.Entity.QuestionTemplate temp) 
       {
           return Data.DAL_QuestionTemplate.Add(temp);
       }
    }
}
