using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Service
{
    public class SuggestDetailList:HPStudent.Entity.Suggestions
    {
        public List<HPStudent.Entity.SuggestItem> SuggestItemList { get; set; }

        public SuggestDetailList(HPStudent.Entity.Suggestions mySuggest)
        {
            SID = mySuggest.SID;
            SchoolID = mySuggest.SchoolID;
            StudentID = mySuggest.StudentID;
            Status = mySuggest.Status;
            Category = mySuggest.Category;
            IsSuggest = mySuggest.IsSuggest;
            PostDate = mySuggest.PostDate;
            Title = mySuggest.Title;
            Content = mySuggest.Content;
            SuggestImg = mySuggest.SuggestImg;
            sResult = mySuggest.sResult;
            TeacherID = mySuggest.TeacherID;
        }
    }
}
