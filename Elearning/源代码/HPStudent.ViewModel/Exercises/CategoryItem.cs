using System;
using System.Collections.Generic;

namespace HPStudent.ViewModel.Exercises
{
    /// <summary>
    /// 课程项
    /// </summary>
    public class CategoryItem
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary>
        public int CID { get; set; }

        /// <summary>
        /// 是否包含该课程
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
