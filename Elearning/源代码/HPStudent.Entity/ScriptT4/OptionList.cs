//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class OptionList
    {
        public int TID { get; set; }// 
        public string ListTypeCode { get; set; }//不同类型选项列表的 编码，例如 学历类型，薪资范围类型，工作性质，公司规模等等
        public string OptionValue { get; set; }// 
        public int SortCode { get; set; }// 
        public string CodeTypeName { get; set; }// 
        public string Remark { get; set; }// 
    }
}
