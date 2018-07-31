


using System;
using System.Collections.Generic;
namespace HPFv1.Entity
{   

        /// <summary>
        ///  
        /// </summary>
        public partial class Question
        {
             
            /// <summary>
            /// 问卷编号 
            /// </summary>
            public long QID  { get; set; }       

             
            /// <summary>
            /// 创建人编号 
            /// </summary>
            public long UID  { get; set; }       

             
            /// <summary>
            /// 问卷模板编号 
            /// </summary>
            public long TemplateID  { get; set; }       

             
            /// <summary>
            /// 问卷标题 
            /// </summary>
            public string Title  { get; set; }       

             
            /// <summary>
            /// 问卷描述 
            /// </summary>
            public string Description  { get; set; }       

             
            /// <summary>
            /// 生成HTML格式的问卷调查表 
            /// </summary>
            public string QuestionHtml  { get; set; }       

             
            /// <summary>
            /// 生成HTML格式的问卷结果 
            /// </summary>
            public string QuestionResult  { get; set; }       

             
            /// <summary>
            /// 开始时间 
            /// </summary>
            public DateTime StartDate  { get; set; }       

             
            /// <summary>
            /// 结束时间 
            /// </summary>
            public DateTime EndDate  { get; set; }       

             
            /// <summary>
            /// 总问卷数 
            /// </summary>
            public int QNum  { get; set; }       

             
            /// <summary>
            /// 是否已经创建 
            /// </summary>
            public byte IsCraete  { get; set; }       

             
            /// <summary>
            /// 是否已经生成结果 
            /// </summary>
            public byte IsResult { get; set; }

            /// <summary>
            /// 生成的问卷代码
            /// </summary>
            public string QuestionCode { get; set; }
             
        }
}
