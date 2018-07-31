using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSV = HPStudent.ViewModel;
using HSVC = HPStudent.ViewModel.Common;

namespace HPStudent.Business.Admin
{
    public class Service
    {
        //public static List<PVM.Service.Suggest> GetSuggestList() {
        //    return Data.Admin.Service.GetSuggestList();
        //}

        public static HSVC.Datatable<HSV.Service.Suggest> GetSuggestList(int start, int length)
        {
            int TotalRows = 0;
            HSVC.Datatable<HSV.Service.Suggest> SuggestTable = new HSVC.Datatable<HSV.Service.Suggest>();
            SuggestTable.data = Data.Admin.Service.GetSuggestList(start, length, out TotalRows);

            SuggestTable.recordsTotal = TotalRows;
            SuggestTable.recordsFiltered = TotalRows;
            return SuggestTable;
        }

        public static HSVC.RequestResult ReplySuggest(HPStudent.Entity.SuggestItem SuggestItem)
        {
            int iResult = Data.Admin.Service.ReplySuggest(SuggestItem);
            HSVC.RequestResult result = new HSVC.RequestResult();
            if (iResult > 1)
            {
                iResult = 1;
            }
            switch (iResult)
            {
                case 1:
                    result.ResultState = HSVC.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("投诉或建议回复成功！");
                    break;
                case 0:
                    result.ResultState = HSVC.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("提交失败，出现了数据库错误，请联系管理员！");
                    break;
                default:
                    result.ResultState = HSVC.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加失败！");
                    break;

            }

            return result;
        }

        public static HSV.Service.SuggestDetailList GetSuggestDetail(int SID)
        {
            Entity.Suggestions mySuggest = Data.Admin.Service.GetSuggestBySID(SID);
            HSV.Service.SuggestDetailList myDetail = new HSV.Service.SuggestDetailList(mySuggest);
            myDetail.SuggestItemList = Data.Admin.Service.GetSuggestItemListBySID(SID);
            myDetail.Status = mySuggest.Status;
            return myDetail;
        }
        //事件完结方法
        public static HPStudent.ViewModel.Common.RequestResult SuggestEndEvent(int PID)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            int iResult = HPStudent.Data.Admin.Service.SuggestEndEvent(PID);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("设定成功！");
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("设定失败！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，移动操作处理失败！");
                    break;
            }
            return result;
        }

    }
}
