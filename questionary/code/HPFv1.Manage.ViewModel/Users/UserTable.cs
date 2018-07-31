using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPFv1.Manage.ViewModel.Users
{
   public class UserTable
    {
        /// <summary>
        ///   
        /// </summary>
        public long UID { get; set; }


        /// <summary>
        ///   
        /// </summary>
        public string NickName { get; set; }


        /// <summary>
        ///   
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        ///   
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        ///   
        /// </summary>
        public DateTime CreateDate { get; set; }


        /// <summary>
        ///   
        /// </summary>
        public DateTime LastLogin { get; set; }


        /// <summary>
        ///   
        /// </summary>
        public string LastIP { get; set; }


       /// <summary>
       /// 操作
       /// </summary>
        public string Operation { get; set; }
    }
}
