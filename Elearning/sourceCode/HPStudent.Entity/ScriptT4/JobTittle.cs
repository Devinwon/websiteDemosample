//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class JobTittle
    {
        public int JID { get; set; }// 
        public int SID { get; set; }// 
        public string Name { get; set; }// 
        public string City { get; set; }// 
        public string SalaryRange { get; set; }// 
        public string WorkType { get; set; }// 
        public string DegreeRequired { get; set; }// 
        public string ExperienceRequired { get; set; }// 
        public string JobDescription { get; set; }// 
    }
}
