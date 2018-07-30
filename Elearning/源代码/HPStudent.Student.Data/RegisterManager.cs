using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
    public class RegisterManager
    {
        //获取岗位查询结果
        public static List<HPStudent.Student.ViewModel.Account.RegisterCheckInfo> QueryEnterpriseReviewList(int start, int length, out int totalRows, HPStudent.Student.ViewModel.Account.UserRegister KeyWords)
        {
            string condition = "";
            string strCount = "";
            string sql = "";
            if (KeyWords.IsActivated == 2)
            {
                strCount = string.Format(@" select count(s.StudentID) from StudentInfo as s inner join CompanyInfo as c on
                                               s.StudentID=c.SID where s.IsActivated={0} {1}", KeyWords.IsActivated, condition);
                sql = string.Format(@" select TOP {0} s.StudentID,s.Email,s.RealName,c.CompanyName,c.TelPhone from StudentInfo as s inner join CompanyInfo as c on
                                          s.StudentID=c.SID WHERE s.StudentID not in (select top {1} s1.StudentID  from StudentInfo as s1 inner join 
                                          CompanyInfo as c1 on s1.StudentID=c1.SID where s1.IsActivated={3} {2}  ORDER BY s1.StudentID DESC) and s.IsActivated={3} {2}
                                          ORDER BY c.CompanyName ASC,s.StudentID DESC", length, start, condition,KeyWords.IsActivated);

            }
            else 
            {
                strCount = string.Format(@" select count(s.StudentID) from StudentInfo as s inner join CompanyInfo as c on
                                               s.StudentID=c.SID where s.IsActivated=0 {0}", condition);
                sql = string.Format(@" select TOP {0} s.StudentID,s.Email,s.RealName,c.CompanyName,c.TelPhone from StudentInfo as s inner join CompanyInfo as c on
                                          s.StudentID=c.SID WHERE s.StudentID not in (select top {1} s1.StudentID  from StudentInfo as s1 inner join 
                                          CompanyInfo as c1 on s1.StudentID=c1.SID where s1.IsActivated=0 {2}  ORDER BY s1.StudentID DESC) and s.IsActivated=0 {2}
                                          ORDER BY c.CompanyName ASC,s.StudentID DESC", length, start, condition);
            }
            if (!string.IsNullOrEmpty(KeyWords.CompanyName))
            {
                condition += string.Format(" AND CompanyName like'%{0}%'", KeyWords.CompanyName);
            }
             
            totalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strCount));
            if (totalRows <= 0)
            {
                return new List<HPStudent.Student.ViewModel.Account.RegisterCheckInfo>();
            }
             
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Student.ViewModel.Account.RegisterCheckInfo> lstRes = new List<HPStudent.Student.ViewModel.Account.RegisterCheckInfo>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Student.ViewModel.Account.RegisterCheckInfo objTemp = new HPStudent.Student.ViewModel.Account.RegisterCheckInfo();
                    objTemp.CompanyName = Convert.ToString(string.IsNullOrEmpty(item["CompanyName"].ToString()) == true ? "" : (object)item["CompanyName"]);
                    objTemp.Email = Convert.ToString(string.IsNullOrEmpty(item["Email"].ToString()) == true ? "" : (object)item["Email"]);
                    objTemp.Phone = Convert.ToString(string.IsNullOrEmpty(item["TelPhone"].ToString()) == true ? "" : (object)item["TelPhone"]);
                    objTemp.RealName = Convert.ToString(string.IsNullOrEmpty(item["RealName"].ToString()) == true ? "" : (object)item["RealName"]);
                    objTemp.SID = Convert.ToInt32(item["StudentID"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Student.ViewModel.Account.RegisterCheckInfo>();
        }
        //获取学生查询结果
        public static List<HPStudent.Student.ViewModel.Account.RegisterCheckInfo> QueryStudentReviewList(int start, int length, out int totalRows, HPStudent.Student.ViewModel.Account.UserRegister KeyWords)
        {
            string condition = "";
            string strCount = "";
            string sql = "";
            if (!string.IsNullOrEmpty(KeyWords.RealName))
            {
                condition += string.Format(" AND RealName like'%{0}%'", KeyWords.RealName);
            }
            if (KeyWords.IsActivated == 2)
            {
                strCount = string.Format(@" select count(s.StudentID) from StudentInfo as s inner join StudentBaseInfo as sb on
                                               s.StudentID=sb.StudentID where s.IsActivated={0} {1}", KeyWords.IsActivated, condition);
                sql = string.Format(@"select TOP {0} s.StudentID,s.Email,s.RealName,sb.PersonMobile from StudentInfo as s inner join StudentBaseInfo as sb on
                                          s.StudentID=sb.StudentID WHERE s.StudentID not in (select top {1} s1.StudentID  from StudentInfo as s1 inner join 
                                          StudentBaseInfo as sb1 on s1.StudentID=sb1.StudentID where s1.IsActivated={2} {3}  ORDER BY s1.StudentID DESC) and s.IsActivated={2}
                                          {3} ORDER BY s.StudentID DESC", length, start, KeyWords.IsActivated, condition);
            

            }
            else 
            {
                strCount = string.Format(@" select count(s.StudentID) from StudentInfo as s inner join StudentBaseInfo as sb on
                                               s.StudentID=sb.StudentID where s.IsActivated=0 {0}", condition);
                sql = string.Format(@"select TOP {0} s.StudentID,s.Email,s.RealName,sb.PersonMobile from StudentInfo as s inner join StudentBaseInfo as sb on
                                          s.StudentID=sb.StudentID WHERE s.StudentID not in (select top {1} s1.StudentID  from StudentInfo as s1 inner join 
                                          StudentBaseInfo as sb1 on s1.StudentID=sb1.StudentID where s1.IsActivated=0 {2}  ORDER BY s1.StudentID DESC) and s.IsActivated=0
                                          {2} ORDER BY s.StudentID DESC", length, start, condition);
            
            }


            
            
            totalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strCount));
            if (totalRows <= 0)
            {
                return new List<HPStudent.Student.ViewModel.Account.RegisterCheckInfo>();
            }
            
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Student.ViewModel.Account.RegisterCheckInfo> lstRes = new List<HPStudent.Student.ViewModel.Account.RegisterCheckInfo>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Student.ViewModel.Account.RegisterCheckInfo objTemp = new HPStudent.Student.ViewModel.Account.RegisterCheckInfo();
                    objTemp.Email = Convert.ToString(string.IsNullOrEmpty(item["Email"].ToString()) == true ? "" : (object)item["Email"]);
                    objTemp.Phone = Convert.ToString(string.IsNullOrEmpty(item["PersonMobile"].ToString()) == true ? "" : (object)item["PersonMobile"]);
                    objTemp.RealName = Convert.ToString(string.IsNullOrEmpty(item["RealName"].ToString()) == true ? "" : (object)item["RealName"]);
                    objTemp.SID = Convert.ToInt32(item["StudentID"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Student.ViewModel.Account.RegisterCheckInfo>();
        }
        //审核通过  IsActivated 0：未激活，1：已激活，2：已审核
        public static int CheckPass(int SID)
        {

            string sql = @" UPDATE [dbo].[StudentInfo] SET  [IsActivated] = 2 WHERE Studentid=@Studentid  ";
            //需求确定之后，注意区分确定相应的用户账号角色RID
            SqlParameter[] param = { 
                                       new SqlParameter("@Studentid", SID)
                                   };
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            return iResult > 0 ? 0 : 1;
        }
    }
}
