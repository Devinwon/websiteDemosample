using System;
using HPStudent.Entity;
using HPStudent.ViewModel;
using HPStudent.Data;
using System.Collections.Generic;
using HPStudent.Core;

namespace HPStudent.Business.Common
{
    public class SchoolCommon
    {
        /// <summary>
        /// 获取所有校区
        /// </summary>
        /// <returns></returns>
        public static List<HPStudent.ViewModel.Common.SchoolCommon> GetAllSchool()
        {
            List<HPStudent.ViewModel.Common.SchoolCommon> ViewSchoolList = new List<ViewModel.Common.SchoolCommon>();
            List<HPStudent.Entity.Common_School> EntitySchoolList = Data.Common.SchoolCommon.GetAllSchool();
            HPStudent.ViewModel.Common.SchoolCommon ViewSchool;
            for (int i = 0; i < EntitySchoolList.Count; i++)
            {
                ViewSchool = new ViewModel.Common.SchoolCommon();
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
        public static HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Student.StudentSchool> GetSchoolByAreaID(string ParentAID, string AreaID, int start, int length)
        {
            int TotalRows = 0;

            List<HPStudent.ViewModel.Student.StudentSchool> SelectList = new List<ViewModel.Student.StudentSchool>();

            HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Student.StudentSchool> SchoolTable = new ViewModel.Common.Datatable<HPStudent.ViewModel.Student.StudentSchool>();
            SchoolTable.data = new List<HPStudent.ViewModel.Student.StudentSchool>();


            SelectList = Data.Common.SchoolCommon.GetSchoolByAreaID(ParentAID, AreaID,start,length , out TotalRows);

            //初始化返回Datatable
            SchoolTable.recordsTotal = TotalRows;
            SchoolTable.recordsFiltered = TotalRows;

            foreach (HPStudent.ViewModel.Student.StudentSchool item in SelectList)
            {
                HPStudent.ViewModel.Student.StudentSchool table = new HPStudent.ViewModel.Student.StudentSchool();
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
            Entity.Common_School EntitySchool = Data.Common.SchoolCommon.GetSchoolBySchoolID(SchoolID);

            return EntitySchool;
        }

        public static int AddtCommon_School(Entity.Common_School comSchool)
        {
            return Data.Common.SchoolCommon.AddtCommon_School(comSchool);
        }

        public static int EditCommon_School(Entity.Common_School comSchool)
        {
            return Data.Common.SchoolCommon.EditCommon_School(comSchool);
        }

        public static int DeleteCommon_School(string SchoolID)
        {
            return Data.Common.SchoolCommon.DeleteCommon_School(SchoolID);
        }
        public static List<Entity.Common_Area> GetComAreaByParentAID(string ParentAID)
        {
            return Data.Common.SchoolCommon.GetComAreaByParentAID(ParentAID);
        }
        public static List<HPStudent.ViewModel.Student.StudentSchool> GetSchoolListByAreaID(string ParentAID, string AreaID)
        {
            int TotalRows = 0;

            List<HPStudent.ViewModel.Student.StudentSchool> SelectList = new List<ViewModel.Student.StudentSchool>();  

            SelectList = Data.Common.SchoolCommon.GetSchoolByAreaID(ParentAID, AreaID,0 ,1000, out TotalRows);

            
            return SelectList;

        }
    }
}
