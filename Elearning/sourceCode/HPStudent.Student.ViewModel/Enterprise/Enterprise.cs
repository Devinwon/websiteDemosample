using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Enterprise
{
    public class Enterprise
    {
        public Enterprise()
        {
            this.Profile = new Entity.CompanyInfo();
        }
        public int EID { get; set; }
        public string Email { get; set; }
        public string EmailStatus { get; set; }
        public bool IsActivated { get; set; }
        public string Avatar { get; set; }
        public int RoleID { get; set; }
        public HPStudent.Entity.CompanyInfo Profile { get; set; }
    }

    public class EnterpriseItem
    {
        public int EID { get; set; }
        public string EnterpriseName { get; set; }
        public string Agreement { get; set; }
    }

    public class EnterpriseWithJobs
    {
        public EnterpriseWithJobs()
        {
            this.Basic = new Entity.CompanyInfo();
            this.CompanyJobTittle = new List<Entity.JobTittle>();
            this.IsImported = false;
        }
        public bool IsImported { get; set; }//是否已导入,默认为false
        public string AccountEmail { get; set; }
        public HPStudent.Entity.CompanyInfo Basic { get; set; }
        public List<HPStudent.Entity.JobTittle> CompanyJobTittle { get; set; }
    }
}
