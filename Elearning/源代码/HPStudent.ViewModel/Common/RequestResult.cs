using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.ViewModel.Common
{
    public class RequestResult
    {
        public enum StateCode { success = 0, fail = 1 }
        
        private StateCode resultstate;
        /// <summary>
        /// 请求返回的结果码
        /// </summary>
        public StateCode ResultState
        {
            set { this.resultstate = value; }
            get { return this.resultstate; }
        }

        /// <summary>
        /// 请求返回的消息内容
        /// </summary>
        public string ResultMsg { set; get; }

        /// <summary>
        /// 需要跳转的URL地址
        /// </summary>
        public string ReturnUrl { set; get; }
    }
}
