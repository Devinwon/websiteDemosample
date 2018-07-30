using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
    public class StatisticsQuestion_Select
    {
        /// <summary>
        /// 获得选择题答题统计(通过专业)
        /// </summary>
        /// <returns></returns>
        public static List<HPStudent.Student.ViewModel.Exercises.StatisticsQuestion_Select> GetStatisticsQuestion(ViewModel.Exercises.StatisticsQuestion_SelectViewModel SelectViewModel)
        {
            List<HPStudent.Student.ViewModel.Exercises.StatisticsQuestion_Select> list = new List<ViewModel.Exercises.StatisticsQuestion_Select>();
            string condition = "";
            if (SelectViewModel.CID != 0)
            {
                condition += " and c.CID=" + SelectViewModel.CID;
            }
            string sql = string.Format(@"select c.CID,c.QuestionNums,c.CategoryName,COALESCE(s.TestNum,0) as TestNum from QA_Category as c 
                                         left join (select * from  StatisticsQuestion_Select where StudentID={0}) as s 
                                         on c.CID=s.CID where exists(select MID from MajorToCategory as m  where m.MID={1} and c.CID=m.CID) and c.QuestionNums>0 {2}", SelectViewModel.StudentID, SelectViewModel.MID, condition);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                HPStudent.Core.TBToList<ViewModel.Exercises.StatisticsQuestion_Select> tbtolist = new Core.TBToList<ViewModel.Exercises.StatisticsQuestion_Select>();
                list = (List<HPStudent.Student.ViewModel.Exercises.StatisticsQuestion_Select>)tbtolist.ToList(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 获得选择题答题统计(通过课程ID)
        /// </summary>
        /// <returns></returns>
        public static int GetPraticeNum(HPStudent.Entity.StatisticsQuestion_Select model)
        {
            string strSql = string.Format(@"select TestNum from StatisticsQuestion_Select 
                                            where CID={0} and StudentID={0} ", model.CID,model.StudentID);
            int result = (int)SqlHelper.ExecuteScalar(CommandType.Text, strSql);
            return result;
        }

    }
}
