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
    public partial class FormLogin : Form
    {
        private FormUser Formu { get; set; }
        public FormLogin()
        {
            InitializeComponent();
        }

        public FormLogin(FormUser frm)
        {
            InitializeComponent();
            Formu = frm;
        }

        public static void AfterLogin(string user)
        {


        }
        private void FormLogin_Load(object sender, EventArgs e)
        {

            //btnForecastSys.Visible = false;
            //btnInditifySys.Visible = false;
            //btnTrainSys.Visible = false;



        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //this.Hide();
            FormUser FU = new FormUser(this);
            FU.Show();
            FU.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            this.Dispose();
            this.Close();
            Application.Exit();
        }

        private void btnInditifySys_Click(object sender, EventArgs e)
        {
            FormIndentifyDelay FID = new FormIndentifyDelay();
            FID.Show();
            FID.Focus();
        }

        private void btnTrainSys_Click(object sender, EventArgs e)
        {
            FormTrain FM = new FormTrain();
            FM.Show();
            FM.Focus();
        }

        private void btnForecastSys_Click(object sender, EventArgs e)
        {
            FormFrecast FF = new FormFrecast();
            FF.Show();
            FF.Focus();
        }

    }
}
