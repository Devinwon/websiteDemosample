//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class CompanyInfo
    {
        public int SID { get; set; }//对应学生信息表
        public string CompanyName { get; set; }//企业名称
        public string CompanyProfile { get; set; }//企业简介（1000中文）
        public string Address { get; set; }// 
        public string Scale { get; set; }// 
        public string TelPhone { get; set; }// 
        public string Email { get; set; }// 
        public string WebSite { get; set; }// 
        public string Agreement { get; set; }// 
        public int CreaterID { get; set; }// 
        public string CreaterName { get; set; }// 
        public DateTime CreateTime { get; set; }// 
    }
}
