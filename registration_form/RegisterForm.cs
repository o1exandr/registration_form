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
    public partial class RegisterForm : Form
    {
        public string userInfo;
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btRegister_Click(object sender, EventArgs e)
        {
            string strEmail = txtEmail.Text;
            string strPass1 = txtPass1.Text;
            string strPass2 = txtPass2.Text;
            if (strPass1 != strPass2) //якщо не ідентичні, то інформуємо
                MessageBox.Show("Different password. Enter same password");
            else
            {
                Registration(strEmail, strPass1);
                this.DialogResult = DialogResult.OK;
            }

        }

        private void Registration(string login, string pass)
        {
            string conStr = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand sqlCommand = new SqlCommand();
            con.Open();
            string query = $"Select * from tblUsers";
            sqlCommand.Connection = con;
            sqlCommand.CommandText = query;
            SqlDataReader reader = sqlCommand.ExecuteReader();

            // перевіряємо чи такий емелй існує в базі
            while (reader.Read())
            {
                if (reader["Email"].ToString() == login) // якщо вже існує повідомляємо діалоговим вікном
                {
                        MessageBox.Show($"Email '{login}' already exists in the database");
                        con.Close();
                        return;
                }
            }
            reader.Close();
            try
            {
                // якщо не існує, то вносимо запис в базу
                query = "INSERT INTO [dbo].[tblUsers] ([Email], [Password]) " +
                                $"VALUES ('{login}', '{pass}')";
                sqlCommand.CommandText = query;
                sqlCommand.ExecuteNonQuery();
                userInfo = login;
                MessageBox.Show($"User '{login}' added");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            con.Close();
        }
    }
}
