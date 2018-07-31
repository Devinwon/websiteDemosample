using System;
using System.Collections.Generic;

namespace HPStudent.ViewModel.Exercises
{
    public class QA_Select
    {
        public int QID { get; set; }
        public int CID { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string Answer { get; set; }

        public string AnswerAnalysis { get; set; }
        public List<HPStudent.Entity.Major> MajorList { get; set; }

    }
}
