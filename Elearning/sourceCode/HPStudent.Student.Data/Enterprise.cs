using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace HPStudent.Student.Data
{
    public class Enterprise
    {
        ///<summary>
        ///functionName(函数名)：GetEnterpriseByID
        ///通过id获取企业的相关信息
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-23 14:33:10
        ///Update Date(更新日期): 2016-03-23 14:33:10
        /// </summary>
        /// <param name="EID"></param>
        /// <returns></returns>
        public static HPStudent.Student.ViewModel.Enterprise.Enterprise GetEnterpriseByID(HPStudent.Student.ViewModel.Enterprise.Enterprise modelTemp)
        {
            string sql = @"  SELECT TOP 1 T.StudentID,T.Email, T.EmailStatus,T.IsActivated, T.Avatar,T.RoleID, T1.SID,T1.COMPANYNAME,T1.CompanyProfile, T1.Address,T1.Scale,T1.TelPhone,T1.Email AS EML, T1.WebSite  FROM STUDENTINFO T LEFT JOIN COMPANYINFO T1 ON T.STUDENTID=T1.SID  WHERE T.STUDENTID = @EID;";
            SqlParameter[] param = { new SqlParameter("@EID", modelTemp.EID) };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                HPStudent.Student.ViewModel.Enterprise.Enterprise objTemp = new HPStudent.Student.ViewModel.Enterprise.Enterprise();
                DataRow item = ds.Tables[0].Rows[0];
                objTemp.EID = Convert.ToInt32(item["StudentID"] == DBNull.Value ? 0 : (object)item["StudentID"]);
                objTemp.Profile.SID = Convert.ToInt32(item["StudentID"] == DBNull.Value ? 0 : (object)item["StudentID"]);
                objTemp.RoleID = Convert.ToInt32(item["RoleID"] == DBNull.Value ? 0 : (object)item["RoleID"]);
                objTemp.IsActivated = Convert.ToBoolean(item["IsActivated"]);
                objTemp.Email = Convert.ToString(item["Email"]);
                objTemp.EmailStatus = Convert.ToString(item["EmailStatus"]);
                objTemp.Avatar = Convert.ToString(item["Avatar"]);
                objTemp.Profile.CompanyName = Convert.ToString(item["COMPANYNAME"]);
                objTemp.Profile.CompanyProfile = Convert.ToString(item["CompanyProfile"]);
                objTemp.Profile.Address = Convert.ToString(item["Address"]);
                objTemp.Profile.TelPhone = Convert.ToString(item["TelPhone"]);
                objTemp.Profile.Email = Convert.ToString(item["EML"]);
                objTemp.Profile.WebSite = Convert.ToString(item["WebSite"]);
                objTemp.Profile.Scale = Convert.ToString(item["Scale"]);
                objTemp.Profile.SID = Convert.ToInt32(string.IsNullOrEmpty(item["SID"].ToString()) == true ? "-1" : item["SID"]);
                return objTemp;
            }
            else
            {
                return null;
            }
        }

        ///<summary>
        ///functionName(函数名)：InsertNewEnterpriseAccount
        ///添加插入企业登录账号信息
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-23 15:10:10
        ///Update Date(更新日期): 2016-03-23 15:10:10
        /// </summary>       
        /// <param name="strAccount"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public static int InsertNewEnterpriseAccount(string strAccount, string strPassword)
        {
            string sql = @"SP_INSERTNEWCOMPANY ";
            //需求确定之后，注意区分确定相应的用户账号角色RID
            SqlParameter[] param = { 
                                       new SqlParameter("@Res",SqlDbType.Int),
                                       new SqlParameter("@EMAIL", strAccount),
                                       new SqlParameter("@PWD", strPassword),
                                       new SqlParameter("@RID", "0")                                       
                                   };
            param[0].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, param);
            return Convert.ToInt32(param[0].Value);
        }

        ///<summary>
        ///functionName(函数名)：UpdateEnterpriseProfile
        ///修改企业基本信息，如企业名称，企业描述
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-23 15:39:10
        ///Update Date(更新日期): 2016-03-23 15:39:10
        /// </summary>
        /// <param name="EID"></param>
        /// <param name="strName"></param>
        /// <param name="strProfile"></param>
        /// <returns></returns>
        public static int UpdateEnterpriseProfile(HPStudent.Entity.CompanyInfo model)
        {
            string sql = @" UPDATE COMPANYINFO SET COMPANYNAME = @NAME, Address =@ADDR, Email=@EMAIL, Scale=@SCALE,TelPhone=@TEL, WebSite=@WEB, COMPANYPROFILE= @PROFILE WHERE SID = @EID; ";
            //需求确定之后，注意区分确定相应的用户账号角色RID
            SqlParameter[] param = { 
                                       new SqlParameter("@NAME", model.CompanyName),
                                       new SqlParameter("@PROFILE",  model.CompanyProfile),
                                       new SqlParameter("@EID",  model.SID),
                                       new SqlParameter("@ADDR",  model.Address),
                                       new SqlParameter("@EMAIL",  model.Email),
                                       new SqlParameter("@SCALE",  model.Scale),
                                       new SqlParameter("@TEL",  model.TelPhone),
                                       new SqlParameter("@WEB",  model.WebSite),
                                   };
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            return iResult > 0 ? 0 : 1;
        }

        /// <summary>
        /// 就业协议上传
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UploadAgreement(HPStudent.Entity.CompanyInfo model)
        {
            string sql = @" UPDATE COMPANYINFO SET Agreement=@Agreement WHERE SID = @SID; ";
            //需求确定之后，注意区分确定相应的用户账号角色RID
            SqlParameter[] param = { 
                                       new SqlParameter("@Agreement",  model.Agreement),
                                       new SqlParameter("@SID",  model.SID)
                                   };
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            return iResult > 0 ? 0 : 1;
        }

        ///<summary>
        ///functionName(函数名)：QuerySimilarEnterpriseName
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-25 13:59:10
        ///Update Date(更新日期): 2016-03-25 13:59:10
        /// </summary>      
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public static List<HPStudent.Student.ViewModel.Enterprise.EnterpriseItem> QuerySimilarEnterpriseName(HPStudent.Student.ViewModel.Enterprise.CompanyInfoViewModel keywords, int start, int length, out int totalRows)
        {
            string condition = "";
            StringBuilder strLikeString = new StringBuilder();
            if (!string.IsNullOrEmpty(keywords.CompanyName))
            {
                Char[] wordChar = keywords.CompanyName.Trim().ToCharArray();
                strLikeString.Append(string.Empty + "%");
                foreach (char item in wordChar)
                {
                    if (!Char.IsWhiteSpace(item))
                    {
                        strLikeString.Append(item + "%");
                    }
                }
                condition += string.Format(" and COMPANYNAME LIKE '%{0}%' ", strLikeString);
            }
            if (keywords.CreaterID != 0)
            {
                condition += string.Format(" and CreaterID ={0}", keywords.CreaterID);
            }
            string strCount = string.Format(@" SELECT COUNT(SID) FROM COMPANYINFO  WHERE 1=1 {0}", condition);
            totalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strCount));
            if (totalRows <= 0)
            {
                return new List<HPStudent.Student.ViewModel.Enterprise.EnterpriseItem>();
            }
            string sql = string.Format(@" SELECT TOP {0} SID, CompanyName,Agreement FROM COMPANYINFO  
                                          WHERE 1=1 {1} AND SID NOT IN(SELECT TOP {2} SID 
                                          FROM COMPANYINFO  WHERE 1=1 {1}  ORDER BY SID DESC )
                                          ORDER BY SID DESC", length, condition, start);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Student.ViewModel.Enterprise.EnterpriseItem> lstRes = new List<HPStudent.Student.ViewModel.Enterprise.EnterpriseItem>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Student.ViewModel.Enterprise.EnterpriseItem objTemp = new HPStudent.Student.ViewModel.Enterprise.EnterpriseItem();
                    objTemp.EID = Convert.ToInt32(item["SID"]);
                    objTemp.EnterpriseName = Convert.ToString(item["CompanyName"] == DBNull.Value ? "" : (object)item["CompanyName"]);
                    objTemp.Agreement = Convert.ToString(item["Agreement"] == DBNull.Value ? "" : (object)item["Agreement"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return null;
        }


        ///<summary>
        ///functionName(函数名)：CheckExistsTheSameNameCompany
        ///根据提供的名称，检查是否已经存在同名称的企业或者公司
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-31 09:18:10
        ///Update Date(更新日期): 2016-03-31 09:18:10
        /// </summary>       
        /// <param name="strName"></param>
        /// <returns></returns>
        public static int CheckExistsTheSameNameCompany(string strName)
        {
            string sql = @"SELECT SID FROM CompanyInfo WHERE CompanyName = @CNAME;";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql, new SqlParameter[] { new SqlParameter("@CNAME", strName) }));
        }


        public static int CheckEnterpiseExistedByAccount(string strAccount)
        {
            string sql = @"SELECT StudentID FROM StudentInfo WHERE  Email =@CNAME;";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql, new SqlParameter[] { new SqlParameter("@CNAME", strAccount) }));
        }
        ///<summary>
        ///functionName(函数名)：InsertNewJobTittleRecord
        ///增加JobTittle模型对象数据
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-31 16:04
        ///Update Date(更新日期): 2016-03-31 16:04
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int InsertNewJobTittleRecord(HPStudent.Entity.JobTittle model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO JobTittle(");
            sql.Append("SID,Name,City,SalaryRange,WorkType,DegreeRequired,ExperienceRequired,JobDescription)");
            sql.Append(" VALUES (");
            sql.Append("@SID,@Name,@City,@SalaryRange,@WorkType,@DegreeRequired,@ExperienceRequired,@JobDescription)");
            SqlParameter[] paras = {  
							new SqlParameter("@SID", model.SID),  
							new SqlParameter("@Name", model.Name),  
							new SqlParameter("@City", model.City),  
							new SqlParameter("@SalaryRange", model.SalaryRange),  
							new SqlParameter("@WorkType", model.WorkType),  
							new SqlParameter("@DegreeRequired", model.DegreeRequired),  
							new SqlParameter("@ExperienceRequired", model.ExperienceRequired),  
							new SqlParameter("@JobDescription", model.JobDescription),							
							};
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);
            return iResult > 0 ? 0 : 1;
        }
        //发送面试邀请
        public static int SendInvitationInfo(HPStudent.Entity.InterviewInvitation invitation)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into InterviewInvitation(");
            strSql.Append("SID,SenderID,JobTitleID,Content,IsRead,SendDate)");
            strSql.Append(" values (");
            strSql.Append("@SID,@SenderID,@JobTitleID,@Content,@IsRead,@SendDate)");
            SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.BigInt,8),
					new SqlParameter("@SenderID", SqlDbType.BigInt,8),
					new SqlParameter("@JobTitleID", SqlDbType.BigInt,8),
					new SqlParameter("@Content", SqlDbType.VarChar,1024),
					new SqlParameter("@IsRead", SqlDbType.Char,1),
					new SqlParameter("@SendDate", SqlDbType.DateTime)};
            parameters[0].Value = invitation.SID;
            parameters[1].Value = invitation.SenderID;
            parameters[2].Value = invitation.JobTitleID;
            parameters[3].Value = invitation.Content;
            parameters[4].Value = invitation.IsRead;
            parameters[5].Value = invitation.SendDate;

            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        //添加企业信息
        public static int AddCompanyInfo(Entity.CompanyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            string tempAccount = System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + "@houpu.com";
            string pwd = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");
            //创建账号
            strSql.Append(" BEGIN TRY;");
            strSql.Append(" BEGIN TRAN");
            strSql.Append(" DECLARE @studentid bigint");
            strSql.Append(" INSERT INTO StudentInfo(Email,Password,IsActivated,RoleID,SchoolID) VALUES(");
            strSql.Append(string.Format(" '{0}', '{1}',1,0,0);", tempAccount, pwd));
            strSql.Append(" select @studentid=@@IDENTITY;");
            //添加企业
            strSql.Append(" insert into CompanyInfo(");
            strSql.Append(" SID,CompanyName,CompanyProfile,Address,Scale,TelPhone,Email,WebSite,CreaterID,CreaterName,CreateTime)");
            strSql.Append(" values (");
            strSql.Append(" @studentid,@CompanyName,@CompanyProfile,@Address,@Scale,@TelPhone,@Email,@WebSite,@CreaterID,@CreaterName,@CreateTime);");
            strSql.Append(" COMMIT TRAN");
            strSql.Append(" select 0");
            strSql.Append(" END TRY ");
            strSql.Append(" BEGIN CATCH");
            strSql.Append(" ROLLBACK TRAN");
            strSql.Append(" select 1");
            strSql.Append(" END CATCH");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyName", SqlDbType.VarChar,500),
					new SqlParameter("@CompanyProfile", SqlDbType.Text),
					new SqlParameter("@Address", SqlDbType.VarChar,256),
					new SqlParameter("@Scale", SqlDbType.VarChar,64),
					new SqlParameter("@TelPhone", SqlDbType.VarChar,500),
					new SqlParameter("@Email", SqlDbType.VarChar,64),
					new SqlParameter("@WebSite", SqlDbType.VarChar,1024),
					new SqlParameter("@CreaterID", SqlDbType.BigInt,8),
					new SqlParameter("@CreaterName", SqlDbType.VarChar,64),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.CompanyName;
            parameters[1].Value = model.CompanyProfile;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.Scale;
            parameters[4].Value = model.TelPhone;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.WebSite;
            parameters[7].Value = model.CreaterID;
            parameters[8].Value = model.CreaterName;
            parameters[9].Value = model.CreateTime;
            object iResult = SqlHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            return Convert.ToInt32(iResult);
        }
        //修改企业信息
        public static int UpdateCompanyInfo(Entity.CompanyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CompanyInfo set ");
            strSql.Append("CompanyName=@CompanyName,");
            strSql.Append("CompanyProfile=@CompanyProfile,");
            strSql.Append("Address=@Address,");
            strSql.Append("Scale=@Scale,");
            strSql.Append("TelPhone=@TelPhone,");
            strSql.Append("Email=@Email,");
            strSql.Append("WebSite=@WebSite");
            strSql.Append(" where SID=@SID");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyName", SqlDbType.VarChar,500),
					new SqlParameter("@CompanyProfile", SqlDbType.Text),
					new SqlParameter("@Address", SqlDbType.VarChar,256),
					new SqlParameter("@Scale", SqlDbType.VarChar,64),
					new SqlParameter("@TelPhone", SqlDbType.VarChar,500),
					new SqlParameter("@Email", SqlDbType.VarChar,64),
					new SqlParameter("@WebSite", SqlDbType.VarChar,1024),
					new SqlParameter("@SID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.CompanyName;
            parameters[1].Value = model.CompanyProfile;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.Scale;
            parameters[4].Value = model.TelPhone;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.WebSite;
            parameters[7].Value = model.SID;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        //验证企业名称是否重复
        public static int CheckCompanyName(Entity.CompanyInfo model)
        {
            string sql = string.Format(@"SELECT count(sid) FROM CompanyInfo
                                         WHERE  sid !={0} and Replace(CompanyName, ' ', '')='{1}';"
                                         , model.SID, model.CompanyName);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql));
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static HPStudent.Student.ViewModel.Enterprise.CompanyInfoResult GetCompanyInfoModel(int SID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SID,CompanyName,CompanyProfile,Address,Scale,TelPhone,Email,WebSite,Agreement,CreaterID,CreaterName,CreateTime from CompanyInfo ");
            strSql.Append(" where SID=@SID");
            SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.BigInt)
			};
            parameters[0].Value = SID;

            HPStudent.Student.ViewModel.Enterprise.CompanyInfoResult model = new HPStudent.Student.ViewModel.Enterprise.CompanyInfoResult();
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static HPStudent.Student.ViewModel.Enterprise.CompanyInfoResult DataRowToModel(DataRow row)
        {
            HPStudent.Student.ViewModel.Enterprise.CompanyInfoResult model = new HPStudent.Student.ViewModel.Enterprise.CompanyInfoResult();
            if (row != null)
            {
                if (row["SID"] != null && row["SID"].ToString() != "")
                {
                    model.SID = Convert.ToInt32(row["SID"].ToString());
                }
                if (row["CompanyName"] != null)
                {
                    model.CompanyName = row["CompanyName"].ToString();
                }
                if (row["CompanyProfile"] != null)
                {
                    model.CompanyProfile = row["CompanyProfile"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["Scale"] != null)
                {
                    model.Scale = row["Scale"].ToString();
                }
                if (row["TelPhone"] != null)
                {
                    model.TelPhone = row["TelPhone"].ToString();
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["WebSite"] != null)
                {
                    model.WebSite = row["WebSite"].ToString();
                }
                if (row["Agreement"] != null)
                {
                    model.Agreement = row["Agreement"].ToString();
                }
                if (row["CreaterID"] != null && row["CreaterID"].ToString() != "")
                {
                    model.CreaterID = Convert.ToInt32(row["CreaterID"].ToString());
                }
                if (row["CreaterName"] != null)
                {
                    model.CreaterName = row["CreaterName"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
            }
            return model;
        }
    }
}
