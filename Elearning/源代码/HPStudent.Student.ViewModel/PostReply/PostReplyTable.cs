using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.PostReply
{
    public class PostReplyTable : HPStudent.Entity.PostReply
    {
        public string RealName { get; set; }

        /// <summary>
        /// 更新或删除操作是否成功
        /// </summary>
        public int IsSuccess { get; set; }

        public int LoginStudentID { get; set; }

    }
}
