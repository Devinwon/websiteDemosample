using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HPStudent.Logic.Attribute;
using System.Data;
using System.Web.Script.Serialization;

namespace HPStudent.Logic
{

    public class HomeController : BaseContorller
    {

        public ActionResult Index()
        {
            //ViewBag.Username = HPStudent.Core.CookieHelper.GetCookieValue("LoginUserInfo");
            //ViewData["ProjectName"] = "My Test Project";
            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString))
            //{
            //    SqlCommand cmd = new SqlCommand("SELECT * FROM A",con);
            //    con.Open();
            //    SqlDataReader dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        ViewData["ProjectName"] = (string)dr["username"];
            //    }
            //    dr.Close();

            //}

            //c测试log
            //log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            //logger.Debug("测试------------->");
            //logger.Debug("调用RegisterStartupScript()方法");
            //logger.Debug("调试");
            //logger.Error("这是一个错误日志");
            //logger.Fatal("这是一个致命的错误日志");
            //logger.Warn("这是一条警告日志");
            //logger.Info("这是一条普通信息");

            return View();
        }


        [AllowAnonymous]
        public ActionResult Test()
        {
            //throw new DivideByZeroException();

            string decode = HPStudent.Core.Security.GetConnectionStr("758DE4E3D4D79D297445F8C769B8C7C1B94205700B481BD86E78F4DCDFF45527D532B29BCC7CA7C82804EEC92BD16234DEDF8094E0048972824FCB2D07237F311F3EC8E08AF59A63CCED22CFBAB071008347D6A1376E0273849E47C7070EA461120F00CDCD26F05A");
            string encode = HPStudent.Core.Security.SEncryptString("Data Source=192.168.1.196;Initial Catalog=HPStudent_test;User ID=Hpstudent;Password=Hpstudent@2016;");
            // 数据库连接串-格式(中文为用户修改的内容)：Data Source=数据库服务器地址;UserPassword ID=您的数据库用户名;Password=您的数据库用户密码;Initial Catalog=数据库名称;Pooling=true
            //ViewBag.Password = decode;
            //ViewBag.EnPassword = encode;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Test(FormCollection fc)
        {

            string encode = HPStudent.Core.Security.SEncryptString(fc["tbConnect"]);
            string decode = HPStudent.Core.Security.GetConnectionStr(encode);
            ViewBag.Password = decode;
            ViewBag.EnPassword = encode;
            return View();
        }

        public ActionResult Demo()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult login()
        {
            HPStudent.Core.CookieHelper.SetCookie("LoginUserInfo", "Jack Johnson");
            ViewBag.Username = HPStudent.Core.CookieHelper.GetCookieValue("uname");
            return View();
        }

        [AllowAnonymous]
        public ActionResult logout()
        {
            HPStudent.Core.CookieHelper.ClearCookie("LoginUserInfo");
            ViewBag.Username = HPStudent.Core.CookieHelper.GetCookieValue("LoginUserInfo");
            return View("login");
        }

        [HttpPost]
        public ContentResult GetUserVisitLog()
        {
            DataTable dt = Business.Admin.VisitorLog.GetVisitorList();
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string json = ser.Serialize("");
            if (dt.Rows.Count > 0) 
            {
                json = HPStudent.Core.DtToJson.DataTableJson(dt);
            }
            return Content(json);
        }

    }
}
