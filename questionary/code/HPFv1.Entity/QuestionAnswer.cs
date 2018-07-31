


using System;
using System.Collections.Generic;
namespace HPFv1.Entity
{   

        /// <summary>
        ///  
        /// </summary>
        public  class QuestionAnswer
        {
             
            /// <summary>
            ///   答案ID
            /// </summary>
            public long AID  { get; set; }       

             
            /// <summary>
            ///   题目编号
            /// </summary>
            public string Number  { get; set; }       

             
            /// <summary>
            ///   题目类型
            /// </summary>
            public string Type  { get; set; }       

             
            /// <summary>
            ///   题目选择项
            /// </summary>
            public string Choice  { get; set; }       

             
            /// <summary>
            ///   标题
            /// </summary>
            public string Title  { get; set; }       

             
            /// <summary>
            ///   题目内容
            /// </summary>
            public string Content  { get; set; }       

             
            /// <summary>
            ///   登录代码
            /// </summary>
            public string PCode  { get; set; }


            /// <summary>
            /// 多选题集合
            /// </summary>
            public Dictionary<string, string> CheckContent { get; set; }

        }
}
