using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Account
{
    public class UserRegister
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        /// <summary>
        /// 0:企业；1：学生
        /// </summary>
        public int RegisterType { get; set; }

        public int IsActivated { get; set; }
    }
    public class RegisterCheckInfo
    {
        public int SID { get; set; }
        public string Email { get; set; }
        public string RealName { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
