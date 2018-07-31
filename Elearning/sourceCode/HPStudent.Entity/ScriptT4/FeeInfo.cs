//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class FeeInfo
    {
        public int FeeID { get; set; }//费用编号
        public int SID { get; set; }//学生编号
        public int Year { get; set; }//学年
        public string NeedFee { get; set; }//应缴费用
        public string PaidFee { get; set; }//实缴费用
        public int IsCheck { get; set; }//0：学生上传；1：老师审核通过；2：退回
    }
}
