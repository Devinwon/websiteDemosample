using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HPStudent.Entity;
using System.Text;

namespace HPStudent.Student.Data
{
    public class Account
    {  /// <summary>
        /// 学生登录验证
        /// </summary>
        /// <param name="student">学生信息</param>
        /// <returns></returns>
        public static HPStudent.Student.ViewModel.Account.UserLogin IsUserLogin(HPStudent.Student.ViewModel.Account.UserLogin student)
        {
            string sql = @" IF EXISTS (SELECT StudentID FROM StudentInfo WHERE Email=@Email and [Password]=@Password)
                                    BEGIN
                                        select * from StudentInfo where Email=@Email and [Password]=@Password;
	                                    UPDATE StudentInfo SET LastLoginTime = GETDATE() WHERE Email=@Email
                                    END";

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@Email", student.Email), 
                new SqlParameter("@Password", student.Password)
            };

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras);
            HPStudent.Student.ViewModel.Account.UserLogin UserInfo;

            if (ds != null && ds.Tables.Count > 0)
            {
                UserInfo = new HPStudent.Student.ViewModel.Account.UserLogin();
                UserInfo.StudentID = ds.Tables[0].Rows[0]["StudentID"].ToString();
                UserInfo.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                UserInfo.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                UserInfo.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                UserInfo.IsActivated = Convert.ToInt32(ds.Tables[0].Rows[0]["IsActivated"]);
                UserInfo.RoleID = Convert.ToInt32(string.IsNullOrEmpty(ds.Tables[0].Rows[0]["RoleID"].ToString()) == true ? "0" : ds.Tables[0].Rows[0]["RoleID"].ToString());
                UserInfo.LastLoginTime = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LastLoginTime"].ToString()) == true ? DateTime.Now : DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
                UserInfo.OnlineTime = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["OnlineTime"].ToString()) == true ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["OnlineTime"]);
                return UserInfo;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 验证用户登录信息是否正确
        /// </summary>
        /// <param name="Email">用户的Email</param>
        /// <param name="Password">用户的登录密码</param>
        /// <returns></returns>
        public static bool CheckUserLogin(string Email, string Password)
        {
            string sql = " select COUNT(1) from StudentInfo where Email=@Email and [Password]=@Password";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@Email", Email), 
                new SqlParameter("@Password", Password)
            };

            return 0 < (int)SqlHelper.ExecuteScalar(CommandType.Text, sql, paras) ? true : false;
        }
        /// <summary>
        /// 验证用户是否有权限访问页面
        /// </summary>
        /// <param name="Email">用户的Email</param>
        /// <param name="Password">用户的登录密码</param>
        /// <returns></returns>
        public static int CheckUserRight(string Email, string Password, string Controller, string Action)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(" declare @roleid int;");
            sqlstr.Append(" declare @Navigation varchar(300);");
            sqlstr.Append(" if exists(select roleid from StudentInfo where Email=@Email and [Password]=@Password)");
            sqlstr.Append(" begin");
            //验证菜单是否能访问
            sqlstr.Append(" select @roleid=roleid from StudentInfo where Email=@Email and [Password]=@Password;");
            sqlstr.Append(" select @Navigation=Navigation from UserRole_NavPowrer where RID=@roleid;");
            //排除非菜单动作
            sqlstr.Append(" if not exists(select MID from Sys_Menu as m ");
            sqlstr.Append(" where  m.Action=@Action and m.Controller=@Controller)");
            sqlstr.Append(" begin");
            sqlstr.Append(" select 0");
            sqlstr.Append(" end");
            sqlstr.Append(" else");
            sqlstr.Append(" begin");
            //验证菜单
            sqlstr.Append(" if exists(");
            sqlstr.Append(" select * from Sys_Menu as m ");
            sqlstr.Append(" where (exists(select col from dbo.f_splitSTR(@Navigation,',') where col=m.MID))");
            sqlstr.Append(" and m.Action=@Action and m.Controller=@Controller)");
            sqlstr.Append(" begin");
            //验证成功
            sqlstr.Append(" select 0;");
            sqlstr.Append(" end");
            sqlstr.Append(" else");
            sqlstr.Append(" begin");
            //无菜单访问权限
            sqlstr.Append(" select 2;");
            sqlstr.Append(" end");
            sqlstr.Append(" end");
            sqlstr.Append(" end");
            sqlstr.Append(" else");
            sqlstr.Append(" begin");
            //账号密码不存在
            sqlstr.Append(" select 1;");
            sqlstr.Append(" end");
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@Email", Email), 
                new SqlParameter("@Password", Password),
                new SqlParameter("@Controller", Controller),
                new SqlParameter("@Action", Action),
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlstr.ToString(), paras));
        }
        /// <summary>
        /// 企业注册
        /// </summary>
        /// <param name="studentRegist"></param>
        /// <returns></returns>
        public static int RegistEnterpriseAccount(HPStudent.Student.ViewModel.Account.UserRegister studentRegist)
        {
            string sql = @"SP_ResigerEnterprise ";
            //需求确定之后，注意区分确定相应的用户账号角色RID
            SqlParameter[] param = { 
                                       new SqlParameter("@ReturnInfo",SqlDbType.Int),
                                       new SqlParameter("@EnterpriseEmailName",studentRegist.Email),
                                       new SqlParameter("@EnterprisePassword", studentRegist.Password),
                                       new SqlParameter("@EnterpriseRealName", studentRegist.RealName),
                                       new SqlParameter("@EnterpriseName", studentRegist.CompanyName),
                                       new SqlParameter("@EnterprisePhone", studentRegist.Phone),
                                       new SqlParameter("@RoleId", "0")                                       
                                   };
            param[0].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, param);
            return Convert.ToInt32(param[0].Value);
        }
        /// <summary>
        /// 学生注册
        /// </summary>
        /// <param name="studentRegist"></param>
        /// <returns></returns>
        public static int RegistStudentAccount(HPStudent.Student.ViewModel.Account.UserRegister studentRegist)
        {
            string sql = @"SP_ResigerStudent ";
            int RoleID = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "select OptionValue from OptionList where ListTypeCode = 'Student' "));
            //需求确定之后，注意区分确定相应的用户账号角色RID
            SqlParameter[] param = { 
                                       new SqlParameter("@ReturnInfo",SqlDbType.Int),
                                       new SqlParameter("@StudentEmailName",studentRegist.Email),
                                       new SqlParameter("@StudentPassword", studentRegist.Password),
                                       new SqlParameter("@StudentRealName", studentRegist.RealName),
                                       new SqlParameter("@StudentPhone", studentRegist.Phone),
                                       new SqlParameter("@RoleId", RoleID)                                       
                                   };    
            param[0].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, param);
            return Convert.ToInt32(param[0].Value);
        }
    }
}
