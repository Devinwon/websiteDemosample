using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HPStudent.Student.Logic.ClassManage
{
    public class ClassManageController : Controller
    {
        public ActionResult Index() 
        {
            HPStudent.Student.ViewModel.Common.GetYear result = new ViewModel.Common.GetYear();
            result.GetYearList = HPStudent.Student.Business.ClassManage.GetYear();
            return View(result);
        }

        /// <summary>
        /// 获取实训中心所有班级
        /// </summary>
        /// <param name="Year">年级</param>
        /// <returns>班级集合</returns>
        public JsonResult GetPTClassTable(int Year) 
        {
            List<HPStudent.Student.ViewModel.ClassManage.PTClassTable> table = HPStudent.Student.Business.ClassManage.GetPTClassTable(Year);
            return Json(table);
        }



        public ActionResult ClassManageEdit() 
        {
            return View();
        }

         /// <summary>
         /// 获取学校下拉列表
         /// </summary>
         /// <returns></returns>
        public JsonResult GetSchoolSelBind() 
        {
            List<HPStudent.Entity.Common_School> result = HPStudent.Student.Business.ClassManage.GetSchoolSelBind();
            return Json(result);
        }

        /// <summary>
        /// 获取学校班级下拉列表
        /// </summary>
        /// <param name="Year">年级</param>
        /// <param name="School">学校ID</param>
        /// <returns></returns>
        public JsonResult GetClassSelBind(int Year,int School) 
        {
            List<HPStudent.Entity.StudentClass> result = HPStudent.Student.Business.ClassManage.GetClassSelBind(Year, School);
            return Json(result);
        }

        
        /// <summary>
        /// 获取班级学员
        /// </summary>
        /// <param name="Class">班级ID</param>
        /// <returns>学员集合</returns>
        public JsonResult GetClassStudentBind(string PTCID, int School, int Class, int Year) 
        {
            if (PTCID == "") 
            {
                PTCID = "0";
            }
            List<HPStudent.Entity.StudentInfo> result = HPStudent.Student.Business.ClassManage.GetClassStudentBind(Convert.ToInt32(PTCID), School, Class, Year);
            return Json(result);
        }

        /// <summary>
        /// 新增实训班级
        /// </summary>
        /// <param name="PTCName">实训班级名称</param>
        /// <param name="StudentID">学员ID集合</param>
        /// <param name="TeacherID">老师ID集合</param>
        /// <returns>是否成功</returns>
        public int AddPTClass( string PTCName, string StudentID, string TeacherID,string Year) 
        {
            HPStudent.Entity.PTClass entity = new Entity.PTClass();
            entity.PTCName = PTCName;
            entity.StudentID = StudentID;
            entity.TeacherID = TeacherID;
            entity.Year = Year;
            int result = HPStudent.Student.Business.ClassManage.Add(entity);
            return result;
        }

        /// <summary>
        /// 修改实训班级
        /// </summary>
        /// <param name="PTCID">ID</param>
        /// <param name="PTCName">实训班级名称</param>
        /// <param name="StudentID">学员ID集合</param>
        /// <param name="TeacherID">老师ID集合</param>
        /// <returns></returns>
        public int UpdatePTClass(int PTCID, string PTCName, string StudentID, string TeacherID, string Year)
        {
            HPStudent.Entity.PTClass entity = new Entity.PTClass();
            entity.PTCID = PTCID;
            entity.PTCName = PTCName;
            entity.StudentID = StudentID;
            entity.TeacherID = TeacherID;
            entity.Year = Year;
            int result = HPStudent.Student.Business.ClassManage.Update(entity);
            return result;
        }


        public JsonResult GetClass(int PTCID) 
        {
            HPStudent.Student.ViewModel.ClassManage.PTClassTable entity = HPStudent.Student.Business.ClassManage.GetClass(PTCID);
            return Json(entity);
        }


        public int DeleteClass(int PTCID) 
        {
            int result = HPStudent.Student.Business.ClassManage.Delete(PTCID);
            return result;
        }

        public ActionResult PTClassStudentInfo() 
        {
            return View();
        }
        public JsonResult GetClassStudentInfo(int draw, int start, int length,int PTCID) 
        {
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.ClassManage.ClassStudentTable> table = HPStudent.Student.Business.ClassManage.GetClassStudentInfo(start, length,Convert.ToInt32(PTCID));
            table.draw = draw;
            return Json(table);
        }

        public ActionResult EditStuInfo(string SID) 
        {
            int year = 2009;
            int nowYear = DateTime.Now.Year;
            int[] YearList = new int[nowYear - year + 1];
            for (int i = 0; i < YearList.Length; i++)
            {
                YearList[i] = year + i;
            }
            ViewData["StartYear"] = YearList;

            HPStudent.Student.ViewModel.Student.StudentInfo student = HPStudent.Student.Business.ClassManage.GetStudentByID(SID);
            ViewBag.RolId = student.RoleID;
            List<HPStudent.Entity.Major> MajorList =  HPStudent.Student.Business.ClassManage.GetMajorList();

            ViewData["MajorList"] = MajorList;
            //根据学校ID和入学年份获取班级
            List<HPStudent.Student.ViewModel.Student.StudentClass> ClassList = HPStudent.Student.Business.ClassManage.GetStudentClassBySchoolID(Convert.ToInt32(student.SchoolID), Convert.ToInt32(student.StartYear));
            //获取用户端角色
            List<HPStudent.Entity.UserRole> UserRoleList = HPStudent.Student.Business.ClassManage.GetUserRoleListNotPage();
            ViewBag.UserRoleDate = UserRoleList;
            ViewData["ClassList"] = ClassList;
            return View(student); 
        }

        public int UpdateStuInfo(int StudentID, string Name, int StartYear, int Sex, string Email, string Nation, string HomeAddress, string HomeMobile, string QQ, string IDCard, string Password) 
        {
            HPStudent.Entity.StudentInfo stuInfo = new Entity.StudentInfo();
            stuInfo.StudentID = StudentID;
            stuInfo.RealName = Name;
            stuInfo.StartYear = StartYear;
            stuInfo.Sex = Sex;
            stuInfo.Email = Email;
            stuInfo.Password = Password;
            HPStudent.Entity.StudentBaseInfo stuBaseInfo = new Entity.StudentBaseInfo();
            stuBaseInfo.Nation = Nation;
            stuBaseInfo.HomeAddress = HomeAddress;
            stuBaseInfo.HomeMobile = HomeMobile;
            stuBaseInfo.QQ = QQ;
            stuBaseInfo.IDCard = IDCard;
            int result = HPStudent.Student.Business.ClassManage.UpdateStuInfo(stuInfo, stuBaseInfo);

            return result;
        }

    }
}
