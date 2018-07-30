using System;
using HPStudent.Entity;
using HPStudent.Student.ViewModel;
using HPStudent.Student.Data;
using System.Collections.Generic;
using HPStudent.Core;

namespace HPStudent.Student.Business.Common
{
    public class SchoolCommon
    {
        /// <summary>
        /// 获取所有校区
        /// </summary>
        /// <returns></returns>
        public static List<HPStudent.Student.ViewModel.Common.SchoolCommon> GetAllSchool()
        {
            List<HPStudent.Student.ViewModel.Common.SchoolCommon> ViewSchoolList = new List<HPStudent.Student.ViewModel.Common.SchoolCommon>();
            List<HPStudent.Entity.Common_School> EntitySchoolList = HPStudent.Student.Data.Common.SchoolCommon.GetAllSchool();
            HPStudent.Student.ViewModel.Common.SchoolCommon ViewSchool;
            for (int i = 0; i < EntitySchoolList.Count; i++)
            {
                ViewSchool = new HPStudent.Student.ViewModel.Common.SchoolCommon();
                ViewSchool.SchoolID = EntitySchoolList[i].SchoolID;
                ViewSchool.AreaID = EntitySchoolList[i].AreaID;
                ViewSchool.SchoolName = EntitySchoolList[i].SchoolName;
                ViewSchool.DisplayOrder = EntitySchoolList[i].DisplayOrder;
                ViewSchoolList.Add(ViewSchool);
            }
            return ViewSchoolList;
        }
        /// <summary>
        /// 根据区域获取校区
        /// </summary>
        /// <param name="AreaID"></param>
        /// <returns></returns>
        public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Student.StudentSchool> GetSchoolByAreaID(string ParentAID, string AreaID, int start, int length)
        {
            int TotalRows = 0;

            List<HPStudent.Student.ViewModel.Student.StudentSchool> SelectList = new List<HPStudent.Student.ViewModel.Student.StudentSchool>();

            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Student.StudentSchool> SchoolTable = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Student.StudentSchool>();
            SchoolTable.data = new List<HPStudent.Student.ViewModel.Student.StudentSchool>();


            SelectList = HPStudent.Student.Data.Common.SchoolCommon.GetSchoolByAreaID(ParentAID, AreaID, start, length, out TotalRows);

            //初始化返回Datatable
            SchoolTable.recordsTotal = TotalRows;
            SchoolTable.recordsFiltered = TotalRows;

            foreach (HPStudent.Student.ViewModel.Student.StudentSchool item in SelectList)
            {
                HPStudent.Student.ViewModel.Student.StudentSchool table = new HPStudent.Student.ViewModel.Student.StudentSchool();
                table.SchoolID = item.SchoolID;
                table.AreaID = item.AreaID;
                table.AreaName = item.AreaName;
                table.CityName = item.CityName;
                table.SchoolName = item.SchoolName;

                // table.Level = "<img src=\"" + HttpHelper.GetRootUrl() + "/content/img/ui/star_0" + item.level + ".gif\"/>";
                table.Operation = "<button class=\"btn btn-primary btn-sm\" data-id=\"" + item.SchoolID + "\" data-action=\"edit\">"
                + "<span class=\"fa fa-pencil\"></span>编辑</button> "
                + "<button class=\"btn btn-primary btn-sm disabled\" type=\"button\" data-id=\"" + item.SchoolID + "\">"
                + "<span class=\"fa fa-times\"></span> 删除</button>";
                SchoolTable.data.Add(table);
            }
            return SchoolTable;

        }


        public static Entity.Common_School GetSchoolBySchoolID(int SchoolID)
        {
            Entity.Common_School EntitySchool = HPStudent.Student.Data.Common.SchoolCommon.GetSchoolBySchoolID(SchoolID);

            return EntitySchool;
        }

        public static int AddtCommon_School(Entity.Common_School comSchool)
        {
            return HPStudent.Student.Data.Common.SchoolCommon.AddtCommon_School(comSchool);
        }

        public static int EditCommon_School(Entity.Common_School comSchool)
        {
            return HPStudent.Student.Data.Common.SchoolCommon.EditCommon_School(comSchool);
        }

        public static int DeleteCommon_School(string SchoolID)
        {
            return HPStudent.Student.Data.Common.SchoolCommon.DeleteCommon_School(SchoolID);
        }
        public static List<Entity.Common_Area> GetComAreaByParentAID(string ParentAID)
        {
            return HPStudent.Student.Data.Common.SchoolCommon.GetComAreaByParentAID(ParentAID);
        }
        //通过市找出省份
        public static Entity.Common_Area GetComAreaByAreaName(string AreaName)
        {
            return HPStudent.Student.Data.Common.SchoolCommon.GetComAreaByAreaName(AreaName);
        }
        public static List<HPStudent.Student.ViewModel.Student.StudentSchool> GetSchoolListByAreaID(string ParentAID, string AreaID)
        {
            int TotalRows = 0;

            List<HPStudent.Student.ViewModel.Student.StudentSchool> SelectList = new List<HPStudent.Student.ViewModel.Student.StudentSchool>();

            SelectList = HPStudent.Student.Data.Common.SchoolCommon.GetSchoolByAreaID(ParentAID, AreaID, 0, 1000, out TotalRows);


            return SelectList;

        }
    }
}
