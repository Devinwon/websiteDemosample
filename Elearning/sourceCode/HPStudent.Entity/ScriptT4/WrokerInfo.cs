//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class WrokerInfo
    {
        public int SID { get; set; }//就业时学生编号自动导入过来
        public int IsMarried { get; set; }// 
        public string Mobile { get; set; }// 
        public int Status { get; set; }//0：在职，看看机会   1：在职，急寻新工作   2：在职，暂无跳槽打算   3：离职，正在找工作
        public string AreaText { get; set; }// 
        public int AreaID { get; set; }// 
    }
}
