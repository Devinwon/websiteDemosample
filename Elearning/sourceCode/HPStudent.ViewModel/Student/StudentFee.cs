using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HPStudent.ViewModel.Student
{
    public class StudentFee
    {
        public string FeeID { get; set; }//费用编号
        public string SID { get; set; }//学生编号
        public string Year { get; set; }//费用学年
        public string NeedFee { get; set; } //应交费用
        public string PaidFee { get; set; }//实交费用
        public string IsCheck { get; set; }//是否审核
        public string Attachment { get; set; }//费用编号
        public string Dateline { get; set; }//费用编号
        public string FAID { get; set; }//费用编号
        public string Fee { get; set; }//费用编号
        public string FeeDescription { get; set; }//费用编号
        public string FeeTitle { get; set; }//费用编号
    }
}
