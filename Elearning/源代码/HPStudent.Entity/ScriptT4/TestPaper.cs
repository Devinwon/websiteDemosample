//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class TestPaper
    {
        public int StudentID { get; set; }//学生编号
        public int SID { get; set; }//创建人
        public DateTime CreateDate { get; set; }//创建时间
        public string SelectIDS { get; set; }//选择题，选择题题目编号，以逗号分隔
        public string SelectRightItem { get; set; }//选择题正确答案，以逗号分隔
        public string SelectAnswer { get; set; }//选择题提交的答案，以逗号分隔
        public string FillIDS { get; set; }//填空题，填空题题目编号，以逗号分隔
        public string QuestionIDS { get; set; }//问答题，问答题题目编号，以逗号分隔
        public int IsComplete { get; set; }//是否答题完成，0：未提交，1：完成已提交
        public float Score { get; set; }//测试成绩
        public string Range { get; set; }//考试范围
        public DateTime EndDate { get; set; }//完成日期
        public int TID { get; set; }// 
    }
}
