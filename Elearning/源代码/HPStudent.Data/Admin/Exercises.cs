using System;
using HPStudent.Entity;
using HPStudent.ViewModel;
using HPStudent.Data;
using System.Collections.Generic;
using HPStudent.Core;

namespace HPStudent.Business.Admin
{
    public class Exercises
    {
        //public static ViewModel.Exercises.Category GetAdminSideBar()
        //{
        //    //获得导航菜单
        //    ViewModel.Exercises.Category category = new ViewModel.Exercises.Category();
        //    category.SideBarList = Data.Common.AdminCommon.GetSideBar();

        //    return category;
        //}

        public static List<HPStudent.Entity.Major> GetMajorList()
        {
            return Data.Admin.Exercises.GetMajorList();
        }

        public static List<ViewModel.Exercises.CategoryItem> GetCategoryItemListByMajorName(string MajorName)
        {
            //MajorName = "软件技术JAVA";
            List<ViewModel.Exercises.CategoryItem> itemList = new List<ViewModel.Exercises.CategoryItem>();
            List<HPStudent.Entity.QA_Category> CategoryList = new List<QA_Category>();
            CategoryList = Data.Admin.Exercises.GetAllCategory();
            int[] CIDList = Data.Admin.Exercises.GetCIDListByMajorName(MajorName);

            foreach (HPStudent.Entity.QA_Category item in CategoryList)
            {
                bool CheckState = false;
                if (Array.IndexOf(CIDList, item.CID) >= 0)
                {
                    CheckState = true;
                }
                itemList.Add(new ViewModel.Exercises.CategoryItem()
                {
                    CID = item.CID,
                    CategoryName = item.CategoryName,
                    IsChecked = CheckState
                });
            }
            return itemList;
        }

        public static ViewModel.Common.RequestResult EditCategoryByMajoyName(string MajorName, string selectCategory)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            string[] CategoryID = selectCategory.Split(',');

            if (Data.Admin.Exercises.EditCategoryByMajoyName(MajorName, CategoryID))
            {
                result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                result.ResultMsg = "【" + MajorName + "】 修改成功！";
            }
            else
            {
                result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                result.ResultMsg = "【" + MajorName + "】 修改失败！";

            }
            return result;
        }
        public static List<ViewModel.Exercises.QA_Category> GetCategoryListByMajorID(string MajorID)
        {
            List<ViewModel.Exercises.QA_Category> itemList = new List<ViewModel.Exercises.QA_Category>();
            List<HPStudent.Entity.QA_Category> CategoryList = new List<QA_Category>();
            CategoryList = Data.Admin.Exercises.GetCategoryListByMajorID(MajorID);
            foreach (QA_Category item in CategoryList)
            {
                ViewModel.Exercises.QA_Category view = new ViewModel.Exercises.QA_Category();
                view.CID = item.CID;
                view.CategoryName = item.CategoryName;

                itemList.Add(view);
            }
            return itemList;
        }

        public static List<ViewModel.Exercises.QA_Category> GetCategoryListByMajorIDNotNone(string MajorID)
        {
            List<ViewModel.Exercises.QA_Category> itemList = new List<ViewModel.Exercises.QA_Category>();
            List<HPStudent.Entity.QA_Category> CategoryList = new List<QA_Category>();
            CategoryList = Data.Admin.Exercises.GetCategoryListByMajorIDNotNone(MajorID);
            foreach (QA_Category item in CategoryList)
            {
                ViewModel.Exercises.QA_Category view = new ViewModel.Exercises.QA_Category();
                view.CID = item.CID;
                view.CategoryName = item.CategoryName;

                itemList.Add(view);
            }
            return itemList;
        }
        public static List<ViewModel.Exercises.QA_Category> GetCategoryListByMajorName(string MajorName)
        {
            List<ViewModel.Exercises.QA_Category> itemList = new List<ViewModel.Exercises.QA_Category>();
            List<HPStudent.Entity.QA_Category> CategoryList = new List<QA_Category>();
            CategoryList = Data.Admin.Exercises.GetCategoryListByMajorName(MajorName);
            foreach (QA_Category item in CategoryList)
            {
                ViewModel.Exercises.QA_Category view = new ViewModel.Exercises.QA_Category();
                view.CID = item.CID;
                view.CategoryName = item.CategoryName;

                itemList.Add(view);
            }
            return itemList;
        }

        /// <summary>
        /// 根据课程编号查询题目列表
        /// </summary>
        /// <param name="CID">课程编号</param>
        /// <returns></returns>
        public static HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Exercises.QuestionTable> GetQA_SelectListByCID(int CID, int start, int length)
        {
            int TotalRows = 0;
            List<HPStudent.Entity.QA_Select> SelectList = new List<QA_Select>();
            HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Exercises.QuestionTable> QuestionTable = new ViewModel.Common.Datatable<ViewModel.Exercises.QuestionTable>();
            QuestionTable.data = new List<HPStudent.ViewModel.Exercises.QuestionTable>();
            SelectList = Data.Admin.Exercises.GetQA_SelectListByCID(CID, start, length,out TotalRows);

            //初始化返回Datatable行数
            QuestionTable.recordsTotal = TotalRows;
            QuestionTable.recordsFiltered = TotalRows;

            foreach (QA_Select item in SelectList)
            {
                HPStudent.ViewModel.Exercises.QuestionTable table = new HPStudent.ViewModel.Exercises.QuestionTable();
                table.QID = item.QID;
                table.Title = item.Title;
                table.Level = "<img src=\"" + HttpHelper.GetRootUrl() + "/content/img/ui/star_0" + item.Level + ".gif\"/>";
                table.Operation = "<button class=\"btn btn-primary btn-sm\" data-id=\""+ item.QID +"\" data-action=\"edit\">"
                +"<span class=\"fa fa-pencil\"></span>编辑</button> "
                + "<button class=\"btn btn-primary btn-sm disabled\" type=\"button\" data-id=\"" + item.QID + "\">"
                +"<span class=\"fa fa-times\"></span> 删除</button>";
                QuestionTable.data.Add(table);
            }
            return QuestionTable;
        }
        /// <summary>
        /// 编辑题目
        /// </summary>
        /// <param name="sel">题目实体对象</param>
        /// <returns></returns>
        public static int EditQA_Select(QA_Select sel)
        {
            return Data.Admin.Exercises.EditQA_Select(sel);
        }

        /// <summary>
        /// 查询题目信息
        /// </summary>
        /// <param name="QID"></param>
        /// <returns></returns>
        public static ViewModel.Exercises.QA_Select GetQA_SelectByQID(string QID)
        {
            ViewModel.Exercises.QA_Select viewSel = new ViewModel.Exercises.QA_Select();
            HPStudent.Entity.QA_Select entitySel = Data.Admin.Exercises.GetQA_SelectByQID(QID);
            viewSel.CID = entitySel.CID;
            viewSel.QID = entitySel.QID;
            viewSel.Title = System.Web.HttpUtility.HtmlDecode(entitySel.Title);
            viewSel.Level = entitySel.Level;
            viewSel.A = System.Web.HttpUtility.HtmlDecode(entitySel.A);
            viewSel.B = System.Web.HttpUtility.HtmlDecode(entitySel.B);
            viewSel.C = System.Web.HttpUtility.HtmlDecode(entitySel.C);
            viewSel.D = System.Web.HttpUtility.HtmlDecode(entitySel.D);
            viewSel.Answer = entitySel.Answer;
            viewSel.AnswerAnalysis = entitySel.AnswerAnalysis;


            return viewSel;
        }
        /// <summary>
        /// 根据题目编号删除题目
        /// </summary>
        /// <param name="QID">题目编号</param>
        /// <returns></returns>
        public static int DelQA_Select(string QID)
        {
            return Data.Admin.Exercises.DelQA_Select(QID);
        }

        public static int ExercisesImport(System.Data.DataSet ds, string CID, int CreaterID)
        {
            return Data.Admin.Exercises.ExercisesImport(ds, CID, CreaterID);
        }
    }
}
