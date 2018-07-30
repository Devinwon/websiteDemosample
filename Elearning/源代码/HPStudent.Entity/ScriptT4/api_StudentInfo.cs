//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class api_StudentInfo
    {
        public int Sid { get; set; }//学生编号
        public string RealName { get; set; }//学生真实姓名
        public string Mobile { get; set; }//学生手机号码
        public int IsExport { get; set; }//0：未导出，1：已导出
        public DateTime PostTime { get; set; }//提交时间
        public string Email { get; set; }//联系邮箱
    }
}
