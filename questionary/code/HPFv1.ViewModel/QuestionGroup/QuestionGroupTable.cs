using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPFv1.ViewModel.QuestionGroup
{
   public class QuestionGroupTable
    {

        /// <summary>
        /// 问卷分组编号 
        /// </summary>
        public long GID { get; set; }


        /// <summary>
        /// 问卷编号 
        /// </summary>
        public long QID { get; set; }

       /// <summary>
       /// 问卷标题
       /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 问卷列表
        /// </summary>
        public List<HPFv1.Entity.Question> QuestionList { get; set; }

        /// <summary>
        /// 分组所属用户编号 
        /// </summary>
        public long UID { get; set; }

        /// <summary>
        /// 问卷列表
        /// </summary>
        public List<HPFv1.Entity.Users> UsersList { get; set; }

       /// <summary>
        /// 分组所属用户名称
       /// </summary>
        public string UName { get; set; }

        /// <summary>
        /// 分组名称 
        /// </summary>
        public string GroupName { get; set; }


        /// <summary>
        /// 分组密码，空表示无密码 
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// 分组创建时间 
        /// </summary>
        public DateTime CreateDate { get; set; }



        public string QuestionCode { get; set; }
       /// <summary>
       /// 操作
       /// </summary>
        public string Operation { get; set; }
    }
}
