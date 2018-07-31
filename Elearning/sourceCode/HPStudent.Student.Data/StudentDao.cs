using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
    public class StudentDao
    {
        //获取人才查询结果
        public static List<HPStudent.Student.ViewModel.Student.EnterpriseSearchBase> QueryStudentList(int start, int length, out int totalRows, HPStudent.Student.ViewModel.Student.EnterpriseSearchViewModel KeyWords)
        {
            string condition = "";
            if (KeyWords.City != 0)
            {
                condition += string.Format(" and City={0}", KeyWords.City);
            }
            if (!string.IsNullOrEmpty(KeyWords.Job))
            {
                condition += string.Format(" and HuntJob like'%{0}%'", KeyWords.Job);
            }
            if (!string.IsNullOrEmpty(KeyWords.SkillKey))
            {
                string[] arrskill = KeyWords.SkillKey.Split(',');
                condition += "and (";
                for (int i = 0; i < arrskill.Length; i++)
                {
                    if (i == arrskill.Length - 1)
                    {
                        condition += string.Format(" TechKeyWord like'%{0}%'", arrskill[i]);
                    }
                    else
                    {
                        condition += string.Format(" TechKeyWord like'%{0}%' or", arrskill[i]);
                    }
                }
                condition += ")";
            }
            if (KeyWords.state != 0)
            {
                condition += string.Format(" and Status={0}", KeyWords.state);
            }
            string strCount = string.Format(@" select Count(s.StudentID) from StudentInfo as s inner join StudentResume as sr 
                                               on s.StudentID=sr.SID where  sr.Status!=0 {0}", condition);
            totalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strCount));
            if (totalRows <= 0)
            {
                return new List<HPStudent.Student.ViewModel.Student.EnterpriseSearchBase>();
            }
            string sql = string.Format(@" select TOP {0} s.StudentID,s.RealName,s.Brithday,s.Sex,sr.City,sr.Status from StudentInfo as s
                                          inner join StudentResume as sr on s.StudentID=sr.SID WHERE s.StudentID not in (select 
                                          top {1} s1.StudentID from StudentInfo as s1 inner join StudentResume as sr1 on s1.StudentID=sr1.SID 
                                          where  sr1.Status!=0 {2}  ORDER BY s1.StudentID DESC) and sr.Status!=0 {2}
                                          ORDER BY s.StudentID DESC", length, start, condition);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Student.ViewModel.Student.EnterpriseSearchBase> lstRes = new List<HPStudent.Student.ViewModel.Student.EnterpriseSearchBase>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Student.ViewModel.Student.EnterpriseSearchBase objTemp = new HPStudent.Student.ViewModel.Student.EnterpriseSearchBase();
                    objTemp.Birthday = Convert.ToString(string.IsNullOrEmpty(item["Brithday"].ToString()) == true ? "" : (object)DateTime.Parse(item["Brithday"].ToString()).ToString("yyyy-MM-dd"));
                    objTemp.CityName = Convert.ToString(string.IsNullOrEmpty(item["City"].ToString()) == true ? "" : HPStudent.Student.Data.Common.StudentCommon.GetCityNameByCityID(Convert.ToInt32(item["City"].ToString())));
                    objTemp.Name = Convert.ToString(string.IsNullOrEmpty(item["RealName"].ToString()) == true ? "" : (object)item["RealName"]);
                    objTemp.Sex = HPStudent.Student.ViewModel.Student.EnterpriseSearchBase.ConvertSexFromBool(Convert.ToBoolean(item["Sex"]));
                    objTemp.CurrentStatus = Convert.ToString(HPStudent.Student.ViewModel.Resume.ResumeBasic.HuntJobStatus[Convert.ToInt32(item["Status"] == System.DBNull.Value ? 0 : item["Status"])]);
                    objTemp.StudentID = Convert.ToInt32(item["StudentID"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Student.ViewModel.Student.EnterpriseSearchBase>();
        }
    }
}
