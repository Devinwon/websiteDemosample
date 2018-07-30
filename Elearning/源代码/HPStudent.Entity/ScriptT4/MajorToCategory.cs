//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class MajorToCategory
    {
        public int MID { get; set; }//专业分类编号
        public int CID { get; set; }//分类编号（课程编号）
    }
}
