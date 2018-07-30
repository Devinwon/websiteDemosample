//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class ProjectBook
    {
        public int PID { get; set; }//项目编号
        public int MID { get; set; }//所属专业编号
        public string ProjectName { get; set; }//项目名称
        public int TeacherID { get; set; }//课程讲师
        public int ClassHour { get; set; }//课时数量
        public string UseTechnology { get; set; }//涉及到的技术
        public DateTime CreateDate { get; set; }//创建时间
        public DateTime EditDate { get; set; }//最后修改时间
        public string ProjectPic { get; set; }//项目图片
        public string ProjectDesc { get; set; }// 
        public int Status { get; set; }// 
        public int ShowPart { get; set; }// 
    }
}
