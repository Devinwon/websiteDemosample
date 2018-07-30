//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class QA_Question
    {
        public int QID { get; set; }// 
        public int CID { get; set; }// 
        public string Title { get; set; }// 
        public string Answer { get; set; }// 
        public string K1 { get; set; }// 
        public string K2 { get; set; }// 
        public string K3 { get; set; }// 
        public string K4 { get; set; }// 
        public string K5 { get; set; }// 
        public string K6 { get; set; }// 
        public string K7 { get; set; }// 
        public string K8 { get; set; }// 
        public string K9 { get; set; }// 
        public string K10 { get; set; }// 
        public DateTime CreateDate { get; set; }// 
        public int Creater { get; set; }// 
    }
}
