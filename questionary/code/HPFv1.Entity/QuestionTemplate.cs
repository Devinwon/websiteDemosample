


using System;
using System.Collections.Generic;
namespace HPFv1.Entity
{   

        /// <summary>
        ///  
        /// </summary>
        public  class QuestionTemplate
        {
             
            /// <summary>
            /// 模板编号 
            /// </summary>
            public long TID  { get; set; }       

             
            /// <summary>
            /// 创建人编号 
            /// </summary>
            public long UID  { get; set; }       

             
            /// <summary>
            /// 模板标题 
            /// </summary>
            public string Title  { get; set; }       

             
            /// <summary>
            /// 模板内容 
            /// </summary>
            public string Content  { get; set; }       

             
            /// <summary>
            /// 创建时间 
            /// </summary>
            public DateTime CreateDate  { get; set; }       

             
            /// <summary>
            /// 修改时间 
            /// </summary>
            public DateTime EditDate  { get; set; }


            /// <summary>
            /// 题目总数
            /// </summary>
            public int TopicCount
            {
                get;
                set;

            }


            /// <summary>
            /// 修改人ID
            /// </summary>
            public long EditID { get; set; }


        }
}
