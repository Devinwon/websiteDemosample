//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Suggestions
    {
        public int SID { get; set; }//班级编号
        public int SchoolID { get; set; }//校区编号
        public int StudentID { get; set; }//入学年份
        public int Status { get; set; }//0：未处理，1：处理中，2：已完结
        public int Category { get; set; }//0：其他，1：课程，2：寝室，3：教学
        public int IsSuggest { get; set; }//0：建议，1：投诉
        public DateTime PostDate { get; set; }//班级编码
        public string Title { get; set; }//投诉或建议的标题
        public string Content { get; set; }//班级名称
        public string SuggestImg { get; set; }//反馈的截图
        public string sResult { get; set; }//处理结果描述
        public int TeacherID { get; set; }//老师编号
        public int LastReply { get; set; }//0：学生，1：老师
        public string ScoreDetail { get; set; }// 
        public int ScoreStar { get; set; }// 
    }
}
