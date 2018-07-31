//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class SuggestItem
    {
        public int ID { get; set; }//子项目编号
        public int SID { get; set; }//建议与投诉父项编号
        public string Content { get; set; }//子项内容
        public DateTime CreateDate { get; set; }//创建时间
        public int StudentID { get; set; }//学生编号
        public int IsStudent { get; set; }//0：是学生回复，1：是老师回复
        public int TeacherID { get; set; }//老师编号
    }
}
