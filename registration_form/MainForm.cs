using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace registration_form
{
    public partial class MainForm : Form
    {
        private int _userId = 0;
        public MainForm()
        {
            this.InitializeComponent();
            
            LoginForm dlgLogin = new LoginForm();
            if (dlgLogin.ShowDialog() == DialogResult.OK)
            {
                _userId = dlgLogin.UserId;
                lblUserId.Text = "Connected! User id = "
                    + _userId.ToString() 
                    + "; User Email: " + dlgLogin.UserEmail;
            }
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {
            if (_userId != 0)
            {
                this.Close();
            }
        }
    }
}
