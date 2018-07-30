//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class StudentScore
    {
        public int SID { get; set; }//学生编号
        public int Term { get; set; }//学期
        public float Examination1 { get; set; }//认证考试（笔试）
        public float Examination2 { get; set; }//认证考试（机试）
        public string Evaluate { get; set; }//评价
    }
}
