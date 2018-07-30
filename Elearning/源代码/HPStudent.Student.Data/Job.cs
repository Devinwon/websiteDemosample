using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
    public class Job
    {
        //获取岗位查询结果
        public static List<HPStudent.Student.ViewModel.Job.JobList> QueryJobList(HPStudent.Student.ViewModel.Job.JobListParameter keyWord, int start, int length, out int totalRows)
        {
            //获取条件
            string condition = "";
            string conditionson = "";
            if (!string.IsNullOrEmpty(keyWord.City))
            {
                condition += string.Format(" AND  City='{0}'", keyWord.City);
                conditionson += string.Format(" AND  City='{0}'", keyWord.City);
            }
            if (!string.IsNullOrEmpty(keyWord.Name))
            {
                condition += string.Format(" AND  name like'%{0}%'", keyWord.Name);
                conditionson += string.Format(" AND  name like'%{0}%'", keyWord.Name);
            }
            if (keyWord.Salaryrange != "== 薪资范围 ==" && !string.IsNullOrEmpty(keyWord.Salaryrange))
            {
                condition += string.Format(" AND  salaryrange='{0}'", keyWord.Salaryrange);
                conditionson += string.Format(" AND  salaryrange='{0}'", keyWord.Salaryrange);
            }
            if (!string.IsNullOrEmpty(keyWord.CompanyName))
            {
                condition += string.Format(" AND  CompanyName like'%{0}%'", keyWord.CompanyName);
                conditionson += string.Format(" AND  CompanyName like'%{0}%'", keyWord.CompanyName);
            }
            if (keyWord.Sid != 0)
            {
                condition += string.Format(" AND  com.Sid={0}", keyWord.Sid);
                conditionson += string.Format(" AND  comT.Sid={0}", keyWord.Sid);
            }
            string strCount = string.Format(@" select Count(job.JID) from dbo.JobTittle as job inner join dbo.CompanyInfo as com
                                          on job.SID=com.SID where 1=1 {0} ", condition);
            totalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strCount));
            if (totalRows <= 0)
            {
                return new List<HPStudent.Student.ViewModel.Job.JobList>();
            }
            string sql = string.Format(@" select TOP {0} com.CompanyName,job.JID,job.City,job.DegreeRequired,job.ExperienceRequired,job.Name
                                          ,job.SalaryRange,job.WorkType,com.SID from dbo.JobTittle as job inner join dbo.CompanyInfo as com
                                          on job.SID=com.SID WHERE job.JID not in (select top {1}  jobT.JID from JobTittle as jobT inner join 
                                          dbo.CompanyInfo as comT on jobT.SID=comT.SID where 1=1 {2} ORDER BY jobT.JID DESC) {3}
                                          ORDER BY job.JID DESC", length, start, conditionson, condition);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Student.ViewModel.Job.JobList> lstRes = new List<HPStudent.Student.ViewModel.Job.JobList>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Student.ViewModel.Job.JobList objTemp = new HPStudent.Student.ViewModel.Job.JobList();
                    objTemp.City = Convert.ToString(item["city"] == DBNull.Value ? "" : (object)item["city"]);
                    objTemp.CompanyName = Convert.ToString(item["CompanyName"] == DBNull.Value ? "" : (object)item["CompanyName"]);
                    objTemp.Degreerequired = Convert.ToString(item["degreerequired"] == DBNull.Value ? "" : (object)item["degreerequired"]);
                    objTemp.Experiencerequired = Convert.ToString(item["experiencerequired"] == DBNull.Value ? "" : (object)item["experiencerequired"]);
                    objTemp.Name = Convert.ToString(item["name"] == DBNull.Value ? "" : (object)item["name"]);
                    objTemp.Salaryrange = Convert.ToString(item["salaryrange"] == DBNull.Value ? "" : (object)item["salaryrange"]);
                    objTemp.Worktype = Convert.ToString(item["worktype"] == DBNull.Value ? "" : (object)item["worktype"]);
                    objTemp.JId = Convert.ToInt32(item["jid"]);
                    objTemp.SId = Convert.ToInt32(item["SID"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Student.ViewModel.Job.JobList>();
        }


        //获取岗位查询结果
        public static List<HPStudent.Student.ViewModel.Job.JobList> GetSeeSenderCompanyTable(HPStudent.Student.ViewModel.Job.JobListParameter keyWord, string SenderID, int start, int length, out int totalRows)
        {
            //获取条件
            string condition = "";
            if (!string.IsNullOrEmpty(keyWord.City))
            {
                condition += string.Format("AND  job.City='{0}'", keyWord.City);
            }
            if (!string.IsNullOrEmpty(keyWord.Name))
            {
                condition += string.Format("AND  job.name like '%{0}%'", keyWord.Name);
            }
            if (keyWord.Salaryrange != "== 薪资范围 ==" && !string.IsNullOrEmpty(keyWord.Salaryrange))
            {
                condition += string.Format("AND  job.salaryrange='{0}'", keyWord.Salaryrange);
            }
            if (!string.IsNullOrEmpty(keyWord.CompanyName))
            {
                condition += string.Format("AND  com.CompanyName like '%{0}%'", keyWord.CompanyName);
            }
            string strCount = string.Format(@"  select Count(job.JID) from dbo.JobTittle as job inner join dbo.CompanyInfo as com
                                          on job.SID=com.SID  inner join SendResume sender on job.Jid  = sender.JobTitleID WHERE sender.SenderID = {0}  {1} ", SenderID, condition);
            totalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strCount));
            if (totalRows <= 0)
            {
                return new List<HPStudent.Student.ViewModel.Job.JobList>();
            }
            string sql = string.Format(@"   select TOP  {0} sender.IsRead,com.CompanyName,job.JID,job.City,job.DegreeRequired,job.ExperienceRequired,job.Name
                                          ,job.SalaryRange,job.WorkType from dbo.JobTittle as job inner join dbo.CompanyInfo as com
                                          on job.SID=com.SID inner join SendResume sender on job.Jid  = sender.JobTitleID WHERE sender.SenderID = {1} {2}  and job.JID not in (select top {3}  jobT.JID from JobTittle as jobT inner join 
                                          dbo.CompanyInfo as comT on jobT.SID=comT.SID where 1=1  ORDER BY jobT.JID DESC) 
                                          ORDER BY job.JID DESC", length, SenderID, condition,start );
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Student.ViewModel.Job.JobList> lstRes = new List<HPStudent.Student.ViewModel.Job.JobList>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Student.ViewModel.Job.JobList objTemp = new HPStudent.Student.ViewModel.Job.JobList();
                    objTemp.City = Convert.ToString(item["city"] == DBNull.Value ? "" : (object)item["city"]);
                    objTemp.CompanyName = Convert.ToString(item["CompanyName"] == DBNull.Value ? "" : (object)item["CompanyName"]);
                    objTemp.Degreerequired = Convert.ToString(item["degreerequired"] == DBNull.Value ? "" : (object)item["degreerequired"]);
                    objTemp.Experiencerequired = Convert.ToString(item["experiencerequired"] == DBNull.Value ? "" : (object)item["experiencerequired"]);
                    objTemp.Name = Convert.ToString(item["name"] == DBNull.Value ? "" : (object)item["name"]);
                    objTemp.Salaryrange = Convert.ToString(item["salaryrange"] == DBNull.Value ? "" : (object)item["salaryrange"]);
                    objTemp.Worktype = Convert.ToString(item["worktype"] == DBNull.Value ? "" : (object)item["worktype"]);
                    objTemp.JId = Convert.ToInt32(item["jid"]);
                    objTemp.IsRead = Convert.ToInt32(item["IsRead"]) == 0 ? "未查看" : "已查看";

                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Student.ViewModel.Job.JobList>();
        }


        //获取职位详情
        public static HPStudent.Student.ViewModel.Job.JobList GetJobInfoByID(int JId)
        {
            string sql = @"  select com.SID,com.CompanyName,job.JID,job.City,job.DegreeRequired,job.ExperienceRequired,job.Name
                            ,job.SalaryRange,job.WorkType,job.JobDescription from dbo.JobTittle as job inner join dbo.CompanyInfo as com
                            on job.SID=com.SID where job.JID=@JID";
            SqlParameter[] param = { new SqlParameter("@JID", JId) };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow item = ds.Tables[0].Rows[0];
                HPStudent.Student.ViewModel.Job.JobList objTemp = new HPStudent.Student.ViewModel.Job.JobList();
                objTemp.City = Convert.ToString(item["city"] == DBNull.Value ? "" : (object)item["city"]);
                objTemp.CompanyName = Convert.ToString(item["CompanyName"] == DBNull.Value ? "" : (object)item["CompanyName"]);
                objTemp.Degreerequired = Convert.ToString(item["degreerequired"] == DBNull.Value ? "" : (object)item["degreerequired"]);
                objTemp.Experiencerequired = Convert.ToString(item["experiencerequired"] == DBNull.Value ? "" : (object)item["experiencerequired"]);
                objTemp.Name = Convert.ToString(item["name"] == DBNull.Value ? "" : (object)item["name"]);
                objTemp.Salaryrange = Convert.ToString(item["salaryrange"] == DBNull.Value ? "" : (object)item["salaryrange"]);
                objTemp.Worktype = Convert.ToString(item["worktype"] == DBNull.Value ? "" : (object)item["worktype"]);
                objTemp.JId = Convert.ToInt32(item["jid"]);
                objTemp.JobDescription = Convert.ToString(item["JobDescription"] == DBNull.Value ? "" : (object)item["JobDescription"]);
                objTemp.SId = Convert.ToInt32(item["SId"]);
                return objTemp;
            }
            else
            {
                return null;
            }
        }
        //获取企业职位总数
        public static int GetJobCountBySID(int SID)
        {
            string sql = "select COUNT(jid) as allcount from dbo.JobTittle where SID=@SID";
            SqlParameter[] param = { new SqlParameter("@SID", SID) };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow item = ds.Tables[0].Rows[0];
                return Convert.ToInt32(item["allcount"]);
            }
            return 0;
        }
        //简历申请
        public static int SendResume(HPStudent.Student.ViewModel.Job.JobSendViewModel jobsend)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SendResume(");
            strSql.Append("SenderID,EID,JobTitleID,IsRead,SendDate)");
            strSql.Append(" values (");
            strSql.Append("@SenderID,@EID,@JobTitleID,@IsRead,@SendDate)");
            SqlParameter[] parameters = {
					new SqlParameter("@SenderID", SqlDbType.Int,4),
					new SqlParameter("@EID", SqlDbType.Int,4),
					new SqlParameter("@JobTitleID", SqlDbType.Int,4),
					new SqlParameter("@IsRead", SqlDbType.TinyInt,1),
					new SqlParameter("@SendDate", SqlDbType.DateTime)};
            parameters[0].Value = jobsend.SenderID;
            parameters[1].Value = jobsend.SId;
            parameters[2].Value = jobsend.JId;
            parameters[3].Value = 0;
            parameters[4].Value = DateTime.Now;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        //检查30天内是否投递过
        public static int CheckIsSend(HPStudent.Student.ViewModel.Job.JobSendViewModel jobsend)
        {
            string sql = @"select id from SendResume 
                           where senderid=@senderid and eid=@eid and jobtitleid=@jobtitleid
                           and DATEDIFF(d,senddate,GETDATE())<=30";
            SqlParameter[] param = { new SqlParameter("@senderid", jobsend.SenderID),
                                     new SqlParameter("@eid", jobsend.SId),
                                     new SqlParameter("@jobtitleid", jobsend.JId)};
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            return ds.Tables[0].Rows.Count;
        }
        //获取企业职位信息
        public static List<HPStudent.Entity.JobTittle> GetJobInfoBySID(int SID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select JID,Name");
            strSql.Append(" FROM JobTittle where SID=@SID");
            SqlParameter[] param = { new SqlParameter("@SID", SID) };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.JobTittle> li = new List<Entity.JobTittle>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.JobTittle objTemp = new Entity.JobTittle();
                    objTemp.JID = Convert.ToInt32(item["JID"]);
                    objTemp.Name = objTemp.Name = Convert.ToString(item["Name"] == DBNull.Value ? "" : (object)item["Name"]);
                    li.Add(objTemp);
                }
                return li;
            }
            return new List<Entity.JobTittle>();
        }
        //添加职位
        public static int AddJobItem(HPStudent.Entity.JobTittle model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into JobTittle(");
            strSql.Append("SID,Name,City,SalaryRange,WorkType,DegreeRequired,ExperienceRequired,JobDescription)");
            strSql.Append(" values (");
            strSql.Append("@SID,@Name,@City,@SalaryRange,@WorkType,@DegreeRequired,@ExperienceRequired,@JobDescription)");
            SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.BigInt,8),
					new SqlParameter("@Name", SqlDbType.VarChar,256),
					new SqlParameter("@City", SqlDbType.VarChar,32),
					new SqlParameter("@SalaryRange", SqlDbType.VarChar,64),
					new SqlParameter("@WorkType", SqlDbType.VarChar,64),
					new SqlParameter("@DegreeRequired", SqlDbType.VarChar,64),
					new SqlParameter("@ExperienceRequired", SqlDbType.VarChar,64),
					new SqlParameter("@JobDescription", SqlDbType.Text)};
            parameters[0].Value = model.SID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.City;
            parameters[3].Value = model.SalaryRange;
            parameters[4].Value = model.WorkType;
            parameters[5].Value = model.DegreeRequired;
            parameters[6].Value = model.ExperienceRequired;
            parameters[7].Value = model.JobDescription;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
        //修改职位
        public static int UpdateJobItem(HPStudent.Entity.JobTittle model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JobTittle set ");
            strSql.Append("SID=@SID,");
            strSql.Append("Name=@Name,");
            strSql.Append("City=@City,");
            strSql.Append("SalaryRange=@SalaryRange,");
            strSql.Append("WorkType=@WorkType,");
            strSql.Append("DegreeRequired=@DegreeRequired,");
            strSql.Append("ExperienceRequired=@ExperienceRequired,");
            strSql.Append("JobDescription=@JobDescription");
            strSql.Append(" where JID=@JID");
            SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.BigInt,8),
					new SqlParameter("@Name", SqlDbType.VarChar,256),
					new SqlParameter("@City", SqlDbType.VarChar,32),
					new SqlParameter("@SalaryRange", SqlDbType.VarChar,64),
					new SqlParameter("@WorkType", SqlDbType.VarChar,64),
					new SqlParameter("@DegreeRequired", SqlDbType.VarChar,64),
					new SqlParameter("@ExperienceRequired", SqlDbType.VarChar,64),
					new SqlParameter("@JobDescription", SqlDbType.Text),
					new SqlParameter("@JID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.SID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.City;
            parameters[3].Value = model.SalaryRange;
            parameters[4].Value = model.WorkType;
            parameters[5].Value = model.DegreeRequired;
            parameters[6].Value = model.ExperienceRequired;
            parameters[7].Value = model.JobDescription;
            parameters[8].Value = model.JID;
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return iResult > 0 ? 0 : 1;
        }
    }
}
