using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace HPStudent.Student.ViewModel.Resume
{

  public  class ResumeBasic:HPStudent.Entity.StudentResume
    {
      public static Dictionary<int, string> HuntJobStatus = new Dictionary<int, string>() {
       { 0, "暂时不找" },
       { 1,"在职打算换工作"},
       {2,"离职可快速到岗"},
       { 3,"实训"},
       { 4,"应届"}
       };

      public static string ConvertGenderFromBool(bool bolGender)
      { 
       return bolGender?"女":"男";//对应数据库 1 ：女， 0：男，
      }

      public string CityName
      {
          get;
          set;
      }

      public string CurrentStatus
      {
          get;
          set;
      }

      public string RealName { get; set; }

      public string TelPhone { get; set; }

      public string Email { get; set; }

      public string Gender { get; set; }
    }
}
