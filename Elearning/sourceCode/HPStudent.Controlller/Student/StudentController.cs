using System;
using System.Web.Mvc;
using HPStudent.Core;
using HPStudent.Business;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace HPStudent.Logic
{
    public class StudentController : Controller
    {
        #region StudentClass 班级
        [HttpGet]
        public ActionResult Class()
        {
            List<ViewModel.Common.SchoolCommon> ViewSchoolList = Business.Common.SchoolCommon.GetAllSchool();
            ViewData["ViewSchoolList"] = ViewSchoolList;
            int y = DateTime.Now.Year;
            int a = 2009;
            ViewData["year"] = y - a;
            return View();
        }

        [HttpPost]
        public JsonResult GetStudentClassListBySchoolID(int SchoolID, int Year, int draw, int start, int length)
        {
            HPStudent.ViewModel.Common.Datatable<ViewModel.Student.SchoolCLass> StudentClassList = Business.Common.StudentClass.GetStudentClassListBySchoolID(SchoolID, Year, start, length);
            StudentClassList.draw = draw;
            return Json(StudentClassList);
        }

        [HttpPost]
        public JsonResult GetAllSchool()
        {
            List<ViewModel.Common.SchoolCommon> ViewSchoolList = Business.Common.SchoolCommon.GetAllSchool();
            return Json(ViewSchoolList);
        }

        public ActionResult Pop_Class_Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Pop_Class_Edit(string CID)
        {
            ViewModel.Common.StudentClass stuClass = Business.Common.StudentClass.GetStudentClassByCID(CID);
            return View(stuClass);
        }

        [HttpPost]
        public JsonResult AddStudentClass(string SchoolID, string Year, string CCode, string CName)
        {
            Entity.StudentClass stuClass = new Entity.StudentClass();
            stuClass.CCode = CCode;
            stuClass.CName = CName;
            stuClass.SchoolID = Convert.ToInt32(SchoolID);
            stuClass.Year = Convert.ToInt32(Year);

            int i = Business.Common.StudentClass.AddStudentClass(stuClass);
            return Json(i);
        }

        [HttpPost]
        public JsonResult EditStudentClass(string CID, string SchoolID, string Year, string CCode, string CName)
        {
            Entity.StudentClass stuClass = new Entity.StudentClass();
            stuClass.CID = Convert.ToInt32(CID);
            stuClass.CCode = CCode;
            stuClass.CName = CName;
            stuClass.SchoolID = Convert.ToInt32(SchoolID);
            stuClass.Year = Convert.ToInt32(Year);
            int i = Business.Common.StudentClass.EditStudentClass(stuClass);
            return Json(i);
        }

        [HttpPost]
        public JsonResult DeleteStudentClass(string CID)
        {
            int i = Business.Common.StudentClass.DeleteStudentClass(CID);
            return Json(i);
        }

        [HttpPost]
        public JsonResult GetStudentClassByCID(string CID)
        {
            ViewModel.Common.StudentClass stuClass = Business.Common.StudentClass.GetStudentClassByCID(CID);
            return Json(stuClass);
        }

        #endregion

        #region StudentSchool 校区
        public ActionResult School()
        {
            ViewData["Province"] = Business.Common.SchoolCommon.GetComAreaByParentAID("0");
            return View();
        }
        public ActionResult Pop_School_Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Pop_School_Edit(string SchoolID)
        {
            Entity.Common_School stuSchool = Business.Common.SchoolCommon.GetSchoolBySchoolID(Convert.ToInt32(SchoolID));
            return View(stuSchool);
        }

        [HttpPost]
        public int AddStudentSchool(string AreaID, string SchoolName)
        {
            Entity.Common_School comSchool = new Entity.Common_School();
            comSchool.SchoolName = SchoolName;
            comSchool.AreaID = Convert.ToInt32(AreaID);
            int i = Business.Common.SchoolCommon.AddtCommon_School(comSchool);
            return i;
        }

        [HttpPost]
        public int EditStudentSchool(string SchoolID, string SchoolName)
        {
            Entity.Common_School comSchool = new Entity.Common_School();
            comSchool.SchoolID = Convert.ToInt32(SchoolID);
            comSchool.SchoolName = SchoolName;

            int i = Business.Common.SchoolCommon.EditCommon_School(comSchool);
            return i;
        }

        [HttpPost]
        public int DeleteStudentSchool(string SchoolID)
        {
            int i = Business.Common.SchoolCommon.DeleteCommon_School(SchoolID);
            return i;
        }

        [HttpPost]
        public JsonResult GetSchoolByAreaID(string ParentAID, string AreaID, int draw, int start, int length)
        {
            HPStudent.ViewModel.Common.Datatable<ViewModel.Student.StudentSchool> comSchoolList = Business.Common.SchoolCommon.GetSchoolByAreaID(ParentAID, AreaID, start, length);
            comSchoolList.draw = draw;
            return Json(comSchoolList);
        }

        [HttpPost]
        public JsonResult GetComSchoolByParentAID(string ParentAID)
        {
            return Json(Business.Common.SchoolCommon.GetComAreaByParentAID(ParentAID));
        }
        #endregion

        #region StudentInfo 学生信息

        [HttpGet]
        public ActionResult Index()
        {
            List<ViewModel.Common.SchoolCommon> ViewSchoolList = Business.Common.SchoolCommon.GetAllSchool();
            ViewData["ViewSchoolList"] = ViewSchoolList;
            return View();
        }
        [HttpGet]
        public ActionResult Pop_Index_Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Student_Index_Edit(string id)
        {
            int year = 2009;
            int nowYear = DateTime.Now.Year;
            int[] YearList = new int[nowYear - year + 1];
            for (int i = 0; i < YearList.Length; i++)
            {
                YearList[i] = year + i;
            }
            ViewData["StartYear"] = YearList;

            ViewModel.Student.StudentInfo student = Business.Admin.StudentInfo.GetStudentByID(id);
            ViewBag.RolId = student.RoleID;
            List<Entity.Major> MajorList = HPStudent.Business.Admin.Exercises.GetMajorList();

            ViewData["MajorList"] = MajorList;
            //根据学校ID和入学年份获取班级
            List<ViewModel.Common.StudentClass> ClassList = HPStudent.Business.Common.StudentClass.GetStudentClassBySchoolID(Convert.ToInt32(student.SchoolID), Convert.ToInt32(student.StartYear));
            //获取用户端角色
            List<HPStudent.Entity.UserRole> UserRoleList = HPStudent.Business.Admin.UserRole.GetUserRoleListNotPage();
            ViewBag.UserRoleDate = UserRoleList;
            ViewData["ClassList"] = ClassList;
            return View(student);
        }

        [HttpPost]
        public JsonResult GetStudentClassListBySchoolIDAndYear(int SchoolID, int Year)
        {
            List<ViewModel.Student.SchoolCLass> classList = new List<ViewModel.Student.SchoolCLass>();
            HPStudent.ViewModel.Common.Datatable<ViewModel.Student.SchoolCLass> StudentClassList = Business.Common.StudentClass.GetStudentClassListBySchoolID(SchoolID, Year, 0, 1000);
            foreach (ViewModel.Student.SchoolCLass item in StudentClassList.data)
            {
                classList.Add(item);
            }
            return Json(classList);
        }

        [HttpPost]
        public JsonResult GetStudentList(string SchoolID, string CID, string StartYear, string RealName, int draw, int start, int length)
        {
            HPStudent.ViewModel.Common.Datatable<ViewModel.Student.StudentInfo> TableList = Business.Admin.StudentInfo.GetStudentList(SchoolID, CID, StartYear, RealName, start, length);
            TableList.draw = draw;
            return Json(TableList);
        }
        [HttpPost]
        public JsonResult UpdateBaseInfo(string MajorID, string CID, string StudentID)
        {
            int Result = HPStudent.Business.Admin.StudentInfo.UpdateBaseInfo(MajorID, CID, StudentID);
            return Json(Result);

        }

        [HttpPost]
        public JsonResult UpdateStudentInfo(string RealName, string Brithday, string Sex, string StartYear, string StudentID, string UserRole)
        {
            int Result = HPStudent.Business.Admin.StudentInfo.UpdateStudentInfo(RealName, Brithday, StartYear, Sex, StudentID, UserRole);
            return Json(Result);
        }

        [HttpGet]
        public ActionResult Pop_Student_Index_Edit_Add()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Pop_Student_Index_Edit_Edit(string StudentID, string Term)
        {
            Entity.StudentScore Score = HPStudent.Business.Admin.StudentInfo.GetStudentScore(StudentID, Term);
            return View(Score);
        }



        [HttpPost]
        public JsonResult AddMark(string Term, string Examination1, string Examination2, string Evaluate, string StudentID)
        {
            Entity.StudentScore Score = new Entity.StudentScore();
            Score.SID = Convert.ToInt32(StudentID);
            Score.Term = Convert.ToInt32(Term);
            Score.Examination1 = float.Parse(Examination1);
            Score.Examination2 = float.Parse(Examination2);
            Score.Evaluate = Evaluate;

            int i = HPStudent.Business.Admin.StudentInfo.AddMark(Score);
            return Json(i);
        }
        [HttpPost]
        public JsonResult EditMark(string Term, string Examination1, string Examination2, string Evaluate, string StudentID)
        {
            Entity.StudentScore Score = new Entity.StudentScore();
            Score.SID = Convert.ToInt32(StudentID);
            Score.Term = Convert.ToInt32(Term);
            Score.Examination1 = float.Parse(Examination1);
            Score.Examination2 = float.Parse(Examination2);
            Score.Evaluate = Evaluate;
            int i = HPStudent.Business.Admin.StudentInfo.EditMark(Score);
            return Json(i);
        }
        [HttpPost]
        public JsonResult DeleteMark(string StudentID, string Term)
        {
            int i = HPStudent.Business.Admin.StudentInfo.DeleteMark(StudentID, Term);
            return Json(i);
        }

        [HttpPost]
        public JsonResult GetStudentScore(string StudentID, int draw)
        {
            HPStudent.ViewModel.Common.Datatable<Entity.StudentScore> ScoreList = HPStudent.Business.Admin.StudentInfo.GetStudentScore(StudentID);
            ScoreList.draw = draw;
            return Json(ScoreList);
        }

        [HttpPost]
        public JsonResult GetStudentScoreByStudentIDAndTerm(string StudentID, string Term)
        {
            Entity.StudentScore Score = HPStudent.Business.Admin.StudentInfo.GetStudentScore(StudentID, Term);
            return Json(Score);
        }

        [HttpGet]
        public ActionResult Pop_Student_Import()
        {
            List<ViewModel.Common.SchoolCommon> ViewSchoolList = Business.Common.SchoolCommon.GetAllSchool();
            ViewData["ViewSchoolList"] = ViewSchoolList;
            int y = DateTime.Now.Year;
            int a = 2009;
            ViewData["year"] = y - a;
            return View(); ;
        }
        [HttpPost]
        public JsonResult Upload(string SchoolID, string Year)
        {
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string imgPath = "";
            string PhysicalPath = "";
            if (hfc.Count > 0)
            {
                imgPath = "/Upload/Temp/" + hfc[0].FileName.Substring(hfc[0].FileName.LastIndexOf("\\") + 1);
                PhysicalPath = Server.MapPath(imgPath);
                hfc[0].SaveAs(PhysicalPath);
            }

            string[] SheetNames = HPStudent.Core.ExcelHelper.GetExcelSheetNames(PhysicalPath);

            bool version = HPStudent.Core.ExcelHelper.IsExcel2007(imgPath);
            System.Data.DataSet ds = HPStudent.Core.ExcelHelper.GetExcelToDataSet(PhysicalPath, false);

            object[] obj = HPStudent.Business.Admin.StudentInfo.StudentInfoImport(ds, SheetNames, SchoolID, Year);


            return Json(obj);
        }

        //[HttpPost]
        //public JsonResult GetExcelData(string SchoolID, string Year, string FileName,HttpPostedFileBase file)
        //{

        //    string myPath = "";
        //    string myFileName = "";
        //    myPath = Server.MapPath("Upload/");
        //    myFileName = FileName.Substring(FileName.LastIndexOf("") + 1); 

        //    //string filePath = "";
        //    //string fileExtName = "";
        //    //fileExtName = FileName.Substring(filePath.LastIndexOf(".") + 1); 
        //    //string myPath; 
        //    //string FullName = "";//保存文件的完整文件名
        //    //if(FileName.PostedFile.FileName!="") 
        //    //{ 
        //    ////取得文件路径

        //    //filePath = FileName.PostedFile.FileName; 

        //    ////取得文件扩展名



        //    ////判断是否为Excel文件

        //    //if (fileExtName == "xls") 

        //    //{ 
        //    //try 
        //    //{ 
        //    ////取得与web服务器上指定的虚拟路径相对应的物理路径
        //    //myPath = Server.MapPath("Upfiles/");
        //    ////取得文件名

        //    ////
        //    //    myFileName = filePath.Substring(filePath.LastIndexOf("")+1); 
        //    ////取得当前时间，以“时时分分秒秒”来命名，以免重复
        //    //string strDateName = DateTime.Now.ToString("hhmmss"); 
        //    ////保存上传文件到指定目录

        //    //FullName = myPath + strDateName + "." + fileExtName; 
        //    //fileUp.PostedFile.SaveAs(FullName); 

        //    //} 
        //    //catch (Exception ex) 

        //    //{ 
        //    //Response.Write(ex.Message);
        //    //} 

        //    //} 
        //    //else 

        //    //{ 
        //    //Page.RegisterStartupScript("","<SCRIPT>alert('文件格式不正确');</SCRIPT>"); 
        //    //return; 
        //    //} 
        //    //} 

        //    //HPStudent.Core.ExcelHelper_1 ex = new ExcelHelper_1();
        //    //System.Data.DataTable dt = ex.ImportExcel(FileName);
        //    string[] SheetNames = HPStudent.Core.ExcelHelper.GetExcelSheetNames(FileName);

        //    bool version = HPStudent.Core.ExcelHelper.IsExcel2007(FileName);
        //    System.Data.DataSet ds = HPStudent.Core.ExcelHelper.GetExcelToDataSet(FileName, version);

        //    int i = HPStudent.Business.Admin.StudentInfo.StudentInfoImport(ds, SheetNames, SchoolID, Year);


        //    return Json(i);
        //}
        #endregion

        #region 费用设置
        [HttpGet]
        public ActionResult Fee()
        {
            ViewData["Province"] = Business.Common.SchoolCommon.GetComAreaByParentAID("0");
            return View();
        }
        [HttpPost]
        public JsonResult GetSchoolListByAreaID(string ParentAID, string AreaID)
        {
            List<ViewModel.Student.StudentSchool> comSchoolList = Business.Common.SchoolCommon.GetSchoolListByAreaID(ParentAID, AreaID);
            return Json(comSchoolList);
        }
        [HttpPost]
        public JsonResult GetClassListBySchoolIDAndYear(int SchoolID, int Year)
        {
            List<ViewModel.Student.SchoolCLass> StudentClassList = Business.Common.StudentClass.GetClassListBySchoolIDAndYear(SchoolID, Year, 0, 1000);

            return Json(StudentClassList);
        }
        [HttpPost]
        public JsonResult GetStudentFeeList(string SchoolID, string CID, string StartYear, string RealName, bool IsCheck, int draw, int start, int length)
        {
            HPStudent.ViewModel.Common.Datatable<ViewModel.Student.StudentInfo> StudentFeeList = Business.Admin.StudentInfo.GetStudentFeeList(SchoolID, CID, StartYear, RealName, IsCheck, start, length);
            StudentFeeList.draw = draw;
            return Json(StudentFeeList);
        }
        [HttpGet]
        public ViewResult Pop_Student_Feelog(string StudentID)
        {
            HPStudent.ViewModel.Common.Datatable<ViewModel.Student.StudentFee> StudentFee = Business.Admin.StudentInfo.GetStudentFeeBySID(StudentID);
            //StudentFee.draw = draw;
            List<ViewModel.Student.StudentFee> FeeList = new List<ViewModel.Student.StudentFee>();
            foreach (ViewModel.Student.StudentFee item in StudentFee.data)
            {
                FeeList.Add(item);
            }
            ViewData["FeeList"] = FeeList;

            return View();
        }
        /// <summary>
        /// 取消 0，通过1，退回 2
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Pop_Student_Feelog(string StudentID, string FeeID, string IsCheck)
        {
            HPStudent.Business.Admin.StudentInfo.EditStudentFeeIsCheck(FeeID, Convert.ToInt32(IsCheck));

            HPStudent.ViewModel.Common.Datatable<ViewModel.Student.StudentFee> StudentFee = Business.Admin.StudentInfo.GetStudentFeeBySID(StudentID);
            List<ViewModel.Student.StudentFee> FeeList = new List<ViewModel.Student.StudentFee>();
            foreach (ViewModel.Student.StudentFee item in StudentFee.data)
            {
                FeeList.Add(item);
            }
            ViewData["FeeList"] = FeeList;
            return View();

        }


        #endregion
    }
}
