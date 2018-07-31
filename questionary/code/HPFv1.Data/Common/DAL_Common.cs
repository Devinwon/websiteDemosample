using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPFv1.Entity;
using System.Data;
using System.Data.SqlClient;


namespace HPFv1.Data.Common
{
    public class DAL_Common
    {
        /// <summary>
        /// 获得左导航栏的所有节点
        /// </summary>
        /// <returns></returns>
        public static List<UsersMenu> GetSideBar()
        {
            List<UsersMenu> SideBarList = new List<UsersMenu>();
            string sql = "select * from UsersMenu where IsManager = 0";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text,sql))
            {
            while (reader.Read())
            {
                SideBarList.Add(
                    new UsersMenu()
                    {
                        MID = Convert.ToInt32(reader["MID"].ToString()),
                        PID = Convert.ToInt32(reader["PID"].ToString()),
                        MenuName = reader["MenuName"].ToString(),
                        ControllerName = reader["ControllerName"].ToString(),
                        ActionName = reader["ActionName"].ToString(),
                        Sort = Convert.ToInt32(reader["Sort"].ToString()),
                        ChildNum = Convert.ToInt32(reader["ChildNum"].ToString()),
                        Icon = reader["Icon"].ToString()
                    }
                );
            }
            }
            return SideBarList;




        }
    }
}
