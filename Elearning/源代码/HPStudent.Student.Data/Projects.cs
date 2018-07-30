using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;
using PorjcetVModel = HPStudent.Student.ViewModel.Project;

namespace HPStudent.Student.Data
{
    public class Projects
    {
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

        /// <summary>
       /// 获得短训推荐项目列表
       /// </summary>
       /// <param name="start"></param>
       /// <param name="length"></param>
       /// <param name="TotalRows"></param>
       /// <returns></returns>
        public static List<HPStudent.Entity.ProjectBook> GetProjectListByShort(int start, int length, out int TotalRows)
        {
            //获得符合要求的记录总数
            string countSql = "select count(pid) from ProjectBook where ShowPart = 2 OR ShowPart = 3";
            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql);

            //获取记录明细
            List<HPStudent.Entity.ProjectBook> ProjectList = new List<HPStudent.Entity.ProjectBook>();
            string sql = string.Format(@"select top {0} * from ProjectBook where pid not in 
                                    (select top {1} pid from ProjectBook  where (ShowPart = 2 OR ShowPart = 3) order by pid desc)
                                    and (ShowPart = 2 OR ShowPart = 3)
                                    order by pid desc", length, start);
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime _Date;
                    HPStudent.Entity.ProjectBook projectBook = new ProjectBook();
                    projectBook.ClassHour = Convert.ToInt32(dt.Rows[i]["ClassHour"].ToString());
                    DateTime.TryParse(dt.Rows[i]["CreateDate"].ToString(), out _Date);
                    projectBook.CreateDate = _Date;
                    DateTime.TryParse(dt.Rows[i]["EditDate"].ToString(), out _Date);
                    projectBook.EditDate = _Date;
                    projectBook.MID = Convert.ToInt32(dt.Rows[i]["MID"].ToString());
                    projectBook.PID = Convert.ToInt32(dt.Rows[i]["PID"].ToString());
                    projectBook.ProjectName = dt.Rows[i]["ProjectName"].ToString();
                    projectBook.ProjectPic = dt.Rows[i]["ProjectPic"].ToString();
                    projectBook.TeacherID = Convert.ToInt32(dt.Rows[i]["TeacherID"].ToString());
                    projectBook.UseTechnology = dt.Rows[i]["UseTechnology"].ToString();

                    ProjectList.Add(projectBook);
                }
            }
            return ProjectList;
        }


        public static List<HPStudent.Entity.ProjectBook> GetAllProjectBookList(int start, int length, out int TotalRows)
        { 
            //获得符合要求的记录总数
            string countSql = "select count(pid) from ProjectBook where Status <>1 and (ShowPart = 1 or ShowPart = 3)";
            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql);


            //获取记录明细
            List<HPStudent.Entity.ProjectBook> ProjectList = new List<HPStudent.Entity.ProjectBook>();
            string sql = string.Format(@"select top {0} * from ProjectBook where Status <>1 and (ShowPart = 1 or ShowPart = 3) and pid not in 
                                    (select top {1} pid from ProjectBook where Status <>1 and (ShowPart = 1 or ShowPart = 3) order by pid desc)
                                    order by pid desc", length, start);
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime _Date;
                    HPStudent.Entity.ProjectBook projectBook = new ProjectBook();
                    projectBook.ClassHour = Convert.ToInt32(dt.Rows[i]["ClassHour"].ToString());
                    DateTime.TryParse(dt.Rows[i]["CreateDate"].ToString(), out _Date);
                    projectBook.CreateDate = _Date;
                    DateTime.TryParse(dt.Rows[i]["EditDate"].ToString(), out _Date);
                    projectBook.EditDate = _Date;
                    projectBook.MID = Convert.ToInt32(dt.Rows[i]["MID"].ToString());
                    projectBook.PID = Convert.ToInt32(dt.Rows[i]["PID"].ToString());
                    projectBook.ProjectName = dt.Rows[i]["ProjectName"].ToString();
                    projectBook.ProjectPic = dt.Rows[i]["ProjectPic"].ToString();
                    projectBook.TeacherID = Convert.ToInt32(dt.Rows[i]["TeacherID"].ToString());
                    projectBook.UseTechnology = dt.Rows[i]["UseTechnology"].ToString();

                    ProjectList.Add(projectBook);
                }
            }
            return ProjectList;
        }
        /// <summary>
        /// 根据MajorID获得该专业下的项目（分页）
        /// </summary>
        /// <returns></returns>
        public static List<HPStudent.Entity.ProjectBook> GetProjectBookList(int MID, int start, int length, out int TotalRows)
        {
            //获得符合要求的记录总数
            string countSql = "select count(pid) from ProjectBook where Status <>1 and (ShowPart = 1 or ShowPart = 3) and  MID = @MID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MID", MID)
            };
            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql, paras);


            //获取记录明细
            List<HPStudent.Entity.ProjectBook> ProjectList = new List<HPStudent.Entity.ProjectBook>();
            string sql = string.Format(@"select top {0} * from ProjectBook where Status <>1 and (ShowPart = 1 or ShowPart = 3) and  pid not in 
                                    (select top {1} pid from ProjectBook where Status <>1 and (ShowPart = 1 or ShowPart = 3) and MID=@MID order by pid desc)
                                    and MID =@MID order by pid desc", length, start);
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras).Tables[0];
            if(dt.Rows.Count >0)
            {
                 for (int i = 0; i < dt.Rows.Count; i++)
                {
                     DateTime _Date;
                     HPStudent.Entity.ProjectBook projectBook = new ProjectBook();
                     projectBook.ClassHour = Convert.ToInt32(dt.Rows[i]["ClassHour"].ToString());
                     DateTime.TryParse(dt.Rows[i]["CreateDate"].ToString(), out _Date);
                     projectBook.CreateDate = _Date;
                     DateTime.TryParse(dt.Rows[i]["EditDate"].ToString(), out _Date);
                     projectBook.EditDate = _Date;
                     projectBook.MID = Convert.ToInt32(dt.Rows[i]["MID"].ToString());
                     projectBook.PID = Convert.ToInt32(dt.Rows[i]["PID"].ToString());
                     projectBook.ProjectName = dt.Rows[i]["ProjectName"].ToString();
                     projectBook.ProjectPic = dt.Rows[i]["ProjectPic"].ToString();
                     projectBook.TeacherID = Convert.ToInt32(dt.Rows[i]["TeacherID"].ToString());
                     projectBook.UseTechnology = dt.Rows[i]["UseTechnology"].ToString();

                     ProjectList.Add(projectBook);
                 }
            }
            return ProjectList;

        }
        
        public static ProjectBook GetProjectBookByPID(int PID){
            ProjectBook myPorjectBook = new ProjectBook();

            string sql = @"SELECT T.TeacherName,MID,ProjectName,TeacherID,ClassHour,UseTechnology,CreateDate,EditDate,ProjectPic,ProjectDesc 
                                    FROM ProjectBook P LEFT JOIN TeacherInfo T ON P.TeacherID = T.TID
                                    WHERE PID= @PID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@PID", PID)
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql,paras))
            {
                while (reader.Read())
                {
                    DateTime myDate = new DateTime ();
                    myPorjectBook.ClassHour = Convert.ToInt32(reader["ClassHour"].ToString());
                    DateTime.TryParse(reader["CreateDate"].ToString(),out myDate);
                    myPorjectBook.CreateDate = myDate;
                    DateTime.TryParse(reader["EditDate"].ToString(),out myDate);
                    myPorjectBook.EditDate = myDate;
                    myPorjectBook.MID =  Convert.ToInt32(reader["MID"].ToString());
                    myPorjectBook.PID = Convert.ToInt32(reader["PID"].ToString());
                    myPorjectBook.ProjectDesc = reader["ProjectDesc"].ToString();
                    myPorjectBook.ProjectName = reader["ProjectName"].ToString();
                    myPorjectBook.ProjectPic = reader["ProjectPic"].ToString();
                    myPorjectBook.TeacherID = Convert.ToInt32(reader["PID"].ToString());
                    myPorjectBook.UseTechnology = reader["UseTechnology"].ToString();
                }
            }
            return myPorjectBook;
        }

        public static PorjcetVModel.DetailProject GetDetailProjectByPID(int PID)
        {
            PorjcetVModel.DetailProject myDetalproject = new PorjcetVModel.DetailProject();
            myDetalproject.ProjectItemList = new List<ProjectItem>();
            //获取PorjectDetail
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
                    myDetalproject.ClassHour = Convert.ToInt32(reader["ClassHour"].ToString());
                    DateTime.TryParse(reader["CreateDate"].ToString(), out myDate);
                    myDetalproject.CreateDate = myDate;
                    DateTime.TryParse(reader["EditDate"].ToString(), out myDate);
                    myDetalproject.EditDate = myDate;
                    myDetalproject.MID = Convert.ToInt32(reader["MID"].ToString());
                    myDetalproject.PID = Convert.ToInt32(reader["PID"].ToString());
                    myDetalproject.ProjectDesc = reader["ProjectDesc"].ToString();
                    myDetalproject.ProjectName = reader["ProjectName"].ToString();
                    myDetalproject.ProjectPic = reader["ProjectPic"].ToString();
                    myDetalproject.TeacherID = Convert.ToInt32(reader["PID"].ToString());
                    myDetalproject.UseTechnology = reader["UseTechnology"].ToString();
                    myDetalproject.TeacherName = reader["TeacherName"].ToString();
                }
            }
            //获取ProjectItemList
            string itemSql = @"SELECT ID,PID,ProjectName,PPT,WORD,PDF,Video,URL,CreateDate,EditDate FROM dbo.ProjectItem
                                            WHERE PID=@PID";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, itemSql, paras))
            {
                while (reader.Read())
                {
                    HPStudent.Entity.ProjectItem myProjectItem= new ProjectItem();
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
                    myProjectItem.WORD = reader["WORD"].ToString();
                    myProjectItem.URL = reader["URL"].ToString();

                    myDetalproject.ProjectItemList.Add(myProjectItem);
                }
            }

            return myDetalproject;
        }
    }
}
