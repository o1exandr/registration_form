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
    public partial class LoginForm : Form
    {
        public int UserId { get; set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string strLogin = txtLogin.Text;
            string strPassword = txtPass.Text;
            string login = "admin@gmail.com";
            string password = "123456";
            if (strLogin == login && strPassword == password)
            {
                UserId = 1;
                this.DialogResult = DialogResult.OK;
                return;
            }

            MessageBox.Show($"Не вгадав {strLogin} {strPassword}");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
