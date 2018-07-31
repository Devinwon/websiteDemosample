//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Sys_Admin_SideBar
    {
        public int SID { get; set; }//导航编号
        public int PID { get; set; }//父级编号
        public string Text { get; set; }//菜单名称
        public string Controller { get; set; }//控制器名
        public string Action { get; set; }//动作名
        public int Order { get; set; }//排序
        public int ChildNum { get; set; }//包含子元素数量
        public string Icon { get; set; }//图标
    }
}
