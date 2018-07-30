using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HPStudent.ViewModel;
using HPStudent.Entity;
using System.Text;

namespace HPStudent.Data.Admin
{
    public class StudentInfo
    {
        public static List<ViewModel.Student.StudentInfo> GetStudentList(string SchoolID, string CID, string StartYear, string RealName, int start, int length, out int TotalRows)
        {
            string countSql = @"select  count(1) from dbo.StudentInfo a
left join StudentClass b on a.CID =b.CID 
left join  Common_School c on c.SchoolID =a.SchoolID
left join (
select SID,SUM(PaidFee)PaidFee
                         from FeeInfo f
                        where  f.IsCheck=1
                        group by SID
)e on a.StudentID=e.SID 
where a.SchoolID =@SchoolID and ( a.CID =@CID or @CID='') and (a.StartYear =@StartYear or @StartYear='')
and a.RealName like @RealName";
            SqlParameter[] param = { 
                                  new SqlParameter("@SchoolID",SchoolID ),
                                  new SqlParameter("@CID",CID ),
                                  new SqlParameter("@StartYear",StartYear ),
                                  new SqlParameter("@RealName","%"+RealName+"%" ),                       
                                   };
            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql, param);


            string sql = string.Format(@"select top {0} a.StudentID,  a.SchoolID , c.SchoolName,b.CID,b.CName, a.Avatar ,a.RealName,e.PaidFee,a.Credits,a.Sex ,a.IsActivated from dbo.StudentInfo a
left join StudentClass b on a.CID =b.CID 
left join  Common_School c on c.SchoolID =a.SchoolID
left join 
(select SID,SUM(PaidFee)PaidFee
                         from FeeInfo f
                        where  f.IsCheck=1
                        group by SID)e on a.StudentID=e.SID
where  a.StudentID not in(
select top {1} a.StudentID from
 StudentInfo a
left join StudentClass b on a.CID =b.CID 
left join  Common_School c on c.SchoolID =a.SchoolID
left join (select SID,SUM(PaidFee)PaidFee
                         from FeeInfo f
                        where  f.IsCheck=1
                        group by SID) e on a.StudentID=e.SID
where  a.SchoolID =@SchoolID and ( a.CID =@CID or @CID='') and (a.StartYear =@StartYear or @StartYear='')
and a.RealName like @RealName  order by  a.StudentID desc 
) and a.SchoolID =@SchoolID and ( a.CID =@CID or @CID='') and (a.StartYear =@StartYear or @StartYear='')
and a.RealName like @RealName order by  a.StudentID desc", length, start);

            //            string sql = @"select a.StudentID,  a.SchoolID , c.SchoolName,b.CID,b.CName, a.Avatar ,a.RealName,e.PaidFee,a.Credits,a.Sex ,a.IsActivated from dbo.StudentInfo a
            //left join StudentClass b on a.CID =b.CID 
            //left join  Common_School c on c.SchoolID =a.SchoolID
            //left join dbo.FeeInfo e on a.StudentID=e.SID 
            //where a.SchoolID =@SchoolID and ( a.CID =@CID or @CID='') and (a.StartYear =@StartYear or @StartYear='')
            //and a.RealName like @RealName
            //";
            List<ViewModel.Student.StudentInfo> StudentList = new List<ViewModel.Student.StudentInfo>();

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (System.Data.DataRow item in ds.Tables[0].Rows)
                {
                    ViewModel.Student.StudentInfo ViewStudent = new ViewModel.Student.StudentInfo();
                    ViewStudent.SchoolID = item["SchoolID"].ToString();
                    ViewStudent.SchoolName = item["SchoolName"].ToString();
                    ViewStudent.CID = item["CID"].ToString();
                    ViewStudent.CName = item["CName"].ToString();
                    ViewStudent.Credits = item["Credits"].ToString();
                    ViewStudent.Avatar = item["Avatar"].ToString();
                    ViewStudent.IsActivated = item["IsActivated"].ToString();
                    ViewStudent.PaidFee = item["PaidFee"].ToString();
                    ViewStudent.RealName = item["RealName"].ToString();
                    ViewStudent.Sex = item["Sex"].ToString();
                    ViewStudent.StudentID = item["StudentID"].ToString();

                    StudentList.Add(ViewStudent);
                }
            }
            return StudentList;
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
        public static int UpdateBaseInfo(string MajorID, string CID, string StudentID)
        {
            string sql = @"update StudentBaseInfo set MajorID =@MajorID where StudentID =@StudentID 
                            update StudentInfo set CID =@CID where StudentID =@StudentID ";
            SqlParameter[] param = { 
                                   new SqlParameter ("@MajorID",MajorID),
                                   new SqlParameter ("@CID",CID),
                                   new SqlParameter ("@StudentID",StudentID),
                                   };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);

            return i;

        }

        public static int UpdateStudentInfo(string RealName, string Brithday, string StartYear, string Sex, string StudentID,string UserRole)
        {
            string sql = @" update StudentInfo set RealName=@RealName,Brithday=@Brithday,StartYear=@StartYear,Sex =@Sex,RoleID=@RoleID  where StudentID =@StudentID 
                             ";
            SqlParameter[] param = { 
                                   new SqlParameter ("@RealName",RealName),
                                   new SqlParameter ("@Brithday",Brithday),
                                   new SqlParameter ("@StartYear",StartYear),
                                   new SqlParameter ("@Sex",Sex),
                                   new SqlParameter ("@StudentID",StudentID),
                                   new SqlParameter ("@RoleID",UserRole)
                                   };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);

            return i;

        }

        public static DataSet GetStudentScore(string SID)
        {
            string sql = "select * from StudentScore where [SID]=@SID";
            SqlParameter[] param = { 
                                   new SqlParameter ("@SID",SID)
                                   };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            return ds;
        }

        public static DataSet GetStudentScore(string SID, string Term)
        {
            string sql = "select * from StudentScore where [SID]=@SID and Term=@Term";
            SqlParameter[] param = { 
                                   new SqlParameter ("@SID",SID),
                                   new SqlParameter ("@Term",Term),
                                   };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            return ds;
        }

        public static int AddMark(Entity.StudentScore Score)
        {
            string sql = @"INSERT INTO [StudentScore]
           ([SID]
           ,[Term]
           ,[Examination1]
           ,[Examination2]
           ,[Evaluate])
     VALUES
           (
            @SID,@Term,@Examination1,@Examination2,@Evaluate
            )";
            SqlParameter[] param = { 
                                   new SqlParameter ("@SID",Score.SID),
                                   new SqlParameter ("@Term",Score.Term ),
                                    new SqlParameter ("@Examination1",Score.Examination1),
                                   new SqlParameter ("@Examination2",Score.Examination2),
                                    new SqlParameter ("@Evaluate",Score.Evaluate),                                  
                                   };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            return i;
        }

        public static int EditMark(Entity.StudentScore Score)
        {
            string sql = @"Update [StudentScore] set Examination1=@Examination1,@Examination2=Examination2,Evaluate=@Evaluate 
 where [SID]=@SID and Term=@Term";
            SqlParameter[] param = { 
                                   new SqlParameter ("@SID",Score.SID),
                                   new SqlParameter ("@Term",Score.Term ),
                                    new SqlParameter ("@Examination1",Score.Examination1),
                                   new SqlParameter ("@Examination2",Score.Examination2),
                                    new SqlParameter ("@Evaluate",Score.Evaluate),                                  
                                   };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            return i;
        }
        public static int DeleteMark(string SID, string Term)
        {
            string sql = @"delete from  [StudentScore] where [SID]=@SID and Term=@Term";
            SqlParameter[] param = { 
                                   new SqlParameter ("@SID",SID),
                                   new SqlParameter ("@Term",Term ),                                                               
                                   };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            return i;

        }

        public static object[] StudentInfoImport(DataSet ds, string[] SheetNames, string SchoolID, string Year)
        {
            StringBuilder sb = new StringBuilder();

            List<Entity.Major> MajorList = Admin.Exercises.GetMajorList();



            int SheetCount = 0;

            for (int i = 0; i < ds.Tables.Count; i = i + 2)
            {
                if (ds.Tables[i].Rows.Count > 0 && ds.Tables[i].Columns.Count > 1)
                {
                    SheetCount++;
                }
            }
            object[] obj = new object[SheetCount];

            for (int i = 0; i < ds.Tables.Count; i = i + 2)
            {


                if (ds.Tables[i].Rows.Count > 0 && ds.Tables[i].Columns.Count > 1)
                {
                    string stuClassName = "";
                    for (int k = 0; k < SheetNames.Length; k++)
                    {
                        if (SheetNames[k].ToString() + "class" == ds.Tables[i].TableName)
                        {
                            //创建班级

                            sb.Append(@"  declare @CID int 
                                          declare @numberCount int 
                                            set @numberCount=0
                                        if not  exists (select * from StudentClass where SchoolID ='");
                            sb.Append(SchoolID);
                            sb.Append(@"' and [Year] ='");
                            sb.Append(Year);
                            sb.Append(@"' and CCode='");
                            sb.Append(ds.Tables[i].Rows[0][0].ToString());
                            sb.Append(@"' and CName='");
                            stuClassName = ds.Tables[i].Rows[0][1].ToString();

                            sb.Append(stuClassName);
                            sb.Append(@"')
                                        begin
                                        insert into StudentClass(SchoolID,[Year] ,CCode ,CName ) values(
                                        '");
                            sb.Append(SchoolID);

                            sb.Append(@"   ','");
                            sb.Append(Year);

                            sb.Append(@"','");
                            sb.Append(ds.Tables[i].Rows[0][0].ToString());

                            sb.Append(@" ','");
                            sb.Append(ds.Tables[i].Rows[0][1].ToString());

                            sb.Append(@" '
                                        )
                                        select @CID=@@IDENTITY
                                        end
                                        else
                                        begin
                                        select  @CID=CID from StudentClass where SchoolID ='");
                            sb.Append(SchoolID);

                            sb.Append(@"   'and [Year] ='");
                            sb.Append(Year);
                            sb.Append(@"'and CCode='");
                            sb.Append(ds.Tables[i].Rows[0][0].ToString());
                            sb.Append(@" 'and CName='");
                            sb.Append(ds.Tables[i].Rows[0][1].ToString());
                            sb.Append(@" '
                                        end 
                                         ");
                            sb.Append(@"  DECLARE @StudentID int
                                        DECLARE @tran_error int ");
                            for (int j = 0; j < ds.Tables[i + 1].Rows.Count; j++)
                            {
                                DataRow row = ds.Tables[i + 1].Rows[j];
                                //如果该行为空行，则跳过
                                if (row["身份证号"] == null || row["身份证号"].ToString() == "" || row["身份证号"].ToString().Trim().Length > 18)
                                {
                                    continue;
                                }
                                DateTime myBirthday = new DateTime();
                                if (!DateTime.TryParse(row["出生年月"].ToString(), out myBirthday))
                                {
                                    continue;
                                }
                                //开始导入时，查询学生身份证号是否已存在数据库中，存在不导入
                                sb.Append(@"  if not exists( select * from 
                                       StudentbaseInfo where IDCard ='");
                                sb.Append(row["身份证号"].ToString().Trim());
                                sb.Append(@"')
                                       begin");
                                string TranName = "Tran_" + SheetNames[k] + "_" + j;
                                sb.Append(" BEGIN TRAN ");//+"Tran_AddStudent"+   --开始事务
                                sb.Append(TranName);
                                sb.Append(@" 
                                        SET @tran_error = 0
                                            BEGIN TRY       
                                            --开始添加学生 
                                            INSERT INTO StudentInfo
                                                   ([SchoolID],[RealName],[Email],[EmailStatus],[Password]
                                                   ,[Sex],[Brithday],[Avatar],[StartYear],[NewPM],[Credits]
                                                   ,[IsActivated],[Rank],[CID])
                                             VALUES(");
                                sb.Append("'" + SchoolID + "',");
                                sb.Append("'" + row["姓名"].ToString() + "',");
                                sb.Append("'" + row["QQ号"].ToString() + "@qq.com',");
                                sb.Append("'" + "0" + "',");
                                sb.Append("'" + row["身份证号"].ToString().Substring(row["身份证号"].ToString().Length - 6, 6) + "',");
                                sb.Append("'" + (row["性别"].ToString() == "男" ? 0 : 1) + "',");

                                sb.Append("'" + myBirthday + "',");
                                sb.Append("'" + "/Upload/Avatars/avatar.jpg" + "',");
                                sb.Append("'" + Year + "',");
                                sb.Append("'" + "0" + "',");
                                sb.Append("'" + "0" + "',");
                                sb.Append("'" + "0" + "',");
                                sb.Append("'" + "0" + "',");
                                sb.Append(@"@CID)");
                                sb.Append(@"
                                        select @StudentID=@@IDENTITY
                                        SET @tran_error = @tran_error + @@ERROR;
                                        INSERT INTO StudentBaseInfo
                                                   ([StudentID]
                                                   ,[StudentCode]
                                                   ,[MajorID]
                                                   ,[HighSchool]
                                                   ,[HomeAddress]
                                                   ,[PersonMobile]
                                                   ,[HomeMobile]
                                                   ,[Nation]
                                                   ,[QQ]
                                                   ,[IDCard]
                                                   ,[Unify])
                                             VALUES
                                                   (@StudentID,'");
                                sb.Append(row["学号"].ToString());
                                sb.Append("','");
                                foreach (Entity.Major item in MajorList)
                                {
                                    if (row["专业"].ToString() == item.MajorName)
                                    {
                                        sb.Append(item.MID);
                                    }
                                }
                                sb.Append("','");
                                sb.Append(row["毕业学校"].ToString());
                                sb.Append("','");
                                sb.Append(row["家庭详细通讯地址"].ToString());
                                sb.Append("','");
                                sb.Append(row["个人联系方式"].ToString());
                                sb.Append("','");
                                sb.Append(row["家庭联系方式"].ToString());
                                sb.Append("','");
                                sb.Append(row["民族"].ToString());
                                sb.Append("','");
                                sb.Append(row["QQ号"].ToString());
                                sb.Append("','");
                                sb.Append(row["身份证号"].ToString());
                                sb.Append("','");
                                sb.Append(row["学历"].ToString() == "统招" ? 0 : 1);
                                sb.Append(@"')
                                             SET @tran_error = @@ERROR;
                                             END TRY
                                             BEGIN CATCH   
                                                    SET @tran_error = @@ERROR
                                             END CATCH

                                             IF(@tran_error > 0)
                                                    BEGIN
                                                        --执行出错，回滚事务
  ROLLBACK TRAN;    
                                                        insert into ImportStudentError values('");
                                sb.Append(row["姓名"].ToString());
                                sb.Append(@"','");
                                sb.Append(row["身份证号"].ToString());
                                sb.Append(@"',GETDATE (),(select text from sys.messages where language_id='2052' and message_id = @tran_error ))                              
                                                         
                                                    END
                                             ELSE
                                                    BEGIN
                                                        --没有异常，提交事务
                                                         set @numberCount=@numberCount+1
                                                        COMMIT TRAN;       
                                                    END
                                        ");
                                sb.Append(" end");

                            }


                            //    HPStudent.Entity.StudentClass stuClass = new Entity.StudentClass();
                            //    stuClass.CCode = ds.Tables[i].Rows[0]["班级代码"].ToString();
                            //    stuClass.CName = ds.Tables[i].Rows[0]["班级名称"].ToString();
                            //    stuClass.SchoolID = Convert.ToInt32(SchoolID);
                            //    stuClass.Year = Convert.ToInt32(Year);

                            //    for (int j = 0; j < ds.Tables[i + 1].Rows.Count; j++)
                            //    {
                            //        DataRow row = ds.Tables[i + 1].Rows[j];
                            //        //   序号	班级	姓名	出生年月	性别	民族	学号	身份证号	学历	专业	毕业学校	个人联系方式	家庭联系方式	QQ号	家庭详细通讯地址
                            //        HPStudent.Entity.StudentInfo stuInfo = new Entity.StudentInfo();
                            //        stuInfo.Brithday = Convert.ToDateTime(row["出生年月"].ToString());
                            //        stuInfo.Sex = row["性别"].ToString() == "男" ? 0 : 1;
                            //        stuInfo.SchoolID = Convert.ToInt32(SchoolID);
                            //        stuInfo.Avatar = "";
                            //        stuInfo.Credits = 0;
                            //        stuInfo.Email = "";
                            //        stuInfo.IsActivated = 0;
                            //        stuInfo.LastLoginTime = DateTime.Now;
                            //        stuInfo.NewPM = 0;
                            //        stuInfo.Password = "123456";
                            //        stuInfo.Rank = 1;
                            //        stuInfo.RealName = row["姓名"].ToString();
                            //        stuInfo.StartYear = Convert.ToInt32(Year);
                            //        stuInfo.EmailStatus = 0;

                            //        HPStudent.Entity.StudentBaseInfo BaseInfo = new Entity.StudentBaseInfo();
                            //        BaseInfo.ClassCode = ds.Tables[i].Rows[0]["班级代码"].ToString();
                            //        BaseInfo.Class = ds.Tables[i].Rows[0]["班级名称"].ToString();
                            //        BaseInfo.HighSchool = row["毕业学校"].ToString();
                            //        BaseInfo.HomeAddress = row["家庭详细通讯地址"].ToString();
                            //        BaseInfo.HomeMobile = row["家庭联系方式"].ToString();
                            //        BaseInfo.IDCard = row["身份证号"].ToString();
                            //        foreach (Entity.Major item in MajorList)
                            //        {
                            //            if (row["专业"].ToString() == item.MajorName)
                            //            {
                            //                BaseInfo.MajorID = item.MID;
                            //            }
                            //        }
                            //        BaseInfo.Nation = row["民族"].ToString();
                            //        BaseInfo.PersonMobile = row["个人联系方式"].ToString();
                            //        BaseInfo.QQ = row["QQ号"].ToString();
                            //        BaseInfo.StudentCode = row["学号"].ToString();
                            //        BaseInfo.Unify = row["学历"].ToString() == "统招" ? 0 : 1;
                            //    }
                        }
                    }
                    sb.Append("   select @numberCount ");
                    string sql = sb.ToString();
                    object result = SqlHelper.ExecuteScalar(CommandType.Text, sql);
                    obj[i / 2] = new { successNum = result, className = stuClassName };
                }
            }


            return obj;
        }

        public static DataSet GetStudentFeeList(string SchoolID, string CID, string StartYear, string RealName, bool IsCheck, int start, int length, out int TotalRows)
        {
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append(@"select count(1)
                        from dbo.StudentInfo a
                        left join StudentClass b on a.CID =b.CID 
                        left join  Common_School c on c.SchoolID =a.SchoolID
                        left join  (
                        select SID,SUM(PaidFee)PaidFee,COUNT (1)  AllCount,
                        (select COUNT(1) from FeeInfo as fi where fi.SID = f.SID and fi.IsCheck=1)  CheckCount
                         from FeeInfo f
                        where  f.IsCheck<>2 
                        group by SID
                        ) e on a.StudentID=e.SID 
                        where 
                        a.SchoolID =@SchoolID and 
                        ( a.CID =@CID or @CID='') 
                        and (a.StartYear =@StartYear or @StartYear='')
                        and a.RealName like @RealName
                         ");
            if (IsCheck == true)
            {
                sbCount.Append(" and e.AllCount <> e.CheckCount ");
            }
            else
            {
                sbCount.Append(" and (e.AllCount = e.CheckCount or  e.AllCount <> e.CheckCount ) ");
            }
            SqlParameter[] param = { 

                                   new SqlParameter ("@SchoolID",SchoolID),
                                   new SqlParameter ("@CID",CID),
                                     new SqlParameter ("@StartYear",StartYear),
                                   new SqlParameter ("@RealName","%"+RealName+"%"),

                                   };
            TotalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sbCount.ToString(), param));

            StringBuilder sb = new StringBuilder();
            sb.Append(@"select top {0}  *   from dbo.StudentInfo a
                        left join StudentClass b on a.CID =b.CID 
                        left join  Common_School c on c.SchoolID =a.SchoolID
                        inner join  (
                        select SID,SUM(PaidFee)PaidFee,COUNT (1)  AllCount,
                        (select COUNT(1) from FeeInfo as fi where fi.SID = f.SID and fi.IsCheck=1)  CheckCount
                         from FeeInfo f
                        where  f.IsCheck<>2 
                        group by SID
                        ) e on a.StudentID=e.SID 
                         where  1=1
                         and a.SchoolID =@SchoolID and 
                        ( a.CID =@CID or @CID='') 
                        and (a.StartYear =@StartYear or @StartYear='')
                        and a.RealName like @RealName ");
            if (IsCheck == true)
            {
                sb.Append(" and e.AllCount <> e.CheckCount ");
            }
            else
            {
                sb.Append(" and (e.AllCount = e.CheckCount or  e.AllCount <> e.CheckCount ) ");
            }
            sb.Append(@"   and a.StudentID not in(
select top {1} a.StudentID
                        from dbo.StudentInfo a
                        left join StudentClass b on a.CID =b.CID 
                        left join  Common_School c on c.SchoolID =a.SchoolID
                        inner join  (
                        select SID,SUM(PaidFee)PaidFee,COUNT (1)  AllCount,
                        (select COUNT(1) from FeeInfo as fi where fi.SID = f.SID and fi.IsCheck=1)  CheckCount
                         from FeeInfo f
                        where  f.IsCheck<>2 
                        group by SID
                        ) e on a.StudentID=e.SID 
                        where 
                        a.SchoolID =@SchoolID and 
                        ( a.CID =@CID or @CID='') 
                        and (a.StartYear =@StartYear or @StartYear='')
                        and a.RealName like @RealName ");
            if (IsCheck == true)
            {
                sb.Append(" and e.AllCount <> e.CheckCount ");
            }
            else
            {
                sb.Append(" and (e.AllCount = e.CheckCount or  e.AllCount <> e.CheckCount ) ");
            }
            sb.Append(" )");

            string sql = string.Format(sb.ToString(), length, start);
            //            StringBuilder sb = new StringBuilder();
            //            sb.Append(@"select a.StudentID,  a.SchoolID , c.SchoolName,b.CID,b.CName, a.Avatar ,a.RealName,e.PaidFee,a.Credits,a.Sex ,a.IsActivated 
            //                        ,isnull(e.AllCount,0) AllCount ,isnull(e.CheckCount,0) CheckCount
            //                        from dbo.StudentInfo a
            //                        left join StudentClass b on a.CID =b.CID 
            //                        left join  Common_School c on c.SchoolID =a.SchoolID
            //                        left join  (
            //                        select SID,SUM(PaidFee)PaidFee,COUNT (1)  AllCount,
            //                        (select COUNT(1) from FeeInfo as fi where fi.SID = f.SID and fi.IsCheck=1)  CheckCount
            //                         from FeeInfo f
            //                        where  f.IsCheck<>2 
            //                        group by SID
            //                        ) e on a.StudentID=e.SID 
            //                        where 
            //                        a.SchoolID =@SchoolID and 
            //                        ( a.CID =@CID or @CID='') 
            //                        and (a.StartYear =@StartYear or @StartYear='')
            //                        and a.RealName like @RealName
            //                         ");
            //            if (IsCheck == true)
            //            {
            //                sb.Append(" and e.AllCount <> e.CheckCount ");
            //            }
            //            else
            //            {
            //                sb.Append(" and (e.AllCount = e.CheckCount or  e.AllCount <> e.CheckCount ) ");
            //            }

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            return ds;

        }

        public static DataSet GetStudentFeeBySID(string StudentID)
        {
            string sql = @"select F.*,A.Attachment,isnull(A.Dateline,0)Dateline,A.FAID,A.Fee,A.FeeDescription,A.FeeTitle from FeeInfo F 
                            left join FeeAttachment A on f.FeeID =A.FeeID
                            where [SID] =@StudentID";
            SqlParameter[] param = { 
                                   new SqlParameter ("@StudentID",StudentID),                                                                                       
                                   };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, param);
            return ds;
        }
        /// <summary>
        /// 编辑学生费用审核状态 0，取消，1审核通过，2.退回
        /// </summary>
        /// <param name="FeeID"></param>
        /// <returns></returns>
        public static int EditStudentFeeIsCheck(string FeeID, int IsCheck)
        {
            string sql = @"update FeeInfo set IsCheck=@IsCheck where FeeID=@FeeID";
            SqlParameter[] param = { 
                                   new SqlParameter ("@IsCheck",IsCheck),
                                   new SqlParameter ("@FeeID",FeeID),
                                   };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            return i;
        }
    }
}
