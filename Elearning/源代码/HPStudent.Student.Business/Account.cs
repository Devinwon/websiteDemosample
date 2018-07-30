using System;
using System.Collections.Generic;
using HPStudent.Entity;
using HPStudent.Student.ViewModel;
using HPStudent.Student.Data;

namespace HPStudent.Student.Business
{
    public class Account
    {
        public static HPStudent.Student.ViewModel.Account.UserLogin IsUserLogin(HPStudent.Student.ViewModel.Account.UserLogin UserInfo)
        {

            HPStudent.Student.ViewModel.Account.UserLogin manager = new HPStudent.Student.ViewModel.Account.UserLogin();
            return HPStudent.Student.Data.Account.IsUserLogin(UserInfo);
        }

        public static bool CheckUserLogin(string Email, string Password)
        {
            return HPStudent.Student.Data.Account.CheckUserLogin(Email, Password);
        }
        //验证用户是否有权限访问页面
        public static int CheckUserRight(string Email, string Password, string Controller, string Action)
        {
            return HPStudent.Student.Data.Account.CheckUserRight(Email, Password, Controller, Action);
        }
        /// <summary>
        /// 账号注册
        /// </summary>
        /// <param name="studentRegist">注册信息</param>
        /// <param name="RegistType">0：企业注册，1学生注册</param>
        /// <returns></returns>
        public static HPStudent.Student.ViewModel.Common.RequestResult RegistInfo(HPStudent.Student.ViewModel.Account.UserRegister studentRegist)
        {
            HPStudent.Student.ViewModel.Common.RequestResult result = new HPStudent.Student.ViewModel.Common.RequestResult();
            int iResult = -1;
            if (string.IsNullOrEmpty(studentRegist.Email) || string.IsNullOrEmpty(studentRegist.Password))
            {
                result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                result.ResultMsg = string.Format("用户名或密码无效!");
                return result;
            }
            //判断是否是企业，如果是，验证企业名称
            if (studentRegist.RegisterType == 0)
            {
                if (string.IsNullOrEmpty(studentRegist.CompanyName))
                {
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("企业名称无效!");
                    return result;
                }
                else
                {
                    iResult = HPStudent.Student.Data.Account.RegistEnterpriseAccount(studentRegist);
                }
            }
            else
            {
                iResult = HPStudent.Student.Data.Account.RegistStudentAccount(studentRegist);
            }
            switch (iResult)
            {
                case 0:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("注册失败！");
                    break;
                case 1:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("注册成功！");
                    break;
                case 2:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("账号已经存在！");
                    break;
                case 3:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("企业名称已经存在！");
                    break;
                default:
                    result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，操作处理失败！");
                    break;
            }
            return result;
        }
    }
}
