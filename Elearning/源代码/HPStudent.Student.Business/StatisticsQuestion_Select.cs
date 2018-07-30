using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Business
{
    public class StatisticsQuestion_Select
    {
        /// <summary>
        /// 获得选择题答题统计
        /// </summary>
        /// <returns></returns>
        public static List<ViewModel.Exercises.StatisticsQuestion_Select> GetStatisticsQuestion(ViewModel.Exercises.StatisticsQuestion_SelectViewModel SelectViewModel)
        {
            return Data.StatisticsQuestion_Select.GetStatisticsQuestion(SelectViewModel);
        }
    }
}
