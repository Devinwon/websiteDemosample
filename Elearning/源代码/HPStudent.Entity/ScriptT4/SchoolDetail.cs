//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class SchoolDetail
    {
        public int Did { get; set; }//履历编号
        public int StudentID { get; set; }//学生编号
        public DateTime StartDate { get; set; }//学习开始时间
        public DateTime EndDate { get; set; }//学习结束日期
        public string School { get; set; }//毕业学校名称
        public string Major { get; set; }//所学专业
        public int Year { get; set; }//哪一年毕业
    }
}
