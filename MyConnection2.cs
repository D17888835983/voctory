
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


public partial class MyConnection2 : System.Web.UI.Page
{
    string id = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Get_Data();
            }
        }
    }

    void Get_Data()
    {
        try
        {
            DataTable dt = DB.getDataTable("select * from Manager where ManagerId=" + id);
            if (dt.Rows.Count == 1)
            {
               // txtManagerUser.Enabled = false;
               // txtManagerUser.Text = dt.Rows[0]["ManagerUser"].ToString();
               // txtManagerPwd.Text = dt.Rows[0]["ManagerPwd"].ToString();
               // txtXingMing.Text = dt.Rows[0]["XingMing"].ToString();
               // txtXueHao.Text = dt.Rows[0]["XueHao"].ToString();
               // txtBanJi.Text = dt.Rows[0]["BanJi"].ToString();
               // grade.SelectedValue = dt.Rows[0]["grade"].ToString();
            }
        }
        catch (Exception ex)
        {
            Common.ShowMessage(this.Page, "页面加载时出现异常。", "");
            return;
        }
    }

    /// <summary>
    /// 按钮事件：提交信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string sql2 = string.Empty;


           // byte[] b = System.Text.Encoding.UTF8.GetBytes(txtManagerPwd.Text);//将textBox2中的内容转化为二进制
            MD5 md5 = new MD5CryptoServiceProvider();//对md5的类型对象进行初始化
           // string pass = System.Text.Encoding.UTF8.GetString(md5.ComputeHash(b));//调用md5函数来进行加密



            
            if (string.IsNullOrEmpty(id))//添加
            {
               // if (DB.getDataTable("select * from Manager where ManagerUser='" + txtManagerUser.Text + "'").Rows.Count > 0)
                {
                    JavaScriptHelper.Alert("此管理员帐号已存在。");
                    return;
                }
              //  else
                {
                    sql2 = "insert into Manager(ManagerUser,ManagerPwd,XingMing,XueHao,BanJi,grade) ";
                   // sql2 += "values('" + txtManagerUser.Text + "','" + pass + "','" + txtXingMing.Text + "','" + txtXueHao.Text + "','" + txtBanJi.Text + "','" + grade.SelectedValue + "')";
                }
            }
            else//修改
            {
                sql2 = "update Manager set grade='{0}',ManagerPwd='{1}',XingMing='{2}',XueHao='{3}',BanJi='{4}' where ManagerId=" + id;
               // sql2 = string.Format(sql2, grade.SelectedValue, pass, txtXingMing.Text, txtXueHao.Text, txtBanJi.Text);
            }

            SqlConnection cn = DB.OpenConnection();
            SqlCommand cmd = new SqlCommand(sql2, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
            Common.ShowMessage(this.Page, "信息保存成功！", "", "managermanage.aspx");
        }
        catch (Exception ex)
        {
            Common.ShowMessage(this.Page, "信息保存失败，请稍后重试。", "");
            return;
        }


       // Label1.Text = HttpUtility.UrlDecode(Request.Cookies["loginuser"]["Manager"].ToString());
       // rizhi.SaveLog("用户权限操作 操作人： " + Label1.Text.Trim() + "   被操作的用户名：" + txtManagerUser.Text.Trim() + "  " + grade.Text.Trim() + "  " + txtBanJi.Text.Trim() + "  " + txtXueHao.Text.Trim() + "  " + txtXingMing.Text.Trim());
       // Label1.Text = "";



    }

}
