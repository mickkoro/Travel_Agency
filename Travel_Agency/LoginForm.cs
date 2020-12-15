using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Travel_Agency
{
    public partial class LoginForm : Form
    {
        SqlConnection sqlConnection;
        public LoginForm()
        {
            InitializeComponent();
            string connection = @"Data Source=DESKTOP-V2FKAKC\SQL_EXPRESS;Initial Catalog=TravelAgency;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connection);
            test("User", "User");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string log = textBox1.Text;
            string pass = hash(textBox2.Text);
            string rule = "None"; 
            sqlConnection.Open();
            rule = "SELECT User_role FROM UserData WHERE User_login = '" + log + "' AND User_password = '" + pass + "'";
            SqlCommand cmd = new SqlCommand(rule, sqlConnection);
            string res = (string)cmd.ExecuteScalar();
            if (res == "Менеджер")
            {
                Manager form = new Manager(log);
                form.Show();
                this.Hide();
            }
            else if (res == "Клиент")
            {
                Customer form = new Customer(log);
                form.Show();
                this.Hide();
            }
            else if (res == "Турагент")
            {
                TravelAgent form = new TravelAgent(log);
                form.Show();
                this.Hide();
            }
            else if (cmd.ExecuteNonQuery() < 1)
            {
                MessageBox.Show("Пользователя с таким логином и паролем не существует.\nПопробуйте ещё раз!", "Ошибка", MessageBoxButtons.OK);
            }
            sqlConnection.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private string hash(string text)
        {
            byte[] data = Encoding.Default.GetBytes(text);
            var result = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }

        public int test(string log, string pass)
        {
            sqlConnection.Open();
            string rule = "SELECT User_role FROM UserData WHERE User_login = '" + log + "' AND User_password = '" + pass + "'";
            SqlCommand cmd = new SqlCommand(rule, sqlConnection);
            string res = (string)cmd.ExecuteScalar();
            sqlConnection.Close();
            if (res == null)
            {
                return -1;
            }
            return 1;
        }

        public string role(string log)
        {
            sqlConnection.Open();
            string rule = "SELECT User_role FROM UserData WHERE User_login = '" + log + "'";
            SqlCommand cmd = new SqlCommand(rule, sqlConnection);
            string res = (string)cmd.ExecuteScalar();
            sqlConnection.Close();
            if (res == null)
            {
                return "ERROR";
            }
            else 
            {
                return res;
            }
        }

        public int correctPass(string log, string pass)
        {
            sqlConnection.Open();
            string pass_ = "SELECT User_password FROM UserData WHERE User_login = '" + log + "'";
            SqlCommand cmd = new SqlCommand(pass_, sqlConnection);
            string res = (string)cmd.ExecuteScalar();
            sqlConnection.Close();
            if (hash(pass) == res)
            {
                return 1;
            }
            else return 0;
        }
    }
}
