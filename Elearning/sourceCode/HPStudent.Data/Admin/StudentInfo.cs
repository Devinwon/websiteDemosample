using System;
using System.Data;
using HPStudent.Entity;
using HPStudent.ViewModel;
using HPStudent.Data;
using System.Collections.Generic;

namespace HPStudent.Business.Admin
{
    public class StudentInfo
    {
        public static HPStudent.ViewModel.Common.Datatable<ViewModel.Student.StudentInfo> GetStudentList(string SchoolID, string CID, string StartYear, string RealName, int start, int length)
        {
            int TotalRows = 0;
            List<HPStudent.ViewModel.Student.StudentInfo> SelectList = new List<ViewModel.Student.StudentInfo>();

            HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Student.StudentInfo> SchoolTable = new ViewModel.Common.Datatable<HPStudent.ViewModel.Student.StudentInfo>();
            SchoolTable.data = new List<HPStudent.ViewModel.Student.StudentInfo>();


            SelectList = Data.Admin.StudentInfo.GetStudentList(SchoolID, CID, StartYear, RealName, start, length, out TotalRows);
         
            //初始化返回Datatable
            SchoolTable.recordsTotal = TotalRows;
            SchoolTable.recordsFiltered = TotalRows;

            foreach (HPStudent.ViewModel.Student.StudentInfo item in SelectList)
            {
                HPStudent.ViewModel.Student.StudentInfo table = new ViewModel.Student.StudentInfo();
                table.SchoolID = item.SchoolID;
                table.SchoolName = item.SchoolName;
                table.CID = item.CID;
                table.CName = item.CName;
                table.Credits = item.Credits;
                table.Avatar = item.Avatar;
                table.IsActivated = item.IsActivated;
                table.PaidFee = item.PaidFee;
                table.RealName = item.RealName;
                table.Sex = item.Sex;
                table.StudentID = item.StudentID;

                SchoolTable.data.Add(table);
            }
            return SchoolTable;
        }
        public static ViewModel.Student.StudentInfo GetStudentByID(string StudentID)
        {
            return Data.Admin.StudentInfo.GetStudentByID(StudentID);
        }

        public static int UpdateBaseInfo(string MajorID, string CID, string StudentID)
        {
            return Data.Admin.StudentInfo.UpdateBaseInfo(MajorID, CID, StudentID);
        }

        public static int UpdateStudentInfo(string RealName, string Brithday, string StartYear, string Sex, string StudentID, string UserRole)
        {
            return Data.Admin.StudentInfo.UpdateStudentInfo(RealName, Brithday, StartYear, Sex, StudentID, UserRole);
        }
        public static HPStudent.ViewModel.Common.Datatable<Entity.StudentScore> GetStudentScore(string SID)
        {
            int TotalRows = 0;
            List<HPStudent.ViewModel.Student.StudentInfo> SelectList = new List<ViewModel.Student.StudentInfo>();

            HPStudent.ViewModel.Common.Datatable<HPStudent.Entity.StudentScore> ScoreTable = new ViewModel.Common.Datatable<StudentScore>();
            ScoreTable.data = new List<HPStudent.Entity.StudentScore>();

            DataSet ds = Data.Admin.StudentInfo.GetStudentScore(SID);
            //  SelectList = Data.Admin.StudentInfo.GetStudentList(SchoolID, CID, StartYear, RealName);

            //初始化返回Datatable
            ScoreTable.recordsTotal = TotalRows;
            ScoreTable.recordsFiltered = TotalRows;

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                HPStudent.Entity.StudentScore table = new StudentScore();

                table.SID = Convert.ToInt32(item["SID"].ToString());
                table.Term = Convert.ToInt32(item["Term"].ToString());
                table.Examination1 = float.Parse(item["Examination1"].ToString());
                table.Examination2 = float.Parse(item["Examination2"].ToString());
                table.Evaluate = item["Evaluate"].ToString();
                ScoreTable.data.Add(table);
            }
            return ScoreTable;
            //List<Entity.StudentScore> ScoreList = new List<Entity.StudentScore>();
            //DataSet ds = Data.Admin.StudentInfo.GetStudentScore(SID);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow item in ds.Tables[0].Rows)
            //    {
            //        Entity.StudentScore score = new StudentScore();
            //        score.SID = Convert.ToInt32(item["SID"].ToString());
            //        score.Term = Convert.ToInt32(item["Term"].ToString());
            //        score.Examination1 = float.Parse(item["Examination1"].ToString());
            //        score.Examination2 = float.Parse(item["Examination2"].ToString());
            //        score.Evaluate = item["Evaluate"].ToString();
            //        ScoreList.Add(score);
            //    }
            //}
            //return ScoreList;
        }

        public static Entity.StudentScore GetStudentScore(string SID, string Term)
        {
            Entity.StudentScore Score = new Entity.StudentScore();
            DataSet ds = Data.Admin.StudentInfo.GetStudentScore(SID, Term);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow item = ds.Tables[0].Rows[0];

                Score.SID = Convert.ToInt32(item["SID"].ToString());
                Score.Term = Convert.ToInt32(item["Term"].ToString());
                Score.Examination1 = float.Parse(item["Examination1"].ToString());
                Score.Examination2 = float.Parse(item["Examination2"].ToString());
                Score.Evaluate = item["Evaluate"].ToString();


            }
            return Score;
        }

        public static int AddMark(Entity.StudentScore Score)
        {
            return Data.Admin.StudentInfo.AddMark(Score);
        }

        public static int EditMark(Entity.StudentScore Score)
        {
            return Data.Admin.StudentInfo.EditMark(Score);
        }

        public static int DeleteMark(string StudentID, string Term)
        {
            return Data.Admin.StudentInfo.DeleteMark(StudentID, Term);
        }

        public static object[] StudentInfoImport(DataSet ds, string[] SheetNames, string SchoolID, string Year)
        {
            return Data.Admin.StudentInfo.StudentInfoImport(ds, SheetNames, SchoolID, Year);
        }

        public static HPStudent.ViewModel.Common.Datatable<ViewModel.Student.StudentInfo> GetStudentFeeList(string SchoolID, string CID, string StartYear, string RealName, bool IsCheck,int start,int length)
        {
            int TotalRows = 0;
            List<HPStudent.ViewModel.Student.StudentInfo> SelectList = new List<ViewModel.Student.StudentInfo>();

            HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Student.StudentInfo> StudentTable = new ViewModel.Common.Datatable<HPStudent.ViewModel.Student.StudentInfo>();
            StudentTable.data = new List<HPStudent.ViewModel.Student.StudentInfo>();


            DataSet ds = Data.Admin.StudentInfo.GetStudentFeeList(SchoolID, CID, StartYear, RealName, IsCheck,start,length ,out TotalRows );
 
            //初始化返回Datatable
            StudentTable.recordsTotal = TotalRows;
            StudentTable.recordsFiltered = TotalRows;

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                HPStudent.ViewModel.Student.StudentInfo table = new ViewModel.Student.StudentInfo();
                table.SchoolID = item["SchoolID"].ToString();
                table.SchoolName = item["SchoolName"].ToString();
                table.CID = item["CID"].ToString();
                table.CName = item["CName"].ToString();
                table.Credits = item["Credits"].ToString();
                table.Avatar = item["Avatar"].ToString();
                table.IsActivated = item["IsActivated"].ToString();
                table.PaidFee = item["PaidFee"].ToString();
                table.RealName = item["RealName"].ToString();
                table.Sex = item["Sex"].ToString();
                table.StudentID = item["StudentID"].ToString();
                table.CheckCount = item["CheckCount"].ToString();
                table.AllCount = item["AllCount"].ToString();
                StudentTable.data.Add(table);
            }
            return StudentTable;
        }

        public static HPStudent.ViewModel.Common.Datatable<ViewModel.Student.StudentFee> GetStudentFeeBySID(string StudentID)
        {
            int TotalRows = 0;
            List<HPStudent.ViewModel.Student.StudentInfo> SelectList = new List<ViewModel.Student.StudentInfo>();

            HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Student.StudentFee> StudentFeeTable = new ViewModel.Common.Datatable<ViewModel.Student.StudentFee>();
            StudentFeeTable.data = new List<ViewModel.Student.StudentFee>();


            DataSet ds = Data.Admin.StudentInfo.GetStudentFeeBySID(StudentID);
            TotalRows = ds.Tables[0].Rows.Count;
            //初始化返回Datatable
            StudentFeeTable.recordsTotal = TotalRows;

            StudentFeeTable.recordsFiltered = TotalRows;

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                HPStudent.ViewModel.Student.StudentFee table = new ViewModel.Student.StudentFee();
                table.Attachment = item["Attachment"].ToString();
                table.Dateline = item["Dateline"].ToString();
                table.FAID = item["FAID"].ToString();
                table.Fee = item["Fee"].ToString();
                table.FeeDescription = item["FeeDescription"].ToString();
                table.FeeID = item["FeeID"].ToString();
                table.FeeTitle = item["FeeTitle"].ToString();
                table.PaidFee = item["PaidFee"].ToString();
                table.IsCheck = item["IsCheck"].ToString();
                table.SID = item["SID"].ToString();
                table.Year = item["Year"].ToString();

                StudentFeeTable.data.Add(table);
            }
            return StudentFeeTable;
        }

        public static int EditStudentFeeIsCheck(string FeeID, int IsCheck)
        {
            return Data.Admin.StudentInfo.EditStudentFeeIsCheck(FeeID, IsCheck);
        }
    }
}
