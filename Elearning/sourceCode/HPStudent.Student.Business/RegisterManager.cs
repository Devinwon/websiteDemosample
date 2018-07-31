using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
    public class RegisterManager
    {
        //获得企业注册信息
        public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Account.RegisterCheckInfo> QueryEnterpriseReviewList(int length, int start, HPStudent.Student.ViewModel.Account.UserRegister KeyWords)
        {
            int TotalRows = 0;
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Account.RegisterCheckInfo> SelectedTable = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Account.RegisterCheckInfo>();
            SelectedTable.data = HPStudent.Student.Data.RegisterManager.QueryEnterpriseReviewList(start, length, out TotalRows, KeyWords);
            //初始化返回Datatable行数
            SelectedTable.recordsTotal = TotalRows;
            SelectedTable.recordsFiltered = TotalRows;
            return SelectedTable;
        }
        //获得学生注册信息
        public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Account.RegisterCheckInfo> QueryStudentReviewList(int length, int start, HPStudent.Student.ViewModel.Account.UserRegister KeyWords)
        {
            int TotalRows = 0;
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Account.RegisterCheckInfo> SelectedTable = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Account.RegisterCheckInfo>();
            SelectedTable.data = HPStudent.Student.Data.RegisterManager.QueryStudentReviewList(start, length, out TotalRows, KeyWords);
            //初始化返回Datatable行数
            SelectedTable.recordsTotal = TotalRows;
            SelectedTable.recordsFiltered = TotalRows;
            return SelectedTable;
        }
        //审核通过
        public static HPStudent.Student.ViewModel.Common.RequestResult CheckPass(int SID)
        {
            HPStudent.Student.ViewModel.Common.RequestResult result = new HPStudent.Student.ViewModel.Common.RequestResult();
            int iResult = HPStudent.Student.Data.RegisterManager.CheckPass(SID);
            switch (iResult)
            {
                case 0:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("审核成功！");
                    break;
                case 1:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("审核失败！");
                    break;
                default:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，审核操作处理失败！");
                    break;
            }
            return result;
        }
    }
}
