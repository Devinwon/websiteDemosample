//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class ClassGroup
    {
        public int GID { get; set; }// 
        public string GroupName { get; set; }// 
        public int StudentID { get; set; }// 
        public int TeacherID { get; set; }// 
    }
}
