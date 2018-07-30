using System;
using System.Web.Mvc;
using HPStudent.Core;
using HPStudent.Business;
using System.Collections.Generic;

namespace HPStudent.Logic
{
    public class ExercisesController : Controller
    {
        /// <summary>
        /// 练习题库展示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //QA_Select
            //string cid = "";
            //List<ViewModel.Exercises.QA_Select> QA_Select_list = Business.Admin.Exercises.GetQA_SelectListByCID(cid);
            //ViewData["QA_Select_list"] = QA_Select_list;
            ViewModel.Exercises.QA_Select qa_select = new ViewModel.Exercises.QA_Select();
            qa_select.MajorList = Business.Admin.Exercises.GetMajorList();
            return View(qa_select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(int CID)
        {
            //QA_Select
            //ViewData["QA_Select_list"] = "";
            //ViewModel.Common.Datatable<HPStudent.ViewModel.Exercises.QuestionTable> QA_Select_list = Business.Admin.Exercises.GetQA_SelectListByCID(CID);
            //ViewData["QA_Select_list"] = QA_Select_list;
            return View();
        }


        /// <summary>
        /// 练习题库分类页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Category()
        {
            ViewModel.Exercises.Category category = new ViewModel.Exercises.Category();
            category.MajorList = Business.Admin.Exercises.GetMajorList();
            return View(category);
        }

        /// <summary>
        /// 根据专业名称查询所有课程列表
        /// </summary>
        /// <param name="MajoyName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCategoryByMajoyName(string MajorName)
        {
            List<ViewModel.Exercises.CategoryItem> categoryList = Business.Admin.Exercises.GetCategoryItemListByMajorName(MajorName);

            return Json(categoryList);

        }

        [HttpPost]
        public JsonResult EditCategoryByMajoyName(string MajorName, string selectCategory)
        {
            selectCategory = HPStudent.Core.StringHelper.DelLastComma(selectCategory);
            ViewModel.Common.RequestResult result = Business.Admin.Exercises.EditCategoryByMajoyName(MajorName, selectCategory);

            return Json(result);
        }


        /// <summary>
        /// 根据专业编号查询所有课程列表
        /// </summary>
        /// <param name="MajoyID">专业编号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCategoryListByMajorName(string MajorName)
        {
            List<ViewModel.Exercises.QA_Category> categoryList = Business.Admin.Exercises.GetCategoryListByMajorName(MajorName);

            return Json(categoryList);
        }

        /// <summary>
        /// 根据课程编号获取选择题
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetQA_SelectListByCID(int CID,int draw,int start,int length)
        {
            HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Exercises.QuestionTable> questionList = Business.Admin.Exercises.GetQA_SelectListByCID(CID, start, length);
            questionList.draw = draw;

            return Json(questionList);
        }

        [HttpGet]
        public ActionResult Pop_Index_Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Pop_Exercises_Import()
        {
            return View();
        }
        /// <summary>
        /// 添加题目
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="Title"></param>
        /// <param name="答案A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public int InsertQA_Select(string CID, string Title, string A, string B, string C, string D, string Level, string Answer, string AnswerAnalysis)
        {
            HPStudent.Entity.QA_Select sel = new Entity.QA_Select();
            sel.CID = Convert.ToInt32(CID);
            sel.A = System.Web.HttpUtility.HtmlEncode(A);
            sel.B = System.Web.HttpUtility.HtmlEncode(B);
            sel.C = System.Web.HttpUtility.HtmlEncode(C);
            sel.D = System.Web.HttpUtility.HtmlEncode(D);
            sel.Title = System.Web.HttpUtility.HtmlEncode(Title);
            sel.Level = Convert.ToInt32(Level);
            sel.CreateDate = DateTime.Now;
            sel.Creater = Convert.ToInt32(CookieHelper.GetCookieValue("MID"));
            sel.Answer = Answer;
            sel.AnswerAnalysis = AnswerAnalysis;
            int i = Business.Admin.Exercises.EditQA_Select(sel);
            return i;
        }

        [HttpGet]
        public ActionResult Pop_Index_Edit(string QID)
        {
            ViewModel.Exercises.QA_Select viewSel = Business.Admin.Exercises.GetQA_SelectByQID(QID);
            return View(viewSel);
        }
        /// <summary>
        /// 修改题目
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="Title"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="Level"></param>
        /// <param name="Answer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public int EditQA_Select(string QID, string CID, string Title, string A, string B, string C, string D, string Level, string Answer, string AnswerAnalysis)
        {
            HPStudent.Entity.QA_Select sel = new Entity.QA_Select();
            sel.QID = Convert.ToInt32(QID);
            sel.CID = Convert.ToInt32(CID);
            sel.A = System.Web.HttpUtility.HtmlEncode(A);
            sel.B = System.Web.HttpUtility.HtmlEncode(B);
            sel.C = System.Web.HttpUtility.HtmlEncode(C);
            sel.D = System.Web.HttpUtility.HtmlEncode(D);
            sel.Title = System.Web.HttpUtility.HtmlEncode(Title);
            sel.Level = Convert.ToInt32(Level);
            sel.CreateDate = DateTime.Now;
            sel.Creater = Convert.ToInt32(CookieHelper.GetCookieValue("MID"));
            sel.Answer = Answer;
            sel.AnswerAnalysis = AnswerAnalysis;
            int i = Business.Admin.Exercises.EditQA_Select(sel);
            return i;
        }
        /// <summary>
        /// 删除题目
        /// </summary>
        /// <param name="QID"></param>
        /// <returns></returns>
        [HttpPost]
        public int DelQA_Select(string QID)
        {
            int i = Business.Admin.Exercises.DelQA_Select(QID);
            return i;
        }

        /// <summary>
        /// 批量导入题目
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(string CID)
        {
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string imgPath = "";
            string PhysicalPath = "";
            string DatePre = DateTime.Now.ToString("yyyyMMdd_HHmmss_ffff_");
            if (hfc.Count > 0)
            {
                imgPath = "/Upload/Exercises/" + DatePre + hfc[0].FileName.Substring(hfc[0].FileName.LastIndexOf("\\") + 1);
                PhysicalPath = Server.MapPath(imgPath);
                hfc[0].SaveAs(PhysicalPath);
            }

            string[] SheetNames = HPStudent.Core.ExcelHelper.GetExcelSheetNames(PhysicalPath);

            bool version = HPStudent.Core.ExcelHelper.IsExcel2007(imgPath);
            System.Data.DataSet ds = HPStudent.Core.ExcelHelper.GetExcelToDataSet(PhysicalPath, false, "题库$");
            
            

            int CreaterID = Convert.ToInt32(CookieHelper.GetCookieValue("MID"));
            int iresult = HPStudent.Business.Admin.Exercises.ExercisesImport(ds, CID, CreaterID);

            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
            result.ResultMsg = iresult.ToString();
            return Json(result, "text/html");
        }
    }
}
