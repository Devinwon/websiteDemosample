


using System;
using System.Collections.Generic;
namespace HPFv1.Entity
{   

        /// <summary>
        ///  
        /// </summary>
        public partial class UsersMenu
        {
             
            /// <summary>
            /// 菜单编号 
            /// </summary>
            public int MID  { get; set; }       

             
            /// <summary>
            /// 父级编号 
            /// </summary>
            public int PID  { get; set; }       

             
            /// <summary>
            /// 菜单名称 
            /// </summary>
            public string MenuName  { get; set; }       

             
            /// <summary>
            /// 控制器名 
            /// </summary>
            public string ControllerName  { get; set; }       

             
            /// <summary>
            /// 动作名 
            /// </summary>
            public string ActionName  { get; set; }       

             
            /// <summary>
            /// 排序 
            /// </summary>
            public int Sort { get; set; }       

             
            /// <summary>
            /// 包含子元素个数 
            /// </summary>
            public int ChildNum  { get; set; }       

             
            /// <summary>
            /// 图标 
            /// </summary>
            public string Icon  { get; set; }       

             
        }
}
