using System;
using System.Collections.Generic;
using HPStudent.Entity;
using HPStudent.Student.ViewModel;
using HPStudent.Student.Data;
using StuVModel = HPStudent.Student.ViewModel;



namespace HPStudent.Student.Business
{
    public class TestPaper
    {
        public static HPStudent .Entity .TestPaper  GetTestPaperByCID(int TopCount, string CID, string StudentID)
        {

            return HPStudent.Student.Data.TestPaper.GetTestPaperByCID(TopCount, CID, StudentID);
        }
        public static StuVModel.Common.Datatable<HPStudent.Entity.TestPaper> GetStudentTestPaper(string StudentID, int start, int length)
        {
            int TotalRows = 0;
            List<HPStudent.Entity.TestPaper> SelectList = new List<Entity.TestPaper>();

            StuVModel.Common.Datatable<HPStudent.Entity.TestPaper> TestPaperTable = new StuVModel.Common.Datatable<Entity.TestPaper>();
            TestPaperTable.data = new List<Entity.TestPaper>();
            SelectList = HPStudent.Student.Data.TestPaper.GetTestPaperListBySID(StudentID, start, length, out TotalRows);
            //Data.Admin.Exercises.GetQA_SelectListByCID(CID, start, length, out TotalRows);

            //初始化返回Datatable行数
            TestPaperTable.recordsTotal = TotalRows;
            TestPaperTable.recordsFiltered = TotalRows;

            if (SelectList == null)
            {
                return null;
            }
            foreach (Entity.TestPaper item in SelectList)
            {
                HPStudent.Entity.TestPaper table = new Entity.TestPaper();
                table.TID = item.TID;
                table.Range = item.Range;
                table.IsComplete = item.IsComplete;
                table.CreateDate = item.CreateDate;
                table.EndDate = item.EndDate;
                table.Score = item.Score;
                table.SID = item.SID;
                table.StudentID = item.StudentID;

                TestPaperTable.data.Add(table);
            }
            return TestPaperTable;
        }

        public static List<Entity.QA_Select> GetTestPaperQA_SelectByTID(string TID)
        {
            return HPStudent.Student.Data.TestPaper.GetTestPaperQA_SelectByTID(TID);
        }

        public static Entity.TestPaper GetTestPaperByTID(string TID)
        {

            return HPStudent.Student.Data.TestPaper.GetTestPaperByTID(TID);
        }
        public static int SubmitDoTest(string TID, string Answer)
        {
            Answer = Answer.Replace(",|", "|");
            Entity.TestPaper testPaper = HPStudent.Student.Data.TestPaper.GetTestPaperByTID(TID);
            float Sroce = 0;
            string[] RightItems = testPaper.SelectRightItem.Split('|');
            string[] AnswerItems = Answer.Split('|');
            for (int i = 0; i < AnswerItems.Length; i++)
            {
                string myAnswer = AnswerItems[i].ToString().Replace(",", "");
                if (myAnswer == RightItems[i].ToString())
                {
                    Sroce = Sroce + 10;
                }
            }
            int k = HPStudent.Student.Data.TestPaper.SubmitDoTest(TID, Sroce, Answer);
            return k;
        }
    }
}
