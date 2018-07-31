using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPFv1.ViewModel.QuestionTemplate
{
   public class QuestionTemplateTable
    {
        /// <summary>
        /// 模板编号 
        /// </summary>
        public long TID { get; set; }


        /// <summary>
        /// 创建人ID
        /// </summary>
        public long UID { get; set; }


       /// <summary>
       /// 创建人名称
       /// </summary>
        public string UName { get; set; }
       /// <summary>
       /// 修改人ID
       /// </summary>
        public long EditID { get; set; }

       /// <summary>
       /// 修改人名称
       /// </summary>
        public string EditName { get; set; }

        /// <summary>
        /// 模板标题 
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// 模板内容 
        /// </summary>
        public string Content { get; set; }


        /// <summary>
        /// 创建时间 
        /// </summary>
        public DateTime CreateDate { get; set; }


        /// <summary>
        /// 修改时间 
        /// </summary>
        public DateTime EditDate { get; set; }

       /// <summary>
       /// 操作
       /// </summary>
        public string Operation { get; set; }

    }
}
