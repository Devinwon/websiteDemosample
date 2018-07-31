using System;
using System.Collections.Generic;

namespace HPStudent.ViewModel.Exercises
{
    public class QuestionTable
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int QID { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// 难度
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }

    }
}
