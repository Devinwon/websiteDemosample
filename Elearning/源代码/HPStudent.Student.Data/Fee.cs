using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HPStudent.Entity;
using System.Text;

namespace HPStudent.Student.Data
{
    public class Fee
    {
        public static int UploadFeeAttachment(HPStudent.Student.ViewModel.Fee.FeeInfo Fee)
        {
            string sql = @"declare @FeeID int
                            insert into FeeInfo   ([SID]
           ,[Year]
           ,[NeedFee]
           ,[PaidFee]
           ,[IsCheck]) values(@StudentID,@Year,11000,@PaidFee,@IsCheck)
                            select @FeeID=@@IDENTITY
                            insert into FeeAttachment  ([FeeID]
           ,[FeeDescription]
           ,[FeeTitle]
           ,[Attachment]
           ,[Fee]
           ,[Dateline]) values( @FeeID,@FeeDescription,@FeeTitle,@Attachment,@Fee,getdate())";
            SqlParameter[] param ={
                            new SqlParameter ("@StudentID",Fee.SID),     
                            new SqlParameter ("@Year",Fee.Year),     
                            new SqlParameter ("@PaidFee",Fee.PaidFee),  
                            new SqlParameter ("@IsCheck",Fee.IsCheck ),  
                            new SqlParameter ("@FeeDescription",Fee.FeeDescription ),     
                            new SqlParameter ("@FeeTitle",Fee.FeeTitle),     
                            new SqlParameter ("@Attachment",Fee.Attachment),   
                            new SqlParameter ("@Fee",Fee.Fee),   
                                 };
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            return i;
        }
        public static List<HPStudent.Student.ViewModel.Fee.FeeInfo> GetFeeListBySID(string StudentID, string Year, int start, int length, out int TotalRows)
        {
            string sqlCount = @"select count(1)
                                            from FeeInfo a
                                            inner  join  FeeAttachment b on a.FeeID =b.FeeID  
                                            where (a.[Year]=@Year or @Year='') and a.[SID] =@StudentID";
            string sql = string.Format(@"select top {0} a.FeeID ,a.IsCheck ,a.NeedFee ,a.PaidFee ,a.[SID] ,a.[Year] ,b.FAID ,b.Attachment ,b.Dateline ,b.Fee ,b.FeeDescription ,b.FeeTitle from FeeInfo a
                                            inner  join  FeeAttachment b on a.FeeID =b.FeeID   
                                            where a.FeeID not in (select top {1} a.FeeID 
                                            from FeeInfo a
                                            inner  join  FeeAttachment b on a.FeeID =b.FeeID  
                                            where (a.[Year]=@Year or @Year='') and a.[SID] =@StudentID
                                            ) and (a.[Year]=@Year or @Year='') and a.[SID] =@StudentID ", length, start);
            SqlParameter[] param = { 
                                   new SqlParameter ("@Year",Year),
                                   new SqlParameter ("@StudentID",StudentID),
                                   };
            TotalRows = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlCount, param));
            if (TotalRows == 0)
            {
                return null;
            }
            List<HPStudent.Student.ViewModel.Fee.FeeInfo> FeeList = new List<HPStudent.Student.ViewModel.Fee.FeeInfo>();
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, param).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                HPStudent.Student.ViewModel.Fee.FeeInfo Fee = new HPStudent.Student.ViewModel.Fee.FeeInfo();
                Fee.SID = item["SID"].ToString();
                Fee.Year = item["Year"].ToString();
                Fee.PaidFee = item["PaidFee"].ToString();
                Fee.NeedFee = item["NeedFee"].ToString();
                Fee.IsCheck = item["IsCheck"].ToString();
                Fee.FeeTitle = item["FeeTitle"].ToString();
                Fee.FeeID = item["FeeID"].ToString();
                Fee.FeeDescription = item["FeeDescription"].ToString();
                Fee.Fee = item["Fee"].ToString();
                Fee.FAID = item["FAID"].ToString();
                Fee.Dateline = item["Dateline"].ToString();
                Fee.Attachment = item["Attachment"].ToString();
                FeeList.Add(Fee);
            }
            return FeeList;
        }

        public static HPStudent.Student.ViewModel.Fee.FeeInfo GetFeeInfoByFeeID(string FeeID)
        {
            string sql = @"select a.FeeID ,a.IsCheck ,a.NeedFee ,a.PaidFee ,a.[SID] ,a.[Year] ,b.FAID ,b.Attachment ,b.Dateline ,b.Fee ,b.FeeDescription ,b.FeeTitle from FeeInfo a
                                            inner  join  FeeAttachment b on a.FeeID =b.FeeID   
                                            where a.FeeID =@FeeID ";
            SqlParameter[] param = { 
                                   new SqlParameter ("@FeeID",FeeID),
                                   
                                   };
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql, param).Tables[0];
            HPStudent.Student.ViewModel.Fee.FeeInfo Fee = new HPStudent.Student.ViewModel.Fee.FeeInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow item = dt.Rows[0];
                Fee.SID = item["SID"].ToString();
                Fee.Year = item["Year"].ToString();
                Fee.PaidFee = item["PaidFee"].ToString();
                Fee.NeedFee = item["NeedFee"].ToString();
                Fee.IsCheck = item["IsCheck"].ToString();
                Fee.FeeTitle = item["FeeTitle"].ToString();
                Fee.FeeID = item["FeeID"].ToString();
                Fee.FeeDescription = item["FeeDescription"].ToString();
                Fee.Fee = item["Fee"].ToString();
                Fee.FAID = item["FAID"].ToString();
                Fee.Dateline = item["Dateline"].ToString();
                Fee.Attachment = item["Attachment"].ToString();
            }
            return Fee;
        }
    }
}
