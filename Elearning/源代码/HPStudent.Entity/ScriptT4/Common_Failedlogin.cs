//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Common_Failedlogin
    {
        public string IP { get; set; }// 
        public string Email { get; set; }// 
        public int Count { get; set; }// 
        public DateTime Lastupdate { get; set; }// 
    }
}
