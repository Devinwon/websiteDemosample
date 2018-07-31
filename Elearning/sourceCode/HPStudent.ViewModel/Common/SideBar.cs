using System;
using System.ComponentModel.DataAnnotations;

namespace HPStudent.ViewModel.Common
{
    public class SideBar
    {
        public int SID { get; set; }
        public int PID { get; set; }
        public string Text { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Order { get; set; }
        public int ChildNum { get; set; }
        public string Icon { get; set; }
    }
}
