//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class Credit_Log
    {
        public int StudentID { get; set; }//学生编号
        public int LogID { get; set; }//积分日志编号
        public int UID { get; set; }//所属用户
        public string PointItemName { get; set; }//积分项名称
        public int Point { get; set; }//积分
        public DateTime DateLine { get; set; }//发生时间
        public string Remark { get; set; }//备注
    }
}
