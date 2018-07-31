//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Messages
    {
        public int MID { get; set; }//通知编号
        public int SenderID { get; set; }//发送者编号
        public string Sender { get; set; }//发送者姓名
        public int ReceiverID { get; set; }//接收者编号
        public string Receiver { get; set; }//接收者姓名
        public string Title { get; set; }//消息标题
        public string Body { get; set; }//消息内容
        public int IsRead { get; set; }//是否已读，0：未读，1：已读
        public DateTime DateCreated { get; set; }//创建日期
        public string IP { get; set; }//IP地址
        public int Type { get; set; }//消息类型，0：私信，1：系统通知
    }
}
