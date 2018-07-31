//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class StudentProjectExp
    {
        public int PID { get; set; }//项目经历编号
        public int SID { get; set; }//学生编号
        public string ProjectName { get; set; }//项目名称
        public string Position { get; set; }//你的职责（最多60字）
        public DateTime startDate { get; set; }//起始时间
        public DateTime EndDate { get; set; }//结束时间
        public string JobContent { get; set; }//项目描述
    }


    



}
