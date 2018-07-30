//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class FamilyRelation
    {
        public int StudentID { get; set; }//学生编号
        public int Relation { get; set; }//关系1：父亲 2：母亲 3：哥哥 4：弟弟 5：姐姐 6：妹妹 7：叔叔 8：姨 9：姑姑 10：舅舅 11：伯父
        public DateTime EndDate { get; set; }//学习结束日期
        public string School { get; set; }//毕业学校名称
        public string Reterence { get; set; }//证明人
    }
}
