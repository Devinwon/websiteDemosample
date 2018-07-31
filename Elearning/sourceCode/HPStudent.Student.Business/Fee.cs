using System;
using System.Collections.Generic;
using HPStudent.Entity;
using HPStudent.Student.ViewModel;
using HPStudent.Student.Data;


namespace HPStudent.Student.Business
{
    public class Fee
    {
        public static int UploadFeeAttachment(HPStudent.Student.ViewModel.Fee.FeeInfo Fee)
        {
            return HPStudent.Student.Data.Fee.UploadFeeAttachment(Fee);
        }


        public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Fee.FeeInfo> GetFeeListBySID(string StudentID, string Year, int start, int length)
        {
            int TotalRows = 0;
            List<HPStudent.Student.ViewModel.Fee.FeeInfo> FeeList = new List<HPStudent.Student.ViewModel.Fee.FeeInfo>();

            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Fee.FeeInfo> FeeTable = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Fee.FeeInfo>();
            FeeTable.data = new List<HPStudent.Student.ViewModel.Fee.FeeInfo>();
            FeeList = HPStudent.Student.Data.Fee.GetFeeListBySID(StudentID, Year, start, length, out TotalRows);
            //Data.Admin.Exercises.GetQA_SelectListByCID(CID, start, length, out TotalRows);

            //初始化返回Datatable行数
            FeeTable.recordsTotal = TotalRows;
            FeeTable.recordsFiltered = TotalRows;

            if (FeeList == null)
            {
                return FeeTable;
            }
            foreach (HPStudent.Student.ViewModel.Fee.FeeInfo item in FeeList)
            {
                HPStudent.Student.ViewModel.Fee.FeeInfo table = new HPStudent.Student.ViewModel.Fee.FeeInfo();
                table.Attachment = item.Attachment;
                table.Dateline = item.Dateline;
                table.FAID = item.FAID;
                table.Fee = item.Fee;
                table.FeeDescription = item.FeeDescription;
                table.FeeID = item.FeeID;
                table.SID = item.SID;
                table.FeeTitle = item.FeeTitle;
                table.IsCheck = item.IsCheck;
                table.NeedFee = item.NeedFee;
                table.PaidFee = item.PaidFee;
                table.Year = item.Year;

                FeeTable.data.Add(table);
            }
            return FeeTable;
        }

        public static HPStudent.Student.ViewModel.Fee.FeeInfo GetFeeInfoByFeeID(string FeeID)
        {
            return HPStudent.Student.Data.Fee.GetFeeInfoByFeeID(FeeID);
        }


    }
}
