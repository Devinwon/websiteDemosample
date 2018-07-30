//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Sys_Menu
    {
        public int MID { get; set; }//菜单编号
        public int PID { get; set; }//父级菜单编号
        public string MenuName { get; set; }//菜单名称
        public int SortCode { get; set; }//排序码
        public string Controller { get; set; }//控制器名
        public string Action { get; set; }//动作名
        public int ChildNum { get; set; }//包含子元素数量
        public string Icon { get; set; }//图标
        public int Category { get; set; }//类别：0：用户端菜单 1：管理员端菜单
    }
}
