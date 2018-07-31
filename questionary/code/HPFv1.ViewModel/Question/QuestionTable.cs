using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPFv1.ViewModel.Question
{
   public class QuestionTable
    {

        /// <summary>
        /// 问卷编号 
        /// </summary>
        public long QID { get; set; }


        /// <summary>
        /// 创建人编号 
        /// </summary>
        public long UNAME { get; set; }


        /// <summary>
        /// 问卷模板编号 
        /// </summary>
        public long TemplateID { get; set; }


       /// <summary>
        /// 问卷模板名称
       /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// 问卷标题 
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// 问卷描述 
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 生成HTML格式的问卷调查表 
        /// </summary>
        public string QuestionHtml { get; set; }


        /// <summary>
        /// 生成HTML格式的问卷结果 
        /// </summary>
        public string QuestionResult { get; set; }


        /// <summary>
        /// 开始时间 
        /// </summary>
        public DateTime StartDate { get; set; }


        /// <summary>
        /// 结束时间 
        /// </summary>
        public DateTime EndDate { get; set; }


        /// <summary>
        /// 总问卷数 
        /// </summary>
        public int QNum { get; set; }


        /// <summary>
        /// 是否已经创建 
        /// </summary>
        public byte IsCraete { get; set; }


        /// <summary>
        /// 是否已经生成结果 
        /// </summary>
        public byte IsResult { get; set; }

       /// <summary>
       /// 问卷登录代码
       /// </summary>
        public string QuestionCode { get; set; }

       /// <summary>
       /// 操作
       /// </summary>
        public string Operation { get; set; }

       /// <summary>
       /// 模版集合
       /// </summary>
        public List<HPFv1.Entity.QuestionTemplate> QuestionTemplateList { get; set; }

    }
}
