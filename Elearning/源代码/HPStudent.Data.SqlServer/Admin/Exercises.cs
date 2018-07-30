using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HPStudent.Entity;
using HPStudent.Core;


namespace HPStudent.Data.Admin
{
    public class Exercises
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

        public static List<HPStudent.Entity.QA_Category> GetAllCategory()
        {
            List<HPStudent.Entity.QA_Category> itemList = new List<QA_Category>();
            string sql = @"SELECT CID , CategoryName ,QuestionNums FROM QA_Category";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    itemList.Add(
                        new HPStudent.Entity.QA_Category()
                        {
                            CID = Convert.ToInt32(reader["CID"].ToString()),
                            CategoryName = reader["CategoryName"].ToString(),
                            QuestionNums = Convert.ToInt32(reader["QuestionNums"].ToString())
                        }
                    );
                }
            }
            return itemList;
        }

        public static int[] GetCIDListByMajorName(string MajorName)
        {
            int[] CIDList;
            string sql = @"SELECT CID FROM MajorToCategory WHERE MID = (
			                SELECT MID FROM Major WHERE MajorName = @MajorName
		                )";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MajorName", MajorName)
            };
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras).Tables[0];
            if (dt.Rows.Count > 0)
            {
                CIDList = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CIDList[i] = Convert.ToInt32(dt.Rows[i]["CID"].ToString());
                }
                return CIDList;
            }
            CIDList = new int[] { 0 };
            return CIDList;
        }

        public static bool EditCategoryByMajoyName(string MajorName, string[] CategoryIDS)
        {
            string sql =
            @"DECLARE  @MID INT;
            SELECT @MID = MID FROM Major WHERE MajorName = @MajorName;
            DELETE FROM MajorToCategory WHERE MID = @MID;
            INSERT INTO MajorToCategory ( MID,CID ) ";
            for (int i = 0; i < CategoryIDS.Length; i++)
            {
                if (i == CategoryIDS.Length - 1)
                {
                    sql += " SELECT @MID," + CategoryIDS[i];
                }
                else
                {
                    sql += " SELECT @MID," + CategoryIDS[i] + " UNION";

                }
            }


            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MajorName", MajorName)
            };

            return 0 < (int)SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras) ? true : false;

        }
        /// <summary>
        /// 根据专业ID获取课程列表
        /// </summary>
        /// <param name="MajorID"></param>
        /// <returns></returns>
        public static List<QA_Category> GetCategoryListByMajorID(string MajorID)
        {
            List<QA_Category> CateList;
            string sql = @"select Cate.CID,Cate .CategoryName from MajorToCategory Major
                            inner join QA_Category  Cate on Major .CID =Cate .CID 
                            where MID=@MajorID order by Cate .CID asc";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MajorID", MajorID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras).Tables[0];
            if (dt.Rows.Count > 0)
            {
                CateList = new List<QA_Category>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    QA_Category cate = new QA_Category();
                    cate.CID = Convert.ToInt32(dt.Rows[i]["CID"].ToString());
                    cate.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    CateList.Add(cate);
                }
                return CateList;
            }
            CateList = new List<QA_Category>();
            return CateList;
        }

        /// <summary>
        /// 根据专业ID获取课程列表
        /// </summary>
        /// <param name="MajorID"></param>
        /// <returns></returns>
        public static List<QA_Category> GetCategoryListByMajorIDNotNone(string MajorID)
        {
            List<QA_Category> CateList;
            string sql = @"select Cate.CID,Cate .CategoryName from MajorToCategory Major
                            inner join QA_Category  Cate on Major .CID =Cate .CID 
                            where MID=@MajorID AND QuestionNums >10  order by Cate .CID asc";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MajorID", MajorID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras).Tables[0];
            if (dt.Rows.Count > 0)
            {
                CateList = new List<QA_Category>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    QA_Category cate = new QA_Category();
                    cate.CID = Convert.ToInt32(dt.Rows[i]["CID"].ToString());
                    cate.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    CateList.Add(cate);
                }
                return CateList;
            }
            CateList = new List<QA_Category>();
            return CateList;
        }

        /// <summary>
        /// 根据专业ID获取课程列表
        /// </summary>
        /// <param name="MajorID"></param>
        /// <returns></returns>
        public static List<QA_Category> GetCategoryListByMajorName(string MajorName)
        {
            List<QA_Category> CateList;
            string sql = @"select Cate.CID,Cate .CategoryName from MajorToCategory Major
                            inner join QA_Category  Cate on Major .CID =Cate .CID 
                            where MID = (select top 1 MID from Major WHERE MajorName= @MajorName)
                            order by Cate .CID asc";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@MajorName", MajorName)
            };
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras).Tables[0];
            if (dt.Rows.Count > 0)
            {
                CateList = new List<QA_Category>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    QA_Category cate = new QA_Category();
                    cate.CID = Convert.ToInt32(dt.Rows[i]["CID"].ToString());
                    cate.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    CateList.Add(cate);
                }
                return CateList;
            }
            CateList = new List<QA_Category>();
            return CateList;
        }

        /// <summary>
        /// 根据课程ID查询当前课程的选择题
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public static List<QA_Select> GetQA_SelectListByCID(int CID, int start, int length, out int TotalRows)
        {
            //获取符合要求记录的总行数
            string countSql = "select count(cid) from QA_Select where  CID =@CID ";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@CID", CID)
            };
            TotalRows = (int)SqlHelper.ExecuteScalar(CommandType.Text, countSql, paras);

            //获取记录明细
            List<QA_Select> selList;
            string sql = string.Format("select top {0} * from QA_Select where QID not in (select top {1} QID from QA_Select where  CID =@CID  order by qid desc) and  CID =@CID order by qid desc", length, start);
            //string sql = "select CID,ISNULL(QID,0) QID,TITLE,LEVEL from QA_Select where CID=@CID or @CID='' ";

            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras).Tables[0];
            if (dt.Rows.Count > 0)
            {
                selList = new List<QA_Select>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    QA_Select sel = new QA_Select();
                    sel.CID = Convert.ToInt32(dt.Rows[i]["CID"].ToString());
                    sel.QID = Convert.ToInt32(dt.Rows[i]["QID"].ToString());
                    sel.Title = dt.Rows[i]["Title"].ToString();
                    sel.Level = Convert.ToInt32(dt.Rows[i]["Level"].ToString());

                    selList.Add(sel);
                }
                return selList;
            }
            selList = new List<QA_Select>();
            return selList;
        }
        /// <summary>
        /// 添加选择题
        /// </summary>
        /// <param name="sel"></param>
        /// <returns></returns>
        public static int EditQA_Select(QA_Select sel)
        {
            string sql = string.Empty;

            if (sel.QID != 0)
            {
                sql = @"Update QA_Select set Title=@Title,A=@A,B=@B,C=@C,D=@D,Answer=@Answer,CreateDate=@CreateDate,Creater=@Creater,level=@level 
                        ,AnswerAnalysis=@AnswerAnalysis
                where QID=@QID
                ";
            }
            else
            {
                sql = @"INSERT INTO [QA_Select]
           ([CID],[Title],[A],[B],[C],[D],[Answer],[CreateDate],[Creater],[level],[AnswerAnalysis])
VALUES
           (@CID,@Title,@A,@B,@C,@D,@Answer,@CreateDate,@Creater,@level,@AnswerAnalysis);
update QA_Category set QuestionNums = (select COUNT(1) from QA_Select where CID=@CID ) where CID = @CID";
            }
            SqlParameter[] par ={
                              new SqlParameter("@CID",SqlDbType.BigInt),
                              new SqlParameter("@Title",SqlDbType .VarChar,255),
                              new SqlParameter("@A",SqlDbType .VarChar,255),
                              new SqlParameter("@B",SqlDbType .VarChar,255),
                              new SqlParameter("@C",SqlDbType .VarChar,255),
                              new SqlParameter("@D",SqlDbType .VarChar,255),
                              new SqlParameter("@Answer",SqlDbType .VarChar,255),
                              new SqlParameter("@CreateDate",SqlDbType .VarChar,255),
                              new SqlParameter("@Creater",SqlDbType .VarChar,255),
                              new SqlParameter("@level",SqlDbType .VarChar,255),
                              new SqlParameter("@QID",SqlDbType.BigInt),
                              new SqlParameter("@AnswerAnalysis",SqlDbType.VarChar,8000),
                              };
            par[0].Value = sel.CID;
            par[1].Value = sel.Title;
            par[2].Value = sel.A;
            par[3].Value = sel.B;
            par[4].Value = sel.C;
            par[5].Value = sel.D;
            par[6].Value = sel.Answer;
            par[7].Value = sel.CreateDate;
            par[8].Value = sel.Creater;
            par[9].Value = sel.Level;
            par[10].Value = sel.QID;
            par[11].Value = sel.AnswerAnalysis;
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, par);
            return i;
        }

        public static QA_Select GetQA_SelectByQID(string QID)
        {
            QA_Select sel;
            string sql = "select * from QA_Select where QID=@QID  ";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@QID", QID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras).Tables[0];
            if (dt.Rows.Count > 0)
            {
                sel = new QA_Select();
                sel.CID = Convert.ToInt32(dt.Rows[0]["CID"].ToString());
                sel.QID = Convert.ToInt32(dt.Rows[0]["QID"].ToString());
                sel.Title = dt.Rows[0]["Title"].ToString();
                sel.Level = Convert.ToInt32(dt.Rows[0]["Level"].ToString());
                sel.Answer = dt.Rows[0]["Answer"].ToString();
                sel.A = dt.Rows[0]["A"].ToString();
                sel.B = dt.Rows[0]["B"].ToString();
                sel.C = dt.Rows[0]["C"].ToString();
                sel.D = dt.Rows[0]["D"].ToString();
                sel.AnswerAnalysis = dt.Rows[0]["AnswerAnalysis"].ToString();
                return sel;
            }
            sel = new QA_Select();
            return sel;
        }
        /// <summary>
        /// 通过类别获得题目
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public static QA_Select GetQA_SelectByCID(string CID)
        {
            QA_Select sel;
            string sql = "select * from QA_Select where CID=@CID  ";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@CID", CID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, paras).Tables[0];
            if (dt.Rows.Count > 0)
            {
                sel = new QA_Select();
                sel.CID = Convert.ToInt32(dt.Rows[0]["CID"].ToString());
                sel.QID = Convert.ToInt32(dt.Rows[0]["QID"].ToString());
                sel.Title = dt.Rows[0]["Title"].ToString();
                sel.Level = Convert.ToInt32(dt.Rows[0]["Level"].ToString());
                sel.Answer = dt.Rows[0]["Answer"].ToString();
                sel.A = dt.Rows[0]["A"].ToString();
                sel.B = dt.Rows[0]["B"].ToString();
                sel.C = dt.Rows[0]["C"].ToString();
                sel.D = dt.Rows[0]["D"].ToString();
                sel.AnswerAnalysis = dt.Rows[0]["AnswerAnalysis"].ToString();
                return sel;
            }
            sel = new QA_Select();
            return sel;
        }
        /// <summary>
        /// 删除题目
        /// </summary>
        /// <param name="QID"></param>
        /// <returns></returns>
        public static int DelQA_Select(string QID)
        {
            string sql = "delete from QA_Select where QID=@QID";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@QID", QID)
            };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return i;
        }

        public static int ExercisesImport(DataSet ds, string CID, int CreaterID)
        {
            StringBuilder sb = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Count > 1)
            {
                sb.Append("DECLARE @ImportCount INT \n");
                sb.Append("DECLARE @IsSuccess INT \n");
                sb.Append("SET @ImportCount = 0 \n");
                sb.Append("SET @IsSuccess = 0 \n");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    DataRow row = ds.Tables[0].Rows[i];
                    #region 修复题目为空的数据行
                    if (row["题目"].ToString() == "")
                    {
                        continue;
                    }

                    #endregion
                    sb.Append("BEGIN TRANSACTION IMPORT_TRAN_" + i.ToString() + " \n");
                    sb.Append("BEGIN TRY \n");
                    sb.Append("IF NOT EXISTS (SELECT 1 FROM QA_Select WHERE Title = '" + StringHelper.HtmlEncode(row["题目"].ToString()) + "' ) \n");
                    sb.Append("BEGIN \n");
                    sb.Append("     INSERT INTO [QA_Select] \n");
                    sb.Append("     ([CID],[Title],[A],[B],[C],[D],[Answer],[CreateDate],[Creater],[Level],[AnswerAnalysis]) \n");
                    sb.Append("     VALUES \n");
                    sb.Append("     ( \n");
                    sb.Append("     '" + CID + "',");
                    sb.Append("     '" + StringHelper.HtmlEncode(row["题目"].ToString()) + "',");
                    sb.Append("     '" + StringHelper.HtmlEncode(row["A"].ToString()) + "',");
                    sb.Append("     '" + StringHelper.HtmlEncode(row["B"].ToString()) + "',");
                    sb.Append("     '" + StringHelper.HtmlEncode(row["C"].ToString()) + "',");
                    sb.Append("     '" + StringHelper.HtmlEncode(row["D"].ToString()) + "',");
                    sb.Append("     '" + row["答案"].ToString() + "',");
                    sb.Append("     '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                    sb.Append("     " + CreaterID.ToString() + ",");
                    sb.Append("     1,");
                    sb.Append("     '' \n");
                    sb.Append("     ) \n");
                    sb.Append("  SET @IsSuccess = 1; \n");
                    sb.Append("END \n");
                    sb.Append("END TRY \n");
                    sb.Append("BEGIN CATCH \n");
                    //sb.Append("  IF @@TRANCOUNT > 0 \n");
                    sb.Append("    SET @IsSuccess = 0; \n");
                    sb.Append("    ROLLBACK TRANSACTION IMPORT_TRAN_" + i.ToString() + "; \n");
                    sb.Append("END CATCH; \n");
                    sb.Append("IF @IsSuccess =1 \n");
                    sb.Append("BEGIN \n");
                    sb.Append("    SET @ImportCount = @ImportCount + 1; \n");
                    sb.Append("    COMMIT TRANSACTION IMPORT_TRAN_" + i.ToString() + "; \n");
                    sb.Append("    SET @IsSuccess = 0; \n");
                    sb.Append("END \n");
                    sb.Append("ELSE \n");
                    sb.Append("BEGIN \n");
                    sb.Append("    SET @IsSuccess = 0; \n");
                    sb.Append("    ROLLBACK TRANSACTION IMPORT_TRAN_" + i.ToString() + "; \n");
                    sb.Append("END \n");
                    sb.Append("\n");

                }
                sb.Append("update QA_Category set QuestionNums = (select COUNT(1) from QA_Select where CID=" + CID + " ) where CID = " + CID + " \n");
                sb.Append("select @ImportCount \n");

            }


            int result = (int)SqlHelper.ExecuteScalar(CommandType.Text, sb.ToString());
            return result;
        }
    }
}
