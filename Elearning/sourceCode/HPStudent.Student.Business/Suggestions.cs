using System;
using System.Collections.Generic;
using HSVC = HPStudent.Student.ViewModel.Common;
using HSV = HPStudent.Student.ViewModel;

namespace HPStudent.Student.Business
{
    public class Suggestions
    {
        public static HSVC.Datatable<HPStudent.Entity.Suggestions> GetSuggestList(string StudentID, int start, int length)
        {
            HSVC.Datatable<HPStudent.Entity.Suggestions> SuggestTable = new HSVC.Datatable<HPStudent.Entity.Suggestions>();

            int TotalRows = 0;
            List<HPStudent.Entity.Suggestions> SuggestList = new List<Entity.Suggestions>();
            //初始化返回Datatable行数

            SuggestTable.data = HPStudent.Student.Data.Suggestions.GetSuggestList(StudentID, start, length, out TotalRows);

            SuggestTable.recordsTotal = TotalRows;
            SuggestTable.recordsFiltered = TotalRows;
            return SuggestTable;
        }

        public static HSVC.RequestResult AddSuggest(HPStudent.Entity.Suggestions SuggestItem)
        {
            int iResult = HPStudent.Student.Data.Suggestions.AddSuggest(SuggestItem);
            HSVC.RequestResult result = new HSVC.RequestResult();
            switch (iResult)
            {
                case 1:
                    result.ResultState = HSVC.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("投诉或建议提交成功！");
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

        public static HSVC.RequestResult ReplySuggest(HPStudent.Entity.SuggestItem SuggestItem)
        {
            int iResult = HPStudent.Student.Data.Suggestions.ReplySuggest(SuggestItem);
            HSVC.RequestResult result = new HSVC.RequestResult();
            if (iResult > 1)
            {
                iResult = 1;
            }
            switch (iResult)
            {
                case 1:
                    result.ResultState = HSVC.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("投诉或建议提交成功！");
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
            Entity.Suggestions mySuggest = HPStudent.Student.Data.Suggestions.GetSuggestBySID(SID);
            HSV.Service.SuggestDetailList myDetail = new HSV.Service.SuggestDetailList(mySuggest);
            myDetail.SuggestItemList = HPStudent.Student.Data.Suggestions.GetSuggestItemListBySID(SID);
            return myDetail;
        }
        public static Entity.Suggestions GetSuggestBySID(int SID) 
        {
            return HPStudent.Student.Data.Suggestions.GetSuggestBySID(SID);
        }
        //保存服务评星
        public static HSVC.RequestResult SuggestScoreSave(HPStudent.Entity.Suggestions suggestions) 
        {
            int iResult = HPStudent.Student.Data.Suggestions.SuggestScoreSave(suggestions);
            HSVC.RequestResult result = new HSVC.RequestResult();
            if (iResult > 1)
            {
                iResult = 1;
            }
            switch (iResult)
            {
                case 1:
                    result.ResultState = HSVC.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("评分成功！");
                    break;
                case 0:
                    result.ResultState = HSVC.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("评分失败！");
                    break;
                default:
                    result.ResultState = HSVC.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，添加失败！");
                    break;

            }

            return result;
        }
    }
}
