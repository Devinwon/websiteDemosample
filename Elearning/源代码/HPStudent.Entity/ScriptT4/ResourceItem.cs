//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class ResourceItem
    {
        public int ID { get; set; }//子项目编号
        public int RID { get; set; }//资源编号
        public string CourseName { get; set; }//课程名称
        public string URL { get; set; }//资源链接
        public DateTime CreateDate { get; set; }//创建时间
        public DateTime EditDate { get; set; }//最后修改时间
    }
}
