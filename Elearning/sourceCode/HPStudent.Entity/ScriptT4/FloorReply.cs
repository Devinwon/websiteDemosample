//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class FloorReply
    {
        public int FRID { get; set; }// 
        public int PRID { get; set; }// 
        public DateTime CreateDate { get; set; }// 
        public string FRContent { get; set; }// 
        public int ReplyManID { get; set; }// 
        public int ByReplyManID { get; set; }// 
    }
}
