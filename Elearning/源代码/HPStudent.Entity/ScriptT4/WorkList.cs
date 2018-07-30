//-----------------------------------------------------------------------
//  此代码由T4模板根据数据库结构自动创建
//  创建时间：2016年10月29日 15:38:26 
//  创建人：涂建 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace HPStudent.Entity
{   
    public partial class WorkList
    {
        public int WorkID { get; set; }// 
        public int SID { get; set; }// 
        public string StartYear { get; set; }// 
        public string StartMonth { get; set; }// 
        public string EndYear { get; set; }// 
        public string EndMonth { get; set; }// 
        public int IsToday { get; set; }//0：不是，1：是到今天
        public string Company { get; set; }// 
        public string Department { get; set; }// 
        public string Station { get; set; }// 
        public int CompanySize { get; set; }//1：少于50人，2：50-150人，3：150-500人，4：500-1000人，5：1000-5000人，6：5000-10000人，7，10000人以上
        public int CompanyType { get; set; }//1：外资（欧美），2：外资（非欧美），3：合资，4：国企，5：民营企业，6：国内上市公司，7：外企代表处，8：政府机关，9：事业单位，10：非赢利机构，11：其他性质
        public int IndustryID { get; set; }// 
        public string Description { get; set; }// 
    }
}
