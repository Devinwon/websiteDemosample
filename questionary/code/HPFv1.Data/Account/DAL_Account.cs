using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPFv1.Entity;
using System.Data.SqlClient;
using System.Data;


namespace HPFv1.Data.Account
{
   public class DAL_Account
    {
       public static Users IsAdminLogin(Users user) 
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select * from Users ");
           strSql.Append(" where Email = @Email and PassWord = @PassWrod ");
           SqlParameter[] parameters = {
	                		new SqlParameter("@Email", user.Email),
                                     new SqlParameter("@PassWrod",user.Password) };
 
          
           DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text,strSql.ToString(), parameters);
           if (ds!=null&& ds.Tables[0].Rows.Count > 0)
           {
               Users model = new Users();
                model.UID = Convert.ToInt32(ds.Tables[0].Rows[0]["UID"]) ;
                model.NickName = ds.Tables[0].Rows[0]["NickName"].ToString(); ;
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
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
