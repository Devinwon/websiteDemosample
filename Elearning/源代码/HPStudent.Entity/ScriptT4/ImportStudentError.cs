//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class ImportStudentError
    {
        public int EID { get; set; }//错误编号
        public string RealName { get; set; }//学生姓名
        public string IDCard { get; set; }//身份证号
        public DateTime ETime { get; set; }//错误时间
        public string ErrorMessage { get; set; }//错误描述
    }
}
