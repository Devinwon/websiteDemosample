//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Sys_Log
    {
        public int LID { get; set; }//日志编号
        public string Date { get; set; }//记录时间
        public string Thread { get; set; }//线程
        public string Level { get; set; }//级别
        public string Logger { get; set; }//Logger
        public string Message { get; set; }//日志消息
        public string Exception { get; set; }//异常内容
    }
}
