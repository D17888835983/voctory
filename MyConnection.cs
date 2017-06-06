
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
using System.Data.SqlClient;


using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;//要调用MD5函数必须引入命名空间
using System.IO;


public partial class MyConnection1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void imbtnSubmit_Click(object sender, EventArgs e)
    {
        // ltlMess.Text = "";
        // string user = Common.UrnHtml(txt_user.Value.Trim());
        //string pwd = txt_pwd.Value;
        // string pwd2 = txt_pwd.Value;


        // byte[] b = System.Text.Encoding.UTF8.GetBytes(pwd);//将textBox2中的内容转化为二进制
        MD5 md5 = new MD5CryptoServiceProvider();//对md5的类型对象进行初始化
        // string pass = System.Text.Encoding.UTF8.GetString(md5.ComputeHash(b));//调用md5函数来进行加密




        string sql = string.Empty;
        //  sql = "select * from Manager where ManagerUser='" + user + "' and ManagerPwd='" + pass + "' and grade='" + grade.SelectedValue + "'";

        SqlDataReader dr = DB.getDataReader(sql);
        if (dr.Read())
        {
            //更新登录次数
            //   string sqlupdate = "update Manager set LoginCount=LoginCount+1 where ManagerUser='" + user + "' and ManagerPwd='" + pass + "'";
            SqlConnection cnupdate = DB.OpenConnection();
            // SqlCommand cmdupdate = new SqlCommand(sqlupdate, cnupdate);
            //  cmdupdate.ExecuteNonQuery();
            cnupdate.Close();
            cnupdate.Dispose();


            //Cookie记录用户登录信息
            HttpCookie cookies;
            cookies = new HttpCookie("loginuser");
            //  cookies.Values.Add("Manager", HttpUtility.UrlEncode(this.txt_user.Value.Trim()));
            cookies.Values.Add("ManagerId", dr[0].ToString());
            // cookies.Values.Add("grade", grade.SelectedValue);
            Response.Cookies.Set(cookies);

            dr.Close();
            dr.Dispose();

            Response.Redirect("MainFrame.aspx");
        }
        else
        {
            dr.Close();
            dr.Dispose();
            //   ltlMess.Text = "登录帐号或密码错误.";
        }



    }
}
