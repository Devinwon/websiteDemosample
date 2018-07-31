//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class QA_Category
    {
        public int CID { get; set; }//题目分类编号
        public string CategoryName { get; set; }//分类名称
        public int QuestionNums { get; set; }//测试题目数
    }
}
