//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class ManagerInfo
    {
        public int MID { get; set; }//管理员编号
        public string ManagerName { get; set; }//管理员名称
        public string Password { get; set; }//登录密码
        public int Level { get; set; }//等级，0：普通管理员，1：超级管理员
        public DateTime LastLoginTime { get; set; }//最后登录时间
        public string LastLoginIP { get; set; }//最后登录ip
        public string RndCheckCode { get; set; }//随机验证码
    }
}
