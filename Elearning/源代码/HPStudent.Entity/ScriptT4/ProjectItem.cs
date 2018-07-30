//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class ProjectItem
    {
        public int ID { get; set; }//子项目编号
        public int PID { get; set; }//项目编号
        public string ProjectName { get; set; }//项目名称
        public string PPT { get; set; }//PPT教辅
        public string WORD { get; set; }//WORD教辅
        public string PDF { get; set; }//PDF教辅
        public string Video { get; set; }//视频教辅
        public string URL { get; set; }// 
        public DateTime CreateDate { get; set; }//创建时间
        public DateTime EditDate { get; set; }//最后修改时间
    }
}
