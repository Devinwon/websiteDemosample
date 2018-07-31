using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.ViewModel.Student
{
    public class EnterpriseSearchBase : HPStudent.Entity.StudentResume
    {
        public string Name { get; set; }//学生姓名
        public static string ConvertSexFromBool(bool bolGender)
        {
            return bolGender ? "女" : "男";//对应数据库 1 ：女， 0：男，
        }
        public string Sex { get; set; }//性别
        public string Birthday { get; set; }//生日
        public string CityName { get; set; }//城市名称
        public string CurrentStatus { get; set; }//当前状态
        public int StudentID { get; set; }//学生ID
    }
    //企业搜索人才传入参数
    public class EnterpriseSearchViewModel
    {
        public int City { get; set; }//城市
        public string SkillKey { get; set; }//技术关键词
        public int state { get; set; }//当前状态
        public string Job { get; set; }//求职岗位
    }
}
