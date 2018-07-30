//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class StudentInfo
    {
        public int StudentID { get; set; }//学生编号
        public int SchoolID { get; set; }//所属校区
        public string RealName { get; set; }//学生真实姓名
        public string Email { get; set; }//邮箱
        public int EmailStatus { get; set; }//邮箱状态 0：未验证，1：已验证
        public string Password { get; set; }//登录密码
        public int Sex { get; set; }//学生性别
        public DateTime Brithday { get; set; }//出生日期
        public string Avatar { get; set; }//用户头像
        public int StartYear { get; set; }//入学年份
        public int NewPM { get; set; }//新短消息数
        public int Credits { get; set; }//厚溥币（积分）
        public int IsActivated { get; set; }//账号是否激活，0：未激活，1：已激活
        public int Rank { get; set; }//用户等级
        public DateTime LastLoginTime { get; set; }//上次登录时间
        public int CID { get; set; }//所在班级编号
        public int RoleID { get; set; }// 
        public DateTime CreateTime { get; set; }//创建时间
        public int OnlineTime { get; set; }//在线时长,统计的秒数为单位的时长
    }
}
