using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Job
{
    public class Job
    {
    }
    /// <summary>
    /// 岗位信息搜索实体
    /// </summary>
    public class JobList
    {
        public int SId { get; set; }
        public int JId { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Salaryrange { get; set; }
        public string Worktype { get; set; }
        public string Degreerequired { get; set; }
        public string Experiencerequired { get; set; }
        public string JobDescription { get; set; }

        public string IsRead { get; set; }

    }
    public class JobListParameter
    {
        public int Jid { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Salaryrange { get; set; }
        public string CompanyName { get; set; }
        public int Sid { get; set; }
    }
    //职位申请传入参数结构
    public class JobSendViewModel
    {
        public int SId { get; set; }//公司编号
        public int JId { get; set; }//职位编号
        public int SenderID { get; set; }//简历投递者ID
    }
    //职位申请回传参数
    public class JobSendResultModel
    {
        private int Id;
        private int SenderId;
        private int EId;
        private int JobTitleid;
        private int IsRead;
        private DateTime SendDate;
    }
}
