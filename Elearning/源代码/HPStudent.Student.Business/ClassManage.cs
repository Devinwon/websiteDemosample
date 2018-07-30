using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
   public class ClassManage
    {

       public static int[] GetYear()
       {
           int nowYear = DateTime.Now.Year;
           int year = nowYear - 10;
           int[] YearList = new int[nowYear - year];
           int j = 0;
           for (int i = (YearList.Length); i > 0; i--)
           {
               YearList[j] = year + i;
               j++;
           }
           return YearList;
       }


       public static List<HPStudent.Student.ViewModel.ClassManage.PTClassTable> GetPTClassTable(int Year) 
       {
           return HPStudent.Student.Data.ClassManage.GetPTClassTable(Year);
       
       }

       public static List<HPStudent.Entity.Common_School> GetSchoolSelBind()
       {
           List<HPStudent.Entity.Common_School> list = HPStudent.Student.Data.ClassManage.GetSchoolSelBind();
           
           return list;
       
       }

       public static List<HPStudent.Entity.StudentClass> GetClassSelBind(int Year,int School) 
       {
           List<HPStudent.Entity.StudentClass> list = HPStudent.Student.Data.ClassManage.GetClassSelBind(Year, School);
           return list;
       
       }

       public static List<HPStudent.Entity.StudentInfo> GetClassStudentBind(int PTCID,int School,int Class,int Year) 
       {
           List<HPStudent.Entity.StudentInfo> list = HPStudent.Student.Data.ClassManage.GetClassStudentBind(PTCID, School, Class, Year);
           return list;
       }

       public static int Add(HPStudent.Entity.PTClass entity) 
       {
           return HPStudent.Student.Data.ClassManage.Add(entity);
       
       }

       public static int Update(HPStudent.Entity.PTClass entity)
       {
           return HPStudent.Student.Data.ClassManage.Update(entity);

       }

       public static int Delete(int PTCID) 
       {
           return HPStudent.Student.Data.ClassManage.Delete(PTCID);
       }

       public static HPStudent.Student.ViewModel.ClassManage.PTClassTable GetClass(int PTCID) 
       {
           return HPStudent.Student.Data.ClassManage.GetClass(PTCID);
       
       }

       public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.ClassManage.ClassStudentTable> GetClassStudentInfo(int start,int length,int PTCID ) 
       {
           int TotalRows = 0;
           List<HPStudent.Student.ViewModel.ClassManage.ClassStudentTable> list = new List<HPStudent.Student.ViewModel.ClassManage.ClassStudentTable>();
           HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.ClassManage.ClassStudentTable> resumeIndexList = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.ClassManage.ClassStudentTable>();
           resumeIndexList.data = new List<HPStudent.Student.ViewModel.ClassManage.ClassStudentTable>();

           list = HPStudent.Student.Data.ClassManage.GetClassStudentInfo(start, length, PTCID, out TotalRows);

           resumeIndexList.recordsFiltered = TotalRows;
           resumeIndexList.recordsTotal = TotalRows;

           foreach (HPStudent.Student.ViewModel.ClassManage.ClassStudentTable item in list)
           {

               HPStudent.Student.ViewModel.ClassManage.ClassStudentTable table = new HPStudent.Student.ViewModel.ClassManage.ClassStudentTable();
               table.StudentID = item.StudentID;
               table.RealName = item.RealName;
               table.Sex = item.Sex;
               table.LastLoginTime = item.LastLoginTime;
               table.ResumeStatus = item.ResumeStatus;
               table.Operation = "<a class=\"btn btn-primary btn-sm\" data-id=\"" + item.StudentID + "\" data-action=\"seeResume\">"
                + "<span class=\"fa fa-pencil\"></span>查看简历</a> <a class=\"btn btn-primary btn-sm\" data-id=\"" + item.StudentID + "\" data-action=\"editStuInfo\">"
                + "<span class=\"glyphicon glyphicon-align-justify\"></span>修改信息</a> ";
               resumeIndexList.data.Add(table);

           }

           return resumeIndexList;
       
       }

       public static HPStudent.Student.ViewModel.Student.StudentInfo GetStudentByID(string StudentID)
       {
           return HPStudent.Student.Data.ClassManage.GetStudentByID(StudentID);
       }

       public static List<HPStudent.Entity.Major> GetMajorList()
       {
           return HPStudent.Student.Data.ClassManage.GetMajorList();
       }

       public static List<HPStudent.Student.ViewModel.Student.StudentClass> GetStudentClassBySchoolID(int SchoolID, int year)
       {

           List<HPStudent.Student.ViewModel.Student.StudentClass> ViewClassList = new List<ViewModel.Student.StudentClass>();

           List<HPStudent.Entity.StudentClass> EntityStuClassList = HPStudent.Student.Data.ClassManage.GetStudentClassBySchoolID(SchoolID, year);

           foreach (Entity.StudentClass item in EntityStuClassList)
           {
               ViewClassList.Add(new ViewModel.Student.StudentClass
               {
                   CID = item.CID,
                   SchoolID = item.SchoolID,
                   CName = item.CName,
                   Year = item.Year,
                   CCode = item.CCode,
               }
                   );
           }
           return ViewClassList;
       }


       public static List<HPStudent.Entity.UserRole> GetUserRoleListNotPage()
       {
           return HPStudent.Student.Data.ClassManage.GetUserRoleListNotPage();
       }

       public static int UpdateStuInfo(HPStudent.Entity.StudentInfo stuInfo,HPStudent.Entity.StudentBaseInfo stuBaseInfo) 
       {
           return HPStudent.Student.Data.ClassManage.UpdateStuInfo(stuInfo, stuBaseInfo);
       
       }
    }
}
