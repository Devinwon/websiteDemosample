using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;
using ProjectVModel = HPStudent.ViewModel.Projects;

namespace HPStudent.Data.Admin
{
    public class Projects
    {

        public static ProjectVModel.ProjectBook GetProjectBookByPID(int PID)
        {


            ProjectVModel.ProjectBook myProjectBook = new  ProjectVModel.ProjectBook();
            string sql = string.Format(@"SELECT TOP 1 PID , MID , ProjectName ,TeacherID , ClassHour , UseTechnology , CreateDate , EditDate , ProjectPic , ProjectDesc , ShowPart
                                        FROM ProjectBook 
                                        WHERE PID = @PID");
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@PID", PID),
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                while (reader.Read())
                {
                    DateTime myDate = new DateTime();
                    
                    myProjectBook.ClassHour = Convert.ToInt32(reader["ClassHour"].ToString());
                    DateTime.TryParse(reader["EditDate"].ToString(), out myDate);
                    myProjectBook.EditDate = myDate;
                    DateTime.TryParse(reader["CreateDate"].ToString(), out myDate);
                    myProjectBook.CreateDate = myDate;
                    myProjectBook.MID = Convert.ToInt32(reader["MID"].ToString());
                    myProjectBook.PID = Convert.ToInt32(reader["PID"].ToString());
                    myProjectBook.ProjectDesc= reader["ProjectDesc"].ToString();
                    myProjectBook.ProjectName= reader["ProjectName"].ToString();
                    myProjectBook.ProjectPic= reader["ProjectPic"].ToString();
                    myProjectBook.selTeacherID= Convert.ToInt32(reader["TeacherID"].ToString());
                    myProjectBook.TeacherID= Convert.ToInt32(reader["TeacherID"].ToString());
                    myProjectBook.UseTechnology= reader["UseTechnology"].ToString();

                    myProjectBook.ShowPart = Convert.ToInt32(reader["ShowPart"].ToString());

                    
                }
            }
            return myProjectBook;

        }


        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="myProjectItem"></param>
        /// <returns></returns>
        public static int AddProjectItem(Entity.ProjectItem myProjectItem)
        {
            string sql = "BEGIN TRY; "
                + " BEGIN TRAN; "
                + "      INSERT INTO ProjectItem ([PID] , [ProjectName] , [PPT]  , [WORD] , [PDF] , [Video]  ,[URL] , [CreateDate] , [EditDate]) "
                + "      VALUES(@PID , @ProjectName , @PPT , @WORD , @PDF , @Video , @URL , @CreateDate , @EditDate); "
                + "      UPDATE ProjectBook SET ClassHour = (SELECT  COUNT(ID) FROM ProjectItem WHERE PID=@PID) WHERE PID = @PID; "
                + "      COMMIT TRAN "
                + "  END TRY "
                + "BEGIN CATCH "
                + "  ROLLBACK TRAN "
                + "END CATCH ";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@PID", myProjectItem.PID),
                new SqlParameter("@ProjectName", myProjectItem.ProjectName),
                new SqlParameter("@PPT", myProjectItem.PPT),
                new SqlParameter("@WORD", myProjectItem.WORD),
                new SqlParameter("@PDF", myProjectItem.PDF),
                new SqlParameter("@Video", myProjectItem.Video),
                new SqlParameter("@URL", myProjectItem.URL),
                new SqlParameter("@CreateDate", myProjectItem.CreateDate),
                new SqlParameter("@EditDate", myProjectItem.EditDate),
            };
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;       
        }


        /// <summary>
        /// 得到所有专业列表
        /// </summary>
        /// <returns></returns>
        public static List<HPStudent.Entity.Major> GetMajorList()
        {
            List<HPStudent.Entity.Major> MajorList = new List<Major>();
            string sql = "SELECT * FROM Major";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    MajorList.Add(
                        new Major()
                        {
                            MID = Convert.ToInt32(reader["MID"].ToString()),
                            MajorName = reader["MajorName"].ToString()
                        }
                    );
                }
            }
            return MajorList;

        }

        public static List<HPStudent.ViewModel.Projects.ProjectBook> GetProjectBookList(int mid, int start, int length,out int TotalRows)
        {
            //获得符合要求的记录总数
            string countSql = "select count(pid) from ProjectBook where MID = @MID AND Status =0";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MID", mid)
            };
            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql, paras);

            List<HPStudent.ViewModel.Projects.ProjectBook> ProjectBookList = new List<ViewModel.Projects.ProjectBook>();
            string sql = string.Format(@"SELECT TOP {0} P.PID , P.ProjectName , T.TeacherName , ClassHour ,CreateDate , EditDate  FROM ProjectBook P
                                        LEFT JOIN TeacherInfo T ON P.TeacherID = T.TID 
                                        WHERE P.PID NOT IN (SELECT TOP {1} PID FROM ProjectBook WHERE MID = @MID AND Status =0 ORDER BY PID DESC)
                                        AND MID = @MID AND Status =0 ORDER BY PID DESC", length, start);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                while (reader.Read())
                {
                    ProjectBookList.Add(
                        new HPStudent.ViewModel.Projects.ProjectBook()
                        {
                            PID = Convert.ToInt32(reader["PID"].ToString()),
                            ProjectName = reader["ProjectName"].ToString(),
                            TeacherName = reader["TeacherName"].ToString(),
                            ClassHour = Convert.ToInt32(reader["ClassHour"].ToString()),
                            CreateDate = Core.DateHelper.ToDate(reader["CreateDate"].ToString()),
                            EditDate = Core.DateHelper.ToDate(reader["EditDate"].ToString()),
                        }
                    );
                }
            }
            return ProjectBookList;
        }

        public static bool DelProject(int PID)
        {
            string sql = @"UPDATE [ProjectBook] SET STATUS = 1 WHERE PID = @PID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@PID", PID),
            }; 
            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? true : false;
        }
        public static int AddProject(HPStudent.ViewModel.Projects.ProjectBook project)
        {
            string sql = @"INSERT INTO [ProjectBook] ( [MID],[ProjectName],[TeacherID],[ClassHour],[UseTechnology],[CreateDate] ,[EditDate],[ProjectPic],[ProjectDesc]  , [Status] ,[ShowPart])
                        VALUES(@MID , @ProjectName ,@TeacherID , @ClassHour , @UseTechnology ,  GETDATE() ,  GETDATE() , @ProjectPic , @ProjectDesc , 0 , @ShowPart)";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MID", project.MID),
                new SqlParameter("@ProjectName", project.ProjectName),
                new SqlParameter("@TeacherID", project.TeacherID),
                new SqlParameter("@ClassHour", project.ClassHour),
                new SqlParameter("@UseTechnology", project.UseTechnology),
                new SqlParameter("@ProjectPic", project.ProjectPic),
                new SqlParameter("@ProjectDesc", project.ProjectDesc),
                new SqlParameter("@ShowPart", project.ShowPart),
            };

            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;
        }

        public static int EditProject(HPStudent.ViewModel.Projects.ProjectBook project)
        {
            string sql = @"UPDATE [ProjectBook] SET MID = @MID , ProjectName = @ProjectName , TeacherID = @TeacherID , 
                        ClassHour = (SELECT COUNT(ID) FROM ProjectItem WHERE PID = @PID) ,
                        UseTechnology = @UseTechnology , EditDate = GETDATE() , ProjectPic = @ProjectPic , ProjectDesc = @ProjectDesc , ShowPart = @ShowPart
                        WHERE PID=@PID";

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@PID", project.PID),
                new SqlParameter("@MID", project.MID),
                new SqlParameter("@ProjectName", project.ProjectName),
                new SqlParameter("@TeacherID", project.TeacherID),
                new SqlParameter("@UseTechnology", project.UseTechnology),
                new SqlParameter("@ProjectPic", project.ProjectPic),
                new SqlParameter("@ProjectDesc", project.ProjectDesc),
                new SqlParameter("@ShowPart", project.ShowPart),
            };

            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;
        }

        
        public static ProjectVModel.ProjectDetail GetProjectDetailByPID(int PID)
        {
            ProjectVModel.ProjectDetail myProject = new ProjectVModel.ProjectDetail();
            string sql = @"SELECT T.TeacherName,PID,MID,ProjectName,TeacherID,ClassHour,UseTechnology,CreateDate,EditDate,ProjectPic,ProjectDesc 
                                    FROM ProjectBook P LEFT JOIN TeacherInfo T ON P.TeacherID = T.TID
                                    WHERE PID= @PID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@PID", PID)
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                while (reader.Read())
                {
                    DateTime myDate = new DateTime();
                    myProject.ClassHour = Convert.ToInt32(reader["ClassHour"].ToString());
                    DateTime.TryParse(reader["CreateDate"].ToString(), out myDate);
                    myProject.CreateDate = myDate;
                    DateTime.TryParse(reader["EditDate"].ToString(), out myDate);
                    myProject.EditDate = myDate;
                    myProject.MID = Convert.ToInt32(reader["MID"].ToString());
                    myProject.PID = Convert.ToInt32(reader["PID"].ToString());
                    myProject.ProjectDesc = reader["ProjectDesc"].ToString();
                    myProject.ProjectName = reader["ProjectName"].ToString();
                    myProject.ProjectPic = reader["ProjectPic"].ToString();
                    myProject.TeacherID = Convert.ToInt32(reader["PID"].ToString());
                    myProject.UseTechnology = reader["UseTechnology"].ToString();
                    myProject.TeacherName = reader["TeacherName"].ToString();
                }
            }

            return myProject;
        }

        public static List<HPStudent.Entity.ProjectItem> GetProjectItemListByPID(int PID)
        {
            List<HPStudent.Entity.ProjectItem> ProjectItemList = new List<ProjectItem>();
            //获取ProjectItemList
            string itemSql = @"SELECT ID,PID,ProjectName,PPT,WORD,PDF,Video,URL,CreateDate,EditDate FROM dbo.ProjectItem
                                            WHERE PID=@PID"; 
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@PID", PID)
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, itemSql, paras))
            {
                while (reader.Read())
                {
                    HPStudent.Entity.ProjectItem myProjectItem = new ProjectItem();
                    DateTime myDate = new DateTime();
                    if (DateTime.TryParse(reader["CreateDate"].ToString(), out myDate))
                    {
                        myProjectItem.CreateDate = myDate;
                    }
                    else
                    {
                        myProjectItem.CreateDate = Convert.ToDateTime("1900-01-01");
                    }
                    if (DateTime.TryParse(reader["EditDate"].ToString(), out myDate))
                    {
                        myProjectItem.EditDate = myDate;
                    }
                    else
                    {
                        myProjectItem.EditDate = Convert.ToDateTime("1900-01-01");
                    }
                    myProjectItem.EditDate = myDate;
                    myProjectItem.ID = Convert.ToInt32(reader["ID"].ToString());
                    myProjectItem.PDF = reader["PDF"].ToString();
                    myProjectItem.PID = Convert.ToInt32(reader["PID"].ToString());
                    myProjectItem.PPT = reader["PPT"].ToString();
                    myProjectItem.ProjectName = reader["ProjectName"].ToString();
                    myProjectItem.Video = reader["Video"].ToString();
                    myProjectItem.URL = reader["URL"].ToString();
                    myProjectItem.WORD = reader["WORD"].ToString();

                    ProjectItemList.Add(myProjectItem);
                }
            }

            return ProjectItemList;
        }

        public static Entity.ProjectItem GetProjectItemByID(int ID)
        {
            Entity.ProjectItem myProjectItem = new ProjectItem();
            string sql = @"SELECT ID, PID, ProjectName, PPT, WORD, PDF, Video, URL, CreateDate, EditDate
                        FROM ProjectItem WHERE ID=@ID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@ID", ID)
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
            {
                while (reader.Read())
                {
                    DateTime myDate = new DateTime(); 
                    DateTime.TryParse(reader["CreateDate"].ToString(), out myDate);
                    myProjectItem.CreateDate = myDate;
                    DateTime.TryParse(reader["EditDate"].ToString(), out myDate);
                    myProjectItem.EditDate = myDate;
                    myProjectItem.ID = Convert.ToInt32(reader["ID"].ToString());
                    myProjectItem.PDF = reader["PDF"].ToString();
                    myProjectItem.PID = Convert.ToInt32(reader["PID"].ToString());
                    myProjectItem.PPT = reader["PPT"].ToString();
                    myProjectItem.ProjectName = reader["ProjectName"].ToString();
                    myProjectItem.URL = reader["URL"].ToString();
                    myProjectItem.Video = reader["Video"].ToString();
                    myProjectItem.WORD = reader["WORD"].ToString();
                }
            }

            return myProjectItem;
        }

        public static int EditProjectItem(Entity.ProjectItem projectitem)
        {
            string sql = @"UPDATE ProjectItem SET ProjectName = @ProjectName, PPT = @PPT , WORD = @WORD , PDF = @PDF, 
                        Video = @Video, URL = @URL, EditDate = getdate()
                        WHERE ID=@ID";

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@ID", projectitem.ID),
                new SqlParameter("@ProjectName", projectitem.ProjectName),
                new SqlParameter("@PPT", projectitem.PPT),
                new SqlParameter("@WORD", projectitem.WORD),
                new SqlParameter("@PDF", projectitem.PDF),
                new SqlParameter("@Video", projectitem.Video),
                new SqlParameter("@URL", projectitem.URL),
            };

            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;
        }

        public static int DelProjectItem(int ID)
        {
            string sql = @"DECLARE @PID INT ; "
            + "             SELECT @PID = PID FROM ProjectItem WHERE ID=@ID"
            + "             DELETE FROM ProjectItem  WHERE ID=@ID;"
            + "             UPDATE ProjectBook SET ClassHour = (SELECT  COUNT(ID) FROM ProjectItem WHERE PID=@PID) WHERE PID = @PID;";

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@ID", ID),
            };

            int iResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return iResult > 0 ? 0 : 1;

        }





//        public static ProjectVModel.DetailProject GetDetailProjectByPID(int PID)
//        {
//            ProjectVModel.DetailProject myDetalproject = new ProjectVModel.DetailProject();
//            myDetalproject.ProjectItemList = new List<ProjectItem>();
//            //获取PorjectDetail
//            string sql = @"SELECT T.TeacherName,PID,MID,ProjectName,TeacherID,ClassHour,UseTechnology,CreateDate,EditDate,ProjectPic,ProjectDesc 
//                                    FROM ProjectBook P LEFT JOIN TeacherInfo T ON P.TeacherID = T.TID
//                                    WHERE PID= @PID";
//            SqlParameter[] paras = new SqlParameter[] { 
//                new SqlParameter("@PID", PID)
//            };
//            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, paras))
//            {
//                while (reader.Read())
//                {
//                    DateTime myDate = new DateTime();
//                    myDetalproject.ClassHour = Convert.ToInt32(reader["ClassHour"].ToString());
//                    DateTime.TryParse(reader["CreateDate"].ToString(), out myDate);
//                    myDetalproject.CreateDate = myDate;
//                    DateTime.TryParse(reader["EditDate"].ToString(), out myDate);
//                    myDetalproject.EditDate = myDate;
//                    myDetalproject.MID = Convert.ToInt32(reader["MID"].ToString());
//                    myDetalproject.PID = Convert.ToInt32(reader["PID"].ToString());
//                    myDetalproject.ProjectDesc = reader["ProjectDesc"].ToString();
//                    myDetalproject.ProjectName = reader["ProjectName"].ToString();
//                    myDetalproject.ProjectPic = reader["ProjectPic"].ToString();
//                    myDetalproject.TeacherID = Convert.ToInt32(reader["PID"].ToString());
//                    myDetalproject.UseTechnology = reader["UseTechnology"].ToString();
//                    myDetalproject.TeacherName = reader["TeacherName"].ToString();
//                }
//            }
//            //获取ProjectItemList
//            string itemSql = @"SELECT ID,PID,ProjectName,PPT,WORD,PDF,Video,CreateDate,EditDate FROM dbo.ProjectItem
//                                            WHERE PID=@PID";
//            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, itemSql, paras))
//            {
//                while (reader.Read())
//                {
//                    HPStudent.Entity.ProjectItem myProjectItem = new ProjectItem();
//                    DateTime myDate = new DateTime();
//                    if (DateTime.TryParse(reader["CreateDate"].ToString(), out myDate))
//                    {
//                        myProjectItem.CreateDate = myDate;
//                    }
//                    else
//                    {
//                        myProjectItem.CreateDate = Convert.ToDateTime("1900-01-01");
//                    }
//                    if (DateTime.TryParse(reader["EditDate"].ToString(), out myDate))
//                    {
//                        myProjectItem.EditDate = myDate;
//                    }
//                    else
//                    {
//                        myProjectItem.EditDate = Convert.ToDateTime("1900-01-01");
//                    }
//                    myProjectItem.EditDate = myDate;
//                    myProjectItem.ID = Convert.ToInt32(reader["ID"].ToString());
//                    myProjectItem.PDF = reader["PDF"].ToString();
//                    myProjectItem.PID = Convert.ToInt32(reader["PID"].ToString());
//                    myProjectItem.PPT = reader["PPT"].ToString();
//                    myProjectItem.ProjectName = reader["ProjectName"].ToString();
//                    myProjectItem.Video = reader["Video"].ToString();
//                    myProjectItem.WORD = reader["WORD"].ToString();

//                    myDetalproject.ProjectItemList.Add(myProjectItem);
//                }
//            }

//            return myDetalproject;
//        }
    }
}
