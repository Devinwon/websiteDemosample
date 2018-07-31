using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Exercises
{
    public class StatisticsQuestion_Select
    {
        public int CID { get; set; }
        public int QuestionNums { get; set; }
        public string CategoryName { get; set; }
        public int TestNum { get; set; }
    }
    public class StatisticsQuestion_SelectViewModel
    {
        public int StudentID { get; set; }
        public int CID { get; set; }
        public int MID { get; set; }
    }
    /// <summary>
    /// 练习选择题实体（每次加载10个）
    /// </summary>
    public class QA_SelectResultModel
    {
        /// <summary>
        /// 题目编号
        /// </summary>
        public int ROWID { get; set; }
        public int QID { get; set; }//题目编号
        public int CID { get; set; }//题目分类
        public string Title { get; set; }//题目标题
        public string A { get; set; }//答案A
        public string B { get; set; }//答案B
        public string C { get; set; }//答案C
        public string D { get; set; }//答案D
        public int AnswerType { get; set; }//问题类别1：单选题
        public string AnswerAnalysis { get; set; }//答案分析
    }
    /// <summary>
    /// 练习选择题实体（每次加载10个）
    /// </summary>
    public class QA_SelectResultNotRowidModel
    {
        public int QID { get; set; }//题目编号
        public int CID { get; set; }//题目分类
        public string Title { get; set; }//题目标题
        public string A { get; set; }//答案A
        public string B { get; set; }//答案B
        public string C { get; set; }//答案C
        public string D { get; set; }//答案D
        public string Answer { get; set; }//问题类别1：单选题
        public string AnswerAnalysis { get; set; }//答案分析
    }
    /// <summary>
    /// 获得练习选择题的条件实体
    /// </summary>
    public class QA_SelectViewModel
    {
        /// <summary>
        /// 当前点击序号（查询时以此为起点往后顺延10的区间）
        /// </summary>
        public int RowID { get; set; }
        public int CID { get; set; }
    }
    /// <summary>
    /// 已经作答的JSON属性格式
    /// </summary>
    public class Answer_Json
    {
        public int QID { get; set; }
        public int RowID { get; set; }
        public string TrueAnswer { get; set; }
        public string YourAnswer { get; set; }
        public bool IsTrue { get; set; }
        public string AnswerAnalysis { get; set; }
    }
}

