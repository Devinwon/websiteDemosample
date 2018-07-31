//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class UserOnline
    {
        public int UserID { get; set; }// 
        public string UserName { get; set; }// 
        public int RoleID { get; set; }// 
        public DateTime LastRequestTime { get; set; }// 
        public string ClientIP { get; set; }// 
        public string LastRequestPath { get; set; }// 
    }
}
