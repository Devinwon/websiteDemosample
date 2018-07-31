//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Purpose
    {
        public int PID { get; set; }// 
        public int SID { get; set; }// 
        public string IndustryIDS { get; set; }// 
        public string IndustryText { get; set; }// 
        public string DutyIDS { get; set; }// 
        public string DutyText { get; set; }// 
        public string CityIDS { get; set; }// 
        public string CityText { get; set; }// 
        public int Money { get; set; }//0：面议，1：1500以下，2：1500-1999，3：2000-2999，4：3000-3999，5：4000-4999，6：5000-5999，7：6000-6999，8：7000-7999，9：8000-9999，10：10000-14999，11：15000-19999，12：20000-29999，13：30000-39999，14：40000-49999，15：50000以上
    }
}
