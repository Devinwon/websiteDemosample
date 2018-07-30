using System;
using System.Web.Mvc;
using System.Collections.Generic;
using HPStudent.Core;
using HPStudent.Student.Business;
using System.Web.Script.Serialization;


namespace HPStudent.Student.Logic
{
    public class ExercisesController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Pop_Progressbar(string CID)
        {
            string StudentID = CookieHelper.GetCookieValue("StudentID");
            HPStudent.Entity.TestPaper test = HPStudent.Student.Business.TestPaper.GetTestPaperByCID(10, CID, StudentID);
            return View(test);
        }
        /// <summary>
        /// 绑定个人的测试试卷
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BindTestPaper(int draw, int start, int length)
        {
            string StudentID = CookieHelper.GetCookieValue("StudentID");
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Entity.TestPaper> TestPaperList = HPStudent.Student.Business.TestPaper.GetStudentTestPaper(StudentID, start, length);
            TestPaperList.draw = draw;

            return Json(TestPaperList);

        }
        [HttpGet]
        public ActionResult Pop_ShowTest(String TID)
        {
            HPStudent.Entity.TestPaper test = HPStudent.Student.Business.TestPaper.GetTestPaperByTID(TID);
            List<Entity.QA_Select> SelList = HPStudent.Student.Business.TestPaper.GetTestPaperQA_SelectByTID(TID);
            ViewData["SelList"] = SelList;
            return View(test);
        }

        /// <summary>
        /// 根据专业编号查询所有课程列表
        /// </summary>
        /// <param name="MajoyID">专业编号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCategoryListByMajorID(string MajorID)
        {
            List<HPStudent.Student.ViewModel.Exercises.QA_Category> categoryList = HPStudent.Student.Business.Exercises.GetCategoryListByMajorID(MajorID);

            return Json(categoryList);
        }

        /// <summary>
        /// 根据专业编号查询所有课程列表
        /// </summary>
        /// <param name="MajoyID">专业编号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCategoryListByMajorIDNotNone(string MajorID)
        {
            List<HPStudent.Student.ViewModel.Exercises.QA_Category> categoryList = HPStudent.Student.Business.Exercises.GetCategoryListByMajorIDNotNone(MajorID);

            return Json(categoryList);
        }

        [HttpGet]
        public ActionResult Pop_DoTest(string TID)
        {
            HPStudent.Entity.TestPaper test = HPStudent.Student.Business.TestPaper.GetTestPaperByTID(TID);
            List<Entity.QA_Select> SelList = HPStudent.Student.Business.TestPaper.GetTestPaperQA_SelectByTID(TID);
            ViewData["SelList"] = SelList;
            return View(test);
        }


        [HttpPost]
        public JsonResult SubmitDoTest(string TID, string Answer)
        {
            int i = HPStudent.Student.Business.TestPaper.SubmitDoTest(TID, Answer);
            return Json(i);
        }
        //题库练习页面
        public ActionResult Pop_PracticeIndex()
        {
            //专业下拉列表数据
            List<Entity.Major> MajorList = HPStudent.Student.Business.Projects.GetMajorList();
            ViewBag.MajorList = MajorList;
            return View();
        }
        //通过专业类别查询对应课程报表
        public ActionResult GetStatistics(int mid)
        {
            int studentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
            ViewModel.Exercises.StatisticsQuestion_SelectViewModel SelectViewModel = new ViewModel.Exercises.StatisticsQuestion_SelectViewModel();
            SelectViewModel.StudentID = studentID;
            SelectViewModel.MID = mid;
            List<ViewModel.Exercises.StatisticsQuestion_Select> list = Business.StatisticsQuestion_Select.GetStatisticsQuestion(SelectViewModel);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            String strJson = jss.Serialize(list);
            return Json(strJson);
        }
        //练习题
        public ActionResult Pop_Practice(int cid)
        {
            int studentid = Convert.ToInt16(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            //获得已答题记录
            Entity.AlreadyAnswer_Select AlreadyAnswerModel = new Entity.AlreadyAnswer_Select();
            AlreadyAnswerModel.CID = cid;
            AlreadyAnswerModel.StudentID = studentid;
            List<Entity.AlreadyAnswer_Select> AlreadyAnswerList = Business.Exercises.GetAlreadyAnswerList(AlreadyAnswerModel);
            List<ViewModel.Exercises.Answer_Json> Answer_JsonList = new List<ViewModel.Exercises.Answer_Json>();
            //获得题目
            List<ViewModel.Exercises.QA_SelectResultModel> QA_SelectResultModelList = new List<ViewModel.Exercises.QA_SelectResultModel>();
            ViewModel.Exercises.QA_SelectViewModel QA_SelectViewModel = new ViewModel.Exercises.QA_SelectViewModel();
            QA_SelectViewModel.CID = cid;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            int AlreadyTestNum = 0;
            int TrueNum = 0;
            int FalseNum = 0;
            //如果已经作答从上次最后一个题目开始取题目区间
            if (AlreadyAnswerList != null)
            {
                Answer_JsonList = jss.Deserialize<List<ViewModel.Exercises.Answer_Json>>(AlreadyAnswerList[0].Answer);
                AlreadyTestNum = Answer_JsonList.Count;
                TrueNum = Answer_JsonList.FindAll(x => x.IsTrue == true).Count;
                FalseNum = Answer_JsonList.FindAll(x => x.IsTrue == false).Count;
                QA_SelectViewModel.RowID = Answer_JsonList[Answer_JsonList.Count - 1].RowID + 1;
                QA_SelectResultModelList = Business.Exercises.GetQA_Select(QA_SelectViewModel);
            }
            else
            {
                QA_SelectViewModel.RowID = 1;
                QA_SelectResultModelList = Business.Exercises.GetQA_Select(QA_SelectViewModel);
            }
            //获取该类别总题量
            int TestSumNum = Business.Exercises.GetQA_SelectNum(cid);
            ViewBag.TestSumNum = TestSumNum;
            ViewBag.AlreadyTestNum = AlreadyTestNum;
            ViewBag.TrueNum = TrueNum;
            ViewBag.FalseNum = FalseNum;
            ViewBag.QA_SelectResultModelList = jss.Serialize(QA_SelectResultModelList);
            ViewBag.Answer_JsonList = Answer_JsonList;
            return View();
        }
        //通过题号获取需要的题目（每次加载10题）
        public ActionResult Get_Test(ViewModel.Exercises.QA_SelectViewModel model)
        {
            List<ViewModel.Exercises.QA_SelectResultModel> QA_SelectResultModelList = Business.Exercises.GetQA_Select(model);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            String strJson;
            if (QA_SelectResultModelList != null)
            {
                strJson = jss.Serialize(QA_SelectResultModelList);
            }
            else
            {
                strJson = jss.Serialize(null);
            }

            return Json(strJson);
        }
        //保存答题记录
        public ActionResult SaveAnswer(ViewModel.Exercises.Answer_Json model)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ViewModel.Exercises.StatisticsQuestion_SelectViewModel StatisticsModel = new ViewModel.Exercises.StatisticsQuestion_SelectViewModel();
            StatisticsModel.CID = Convert.ToInt32(Request.Params["NewCID"]);
            StatisticsModel.StudentID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
            ViewModel.Exercises.Answer_Json strJson = Business.Exercises.SaveAnswer(model, StatisticsModel);
            return Json(strJson);
        }
    }
}
