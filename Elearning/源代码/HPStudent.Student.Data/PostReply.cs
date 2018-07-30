using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;



namespace HPStudent.Student.Data
{
   public class PostReply
    {
       public static HPStudent.Student.ViewModel.PostTheme.PostBarInfoTable GetModel(int PTID)
       {
           int PBID = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT PBID FROM PostTheme WHERE PTID = "+PTID+""));
           string sql = @"SELECT * FROM PostBarInfo WHERE PBID = " + PBID + "";
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           HPStudent.Student.ViewModel.PostTheme.PostBarInfoTable entity = new ViewModel.PostTheme.PostBarInfoTable();
           if (dt.Rows.Count > 0)
           {
               entity.PBName = Convert.ToString(dt.Rows[0]["PBName"]);
               entity.PBID = Convert.ToInt32(dt.Rows[0]["PBID"].ToString());
               entity.PBSignature = Convert.ToString(dt.Rows[0]["PBSignature"]);
               entity.Attention = Convert.ToInt32(dt.Rows[0]["Attention"]);
               entity.PostNumber = Convert.ToInt32(dt.Rows[0]["PostNumber"]);
           }
           else
           {
               return null;
           }
           return entity;
       }


       public static HPStudent.Student.ViewModel.PostReply.PostReplyDataTable<HPStudent.Student.ViewModel.PostReply.PostReplyTable> BindPostReplyTable(int start, int length, int PTID, int showType, int StudentID)
       {
           HPStudent.Student.ViewModel.PostReply.PostReplyDataTable<HPStudent.Student.ViewModel.PostReply.PostReplyTable> table = null;
           List<HPStudent.Student.ViewModel.PostReply.PostReplyTable> list = null;
           string sql = "SELECT *,(SELECT RealName FROM StudentInfo WHERE StudentID = A.StudentID) as PostManName FROM PostTheme A WHERE A.PTID = "+PTID+"";
           DataTable dtPT = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           if (dtPT.Rows.Count > 0)
           {
               string sqlWhere = "";
               if (showType == 1)
               {
                   sqlWhere = "AND StudentID=" + StudentID + "";
               }
               table = new ViewModel.PostReply.PostReplyDataTable<ViewModel.PostReply.PostReplyTable>();
               table.PostManID = Convert.ToInt32(dtPT.Rows[0]["StudentID"]);
               table.PostManName = Convert.ToString(dtPT.Rows[0]["PostManName"]);
               table.PostContent = Convert.ToString(dtPT.Rows[0]["PostContent"]);
               table.PostThemeContent = Convert.ToString(dtPT.Rows[0]["PostThemeContent"]);
               table.PostDate = Convert.ToDateTime(Convert.ToDateTime(dtPT.Rows[0]["PostDate"]).ToString("yyyy-MM-dd HH:ss:mm"));
               table.recordsTotal = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM PostReply WHERE PTID = " + PTID + " " + sqlWhere + ""));
               table.IsCollect = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM MyBarHomePage WHERE StudentID=" + StudentID + " and AttentionPost LIKE '%," + PTID + ",%'"));
               sql = string.Format(@"SELECT TOP {0} *,(SELECT RealName FROM StudentInfo WHERE StudentID = A.StudentID) AS RealName
                                            FROM PostReply A 
                                            WHERE A.PTID = {1} {3} AND A.PRID NOT IN (SELECT TOP {2} PRID FROM PostReply WHERE PTID = {1} {3} ORDER BY PRID ASC) ORDER BY A.PRID ASC ", length, PTID, start,sqlWhere);

               DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
               if (dt.Rows.Count > 0)
               {
                   list = new List<ViewModel.PostReply.PostReplyTable>();
                   for (int i = 0; i < dt.Rows.Count; i++)
                   {
                       ViewModel.PostReply.PostReplyTable entity = new ViewModel.PostReply.PostReplyTable();
                       entity.PTID = Convert.ToInt32(dt.Rows[i]["PTID"]);
                       entity.PRID = Convert.ToInt32(dt.Rows[i]["PRID"]);
                       entity.PRContent = Convert.ToString(dt.Rows[i]["PRContent"]);
                       entity.RealName = Convert.ToString(dt.Rows[i]["RealName"]);
                       entity.StudentID = Convert.ToInt32(dt.Rows[i]["StudentID"]);
                       entity.CreateDate = Convert.ToDateTime(dt.Rows[i]["CreateDate"]);
                       entity.FRCount = Convert.ToInt32(dt.Rows[i]["FRCount"]);
                       entity.FRContent = Convert.ToString(dt.Rows[i]["FRContent"]);
                       entity.FloorNumber = Convert.ToInt32(dt.Rows[i]["FloorNumber"]);
                       list.Add(entity);
                   }
               }
               else
               {
                   list = new List<ViewModel.PostReply.PostReplyTable>();
               }
               table.data = list;

           }
           else 
           {
               table = new ViewModel.PostReply.PostReplyDataTable<ViewModel.PostReply.PostReplyTable>();
           }

           return table;
       }

       /// <summary>
       /// 收藏/取消收藏
       /// </summary>
       /// <param name="PTID"></param>
       /// <param name="StudentID"></param>
       /// <returns></returns>
       public static int Collect(int PTID,int StudentID) 
       {
           int result = 0;
           string sql = string.Format("SELECT COUNT(*) FROM MyBarHomePage WHERE StudentID={0} and AttentionPost LIKE '%,{1},%'", StudentID, PTID);
           int judge = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text,sql));
           string AttentionPost = "";
           if (judge > 0)
           {
               sql = string.Format("SELECT AttentionPost FROM MyBarHomePage WHERE StudentID={0} and AttentionPost LIKE '%,{1},%'", StudentID, PTID);
               AttentionPost = Convert.ToString(SqlHelper.ExecuteScalar(CommandType.Text, sql));
               AttentionPost = AttentionPost.Replace("," + PTID + ",", ",");

               sql = string.Format("UPDATE  MyBarHomePage SET AttentionPost = '{0}' WHERE StudentID={1}", AttentionPost, StudentID);
               if (Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, sql)) > 0) 
               {
                   result = 2;
               }

           }
           else 
           {
               sql = string.Format("SELECT AttentionPost FROM MyBarHomePage WHERE StudentID={0}", StudentID);
               AttentionPost = Convert.ToString(SqlHelper.ExecuteScalar(CommandType.Text, sql));
               if (AttentionPost != "")
               {
                 sql = string.Format("UPDATE  MyBarHomePage SET AttentionPost = '{0},' WHERE StudentID={1}",  AttentionPost + PTID ,StudentID);

               }
               else 
               {
                 sql = string.Format("UPDATE  MyBarHomePage SET AttentionPost = ',{0},' WHERE StudentID={1}", "" + PTID + ",",StudentID);
               }
               result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
               
           }
           return result;
       
       }

       public static int FloorReplyAdd(int PRID, string FloorReplyContent, int StudentID, int ByReplyManID) 
       {
           string sql = string.Format("INSERT INTO FloorReply VALUES('{0}','{1}','{2}','{3}','{4}')", PRID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), FloorReplyContent, StudentID, ByReplyManID); ;
           int rusult = Convert.ToInt32(SqlHelper.ExecuteNonQuery(CommandType.Text, sql));
           FloorReplyUpdate(PRID);
           if (rusult>0)
           {
               sql = "SELECT COUNT(*) FROM FloorReply WHERE PRID =" + PRID + "";
               rusult = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql));
           }    

           return rusult;
       
       }

       public static void FloorReplyUpdate(int PRID) 
       {
           int FRCount = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FloorReply FROM FloorReply WHERE PRID=" + PRID + ""));
           List<HPStudent.Student.ViewModel.FloorReply.FloorReplyTable> list = new List<ViewModel.FloorReply.FloorReplyTable>();
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, "SELECT top 5 *,(SELECT RealName FROM StudentInfo WHERE StudentID=a.ReplyManID) AS ReplyManName,(SELECT RealName FROM StudentInfo WHERE StudentID=a.ByReplyManID) as ByReplyManName  FROM FloorReply A WHERE PRID = " + PRID + "").Tables[0];
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               HPStudent.Student.ViewModel.FloorReply.FloorReplyTable entity = new ViewModel.FloorReply.FloorReplyTable();
               entity.FRID = Convert.ToInt32(dt.Rows[i]["FRID"]);
               entity.PRID = Convert.ToInt32(dt.Rows[i]["PRID"]);
               entity.CreateDate = DateTime.Parse(Convert.ToDateTime(dt.Rows[i]["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss"));
               entity.FRContent = Convert.ToString(dt.Rows[i]["FRContent"]);
               entity.ReplyManID = Convert.ToInt32(dt.Rows[i]["ReplyManID"]);
               entity.ReplyManName = Convert.ToString(dt.Rows[i]["ReplyManName"]);
               entity.ByReplyManID = Convert.ToInt32(dt.Rows[i]["ByReplyManID"]);
               entity.ByReplyManName = Convert.ToString(dt.Rows[i]["ByReplyManName"]);
               list.Add(entity);
           }
           IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd HH':'mm':'ss" };
           string FRContent = JsonConvert.SerializeObject(list, timeConverter);
           string sql = string.Format("UPDATE PostReply SET FRCount ='{0}',FRContent='{1}' WHERE PRID = '{2}'", FRCount, FRContent, PRID);

           SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
       
       }


       public static HPStudent.Student.ViewModel.PostReply.PostReplyTable GetFloorReplyTable(int PRID) 
       {
           string sql = "SELECT *,(SELECT RealName FROM StudentInfo WHERE StudentID = A.StudentID) AS RealName FROM PostReply A WHERE PRID = " + PRID + "";
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           HPStudent.Student.ViewModel.PostReply.PostReplyTable entity = null;
           if (dt.Rows.Count > 0)
           {
               entity = new ViewModel.PostReply.PostReplyTable();
               entity.PTID = Convert.ToInt32(dt.Rows[0]["PTID"]);
               entity.PRID = Convert.ToInt32(dt.Rows[0]["PRID"]);
               entity.FRContent = Convert.ToString(dt.Rows[0]["FRContent"]);
               entity.RealName = Convert.ToString(dt.Rows[0]["RealName"]);
               entity.StudentID = Convert.ToInt32(dt.Rows[0]["StudentID"]);
               entity.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
               entity.FRCount = Convert.ToInt32(dt.Rows[0]["FRCount"]);
               entity.FRContent = Convert.ToString(dt.Rows[0]["FRContent"]);
               entity.FloorNumber = Convert.ToInt32(dt.Rows[0]["FloorNumber"]);
           }
           return entity;
       }

       public static HPStudent.Student.ViewModel.PostReply.PostReplyTable FloorReplyDel(int FRID, int PRID) 
       {
           
           string sql = "DELETE FROM FloorReply WHERE FRID = " + FRID + "";
           int result = SqlHelper.ExecuteNonQuery(CommandType.Text,sql);
           FloorReplyUpdate(PRID);
           HPStudent.Student.ViewModel.PostReply.PostReplyTable entity = GetFloorReplyTable(PRID);
           entity.IsSuccess = result;
           return entity;
       
       }

       public static HPStudent.Student.ViewModel.PostReply.PostReplyTable ReplyPagerChangeGetTable(int start, int length, int PRID) 
       {
           HPStudent.Student.ViewModel.PostReply.PostReplyTable entity = GetFloorReplyTable(PRID);
           string sql = string.Format( @"SELECT TOP {0} *,(SELECT RealName FROM StudentInfo WHERE StudentID=A.ReplyManID) AS ReplyManName,(SELECT RealName FROM StudentInfo WHERE StudentID=A.ByReplyManID) AS ByReplyManName  
                        FROM  FloorReply A where A.PRID = {2} and A.FRID  NOT IN (SELECT TOP {1} FRID FROM FloorReply WHERE PRID = {2})", length, start, PRID);
           DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
           List<HPStudent.Student.ViewModel.FloorReply.FloorReplyTable> list = null;
           if(dt.Rows.Count>0)
           {
               list = new List<ViewModel.FloorReply.FloorReplyTable>();
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   HPStudent.Student.ViewModel.FloorReply.FloorReplyTable model = new ViewModel.FloorReply.FloorReplyTable();
                   model.FRID = Convert.ToInt32(dt.Rows[i]["FRID"]);
                   model.PRID = Convert.ToInt32(dt.Rows[i]["PRID"]);
                   model.CreateDate = Convert.ToDateTime(dt.Rows[i]["CreateDate"]);
                   model.FRContent = Convert.ToString(dt.Rows[i]["FRContent"]);
                   model.ReplyManID = Convert.ToInt32(dt.Rows[i]["ReplyManID"]);
                   model.ReplyManName = Convert.ToString(dt.Rows[i]["ReplyManName"]);
                   model.ByReplyManID = Convert.ToInt32(dt.Rows[i]["ByReplyManID"]);
                   model.ByReplyManName = Convert.ToString(dt.Rows[i]["ByReplyManName"]);
                   list.Add(model);
               }
           }
           IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd hh':'mm':'ss" };
           entity.FRContent = JsonConvert.SerializeObject(list, timeConverter);
           return entity;

       
       }

       public static int PostReplyAdd(int PTID, string FRContent, int StudentID,string RealName) 
       {
           HPStudent.Student.ViewModel.PostReply.PostReplyTable entity = new ViewModel.PostReply.PostReplyTable();
           string sql = "SELECT TOP 1 FloorNumber FROM PostReply WHERE PTID = " + PTID + " ORDER BY FloorNumber DESC";
           int FloorNumber = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql)) + 1;
           if (FloorNumber==1)
           {
               FloorNumber = 2;
           }
           sql = string.Format(@"BEGIN TRY
                                INSERT INTO PostReply VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')
                                SELECT  Count(*) from PostReply WHERE PTID = {0}
                                END TRY
                                BEGIN CATCH
                                SELECT 0
                                END CATCH", PTID, FRContent, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), StudentID, 0, "", FloorNumber);
           int result  = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql));  
           return result;
       }

       public static int MessageReportAdd(HPStudent.Entity.MessageReport entity)
       {
           string sql = string.Format("INSERT INTO MessageReport VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", entity.Tid, entity.ReportUID, entity.Reporter, entity.BeReportUID, entity.BeReporter,entity.Body,entity.IsDo,entity.ReportDate,entity.IP,entity.Category);
           int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
           return result;
       }

    }

   
}
