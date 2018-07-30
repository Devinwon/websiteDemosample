using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.ViewModel.Sys_Menu
{
    public class SysMenuModel
    {
    }
    public class SysMenuViewModel
    {
        public int SearchType { get; set; }//查询类别；0:父菜单,1:子菜单
        public int MID { get; set; }//菜单ID
        public int PID { get; set; }//父菜单ID
        public int MoveType { get; set; }//移动类型0:向上 1：向下
        public int Category { get; set; }//菜单类别 0:用户端菜单 1：管理员端菜单
        public string MenuName { get; set; }//菜单名称
    }
}
