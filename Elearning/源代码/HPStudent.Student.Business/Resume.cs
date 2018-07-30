using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPStudent.Entity;
using StuVModel=HPStudent.Student.ViewModel;
using StuData=HPStudent.Student.Data;

namespace HPStudent.Student.Business
{
   public  class Resume
    {
       public static StuVModel.Resume.ResumeMain GetStudentResumeMainInfoByStudentID(int MID)
       {
           return StuData.Resume.GetMainResumeInfoByStudentID(MID);
       }
       
       public static HPStudent.Student.ViewModel.Common.RequestResult UpdateOrCreateResumeBaisic(StudentResume model)
       {
           ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
           //注意对截止 时间的处理；
           int iResult = HPStudent.Student.Data.Resume.UpdateOrCreateResumeBasic(model);
           switch (iResult)
           {
               case 0:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                   result.ResultMsg = string.Format("求职意向添加成功！");
                   break;
               case 1:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                   result.ResultMsg = string.Format("求职意向更新成功！");
                   break;
               default:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("出现异常错误，操作处理失败！");
                   break;
           }
           return result;
       }
       
       public static HPStudent.Student.ViewModel.Common.RequestResult AddWorkExp(StudentWorkExp model)
       {
           ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
           //注意对截止 时间的处理；
           int iResult = HPStudent.Student.Data.Resume.InsertNewWorkExpRecord(model);
           switch (iResult)
           {
               case 0:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                   result.ResultMsg = string.Format("工作经验添加成功！");
                   break;
               case 1:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("工作经验添加失败！");
                   break;
               default:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("出现异常错误，添加操作处理失败！");
                   break;
           }
           return result;
       }

       public static HPStudent.Student.ViewModel.Common.RequestResult UpdateWorkExp(StudentWorkExp model)
       {
           ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
           //注意对截止 时间的处理；
           int iResult = HPStudent.Student.Data.Resume.UpdateWorkExpRecordByID(model);
           switch (iResult)
           {
               case 0:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                   result.ResultMsg = string.Format("工作经验修改成功！");
                   break;
               case 1:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("工作经验修改失败！");
                   break;
               default:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("出现异常错误，更新操作处理失败！");
                   break;
           }
           return result;
       }

       public static HPStudent.Student.ViewModel.Common.RequestResult DeleteWorkExp(int id)
       {
           ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
           int iResult = HPStudent.Student.Data.Resume.DeleteStudentWorkExpByID(id);
           switch (iResult)
           {
               case 0:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                   result.ResultMsg = string.Format("工作经验删除成功！");
                   break;
               case 1:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("工作经验删除失败！");
                   break;
               default:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("出现异常错误，删除操作处理失败！");
                   break;
           }
           return result;
       }

       public static HPStudent.Student.ViewModel.Common.RequestResult AddProjectExp(StudentProjectExp model)
       {
           ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
           int iResult = HPStudent.Student.Data.Resume.InsertNewStudentProjectExpRecord(model);
           switch (iResult)
           {
               case 0:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                   result.ResultMsg = string.Format("项目经验添加成功！");
                   break;
               case 1:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("项目经验添加失败！");
                   break;
               default:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("出现异常错误，添加操作处理失败！");
                   break;
           }
           return result;
       }

       public static HPStudent.Student.ViewModel.Common.RequestResult UpdateProject(StudentProjectExp model)
       {
           ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
           int iResult = HPStudent.Student.Data.Resume.UpdateStudentProjectExpByID(model);
           switch (iResult)
           {
               case 0:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                   result.ResultMsg = string.Format("项目经验修改成功！");
                   break;
               case 1:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("项目经验修改失败！");
                   break;
               default:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("出现异常错误，更新操作处理失败！");
                   break;
           }
           return result;
       }

       public static HPStudent.Student.ViewModel.Common.RequestResult DeleteProjectExp(int id)
       {
           ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
           int iResult = HPStudent.Student.Data.Resume.DeleteStudentProjectExpByID(id);
           switch (iResult)
           {
               case 0:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                   result.ResultMsg = string.Format("项目经验删除成功！");
                   break;
               case 1:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("项目经验删除失败！");
                   break;
               default:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("出现异常错误，删除操作处理失败！");
                   break;
           }
           return result;
       }

       public static HPStudent.Student.ViewModel.Common.RequestResult AddEducationExp(SchoolDetail model)
       {
           ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
           int iResult = HPStudent.Student.Data.Resume.InsertNewEduRecord(model);
           switch (iResult)
           {
               case 0:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                   result.ResultMsg = string.Format("教育经历添加成功！");
                   break;
               case 1:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("教育经历添加失败！");
                   break;
               default:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("出现异常错误，添加操作处理失败！");
                   break;
           }
           return result;
       }

       public static HPStudent.Student.ViewModel.Common.RequestResult UpdateEducationExp(SchoolDetail model)
       {
           ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
           int iResult = HPStudent.Student.Data.Resume.UpdateEduRecordByID(model);
           switch (iResult)
           {
               case 0:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                   result.ResultMsg = string.Format("教育经历修改成功！");
                   break;
               case 1:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("教育经历修改失败！");
                   break;
               default:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("出现异常错误，更新操作处理失败！");
                   break;
           }
           return result;
       }

       public static HPStudent.Student.ViewModel.Common.RequestResult DeleteEducationExpByID(int id)
       {
           ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
           int iResult = HPStudent.Student.Data.Resume.DeleteEduRecordByID(id);
           switch (iResult)
           {
               case 0:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                   result.ResultMsg = string.Format("教育经历删除成功！");
                   break;
               case 1:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("教育经历删除失败！");
                   break;
               default:
                   result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                   result.ResultMsg = string.Format("出现异常错误，删除操作处理失败！");
                   break;
           }
           return result;
       }

    }
}
