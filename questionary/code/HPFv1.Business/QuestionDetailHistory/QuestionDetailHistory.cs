using HPFv1.ViewModel.Common;
using HPFv1.ViewModel.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPFv1.Business.QuestionDetailHistory
{
   public class QuestionDetailHistory
    {
       public static Datatable<HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable> GetQuestionDetailHistoryTable(int start, int length,int QID ,int GID, string startTime, string endTime)
       {
           int TotalRows = 0;
           List<HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable> list = new List<HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable>();
           Datatable<HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable> questionList = new Datatable<HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable>();
           questionList.data = new List<HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable>();

           list = HPFv1.Data.DAL_QuestionDetailHistory.GetQuestionDetailHistoryTable(start, length, QID, GID, startTime, endTime, out TotalRows);

           questionList.recordsFiltered = TotalRows;
           questionList.recordsTotal = TotalRows;

           foreach (HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable item in list)
           {
               StringBuilder sb = new StringBuilder();
               HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable table = new HPFv1.ViewModel.QuestionDetailHistory.QuestionDetailHistoryTable();
               table.GID = item.GID;
               table.QID = item.QID;
               table.DID = item.DID;
               table.Title = item.Title;
               table.GroupName = item.GroupName;
               table.Answer = item.Answer;
               table.PostDate = item.PostDate;
               table.PostIP = item.PostIP;
               table.Operation = "<button class=\"btn btn-primary btn-sm\" data-qid=\"" + item.QID + "\"  data-id=\"" + item.DID + "\" data-action=\"edit\">"
                 + "<span class=\"fa fa-comments\"></span>查看详细</button> ";

               questionList.data.Add(table);
           }

           return questionList;

       }

    }
}
