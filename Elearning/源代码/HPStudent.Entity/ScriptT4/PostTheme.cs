//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class PostTheme
    {
        public int PTID { get; set; }// 
        public int PBID { get; set; }// 
        public string PostThemeContent { get; set; }// 
        public string PostContent { get; set; }// 
        public DateTime PostDate { get; set; }// 
        public int StudentID { get; set; }// 
    }
}
