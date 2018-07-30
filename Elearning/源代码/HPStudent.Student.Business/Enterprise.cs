using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPStudent.Core;

namespace HPStudent.Student.Business
{
    public class Enterprise
    {
        public static HPStudent.Student.ViewModel.Common.RequestResult InsertNewEnterprise(string account, string password)
        {
            HPStudent.Student.ViewModel.Common.RequestResult result = new HPStudent.Student.ViewModel.Common.RequestResult();
            int iResult = -1;

            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                result.ResultState = HPStudent.Student.ViewModel.Common.RequestResult.StateCode.fail;
                result.ResultMsg = string.Format("用户名或密码无效!");
                return result;
            }
            iResult = HPStudent.Student.Data.Enterprise.InsertNewEnterpriseAccount(account, password);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("企业账号注册失败！");
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("企业账号注册成功！");
                    break;
                case 2:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("企业账号已经存在！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，操作处理失败！");
                    break;
            }
            return result;
        }
        public static HPStudent.Student.ViewModel.Enterprise.Enterprise GetEnterpriseByID(HPStudent.Student.ViewModel.Enterprise.Enterprise modelTemp)
        {
            return HPStudent.Student.Data.Enterprise.GetEnterpriseByID(modelTemp);
        }
        public static int GetJobCountBySID(int SID)
        {
            return HPStudent.Student.Data.Job.GetJobCountBySID(SID);
        }
        public static HPStudent.Student.ViewModel.Common.RequestResult UpdateEnterprise(HPStudent.Entity.CompanyInfo model)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            int iResult = HPStudent.Student.Data.Enterprise.UpdateEnterpriseProfile(model);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("企业信息修改成功！");
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("企业信息修改失败！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，更新操作处理失败！");
                    break;
            }
            return result;
        }
        //就业协议上传
        public static HPStudent.Student.ViewModel.Common.RequestResult UploadAgreement(HPStudent.Entity.CompanyInfo model)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            int iResult = HPStudent.Student.Data.Enterprise.UploadAgreement(model);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("就业协议上传成功！");
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("就业协议上传失败！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，更新操作处理失败！");
                    break;
            }
            return result;
        }

        public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Enterprise.EnterpriseItem> QuerySimilarEnterprise(HPStudent.Student.ViewModel.Enterprise.CompanyInfoViewModel keywords, int length, int start)
        {
            int TotalRows = 0;
            if (keywords.SearchType == 1)
            {
                keywords.CreaterID = Convert.ToInt32(HPStudent.Core.CookieHelper.GetCookieValue("StudentID"));
            }
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Enterprise.EnterpriseItem> SelectedTable = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Enterprise.EnterpriseItem>();
            SelectedTable.data = HPStudent.Student.Data.Enterprise.QuerySimilarEnterpriseName(keywords, start, length, out TotalRows);
            //Data.Admin.Exercises.GetQA_SelectListByCID(CID, start, length, out TotalRows);
            //初始化返回Datatable行数
            SelectedTable.recordsTotal = TotalRows;
            SelectedTable.recordsFiltered = TotalRows;
            return SelectedTable;
        }
        /// <summary>
        /// 返回三个数组，分别为，总共导入a条企业信息，成功b条，失败a-b条，成功导入岗位信息c条
        /// </summary>
        /// <param name="lstSource"></param>
        /// <returns></returns>
        public static List<string> ImportEnterpriseAndJobTittles(List<HPStudent.Student.ViewModel.Enterprise.EnterpriseWithJobs> lstSource)
        {
            if (lstSource == null)
            {
                return new List<string>();
            }
            else
            {
                List<string> tempResult = new List<string>();
                foreach (var OuterItem in lstSource)
                {
                    int EID = HPStudent.Student.Data.Enterprise.CheckExistsTheSameNameCompany(OuterItem.Basic.CompanyName);
                    if (EID > 0 || OuterItem.CompanyJobTittle.Count < 1)//已经存在的企业或者 企业岗位数为0
                    {
                        OuterItem.IsImported = true;     //标记为已经导入                
                        tempResult.Add(OuterItem.Basic.CompanyName);
                        continue;
                    }
                }//foreach
                HPStudent.Student.Business.Enterprise.InsertMultipleDataRecords(lstSource);
                return tempResult;
            }

        }

        public static void InsertMultipleDataRecords(List<HPStudent.Student.ViewModel.Enterprise.EnterpriseWithJobs> lstSource)
        {
            //如果不存在对应账号，创建企业账号并创建企业信息，添加企业对应的岗位记录；        
            Dictionary<string, string> dctAccount = new Dictionary<string, string>();
            int iX = 0;
            foreach (var item in lstSource)
            {
                if (!item.IsImported)
                {
                    string tempAccount = System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + (iX++) + @"@houpu.com";
                    string pwd = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    dctAccount.Add(tempAccount, pwd);
                    item.AccountEmail = tempAccount;
                }
            }
            StringBuilder sbd = new StringBuilder();
            string strSqlCcmd = string.Empty;
            //导入的企业账号状态为激活
            sbd.Append(@"INSERT INTO StudentInfo(Email,Password,IsActivated,RoleID,SchoolID) VALUES");
            foreach (var item in dctAccount)
            {
                sbd.Append(string.Format(@"('{0}', '{1}',1,0,0),", item.Key, item.Value));
            }
            if (dctAccount.Count > 0)
            {
                strSqlCcmd = sbd.ToString().Trim().TrimEnd(',');
                HPStudent.Student.Data.SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, strSqlCcmd);
                sbd.Clear();
            }
            else
            {
                return;
            }

            foreach (var item in dctAccount)
            {
                sbd.Append(string.Format(@"'{0}',", item.Key));
            }
            strSqlCcmd = string.Format(@"SELECT StudentID, Email FROM StudentInfo WHERE Email IN ({0})", sbd.ToString().Trim().TrimEnd(','));
            System.Data.DataSet dst = HPStudent.Student.Data.SqlHelper.ExecuteDataset(System.Data.CommandType.Text, strSqlCcmd);
            if (dst.Tables[0].Rows.Count > 0)
            {
                foreach (System.Data.DataRow item in dst.Tables[0].Rows)
                {
                    var currentItem = lstSource.FirstOrDefault(m => m.AccountEmail == Convert.ToString(item[1]));
                    currentItem.Basic.SID = Convert.ToInt32(item[0]);
                }
                sbd.Clear();
                foreach (var item in lstSource)
                {
                    if (!item.IsImported)
                    {
                        sbd.Append(string.Format(@"({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}'),",
                            item.Basic.SID,
                            StringHelper.filtRiskChar(item.Basic.CompanyName.Length > 250 ? item.Basic.CompanyName.Substring(0, 250) : item.Basic.CompanyName),
                            StringHelper.filtRiskChar(item.Basic.CompanyProfile),
                            StringHelper.filtRiskChar(item.Basic.Address.Length > 128 ? item.Basic.Address.Substring(0, 128) : item.Basic.Address),
                            StringHelper.filtRiskChar(item.Basic.Scale.Length > 32 ? item.Basic.Scale.Substring(0, 32) : item.Basic.Scale),
                            StringHelper.filtRiskChar(item.Basic.TelPhone.Length > 250 ? item.Basic.TelPhone.Substring(0, 250) : item.Basic.TelPhone),
                            StringHelper.filtRiskChar(item.Basic.Email.Length > 32 ? item.Basic.Email.Substring(0, 32) : item.Basic.Email),
                            StringHelper.filtRiskChar(item.Basic.WebSite.Length > 500 ? item.Basic.WebSite.Substring(0, 500) : item.Basic.WebSite)));
                    }
                    //SID, CompanyName,CompanyProfile,Address, Scale,TelPhone, Email, WebSite
                }
                strSqlCcmd = string.Format(@" INSERT INTO CompanyInfo(SID, CompanyName,CompanyProfile,Address, Scale,TelPhone, Email, WebSite) VALUES{0}", sbd.ToString().Trim().TrimEnd(','));
                HPStudent.Student.Data.SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, strSqlCcmd);
                sbd.Clear();
                foreach (var item in lstSource)
                {
                    if (!item.IsImported)
                    {
                        foreach (var innerItem in item.CompanyJobTittle)
                        {
                            sbd.Append(string.Format(@"({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}'),",
                                item.Basic.SID,
                                StringHelper.filtRiskChar(innerItem.Name.Length > 128 ? innerItem.Name.Substring(0, 128) : innerItem.Name),
                                StringHelper.filtRiskChar(innerItem.City),
                                StringHelper.filtRiskChar(innerItem.SalaryRange),
                                StringHelper.filtRiskChar(innerItem.WorkType),
                                StringHelper.filtRiskChar(innerItem.DegreeRequired),
                                StringHelper.filtRiskChar(innerItem.ExperienceRequired),
                                StringHelper.filtRiskChar(innerItem.JobDescription)));
                        } // INSERT INTO JobTittle(SID,Name,City,SalaryRange,WorkType,DegreeRequired,ExperienceRequired,JobDescription) VALUES{0}                 
                    }
                }
                strSqlCcmd = string.Format(@" INSERT INTO JobTittle(SID,Name,City,SalaryRange,WorkType,DegreeRequired,ExperienceRequired,JobDescription) VALUES{0}", sbd.ToString().Trim().TrimEnd(','));
                HPStudent.Student.Data.SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, strSqlCcmd);
                sbd.Clear();
            }
        }
        //发送面试邀请
        public static HPStudent.Student.ViewModel.Common.RequestResult SendInvitationInfo(HPStudent.Entity.InterviewInvitation invitation)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            int iResult = -1;
            iResult = HPStudent.Student.Data.Enterprise.SendInvitationInfo(invitation);
            switch (iResult)
            {
                case 0:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                    result.ResultMsg = string.Format("发送面试邀请成功！");
                    break;
                case 1:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("发送面试邀请失败！");
                    break;
                default:
                    result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                    result.ResultMsg = string.Format("出现异常错误，操作处理失败！");
                    break;
            }
            return result;
        }
        //保存公司信息
        public static ViewModel.Common.RequestResult SaveCompanyInfo(Entity.CompanyInfo KeyWords)
        {
            ViewModel.Common.RequestResult result = new ViewModel.Common.RequestResult();
            KeyWords.CompanyName = KeyWords.CompanyName.Trim();
            //验证企业账号是否存在
            if (Data.Enterprise.CheckCompanyName(KeyWords) > 0)
            {
                result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                result.ResultMsg = string.Format("企业名称重复！");
                return result;
            }
            DateTime NowDate = DateTime.Now;
            if (KeyWords.SID == 0)
            {
                KeyWords.CreateTime = NowDate;
                KeyWords.CreaterID = Convert.ToInt32(CookieHelper.GetCookieValue("StudentID"));
                KeyWords.CreaterName = CookieHelper.GetCookieValue("RealName");
                switch (Data.Enterprise.AddCompanyInfo(KeyWords))
                {
                    case 0:
                        result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                        result.ResultMsg = string.Format("企业信息添加成功！");
                        break;
                    case 1:
                        result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                        result.ResultMsg = string.Format("企业信息添加失败！");
                        break;
                }
            }
            else
            {
                switch (Data.Enterprise.UpdateCompanyInfo(KeyWords))
                {
                    case 0:
                        result.ResultState = ViewModel.Common.RequestResult.StateCode.success;
                        result.ResultMsg = string.Format("企业信息修改成功！");
                        break;
                    case 1:
                        result.ResultState = ViewModel.Common.RequestResult.StateCode.fail;
                        result.ResultMsg = string.Format("企业信息修改失败！");
                        break;
                }
            }
            return result;
        }
        //获得公司信息实体
        public static HPStudent.Student.ViewModel.Enterprise.CompanyInfoResult GetCompanyInfoByID(int SID)
        {
            return Data.Enterprise.GetCompanyInfoModel(SID);
        }
    }
}
