//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class StudentResume
    {
        public int SID { get; set; }//学生编号
        public int City { get; set; }//期望城市
        public int Status { get; set; }//0：暂时不找，1：在职打算换工作，2：离职可快速到岗，3：实训，4：应届
        public string TechKeyWord { get; set; }// 
        public string HuntJob { get; set; }// 
    }
}
