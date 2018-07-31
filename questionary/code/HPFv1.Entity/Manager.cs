using System;
using System.Collections.Generic;
namespace HPFv1.Entity
{   

        /// <summary>
        ///  
        /// </summary>
        public partial class Manager
        {
             
            /// <summary>
            /// 管理员编号 
            /// </summary>
            public long MID  { get; set; }       

             
            /// <summary>
            /// 登陆账号 
            /// </summary>
            public string Account  { get; set; }       

             
            /// <summary>
            /// 登陆密码 
            /// </summary>
            public string Password  { get; set; }       

             
            /// <summary>
            /// 最后登陆IP 
            /// </summary>
            public string LastIP  { get; set; }       

             
            /// <summary>
            /// 最后登陆时间 
            /// </summary>
            public DateTime LastDate  { get; set; }       

             
            /// <summary>
            /// 昵称 
            /// </summary>
            public string NickName  { get; set; }       



             
        }
}
