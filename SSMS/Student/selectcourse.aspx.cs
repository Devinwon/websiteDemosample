using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class selectcourse : System.Web.UI.Page
{
    DataSet myds = new DataSet();
    string mysql;
    CommDB mydb = new CommDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int i,j;
            string sn = "";
            mysql = "SELECT * FROM selcourse WHERE sno='" + Session["uno"] +"'";
            i = mydb.Rownum(mysql, "selcourse", ref sn);
            mysql = "SELECT * FROM score WHERE sno='" + Session["uno"] + "'";
            j = mydb.Rownum(mysql, "score", ref sn);
            if (i == 0 && j == 0)  //第一次选课:score和selcourse表中都没有该学号的记录
            {
                mysql = "INSERT INTO selcourse(sno,cno,cname,ctime,cplace,tno,tname,sel) SELECT '" +
                    Session["uno"] + "',cno,cname,ctime,cplace,tno,tname,'×' FROM course";
                mydb.ExecuteNonQuery(mysql);
                bind();
            }
            else if (i > 0)  //selcourse表中有该学号的记录
                bind();
            else
                Response.Redirect("~/dispinfo.aspx?info=" + "你选课已提交,不能再重新选课");
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        e.Cancel = true;
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
    public void bind()  //数据绑定
    {
        mysql = "SELECT * FROM selcourse WHERE sno = '" + Session["uno"] + "'";
        myds = mydb.ExecuteQuery(mysql, "selcourse");
        GridView1.DataSource = myds.Tables["selcourse"];
        GridView1.DataKeyNames = new string[] { "cno" };
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("submitcourse.aspx"); 
    }
   
    protected void Button2_Click(object sender, EventArgs e) //选修
    {
        mysql = "UPDATE selcourse SET sel='√' WHERE sno='" + Session["uno"] +
            "' AND cno='" + ((Button)sender).CommandArgument.ToString() + "'";
        mydb.ExecuteNonQuery(mysql);
        bind();      
    }
    protected void Button3_Click(object sender, EventArgs e)  //取消选课
    {
        mysql = "UPDATE selcourse SET sel='×' WHERE sno='" + Session["uno"] +
            "' AND cno='" + ((Button)sender).CommandArgument.ToString() + "'";
        mydb.ExecuteNonQuery(mysql);
        bind();
    }
}
