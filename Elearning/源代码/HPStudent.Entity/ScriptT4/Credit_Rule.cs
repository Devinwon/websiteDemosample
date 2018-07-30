//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Credit_Rule
    {
        public int RID { get; set; }//规则编号
        public string RuleName { get; set; }//规则名称
        public string Action { get; set; }//动作，规则action唯一KEY
        public int Point { get; set; }//积分
        public int CycleType { get; set; }//奖励周期，0:一次;1:每天;2:整点;3:间隔分钟;4:不限;
        public int CycleTime { get; set; }//自定义间隔时间据，根据周期选择不同而不同
        public string Remark { get; set; }//备注
    }
}
