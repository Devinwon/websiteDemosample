//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class SendResume
    {
        public int ID { get; set; }//唯一主键
        public int SenderID { get; set; }//学生信息表主键StudentInfo
        public int EID { get; set; }//学生信息表主键StudentInfo
        public int JobTitleID { get; set; }//对应企业发布的 岗位id，由此关联招聘岗位的信息  参考 jobtittle 表
        public int IsRead { get; set; }//标记发送的  接收企业是否已读 1 ： yes  0： no
        public DateTime SendDate { get; set; }//简历发送时间
    }
}
