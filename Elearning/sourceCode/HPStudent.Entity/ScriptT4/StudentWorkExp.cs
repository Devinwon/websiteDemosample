//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class StudentWorkExp
    {
        public int WID { get; set; }//工作经历编号
        public int SID { get; set; }//学生编号
        public string Company { get; set; }//公司名称
        public string Position { get; set; }//当时所任职位
        public DateTime startDate { get; set; }//在职起始时间
        public DateTime EndDate { get; set; }//在职起始时间
        public string JobContent { get; set; }//工作内容
    }
}
