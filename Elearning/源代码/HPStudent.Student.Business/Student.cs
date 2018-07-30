using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
    public class Student
    {
        //获得人才信息
        public static HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Student.EnterpriseSearchBase> QueryEnterpriseReviewList(int length, int start, HPStudent.Student.ViewModel.Student.EnterpriseSearchViewModel KeyWords)
        {
            int TotalRows = 0;
            HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Student.EnterpriseSearchBase> SelectedTable = new HPStudent.Student.ViewModel.Common.Datatable<HPStudent.Student.ViewModel.Student.EnterpriseSearchBase>();
            SelectedTable.data = HPStudent.Student.Data.StudentDao.QueryStudentList(start, length, out TotalRows, KeyWords);
            //初始化返回Datatable行数
            SelectedTable.recordsTotal = TotalRows;
            SelectedTable.recordsFiltered = TotalRows;
            return SelectedTable;
        }
    }
}
