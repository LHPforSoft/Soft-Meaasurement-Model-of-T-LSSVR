using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SSmodel
{
    public partial class FormUser : Form
    {
        private FormLogin formlogin { get; set; }

        public FormUser()
        {
            InitializeComponent();
        }

        public FormUser(FormLogin frm)
        {
            InitializeComponent();
            this.formlogin = frm;
        }

        private void FormUser_Load(object sender, EventArgs e)
        {

        }

        private void btnLoginUser_Click(object sender, EventArgs e)
        {
            if (tBUser.Text == "Hepeng Liu" && tBPassword.Text == "123456")
            {

                formlogin.btnForecastSys.Visible = true;
                formlogin.btnInditifySys.Visible = true;
                formlogin.btnTrainSys.Visible = true;

                string UserName = tBUser.Text;
                string TimeLogin = DateTime.Now.ToString();
                formlogin.labelUser.Text = UserName;
                formlogin.labelTime.Text = TimeLogin;


                this.Dispose();
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码不正确！");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //FormLogin FL = new FormLogin();
            //FL.Show();

            this.Dispose();
            this.Close();
        }


    }
}
