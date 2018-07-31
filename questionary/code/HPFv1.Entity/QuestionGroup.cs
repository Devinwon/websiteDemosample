using System;
using System.Collections.Generic;
namespace HPFv1.Entity
{   

        /// <summary>
        ///  
        /// </summary>
        public class QuestionGroup
        {
             
            /// <summary>
            /// 问卷分组编号 
            /// </summary>
            public long GID  { get; set; }       

             
            /// <summary>
            /// 问卷编号 
            /// </summary>
            public long QID  { get; set; }       

             
            /// <summary>
            /// 分组所属用户编号 
            /// </summary>
            public long UID  { get; set; }       

             
            /// <summary>
            /// 分组名称 
            /// </summary>
            public string GroupName  { get; set; }       

             
            /// <summary>
            /// 分组密码，空表示无密码 
            /// </summary>
            public string Password  { get; set; }       

             
            /// <summary>
            /// 分组创建时间 
            /// </summary>
            public DateTime CreateDate  { get; set; }       

             
        }
}
