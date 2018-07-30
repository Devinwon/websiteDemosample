//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class AdminRole
    {
        public int RID { get; set; }//角色编号
        public string RoleName { get; set; }//角色名称
        public int SortCode { get; set; }//排序码
    }
}
