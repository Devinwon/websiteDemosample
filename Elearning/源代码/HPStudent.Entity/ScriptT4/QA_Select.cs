//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class QA_Select
    {
        public int QID { get; set; }//题目编号
        public int CID { get; set; }//题目分类
        public string Title { get; set; }//题目标题
        public string A { get; set; }//答案A
        public string B { get; set; }//答案B
        public string C { get; set; }//答案C
        public string D { get; set; }//答案D
        public string Answer { get; set; }//正确答案
        public DateTime CreateDate { get; set; }//创建时间
        public int Creater { get; set; }//创建人
        public int Level { get; set; }//难度分1～5级
        public string AnswerAnalysis { get; set; }//答案分析
    }
}
