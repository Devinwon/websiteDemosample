using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
    public class Job
    {
        //返回职位搜索
        public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Job.JobList> QueryJobList(HPStudent.Student.ViewModel.Job.JobListParameter keyWord, int length, int start)
        {
            int TotalRows = 0;
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Job.JobList> SelectedTable = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Job.JobList>();
            SelectedTable.data = HPStudent.Student.Data.Job.QueryJobList(keyWord, start, length, out TotalRows);
            //初始化返回Datatable行数
            SelectedTable.recordsTotal = TotalRows;
            SelectedTable.recordsFiltered = TotalRows;
            return SelectedTable;
        }

        public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Job.JobList> GetSeeSenderCompanyTable(HPStudent.Student.ViewModel.Job.JobListParameter keyWord, string SenderID, int length, int start)
        {
            int TotalRows = 0;
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Job.JobList> SelectedTable = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Job.JobList>();
            SelectedTable.data = HPStudent.Student.Data.Job.GetSeeSenderCompanyTable(keyWord, SenderID, start, length, out TotalRows);
            //初始化返回Datatable行数
            SelectedTable.recordsTotal = TotalRows;
            SelectedTable.recordsFiltered = TotalRows;
            return SelectedTable;
        }

        //显示职位详情
        public static HPStudent.Student.ViewModel.Job.JobList GetJobInfoByID(int JId)
        {
            return HPStudent.Student.Data.Job.GetJobInfoByID(JId);
        }
        //简历申请
        public static HPStudent.Student.ViewModel.Common.RequestResult SendResume(HPStudent.Student.ViewModel.Job.JobSendViewModel jobsend)
        {
            HPStudent.Student.ViewModel.Common.RequestResult result = new HPStudent.Student.ViewModel.Common.RequestResult();
            //审核是否已经投递过简历(30天内不能重复投递)
            if (HPStudent.Student.Data.Job.CheckIsSend(jobsend) > 0)
            {
                result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                result.ResultMsg = string.Format("30天内不能重复投递该职位！");
                return result;
            }
            //开始投递简历
            int iResult = HPStudent.Student.Data.Job.SendResume(jobsend);
            switch (iResult)
            {
                case 0:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("简历投递成功！");
                    break;
                case 1:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("简历投递失败！");
                    break;
                default:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，操作处理失败！");
                    break;
            }
            return result;
        }
        //获取职位信息
        public static List<HPStudent.Entity.JobTittle> GetJobInfoSID(int SID)
        {
            List<HPStudent.Entity.JobTittle> li = HPStudent.Student.Data.Job.GetJobInfoBySID(SID);
            return li;
        }
        //编辑职位
        public static HPStudent.Student.ViewModel.Common.RequestResult EditJobItem(HPStudent.Entity.JobTittle jobTittle)
        {
            HPStudent.Student.ViewModel.Common.RequestResult result = new HPStudent.Student.ViewModel.Common.RequestResult();
            int iResult;
            if (jobTittle.JID == 0)
            {
                iResult = HPStudent.Student.Data.Job.AddJobItem(jobTittle);
            }
            else
            {
                iResult = HPStudent.Student.Data.Job.UpdateJobItem(jobTittle);
            }
            switch (iResult)
            {
                case 0:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("添加成功！");
                    break;
                case 1:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("添加失败！");
                    break;
                default:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，操作处理失败！");
                    break;
            }
            return result;
        }
    }
}
