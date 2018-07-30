using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
    public class ClassManage
    {

        /// <summary>
        /// 查询所有实训班级
        /// </summary>
        /// <returns>实训班级集合</returns>
        public static List<HPStudent.Student.ViewModel.ClassManage.PTClassTable> GetPTClassTable(int Year) 
        {
            string sql = string.Format("select *,[StudentName]=stuff((select ','+[RealName] from StudentInfo a where StudentID in (select * from f_splitSTR(b.StudentID,',')) for xml path('')), 1, 1, '') from PTClass  b where b.Year = {0} ", Year);
            List<HPStudent.Student.ViewModel.ClassManage.PTClassTable> list = null;
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dt != null)
            {
                list = new List<HPStudent.Student.ViewModel.ClassManage.PTClassTable>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HPStudent.Student.ViewModel.ClassManage.PTClassTable table = new HPStudent.Student.ViewModel.ClassManage.PTClassTable();
                    table.PTCID = Convert.ToInt32(dt.Rows[i]["PTCID"]);
                    table.PTCName = Convert.ToString(dt.Rows[i]["PTCName"]);
                    table.StudentID = Convert.ToString(dt.Rows[i]["StudentID"]);
                    table.StudentName = Convert.ToString(dt.Rows[i]["StudentName"]);
                    table.TeacherID = Convert.ToString(dt.Rows[i]["TeacherID"]);
                    list.Add(table);
                }

                return list;
            }
            list = new List<HPStudent.Student.ViewModel.ClassManage.PTClassTable>();
            return list;
        
        
        }


        public static List<HPStudent.Entity.Common_School> GetSchoolSelBind()
        {
            List<HPStudent.Entity.Common_School> list = null;
            string sql = "select * from Common_School";
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                list = new List<Entity.Common_School>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entity.Common_School entity = new Entity.Common_School();
                    entity.SchoolID = Convert.ToInt32(dt.Rows[i]["SchoolID"]);
                    entity.SchoolName = Convert.ToString(dt.Rows[i]["SchoolName"]);
                    list.Add(entity);
                }
            }
            else
            {
                list = new List<Entity.Common_School>();
            }

            return list;
        }


        public static List<HPStudent.Entity.StudentClass> GetClassSelBind(int Year, int School)
        {
            List<HPStudent.Entity.StudentClass> list = null;
            string sql = string.Format("select * from StudentClass where Year={0} and SchoolID = {1}", Year, School);
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                list = new List<Entity.StudentClass>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entity.StudentClass entity = new Entity.StudentClass();
                    entity.CID = Convert.ToInt32(dt.Rows[i]["CID"]);
                    entity.CName = Convert.ToString(dt.Rows[i]["CName"]);
                    list.Add(entity);
                }
            }
            else
            {
                list = new List<Entity.StudentClass>();
            }

            return list;

        }

        public static List<HPStudent.Entity.StudentInfo> GetClassStudentBind(int PTCID,int School,int Class,int Year)
        {
            List<HPStudent.Entity.StudentInfo> list = null;
            StringBuilder sb = new StringBuilder();
  
                sb.Append("declare  @StudentID nvarchar(1000)  ");
                sb.Append("set @StudentID  = (select top 1 [values]=stuff((select ','+[StudentID] from PTClass t  for xml path('')), 1, 1, '')  from PTClass where Year=" + Year + ")  ");
                sb.Append("if(@StudentID is null) begin  set @StudentID = '' end  ");
                sb.Append("select a.StudentID,a.RealName from StudentInfo a inner join Common_School b on a.SchoolID = b.SchoolID inner join StudentClass c on a.CID = c.CID where StudentID  not in (select * from f_splitSTR(@StudentID,',')) and a.CID = '" + Class + "' and a.SchoolID = '" + School + "'");
       
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sb.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                list = new List<Entity.StudentInfo>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entity.StudentInfo entity = new Entity.StudentInfo();
                    entity.StudentID = Convert.ToInt32(dt.Rows[i]["StudentID"]);
                    entity.RealName = Convert.ToString(dt.Rows[i]["RealName"]);
                    list.Add(entity);
                }
            }
            else
            {
                list = new List<Entity.StudentInfo>();
            }

            return list;

        }


        public static int Add(HPStudent.Entity.PTClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PTClass(");
            strSql.Append("PTCName,StudentID,TeacherID,Year)");
            strSql.Append(" values (");
            strSql.Append("@PTCName,@StudentID,@TeacherID,@Year)");
            SqlParameter[] parameters = {
                                new SqlParameter("@PTCName", model.PTCName),
                                                        
                                new SqlParameter("@StudentID", model.StudentID),

                                 new SqlParameter("@TeacherID", model.TeacherID),

                                 new SqlParameter("@Year", model.Year),
                    
                                            
            };
            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }


        public static int Update(HPStudent.Entity.PTClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PTClass set ");

            strSql.Append("    PTCName=@PTCName");

            strSql.Append(" ,  StudentID=@StudentID");

            strSql.Append(" ,  TeacherID=@TeacherID");

            strSql.Append(" ,  Year=@Year");

            strSql.Append(" where PTCID=@PTCID ");
            SqlParameter[] parameters = {
                                new SqlParameter("@PTCName", model.PTCName),
                                                        
                                new SqlParameter("@StudentID", model.StudentID),
                                                        
                                new SqlParameter("@TeacherID", model.TeacherID),

                                new SqlParameter("@Year", model.Year),

                                new SqlParameter("@PTCID", model.PTCID)
                                                    
                    };
            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return rows;

        }

        public static int Delete(int PTCID) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PTClass ");
            strSql.Append(" where PTCID=@PTCID ");
            SqlParameter[] parameters = {
            	  	new SqlParameter("@PTCID", PTCID)			};
            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return rows;
        
        }


        public static HPStudent.Student.ViewModel.ClassManage.PTClassTable GetClass(int PTCID) 
        {

            HPStudent.Student.ViewModel.ClassManage.PTClassTable entity = null;
            List<HPStudent.Entity.StudentInfo> list = null;
            string sql = "select * from PTClass where PTCID = " + PTCID + "";
            string getStuSql = string.Format( @"declare  @StudentID nvarchar(1000) 
                                 set @StudentID  = (select top 1 [values]=stuff((select ','+[StudentID] 
                                 from PTClass t where PTCID = {0} for xml path('')), 1, 1, '')  from PTClass ) 
                                 select * from StudentInfo where StudentID in (select * from f_splitSTR(@StudentID,','))", PTCID);
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            DataTable getStuDt = SqlHelper.ExecuteDataset(CommandType.Text, getStuSql).Tables[0];
            if (dt.Rows.Count>0)
            {
                entity = new ViewModel.ClassManage.PTClassTable();
                entity.PTCID = Convert.ToInt32(dt.Rows[0]["PTCID"]);
                entity.StudentID = Convert.ToString(dt.Rows[0]["StudentID"]);
                entity.PTCName = Convert.ToString(dt.Rows[0]["PTCName"]);
                list = new List<HPStudent.Entity.StudentInfo>();
                if (getStuDt.Rows.Count > 0)
                {

                    for (int i = 0; i < getStuDt.Rows.Count; i++)
                    {
                        HPStudent.Entity.StudentInfo model = new Entity.StudentInfo();
                        model.StudentID = Convert.ToInt32(getStuDt.Rows[i]["StudentID"]);
                        model.RealName = Convert.ToString(getStuDt.Rows[i]["RealName"]);
                        list.Add(model);
                    }
                }
                entity.StudentInfoList = list;
            }
            else 
            {
                entity = new ViewModel.ClassManage.PTClassTable();
            }

            return entity;
        }


        public static List<HPStudent.Student.ViewModel.ClassManage.ClassStudentTable> GetClassStudentInfo(int start, int length, int PTCID, out int TotalRows) 
        {
            string countSql = string.Format(@"declare  @StudentID nvarchar(1000) 
                                set @StudentID  = (select top 1 [values]=stuff((select ','+[StudentID] 
                                from PTClass t where PTCID = {0} for xml path('')), 1, 1, '')  from PTClass ) 
                                select Count(*) 
                                from StudentInfo where StudentID in (select * from f_splitSTR(@StudentID,',')) ", PTCID);
            string sql = string.Format(@"declare  @StudentID nvarchar(1000) 
                                set @StudentID  = (select top 1 [values]=stuff((select ','+[StudentID] 
                                from PTClass t where PTCID = {0} for xml path('')), 1, 1, '')  from PTClass ) 
                                select top {1} StudentID,RealName,Sex,lastLoginTime,(select Count(*) from StudentProjectExp  where SID = StudentID) as ResumeStatus
                                from StudentInfo where StudentID in (select * from f_splitSTR(@StudentID,',')) and StudentID not in (select top {2} StudentID from StudentInfo where StudentID in (select * from f_splitSTR(@StudentID,',')) )
            ", PTCID, length, start);

            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql);
            List<HPStudent.Student.ViewModel.ClassManage.ClassStudentTable> list = null;
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dt != null)
            {
                list = new List<HPStudent.Student.ViewModel.ClassManage.ClassStudentTable>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HPStudent.Student.ViewModel.ClassManage.ClassStudentTable table = new HPStudent.Student.ViewModel.ClassManage.ClassStudentTable();
                    table.StudentID = Convert.ToInt32(dt.Rows[i]["StudentID"]);
                    table.RealName = Convert.ToString(dt.Rows[i]["RealName"]);
                    table.Sex = Convert.ToInt32(dt.Rows[i]["Sex"]);
                    table.LastLoginTime = Convert.ToString( dt.Rows[i]["LastLoginTime"]);
                    table.ResumeStatus = Convert.ToInt32(dt.Rows[i]["ResumeStatus"]);
                    list.Add(table);
                }

                return list;
            }
            list = new List<HPStudent.Student.ViewModel.ClassManage.ClassStudentTable>();
            return list;
        
        }


        public static ViewModel.Student.StudentInfo GetStudentByID(string StudentID)
        {
            string sql = @"select a.StartYear,a.Sex,  a.StudentID,f.StudentCode ,  a.SchoolID , c.SchoolName,b.CID,b.CName,b.CCode , a.Avatar ,a.RealName,isnull(e.PaidFee,0)PaidFee,a.Credits,a.Sex ,a.IsActivated 
                           ,a.RoleID,isnull(a.LastLoginTime,0)LastLoginTime,a.Brithday ,f.Nation,a.Email,f.MajorID,f.HighSchool,f.HomeAddress,f.PersonMobile,f.HomeMobile,f.QQ,f.IDCard,f.Unify
                           ,r.MajorName 
                            from dbo.StudentInfo a
                            left join StudentClass b on a.CID =b.CID 
                            left join StudentBaseInfo f on f.StudentID=a.StudentID
                            left join  Common_School c on c.SchoolID =a.SchoolID
                            left join FeeInfo e on a.StudentID=e.SID 
                            left join Major r on r.MID =f.MajorID 
                            where a.StudentID=@StudentID ";
            SqlParameter[] param = { 
                                   new SqlParameter ("@StudentID",StudentID),
                                   };
            ViewModel.Student.StudentInfo student = new ViewModel.Student.StudentInfo();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, param))
            {
                if (dr.Read())
                {
                    student.StudentID = dr["StudentID"].ToString();
                    student.SchoolName = dr["SchoolName"].ToString();
                    student.RealName = dr["RealName"].ToString();
                    student.PaidFee = dr["PaidFee"].ToString();
                    student.Avatar = dr["Avatar"].ToString();
                    student.Credits = dr["Credits"].ToString();
                    student.CID = dr["CID"].ToString();
                    student.CName = dr["CName"].ToString();
                    student.CCode = dr["CCode"].ToString();
                    student.StudentCode = dr["StudentCode"].ToString();
                    student.Sex = dr["Sex"].ToString();
                    student.StartYear = dr["StartYear"].ToString();
                    student.Nation = dr["Nation"].ToString();
                    student.Brithday = Convert.ToDateTime(dr["Brithday"].ToString());
                    student.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"].ToString());
                    student.Email = dr["Email"].ToString();
                    student.HighSchool = dr["HighSchool"].ToString();
                    student.MajorID = dr["MajorID"].ToString();
                    student.QQ = dr["QQ"].ToString();
                    student.HomeAddress = dr["HomeAddress"].ToString();
                    student.HomeMobile = dr["HomeMobile"].ToString();
                    student.IDCard = dr["IDCard"].ToString();
                    student.Unify = dr["Unify"].ToString();
                    student.MajorName = dr["MajorName"].ToString();
                    student.SchoolID = dr["SchoolID"].ToString();
                    student.RoleID = Convert.ToInt32(string.IsNullOrEmpty(dr["RoleID"].ToString()) == true ? "0" : dr["RoleID"].ToString());
                }
            }
            return student;
        }

        public static List<HPStudent.Entity.Major> GetMajorList()
        {
            List<HPStudent.Entity.Major> MajorList = new List<HPStudent.Entity.Major>();
            string sql = "SELECT * FROM Major";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    MajorList.Add(
                        new HPStudent.Entity.Major()
                        {
                            MID = Convert.ToInt32(reader["MID"].ToString()),
                            MajorName = reader["MajorName"].ToString()
                        }
                    );
                }
            }
            return MajorList;

        }

        /// <summary>
        /// 根据校区和年份，搜索班级
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static List<HPStudent.Entity.StudentClass> GetStudentClassBySchoolID(int SchoolID, int year)
        {
            string sql = "select * from StudentClass where SchoolID=@SchoolID and [year]=@year ";
            SqlParameter[] param ={
                                    new SqlParameter("@SchoolID",SchoolID),
                                    new SqlParameter("@year",year),
                                  };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            List<Entity.StudentClass> StuClassList = new List<Entity.StudentClass>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    HPStudent.Entity.StudentClass stuClass = new Entity.StudentClass();
                    stuClass.SchoolID = Convert.ToInt32(ds.Tables[0].Rows[i]["SchoolID"].ToString());
                    stuClass.CID = Convert.ToInt32(ds.Tables[0].Rows[i]["CID"].ToString());
                    stuClass.CName = ds.Tables[0].Rows[i]["CName"].ToString();
                    stuClass.CCode = ds.Tables[0].Rows[i]["CCode"].ToString();
                    stuClass.Year = Convert.ToInt32(ds.Tables[0].Rows[i]["Year"].ToString());
                    StuClassList.Add(stuClass);
                }
            }
            return StuClassList;
        }

        public static List<HPStudent.Entity.UserRole> GetUserRoleListNotPage()
        {
            string sql = string.Format(@" select  RID,RoleName,SortCode from UserRole as u ");
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<HPStudent.Entity.UserRole> lstRes = new List<HPStudent.Entity.UserRole>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HPStudent.Entity.UserRole objTemp = new HPStudent.Entity.UserRole();
                    objTemp.RID = Convert.ToInt32(item["RID"]);
                    objTemp.RoleName = item["RoleName"].ToString();
                    objTemp.SortCode = Convert.ToInt32(item["SortCode"]);
                    lstRes.Add(objTemp);
                }
                return lstRes;
            }
            return new List<HPStudent.Entity.UserRole>();
        }

        public static int UpdateStuInfo(HPStudent.Entity.StudentInfo stuInfo, HPStudent.Entity.StudentBaseInfo stuBaseInfo) 
        {
            string sql = @" update StudentInfo set Email =@Email, RealName=@RealName,StartYear=@StartYear,Sex =@Sex,Password=@Password where StudentID =@StudentID 
                             ";
            SqlParameter[] param = { 
                                   new SqlParameter ("@Email",stuInfo.Email),
                                   new SqlParameter ("@RealName",stuInfo.RealName),
                                   new SqlParameter ("@StartYear",stuInfo.StartYear),
                                   new SqlParameter ("@Sex",stuInfo.Sex),
                                   new SqlParameter ("@StudentID",stuInfo.StudentID),
                                   new SqlParameter ("@Password",stuInfo.Password),
                                   };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);

            sql = @"update StudentBaseInfo set Nation=@Nation, HomeAddress=@HomeAddress,HomeMobile=@HomeMobile,QQ=@QQ,IDCard =@IDCard where StudentID =@StudentID ";
            SqlParameter[] paramBase = { 
                                   new SqlParameter ("@Nation",stuBaseInfo.Nation),
                                   new SqlParameter ("@HomeAddress",stuBaseInfo.HomeAddress),
                                   new SqlParameter ("@HomeMobile",stuBaseInfo.HomeMobile),
                                   new SqlParameter ("@QQ",stuBaseInfo.QQ),
                                   new SqlParameter ("@IDCard",stuBaseInfo.IDCard),
                                   new SqlParameter ("@StudentID",stuInfo.StudentID)
                                   };
            int j = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paramBase);

            if (i == 1 && j == 1)
            {
                return 1;
            }
            else 
            {
                return 0;
            }
            
        
        }

    }
}
