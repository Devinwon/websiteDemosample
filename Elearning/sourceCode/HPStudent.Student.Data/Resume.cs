using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using HPStudent.Student.ViewModel.Resume;

namespace HPStudent.Student.Data
{
    public class Resume
    {

        ///<summary>
        ///functionName(函数名)：GetBasicResumeInfoByStudentID
        ///获取简历基本信息
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 9:10:10
        ///Update Date(更新日期): 2016-03-17 9:15:10
        /// 通过学生编号获取基本的建立信息
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public static ResumeBasic GetBasicResumeInfoByStudentID(int MID)
        {
            string sql = @"  SELECT T1.StudentID AS SID, T2.City,T2.Status, T1.Email,T1.RealName,T1.Sex,T3.PersonMobile  FROM STUDENTINFO T1 LEFT JOIN StudentResume T2 ON T2.SID =T1.StudentID LEFT JOIN StudentBaseInfo T3 ON T3.StudentID=T1.StudentID  WHERE T1.StudentID=@id;";
            SqlParameter[] param = { new SqlParameter("@id", MID) };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ResumeBasic objRB = new ResumeBasic();
                objRB.SID = Convert.ToInt32(ds.Tables[0].Rows[0]["SID"]);
                objRB.City = Convert.ToInt32(ds.Tables[0].Rows[0]["City"] == System.DBNull.Value ? 0 : ds.Tables[0].Rows[0]["City"]);
                objRB.CityName = HPStudent.Student.Data.Common.StudentCommon.GetCityNameByCityID(objRB.City);
                objRB.Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"] == System.DBNull.Value ? 0 : ds.Tables[0].Rows[0]["Status"]);
                objRB.CurrentStatus = Convert.ToString(ResumeBasic.HuntJobStatus[objRB.Status]);
                objRB.RealName = Convert.ToString(ds.Tables[0].Rows[0]["RealName"] == System.DBNull.Value ? "" : ds.Tables[0].Rows[0]["RealName"]);
                objRB.TelPhone = Convert.ToString(ds.Tables[0].Rows[0]["PersonMobile"] == System.DBNull.Value ? "" : ds.Tables[0].Rows[0]["PersonMobile"]);
                objRB.Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"] == System.DBNull.Value ? "" : ds.Tables[0].Rows[0]["Email"]);
                objRB.Gender = ResumeBasic.ConvertGenderFromBool(Convert.ToBoolean(string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Sex"].ToString()) == true ? 0 : ds.Tables[0].Rows[0]["Sex"]));
                return objRB;
            }
            else { return new ResumeBasic(); }
        }

        ///<summary>
        ///functionName(函数名)：GetMainResumeInfoByStudentID
        ///获取建立主要全面的信息
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 11:22:10
        ///Update Date(更新日期): 2016-03-17 11:25:10
        /// </summary>       
        /// <param name="MID"></param>
        /// <returns></returns>
        public static ResumeMain GetMainResumeInfoByStudentID(int MID)
        {
            ResumeMain objRes = new ResumeMain();
            objRes.MID = MID;
            objRes.ResumeBasic = Resume.GetBasicResumeInfoByStudentID(MID);
            objRes.EducationRecord = Resume.GetStudentEducationRecordByStudentID(MID);
            objRes.WorkExperience = Resume.GetStudentWorkExperienceRecordByStudentID(MID);
            objRes.ProjectExperience = Resume.GetStudentProjectRecordByStudentID(MID);
            return objRes;
        }

        /// <summary>
        /// 获取所有学生的求职状态列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetAllCurrentStatusList()
        {
            return ResumeBasic.HuntJobStatus;
        }

        ///<summary>
        ///functionName(函数名)：GetStudentEducationRecordByStudentID
        ///获取学生简历的教育经历信息列表
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 10:10:10
        ///Update Date(更新日期): 2016-03-17 10:36:10
        /// </summary>
        /// <returns>返回参数:学生简历中教育经历列表 List<SchoolDetail></returns>       
        public static List<HPStudent.Entity.SchoolDetail> GetStudentEducationRecordByStudentID(int MID)
        {
            string sql = @"  SELECT Did, StudentID, StartDate, EndDate, School, Major, Year FROM SchoolDetail WHERE StudentID = @id;";
            SqlParameter[] param = { new SqlParameter("@id", MID) };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.SchoolDetail> lstResult = new List<Entity.SchoolDetail>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.SchoolDetail objTemp = new Entity.SchoolDetail();
                    objTemp.Did = Convert.ToInt32(item["Did"]);
                    objTemp.StudentID = Convert.ToInt32(item["StudentID"]);
                    objTemp.StartDate = Convert.ToDateTime((item["StartDate"] == DBNull.Value) ? System.DateTime.MinValue.ToString() : item["StartDate"].ToString());
                    objTemp.EndDate = Convert.ToDateTime((item["EndDate"] == DBNull.Value) ? System.DateTime.MaxValue.ToString() : item["EndDate"].ToString());
                    objTemp.School = Convert.ToString(item["School"]);
                    objTemp.Major = Convert.ToString(item["Major"]);
                    objTemp.Year = Convert.ToInt32(item["Year"]);
                    lstResult.Add(objTemp);
                }
                return lstResult;
            }
            else { return new List<Entity.SchoolDetail>(); }
        }

        ///<summary>
        ///functionName(函数名)：GetStudentWorkExperienceRecordByStudentID
        ///获取学生工作经历信息列表
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 10:53
        ///Update Date(更新日期): 2016-03-17 10:53
        /// </summary>
        /// <returns>返回参数:</returns>       
        public static List<HPStudent.Entity.StudentWorkExp> GetStudentWorkExperienceRecordByStudentID(int MID)
        {
            string sql = @" SELECT WID, SID, Company, Position, startDate, EndDate,JobContent FROM StudentWorkExp WHERE SID=@id ;";
            SqlParameter[] param = { new SqlParameter("@id", MID) };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.StudentWorkExp> lstResult = new List<Entity.StudentWorkExp>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.StudentWorkExp objTemp = new Entity.StudentWorkExp();
                    objTemp.WID = Convert.ToInt32(item["WID"]);
                    objTemp.SID = Convert.ToInt32(item["SID"]);
                    objTemp.startDate = Convert.ToDateTime((item["startDate"] == DBNull.Value) ? System.DateTime.MinValue.ToString() : item["startDate"].ToString());
                    objTemp.EndDate = Convert.ToDateTime((item["EndDate"] == DBNull.Value) ? System.DateTime.MinValue.ToString() : item["EndDate"].ToString());
                    objTemp.Company = Convert.ToString(item["Company"]);
                    objTemp.Position = Convert.ToString(item["Position"]);
                    objTemp.JobContent = Convert.ToString(item["JobContent"]);
                    lstResult.Add(objTemp);
                }
                return lstResult;
            }
            else { return new List<Entity.StudentWorkExp>(); }
        }

        ///<summary>
        ///functionName(函数名)：GetStudentProjectRecordByStudentID
        ///
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 16:08:10
        ///Update Date(更新日期): 2016-03-17 16:10:10
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public static List<HPStudent.Entity.StudentProjectExp> GetStudentProjectRecordByStudentID(int MID)
        {
            string sql = @"  SELECT PID, SID, ProjectName, Position, startDate, EndDate,JobContent FROM StudentProjectExp WHERE SID=@ID ;";
            SqlParameter[] param = { new SqlParameter("@ID", MID) };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.StudentProjectExp> lstResult = new List<Entity.StudentProjectExp>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.StudentProjectExp objTemp = new Entity.StudentProjectExp();
                    objTemp.PID = Convert.ToInt32(item["PID"]);
                    objTemp.SID = Convert.ToInt32(item["SID"]);
                    objTemp.startDate = Convert.ToDateTime((item["startDate"] == DBNull.Value) ? System.DateTime.MinValue.ToString() : item["startDate"].ToString());
                    objTemp.EndDate = Convert.ToDateTime((item["EndDate"] == DBNull.Value) ? System.DateTime.MaxValue.ToString() : item["EndDate"].ToString());
                    objTemp.ProjectName = Convert.ToString(item["ProjectName"]);
                    objTemp.Position = Convert.ToString(item["Position"]);
                    objTemp.JobContent = Convert.ToString(item["JobContent"]);
                    lstResult.Add(objTemp);
                }
                return lstResult;
            }
            else
            {
                return new List<Entity.StudentProjectExp>();
            }

        }

        ///<summary>
        ///functionName(函数名)：InsertNewEduRecord
        ///
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 15:16:10
        ///Update Date(更新日期): 2016-03-17 15:16:10
        /// </summary>
        /// <param name="objEduInfo"></param>
        /// <returns></returns>
        public static int InsertNewEduRecord(HPStudent.Entity.SchoolDetail objEduInfo)
        {
            string sql = @" INSERT INTO SchoolDetail(StudentID, StartDate, EndDate, School, Major, Year)  VALUES(@SID, @STARTDATE,@ENDDATE,@SCHOOL,@MAJOR,@YEAR);";
            //@ID,@SID, @STARTDATE,@ENDDATE,@SCHOOL,@MAJOR,@YEAR
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@SID", objEduInfo.StudentID),
                new SqlParameter("@STARTDATE", objEduInfo.StartDate),
                new SqlParameter("@ENDDATE", objEduInfo.EndDate==DateTime.MinValue?DBNull.Value:(object)objEduInfo.EndDate),
                new SqlParameter("@SCHOOL", objEduInfo.School),
                new SqlParameter("@MAJOR", objEduInfo.Major),
                new SqlParameter("@YEAR", objEduInfo.Year)
            };

            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;
        }

        ///<summary>
        ///functionName(函数名)：UpdateEduRecordByID
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 15:17:10
        ///Update Date(更新日期): 2016-03-17 15:17:10
        /// </summary>
        /// <param name="objEduInfo"></param>
        /// <returns></returns>
        public static int UpdateEduRecordByID(HPStudent.Entity.SchoolDetail objEduInfo)
        {
            string sql = @" UPDATE SchoolDetail SET StartDate = @STARTDATE, EndDate =@ENDDATE, School=@SCHOOL, Major=@MAJOR, Year=@YEAR WHERE Did = @ID;";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@ID", objEduInfo.Did),
                new SqlParameter("@STARTDATE", objEduInfo.StartDate),
                new SqlParameter("@ENDDATE", objEduInfo.EndDate==DateTime.MinValue?DBNull.Value:(object)objEduInfo.EndDate),
                new SqlParameter("@SCHOOL", objEduInfo.School),
                new SqlParameter("@MAJOR", objEduInfo.Major),
                new SqlParameter("@YEAR", objEduInfo.Year)
            };
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;
        }

        ///<summary>
        ///functionName(函数名)：DeleteEduRecordByID
        ///
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 16:24:10
        ///Update Date(更新日期): 2016-03-17 16:24:10
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public static int DeleteEduRecordByID(int MID)
        {
            string sql = @" DELETE FROM SchoolDetail WHERE Did = @ID;";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@ID", MID) };
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;
        }

        ///<summary>
        ///functionName(函数名)：DeleteSchoolDetailByID
        ///通过主键ID列表 批量删除模型对象数据
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 17:36
        ///Update Date(更新日期): 2016-03-17 17:36
        /// </summary>
        /// <param name="lstID"></param>
        /// <returns></returns>
        public static int DeleteMultiEduRecordByIDs(List<int> lstID)
        {
            String ListString = string.Empty;
            foreach (int item in lstID)
            {
                ListString += (item + ",");
            }
            ListString = ListString.TrimEnd(',');
            string sql = "DELETE FROM SchoolDetail   WHERE Did IN (" + ListString + ") ; ";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        ///<summary>
        ///functionName(函数名)：FunctionName
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 15:26:10
        ///Update Date(更新日期): 2016-03-17 15:26:10
        /// </summary>       
        /// <param name="objWorkObj"></param>
        /// <returns></returns>
        public static int InsertNewWorkExpRecord(HPStudent.Entity.StudentWorkExp objWorkObj)
        {
            string sql = @"  INSERT INTO StudentWorkExp(SID,Company,Position, startDate, EndDate, JobContent) VALUES(@SID, @COMPANY,@POSITION, @STARTDATE, @ENDDATE, @JOBCONTENT);";
            SqlParameter[] paras = new SqlParameter[] { 
                //@WID,@SID, @COMPANY,@POSITION, @STARTDATE, @ENDDATE, @JOBCONTENT
                new SqlParameter("@SID", objWorkObj.SID),
                new SqlParameter("@STARTDATE", objWorkObj.startDate),
                new SqlParameter("@ENDDATE", (objWorkObj.EndDate==System.DateTime.MinValue)?System.DBNull.Value:(object)objWorkObj.EndDate),
                new SqlParameter("@COMPANY", objWorkObj.Company),
                new SqlParameter("@POSITION", objWorkObj.Position),
                new SqlParameter("@JOBCONTENT", objWorkObj.JobContent)
            };
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;
        }

        ///<summary>
        ///functionName(函数名)：FunctionName
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 15:43:10
        ///Update Date(更新日期): 2016-03-17 15:43:10
        /// </summary>      
        /// <param name="objWorkObj"></param>
        /// <returns></returns>
        public static int UpdateWorkExpRecordByID(HPStudent.Entity.StudentWorkExp objWorkObj)
        {
            string sql = @" UPDATE StudentWorkExp  SET Company=@COMPANY, Position=@POSITION, startDate=@STARTDATE, EndDate =@ENDDATE, JobContent=@JOBCONTENT WHERE WID=@ID;";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@ID", objWorkObj.WID),
                new SqlParameter("@STARTDATE", objWorkObj.startDate),
                new SqlParameter("@ENDDATE", objWorkObj.EndDate==DateTime.MinValue?DBNull.Value:(object)objWorkObj.EndDate),
                new SqlParameter("@COMPANY", objWorkObj.Company),
                new SqlParameter("@POSITION", objWorkObj.Position),
                new SqlParameter("@JOBCONTENT", objWorkObj.JobContent)
            };
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;
        }

        ///<summary>
        ///functionName(函数名)：DeleteStudentWorkExpByID
        ///通过主键ID删除模型对象数据
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 17:36
        ///Update Date(更新日期): 2016-03-17 17:36
        /// </summary>
        /// <param name="WID"></param>
        /// <returns></returns>
        public static int DeleteStudentWorkExpByID(int WID)
        {
            string sql = " DELETE FROM StudentWorkExp    WHERE WID=@WID ;";
            SqlParameter[] paras = { new SqlParameter("@WID", WID) };
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;
        }

        ///<summary>
        ///functionName(函数名)：DeleteStudentWorkExpByID
        ///通过主键ID列表 批量删除模型对象数据
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 17:36
        ///Update Date(更新日期): 2016-03-17 17:36
        /// </summary>
        /// <param name="lstID"></param>
        /// <returns></returns>
        public static int DeleteMultiStudentWorkExpByIDs(List<int> lstID)
        {
            String ListString = string.Empty;
            foreach (int item in lstID)
            {
                ListString += (item + ",");
            }
            ListString = ListString.TrimEnd(',');
            string sql = "DELETE FROM StudentWorkExp   WHERE WID IN (" + ListString + ") ; ";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        ///<summary>
        ///functionName(函数名)：InsertNewStudentProjectExpRecord
        ///增加StudentProjectExp模型对象数据
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 17:32
        ///Update Date(更新日期): 2016-03-17 17:32
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int InsertNewStudentProjectExpRecord(HPStudent.Entity.StudentProjectExp model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO StudentProjectExp(");
            sql.Append("SID,ProjectName,Position,startDate,EndDate,JobContent)");
            sql.Append(" VALUES (");
            sql.Append("@SID,@ProjectName,@Position,@startDate,@EndDate,@JobContent)");
            SqlParameter[] paras = {  
							new SqlParameter("@SID", model.SID),  
							new SqlParameter("@ProjectName", model.ProjectName),  
							new SqlParameter("@Position", model.Position),  
							new SqlParameter("@startDate", model.startDate),  
							new SqlParameter("@EndDate",  model.EndDate==DateTime.MinValue?DBNull.Value:(object)model.EndDate),  
							new SqlParameter("@JobContent", model.JobContent)						
							};
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);
            return iResult > 0 ? 0 : 1;
        }

        ///<summary>
        ///functionName(函数名)：UpdateStudentProjectExpByID
        ///通过ID识别 更新StudentProjectExp模型对象数据
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 17:58
        ///Update Date(更新日期): 2016-03-17 17:58
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateStudentProjectExpByID(HPStudent.Entity.StudentProjectExp model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE StudentProjectExp SET ");
            sql.Append("   SID=@SID");
            sql.Append(" ,  ProjectName=@ProjectName");
            sql.Append(" ,  Position=@Position");
            sql.Append(" ,  startDate=@startDate");
            sql.Append(" ,  EndDate=@EndDate");
            sql.Append(" ,  JobContent=@JobContent");
            sql.Append(" WHERE PID=@PID ");
            SqlParameter[] paras = {        
                                     new SqlParameter("@PID", model.PID),   new SqlParameter("@SID", model.SID),   new SqlParameter("@ProjectName", model.ProjectName),   new SqlParameter("@Position", model.Position),   new SqlParameter("@startDate", model.startDate),   new SqlParameter("@EndDate", model.EndDate==DateTime.MinValue?DBNull.Value:(object)model.EndDate),   new SqlParameter("@JobContent", model.JobContent),      
									                    
                            };
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);
            return iResult > 0 ? 0 : 1;
        }



        ///<summary>
        ///functionName(函数名)：DeleteStudentProjectExpByID
        ///通过主键ID删除模型对象数据
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 17:44
        ///Update Date(更新日期): 2016-03-17 17:44
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public static int DeleteStudentProjectExpByID(int PID)
        {
            string sql = " DELETE FROM StudentProjectExp    WHERE PID=@PID ;";
            SqlParameter[] paras = { new SqlParameter("@PID", PID) };
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;
        }

        ///<summary>
        ///functionName(函数名)：DeleteStudentProjectExpByID
        ///通过主键ID列表 批量删除模型对象数据
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-17 17:13
        ///Update Date(更新日期): 2016-03-17 17:13
        /// </summary>
        /// <param name="lstID"></param>
        /// <returns></returns>
        public static int DeleteMultiStudentProjectExpByIDs(List<int> lstID)
        {
            String ListString = string.Empty;
            foreach (int item in lstID)
            {
                ListString += (item + ",");
            }
            ListString = ListString.TrimEnd(',');
            string sql = "DELETE FROM StudentProjectExp   WHERE PID IN (" + ListString + ") ; ";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        ///<summary>
        ///functionName(函数名)：UpdateOrCreateResumeBasic
        ///更新或者添加新的求职意向选项
        ///Author(作者)：Sean.j
        ///Created Date(创建日期):2016-03-18 17:10:10
        ///Update Date(更新日期): 2016-03-18 17:10:10
        ///</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateOrCreateResumeBasic(HPStudent.Entity.StudentResume model)
        {
            string sql = @"SP_CREATEORUPDATERESUME ";
            SqlParameter[] paras = { new SqlParameter("@Res", SqlDbType.Int), new SqlParameter("@SID", model.SID), new SqlParameter("@CITYID", model.City), new SqlParameter("@STATUS", model.Status) };
            paras[0].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, paras);
            return Convert.ToInt32(paras[0].Value);
        }
    }
}
