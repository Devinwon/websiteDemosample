using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
    public class MessageReport
    {
        #region 消息举报添加
        /// <summary>
        /// 消息举报添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static HPStudent.Student.ViewModel.Common.RequestResult MessagesAdd(HPStudent.Entity.MessageReport model)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            //填充消息举报数据 如果是0为私信 找站内消息数据
            if (model.Category == 0)
            {
                HPStudent.Entity.Messages temp = new Entity.Messages();
                temp.MID = model.Tid;
                List<HPStudent.Entity.Messages> listMessage = HPStudent.Student.Data.Messages.MessagesListNotPage(temp);
                if (listMessage.Count > 0)
                {
                    model.ReportUID = listMessage[0].ReceiverID;
                    model.Reporter = listMessage[0].Receiver;
                    model.IsDo = 0;
                    model.ReportDate = DateTime.Now;
                    model.BeReporter = listMessage[0].Sender;
                    model.BeReportUID = listMessage[0].SenderID;
                }
                else
                {
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，更新操作处理失败！");
                    return result;
                }
            }
            int iResult = HPStudent.Student.Data.MessageReport.MessagesAdd(model);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("消息举报成功！");
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("消息举报失败！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，更新操作处理失败！");
                    break;
            }
            return result;
        }
        #endregion
        #region 消息举报获取
        /// <summary>
        /// 消息举报获取
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Entity.MessageReport> QueryMessageReportList(HPStudent.Entity.MessageReport keyWord, int length, int start)
        {
            int TotalRows = 0;
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Entity.MessageReport> SelectedTable = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Entity.MessageReport>();
            SelectedTable.data = HPStudent.Student.Data.MessageReport.MessagesReportList(keyWord, start, length, out TotalRows);
            //初始化返回Datatable行数
            SelectedTable.recordsTotal = TotalRows;
            SelectedTable.recordsFiltered = TotalRows;
            return SelectedTable;
        }
        #endregion
    }
}
