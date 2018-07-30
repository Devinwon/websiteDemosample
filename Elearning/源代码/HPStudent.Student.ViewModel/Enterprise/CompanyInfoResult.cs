using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Enterprise
{
    public class CompanyInfoResult
    {
        public int SID { get; set; }//对应学生信息表
        public string CompanyName { get; set; }//企业名称
        public string CompanyProfile { get; set; }//企业简介（1000中文）
        public string Address { get; set; }// 
        public string Scale { get; set; }// 
        public string TelPhone { get; set; }// 
        public string Email { get; set; }// 
        public string WebSite { get; set; }// 
        public string Agreement { get; set; }// 
        public int CreaterID { get; set; }// 
        public string CreaterName { get; set; }// 
        public DateTime CreateTime { get; set; }// 
        public List<string> ScaleList { get; set; }
    }
}
