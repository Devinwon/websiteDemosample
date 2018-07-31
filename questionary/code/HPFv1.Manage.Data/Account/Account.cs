using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPFv1.Entity;
using System.Data.SqlClient;
using System.Data;


namespace HPFv1.Manage.Data.Account
{
    public class Account
    {
        public static Manager IsAdminLogin(Manager manager)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Manager ");
            strSql.Append(" where Account = @Account and PassWord = @PassWrod ");
            SqlParameter[] parameters = {
	                		new SqlParameter("@Account", manager.Account),
                                     new SqlParameter("@PassWrod",manager.Password) };


            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text,strSql.ToString(), parameters);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Manager model = new Manager();
                model.MID = Convert.ToInt32(ds.Tables[0].Rows[0]["MID"]);
                model.NickName = ds.Tables[0].Rows[0]["NickName"].ToString(); ;
                model.Account = ds.Tables[0].Rows[0]["Account"].ToString();
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }



    }
}
