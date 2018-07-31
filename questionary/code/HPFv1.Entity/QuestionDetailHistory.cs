


using System;
using System.Collections.Generic;
namespace HPFv1.Entity
{   

        /// <summary>
        ///  
        /// </summary>
        public partial class QuestionDetailHistory
        {
             
            /// <summary>
            /// 问卷详情表 
            /// </summary>
            public long DID  { get; set; }       

             
            /// <summary>
            /// 主问卷编号 
            /// </summary>
            public long QID  { get; set; }       

             
            /// <summary>
            /// 问卷分组编号 
            /// </summary>
            public long GID  { get; set; }       

             
            /// <summary>
            /// 问卷答案内容 
            /// </summary>
            public string Answer  { get; set; }       

             
            /// <summary>
            /// 问卷提交时间 
            /// </summary>
            public DateTime PostDate  { get; set; }       

             
            /// <summary>
            /// 提交IP 
            /// </summary>
            public string PostIP  { get; set; }       

             
        }
}
