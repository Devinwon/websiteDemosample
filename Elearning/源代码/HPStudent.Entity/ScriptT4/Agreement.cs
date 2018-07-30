//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Agreement
    {
        public int AID { get; set; }// 
        public DateTime DateLine { get; set; }// 
        public string Attachment { get; set; }// 
        public int Status { get; set; }//0：未审核，1：已审核
    }
}
