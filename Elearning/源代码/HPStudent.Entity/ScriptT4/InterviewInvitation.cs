//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class InterviewInvitation
    {
        public int ID { get; set; }//表自身唯一主键索引 id， 支持自增
        public int SID { get; set; }//面试邀请的接收者 学生 主键 sid studentid
   
        public int SenderID { get; set; }//发送者 企业的id 
        public int JobTitleID { get; set; }//对应企业发布的 岗位id，由此关联招聘岗位的信息  参考 jobtittle 表
        public string Content { get; set; }// 
        public string IsRead { get; set; }//标记发送的 面试邀请 接收学生是否已读 1 ： yes  0： no
        public DateTime SendDate { get; set; }//邀请函发送时间
    }
}
