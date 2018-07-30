using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectVModel = HPStudent.ViewModel.Projects;

namespace HPStudent.Business.Admin
{
    public class Projects
    {
        public static ProjectVModel.ProjectBook GetProjectBookByPID(int PID)
        {
            return Data.Admin.Projects.GetProjectBookByPID( PID);
        }
        public static List<HPStudent.Entity.Major> GetMajorList()
        {
            return Data.Admin.Projects.GetMajorList();
        }

        public static HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Projects.ProjectBook> GetProjectBookList(int mid, int start, int length)
        {

            int TotalRows = 0;
            //List<HPStudent.ViewModel.Projects.ProjectBook> pbList = new List<HPStudent.ViewModel.Projects.ProjectBook>();
            HPStudent.ViewModel.Common.Datatable<HPStudent.ViewModel.Projects.ProjectBook> ProjectTable = new ViewModel.Common.Datatable<ViewModel.Projects.ProjectBook>();
            

            //初始化返回Datatable行数
            
            ProjectTable.data =  Data.Admin.Projects.GetProjectBookList(mid, start, length, out TotalRows);

            ProjectTable.recordsTotal = TotalRows;
            ProjectTable.recordsFiltered = TotalRows;
            return ProjectTable;
            //return Data.Admin.Projects.GetProjectBookList(mid, start, length);
        }
         public static ViewModel.Common.RequestResult  DelProject(int PID)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            bool DelStatus = Data.Admin.Projects.DelProject(PID);
            if (DelStatus)
            {
                result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                result.ResultMsg = string.Format("项目删除成功！");
                
            }
            else
            {
                result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                result.ResultMsg = string.Format("项目删除失败，请稍后再试！");
            }
            return result;
        }
        public static ViewModel.Common.RequestResult AddProject(HPStudent.ViewModel.Projects.ProjectBook project)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            if (project.ProjectPic == "")
            {
                result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                result.ResultMsg = string.Format("添加失败,图片无法上传！");
                return result;
            }
            int iResult = Data.Admin.Projects.AddProject(project);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("项目【{0}】添加成功", project.ProjectName);
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("项目【{0}】添加失败！", project.ProjectName);
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加失败！");
                    break;

            }
            return result;
        }

        public static ViewModel.Common.RequestResult EditProject(HPStudent.ViewModel.Projects.ProjectBook project)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            if (project.ProjectPic == "")
            {
                result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                result.ResultMsg = string.Format("添加失败,图片无法上传！");
                return result;
            }
            int iResult = Data.Admin.Projects.EditProject(project);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("项目【{0}】修改成功", project.ProjectName);
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("项目【{0}】修改失败！", project.ProjectName);
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，修改失败！");
                    break;

            }
            return result;
        }


        public static ProjectVModel.ProjectDetail GetProjectDetailByPID(int PID)
        {
            return Data.Admin.Projects.GetProjectDetailByPID(PID);
        }

        public static List<HPStudent.Entity.ProjectItem> GetProjectItemListByPID(int PID)
        {
            return Data.Admin.Projects.GetProjectItemListByPID(PID);
        }

        public static ViewModel.Common.RequestResult AddProjectItem(Entity.ProjectItem myProjectItem)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            //初始化时间
            myProjectItem.CreateDate = DateTime.Now;
            myProjectItem.EditDate = DateTime.Now; 
            int iResult = Data.Admin.Projects.AddProjectItem(myProjectItem);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("课程添加成功！");
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("课程添加失败！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加失败！");
                    break;

            }
            return result;
        }

        public static ViewModel.Common.RequestResult EditProjectItem(Entity.ProjectItem projectitem)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            int iResult = Data.Admin.Projects.EditProjectItem(projectitem);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("课程修改成功！");
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("课程修改失败！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，修改失败！");
                    break;

            }
            return result;
        }
        public static Entity.ProjectItem GetProjectItemByID(int ID)
        {
            return Data.Admin.Projects.GetProjectItemByID(ID);
        }

        public static ViewModel.Common.RequestResult DelProjectItem(int ID)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            int iResult = Data.Admin.Projects.DelProjectItem(ID);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("课程删除成功！");
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("课程删除失败！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，删除失败！");
                    break;

            }
            return result;
        }
    }
}
