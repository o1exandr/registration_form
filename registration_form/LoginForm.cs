using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace registration_form
{
    public partial class LoginForm : Form
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string strLogin = txtLogin.Text;
            string strPassword = txtPass.Text;
            if (Login(strLogin, strPassword))
                this.DialogResult = DialogResult.OK;
            else
                MessageBox.Show($"Wrong email '{strLogin}' or password '{strPassword}'");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Hide();
            RegisterForm dlgRegister = new RegisterForm();
            dlgRegister.ShowDialog();
        }

        // якщо знайдено відповіний емейл в базі і пароль відповідає йому
        private bool Login(string login, string pass)
        {
            string conStr = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand sqlCommand = new SqlCommand();
            con.Open();
            string query = $"Select * from tblUsers";
            sqlCommand.Connection = con;
            sqlCommand.CommandText = query;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (reader["Email"].ToString() == login)
                {
                    if (reader["Password"].ToString() == pass)
                    {
                        UserId = Convert.ToInt32(reader["Id"]);
                        UserEmail = Convert.ToString(reader["Email"]);
                        con.Close();
                        return true;
                    }
                }
            }
            con.Close();
            return false;
        }
    }
}
