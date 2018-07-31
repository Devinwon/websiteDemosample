//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Common_School
    {
        public int SchoolID { get; set; }//校区编号
        public int AreaID { get; set; }//所属地区
        public string SchoolName { get; set; }//校区名称
        public int DisplayOrder { get; set; }//显示顺序
    }
}
