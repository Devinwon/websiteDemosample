using System;
using System.Data;
using HPStudent.Entity;
using HPStudent.ViewModel;
using HPStudent.Data;
using System.Collections.Generic;


namespace HPStudent.Business.Common
{
    public class StudentClass
    {
        /// <summary>
        /// 根据校区和年份获取班级
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static List<HPStudent.ViewModel.Common.StudentClass> GetStudentClassBySchoolID(int SchoolID, int year)
        {

            List<HPStudent.ViewModel.Common.StudentClass> ViewClassList = new List<ViewModel.Common.StudentClass>();

            List<HPStudent.Entity.StudentClass> EntityStuClassList = Data.Common.StudentClass.GetStudentClassBySchoolID(SchoolID, year);

            foreach (Entity.StudentClass item in EntityStuClassList)
            {
                ViewClassList.Add(new ViewModel.Common.StudentClass
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

        public static HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Student.SchoolCLass> GetStudentClassListBySchoolID(int SchoolID, int year, int start, int length)
        {
            int TotalRows = 0;

            List<HPStudent.ViewModel.Student.SchoolCLass> ViewClassList = new List<ViewModel.Student.SchoolCLass>();

            HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Student.SchoolCLass> ClassTable = new ViewModel.Common.Datatable<HPStudent.ViewModel.Student.SchoolCLass>();
            ClassTable.data = new List<HPStudent.ViewModel.Student.SchoolCLass>();


            DataSet ds = Data.Common.StudentClass.GetStudentClassListBySchoolID(SchoolID, year, start, length, out TotalRows);
            //初始化返回Datatable
            ClassTable.recordsTotal = TotalRows;
            ClassTable.recordsFiltered = TotalRows;

            foreach (System.Data.DataRow item in ds.Tables[0].Rows)
            {
                HPStudent.ViewModel.Student.SchoolCLass table = new ViewModel.Student.SchoolCLass();
                table.CCode = item["CCode"].ToString();
                table.CID = Convert.ToInt32(item["CID"].ToString());
                table.Year = Convert.ToInt32(item["Year"].ToString());
                table.CName = item["CName"].ToString();
                table.SchoolName = item["SchoolName"].ToString();
                table.Operation = "<button class=\"btn btn-primary btn-sm\" data-id=\"" + item["CID"].ToString() + "\" data-action=\"edit\">"
                + "<span class=\"fa fa-pencil\"></span>编辑</button> "
                + "<button class=\"btn btn-primary btn-sm disabled\" type=\"button\" data-id=\"" + item["CID"].ToString() + "\">"
                + "<span class=\"fa fa-times  \"></span> 删除</button>";

                ClassTable.data.Add(table);
            }
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        SchoolTable.Add(new HPStudent.ViewModel.Student.SchoolCLass
            //        {
            //            CID = Convert.ToInt32(ds.Tables[0].Rows[i]["CID"].ToString()),
            //            CCode = ds.Tables[0].Rows[i]["CCode"].ToString(),
            //            CName = ds.Tables[0].Rows[i]["CName"].ToString(),
            //            SchoolName = ds.Tables[0].Rows[i]["SchoolName"].ToString(),
            //            Year = Convert.ToInt32(ds.Tables[0].Rows[i]["Year"].ToString()),

            //        });
            //    }
            //}

            return ClassTable;
        }

        /// <summary>
        /// 删除学生班级
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public static int DeleteStudentClass(string CID)
        {
            return Data.Common.StudentClass.DeleteStudentClass(CID);
        }
        /// <summary>
        /// 修改学生班级
        /// </summary>
        /// <param name="stuClass"></param>
        /// <param name="oldCID"></param>
        /// <returns></returns>
        public static int EditStudentClass(Entity.StudentClass stuClass)
        {
            return Data.Common.StudentClass.EditStudentClass(stuClass);
        }
        /// <summary>
        /// 添加学生班级
        /// </summary>
        /// <param name="stuClass"></param>
        /// <returns></returns>
        public static int AddStudentClass(Entity.StudentClass stuClass)
        {
            return Data.Common.StudentClass.AddStudentClass(stuClass);
        }

        public static HPStudent.ViewModel.Common.StudentClass GetStudentClassByCID(string CID)
        {
            Entity.StudentClass EntityStuClass = Data.Common.StudentClass.GetStudentClassByCID(CID);
            HPStudent.ViewModel.Common.StudentClass ViewStuClass = new ViewModel.Common.StudentClass();
            ViewStuClass.CCode = EntityStuClass.CCode;
            ViewStuClass.CName = EntityStuClass.CName;
            ViewStuClass.SchoolID = EntityStuClass.SchoolID;
            ViewStuClass.Year = EntityStuClass.Year;
            ViewStuClass.CID = EntityStuClass.CID;

            return ViewStuClass;
        }

        public static List<HPStudent.ViewModel.Student.SchoolCLass> GetClassListBySchoolIDAndYear(int SchoolID, int year,int start,int length)
        {
            int TotalRows = 0;
            List<HPStudent.ViewModel.Student.SchoolCLass> ViewClassList = new List<ViewModel.Student.SchoolCLass>();
            DataSet ds = Data.Common.StudentClass.GetStudentClassListBySchoolID(SchoolID, year,start ,length , out TotalRows);
        
            foreach (System.Data.DataRow item in ds.Tables[0].Rows)
            {
                HPStudent.ViewModel.Student.SchoolCLass table = new ViewModel.Student.SchoolCLass();
                table.CCode = item["CCode"].ToString();
                table.CID = Convert.ToInt32(item["CID"].ToString());
                table.Year = Convert.ToInt32(item["Year"].ToString());
                table.CName = item["CName"].ToString();
                table.SchoolName = item["SchoolName"].ToString();

                ViewClassList.Add(table);
               
            }
            return ViewClassList;
        }
    }
}
