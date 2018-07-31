using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
    public class Messages
    {
        #region 站内消息添加
        /// <summary>
        /// 站内消息添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static HPStudent.Student.ViewModel.Common.RequestResult MessagesAdd(HPStudent.Entity.Messages model)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            int iResult = HPStudent.Student.Data.Messages.MessagesAdd(model);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("站内消息发送成功！");
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("站内消息发送失败！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，更新操作处理失败！");
                    break;
            }
            return result;
        }
        #endregion

        #region 站内消息获取
        /// <summary>
        /// 站内消息获取
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Entity.Messages> QueryMessageList(HPStudent.Entity.Messages keyWord, int length, int start)
        {
            int TotalRows = 0;
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Entity.Messages> SelectedTable = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Entity.Messages>();
            SelectedTable.data = HPStudent.Student.Data.Messages.MessagesList(keyWord, start, length, out TotalRows);
            //初始化返回Datatable行数
            SelectedTable.recordsTotal = TotalRows;
            SelectedTable.recordsFiltered = TotalRows;
            return SelectedTable;
        }
        #endregion
        #region 获取未读的邮件数量
        /// <summary>
        /// 获取未读的邮件数量
        /// </summary>
        public static int GetNotReaderMessage(int ReceiverID)
        {
            return HPStudent.Student.Data.Messages.GetNotReaderMessage(ReceiverID);
        }
        #endregion
        #region 获取当前消息详情并设为已读
        /// <summary>
        ///获取当前消息详情并设为已读
        /// </summary>
        public static List<HPStudent.Entity.Messages> MessagesListNotPage(HPStudent.Entity.Messages keyWord)
        {
            //设置已读
            HPStudent.Student.Data.Messages.SetMessagesIsReader(keyWord);
            return HPStudent.Student.Data.Messages.MessagesListNotPage(keyWord);
        }
        #endregion
        #region 获取企业发布简历信息
        public static List<HPStudent.Entity.Messages> getMessagesList(int SenderID,int ReceiverID) 
        {
            return HPStudent.Student.Data.Messages.GetList("SenderID='" + SenderID + "' and ReceiverID='" + ReceiverID + "' order by MID desc ");
        
        }


        #endregion
    }
}
