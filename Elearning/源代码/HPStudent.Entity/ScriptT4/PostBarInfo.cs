//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class PostBarInfo
    {
        public int PBID { get; set; }// 
        public string PBName { get; set; }// 
        public string PBCID { get; set; }// 
        public int StudentID { get; set; }// 
        public DateTime CreateDate { get; set; }// 
        public string PBSignature { get; set; }// 
        public string PBHeadPortrait { get; set; }// 
        public int Attention { get; set; }// 
        public int PostNumber { get; set; }// 
        public string DiscussTopic { get; set; }// 
    }
}
