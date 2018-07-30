using System;
using System.Collections.Generic;
using StuVModel = HPStudent.Student.ViewModel    ;

namespace HPStudent.Student.Business
{
    public class Projects
    {
        /// <summary>
        /// 获得短训推荐项目列表
        /// </summary>
        /// <returns></returns>
        public static StuVModel.Common.GridView<StuVModel.Project.MainProject> GetProjectListByShort(int Mid, int start, int length)
        {
            int TotalRecords = 0;
            List<HPStudent.Entity.ProjectBook> SelectList = new List<Entity.ProjectBook>();

            StuVModel.Common.GridView<StuVModel.Project.MainProject> ProjectGridview = new StuVModel.Common.GridView<StuVModel.Project.MainProject>();
            ProjectGridview.data = new List<StuVModel.Project.MainProject>();
            //获得所有的短训推荐项目列表
            SelectList = HPStudent.Student.Data.Projects.GetProjectListByShort(start, length, out TotalRecords);
            
            //初始化返回Datatable行数
            ProjectGridview.recordsTotal = TotalRecords;
            if (SelectList == null)
            {
                return null;
            }
            foreach (HPStudent.Entity.ProjectBook item in SelectList)
            {
                StuVModel.Project.MainProject table = new StuVModel.Project.MainProject();
                table.yoyotui_url = "/Projects/Detail/?id=" + item.PID;
                table.yoyotui_img = item.ProjectPic;
                table.yoyotui_title = item.ProjectName;

                ProjectGridview.data.Add(table);
            }
            return ProjectGridview;
        }

        public static List<HPStudent.Entity.Major> GetMajorList()
        {
            return HPStudent.Student.Data.Projects.GetMajorList();
        }

        public static StuVModel.Common.GridView<StuVModel.Project.MainProject> GetStudentProjectList(int Mid, int start, int length)
        {
            int TotalRecords = 0;
            List<HPStudent.Entity.ProjectBook> SelectList = new List<Entity.ProjectBook>();

            StuVModel.Common.GridView<StuVModel.Project.MainProject> ProjectGridview = new StuVModel.Common.GridView<StuVModel.Project.MainProject>();
            ProjectGridview.data = new List<StuVModel.Project.MainProject>();
            if (Mid == 0)
            {
                SelectList = HPStudent.Student.Data.Projects.GetAllProjectBookList(start, length, out TotalRecords);
            }
            else
            {
                SelectList = HPStudent.Student.Data.Projects.GetProjectBookList(Mid, start, length, out TotalRecords);
            }
            //初始化返回Datatable行数
            ProjectGridview.recordsTotal = TotalRecords;
            if (SelectList == null)
            {
                return null;
            }
            foreach (HPStudent.Entity.ProjectBook item in SelectList)
            {
                StuVModel.Project.MainProject table = new StuVModel.Project.MainProject();
                table.yoyotui_pid = item.PID;
                table.yoyotui_img = item.ProjectPic;
                table.yoyotui_title = item.ProjectName;

                ProjectGridview.data.Add(table);
            }
            return ProjectGridview;
        }

        public static StuVModel.Project.DetailProject GetDetailProjectByPID(int PID)
        {
            return HPStudent.Student.Data.Projects.GetDetailProjectByPID(PID);
        }
    }
}
