using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPStudent.Student.Data
{
    public class AlreadyAnswer_Select
    {
        /// <summary>
        /// 获得已答题记录
        /// </summary>
        /// <returns></returns>
        public static List<Entity.AlreadyAnswer_Select> GetList(Entity.AlreadyAnswer_Select model)
        {
            List<Entity.AlreadyAnswer_Select> list = new List<Entity.AlreadyAnswer_Select>();
            string condition = "";
            if (model.StudentID != 0)
            {
                condition += " and StudentID=" + model.StudentID;
            }
            if (model.CID != 0)
            {
                condition += " and CID=" + model.CID;
            }
            string sql = "select ID,StudentID,CID,Answer FROM AlreadyAnswer_Select where 1=1";
            sql += condition;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                HPStudent.Core.TBToList<Entity.AlreadyAnswer_Select> tbtolist = new Core.TBToList<Entity.AlreadyAnswer_Select>();
                list = (List<Entity.AlreadyAnswer_Select>)tbtolist.ToList(ds.Tables[0]);
            }
            return list;
        }
    }
}
