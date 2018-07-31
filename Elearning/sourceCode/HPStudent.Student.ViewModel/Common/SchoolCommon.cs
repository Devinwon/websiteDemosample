using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Common
{
    public class SchoolCommon
    {
        public int SchoolID { get; set; }//校区编号
        public int AreaID { get; set; }//所属地区
        public string SchoolName { get; set; }//校区名称
        public int DisplayOrder { get; set; }//显示顺序
    }
}
