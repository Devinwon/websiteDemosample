//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class StudentBaseInfo
    {
        public int StudentID { get; set; }//学生编号
        public string StudentCode { get; set; }//学生的学籍号
        public string Class { get; set; }//班级
        public string ClassCode { get; set; }//班级代码
        public int MajorID { get; set; }//所学专业
        public string HighSchool { get; set; }//高中
        public string HomeAddress { get; set; }//家庭住址
        public string PersonMobile { get; set; }//个人联系电话
        public string HomeMobile { get; set; }//家庭联系电话
        public string Nation { get; set; }//民族
        public string QQ { get; set; }//联系QQ
        public string IDCard { get; set; }//身份证号
        public int Unify { get; set; }//是否统招1：统招 0：非统招
        public int Education { get; set; }// 
    }
}
