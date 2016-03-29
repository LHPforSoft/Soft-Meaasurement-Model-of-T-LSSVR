using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace 控制器
{
    public partial class LoginUser : System.Web.UI.Page
    {
        public string PasswordFind;
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelTime.Text = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.DayOfWeek.ToString();
            if (!IsPostBack)
            {
                TextBoxUser.Text = null;
                TextBoxPassword.Text = null;
            }
            if (Page.IsCallback == true)
            {
                TextBoxUser.Text = null;
                TextBoxPassword.Text = null;
            }
        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string UserInput = TextBoxUser.Text;
            string passwordInput = TextBoxPassword.Text;
            if (UserInput == "")
            {
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "success", "<script>alert('请先输入用户名！');</script>", false);
            }
            else if (passwordInput == "")
            {
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "success", "<script>alert('请输入密码！');</script>", false);
            }
            else
            {
                string SqlPathFind = "";
                string SqlSerPath = Server.MapPath("~/Path/");
                string Sqlfilename = "SqlName" + ".txt";
                System.IO.FileInfo Sqlfile = new FileInfo(SqlSerPath + Sqlfilename);
                if (File.Exists(Sqlfile.ToString()))
                {
                    StreamReader sr0 = new StreamReader(Sqlfile.ToString(), Encoding.GetEncoding("gb2312"));
                    string[] str0;
                    str0 = sr0.ReadToEnd().Trim().Split('\r');
                    SqlPathFind = str0[0];
                    sr0.Close();
                }
                SQLConnect DA = new SQLConnect(SqlPathFind);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                string UserExited = "select * from 用户表";
                dt = DA.ExeSQLdt(UserExited);
                int n = dt.Rows.Count;
                string[] name = new string[n];
                string[] password = new string[n];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    name[i] = (string)dt.Rows[i]["用户账号"];
                    name[i] = name[i].Trim();
                    password[i] = (string)dt.Rows[i]["密码"];
                    password[i] = password[i].Trim();
                }
                for (int j = 0; j < n; j++)
                {
                    if (UserInput != name[j] & j < n - 1)
                    {
                        continue;
                    }
                    else if (UserInput != name[j] & j == n - 1)
                    {
                        ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "success", "<script>alert('对不起!无此用户 请注册！');</script>", false);
                        TextBoxUser.Focus();
                    }
                    else
                    {
                        if (passwordInput == password[j])
                        {
                            Session["UserInput"] = UserInput;
                            Response.Redirect("ControllerDisplay.aspx", true);
                            break;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "success", "<script>alert('对不起!登录密码不正确！');</script>", false);
                            TextBoxPassword.Focus();
                            break;
                        }
                    }
                }
                DA.CloseCon();
            }

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }
        protected void BtnKouling_Click(object sender, EventArgs e)
        {
            string SerPath = Server.MapPath("~/Path/");
            if (Directory.Exists(SerPath) == false)
            {
                Directory.CreateDirectory(SerPath);
            }
            string filename = "ApcPath" + ".txt";
            System.IO.FileInfo file = new FileInfo(SerPath + filename);
            if (File.Exists(file.ToString()))
            {
                StreamReader sr0 = new StreamReader(file.ToString(), Encoding.GetEncoding("gb2312"));
                string[] str0;
                str0 = sr0.ReadToEnd().Trim().Split('\n');
                PasswordFind = str0[1].Substring(5, str0[1].Length - 5);
                sr0.Close();
            }
            if (TBKouling.Text == PasswordFind)
            {
                Session["Psw"] = PasswordFind;
                Response.Redirect("RegisterUser.aspx", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "success", "<script>alert('注册口令不正确！');</script>", false);
                TBKouling.Focus();
            }
        }
    }
}