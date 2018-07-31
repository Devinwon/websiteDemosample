//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class MessageReport
    {
        public int RID { get; set; }// 
        public int Tid { get; set; }// 
        public int ReportUID { get; set; }// 
        public string Reporter { get; set; }// 
        public int BeReportUID { get; set; }// 
        public string BeReporter { get; set; }// 
        public string Body { get; set; }// 
        public int IsDo { get; set; }// 
        public DateTime ReportDate { get; set; }// 
        public string IP { get; set; }// 
        public int Category { get; set; }// 
    }
}
