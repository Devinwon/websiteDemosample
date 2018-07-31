//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class FeeAttachment
    {
        public int FAID { get; set; }//费用附件编号
        public int FeeID { get; set; }//费用表编号
        public string FeeDescription { get; set; }//缴费备注
        public string FeeTitle { get; set; }//缴费科目
        public string Attachment { get; set; }//附件
        public string Fee { get; set; }//附件金额
        public DateTime Dateline { get; set; }//上传日期
    }
}
