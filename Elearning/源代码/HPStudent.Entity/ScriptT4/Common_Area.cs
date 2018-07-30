//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Common_Area
    {
        public int AreaID { get; set; }//地区编号
        public int ParentAID { get; set; }//上级地区编号
        public string AreaName { get; set; }//地区名称
        public int Level { get; set; }//级别，1：省级，2：市级，3：县级，4：乡镇
        public int DisplayOrder { get; set; }//显示顺序
    }
}
