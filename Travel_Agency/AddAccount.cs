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
    public partial class AddAccount : Form
    {
        SqlConnection sqlConnection;
        private string login;
        public AddAccount(string log)
        {
            InitializeComponent();
            this.login = log;
            string connection = @"Data Source=DESKTOP-V2FKAKC\SQL_EXPRESS;Initial Catalog=TravelAgency;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connection);
            string str = "SELECT TravelAgent_name FROM TravelAgent";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
                comboBox1.Items.Add(table.Rows[i].ItemArray[0].ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "SELECT Customer_name, TravelAgent_name, Customer_dateOfBirth, Customer_passport, Customer_phoneNumber, User_login FROM Customer, TravelAgent, UserData WHERE TravelAgent_ID = Customer_travelAgent AND Customer.User_ID = UserData.User_ID";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "SELECT TravelAgent_name, TravelAgent_dateOfBirth, TravelAgent_passport, TravelAgent_phoneNumber, User_login FROM TravelAgent, UserData WHERE TravelAgent.User_ID = UserData.User_ID";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Manager form = new Manager(login);
            form.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox2.Text == "" || (comboBox2.Text == "Клиент" && comboBox1.Text == ""))
            {
                MessageBox.Show("Пожалуйста, введите все данные", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                if (comboBox2.Text == "Клиент")
                {
                    sqlConnection.Open();
                    string passport = "SELECT COUNT(Customer_passport) FROM Customer WHERE Customer_passport = '" + textBox2.Text + "'";
                    string passport1 = "SELECT COUNT(TravelAgent_passport) FROM TravelAgent WHERE TravelAgent_passport = '" + textBox2.Text + "'";
                    string passport2 = "SELECT COUNT(Manager_passport) FROM Manager WHERE Manager_passport = '" + textBox2.Text + "'";
                    string login = "SELECT COUNT(User_login) FROM UserData WHERE User_login = '" + textBox4.Text + "'";
                    SqlCommand cmd = new SqlCommand(passport, sqlConnection);
                    SqlCommand cmd1 = new SqlCommand(login, sqlConnection);
                    SqlCommand cmd2 = new SqlCommand(passport1, sqlConnection);
                    SqlCommand cmd3 = new SqlCommand(passport2, sqlConnection);
                    string res = cmd.ExecuteScalar().ToString() + cmd2.ExecuteScalar().ToString() + cmd3.ExecuteScalar().ToString();
                    string res1 = cmd1.ExecuteScalar().ToString();
                    sqlConnection.Close();
                    if (res == "000")
                    {
                        if (res1 == "0")
                        {
                            sqlConnection.Open();
                            string str = "INSERT INTO UserData VALUES('Клиент', '" + textBox4.Text + "', '" + hash(textBox5.Text) + "')";
                            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                            DataSet dataSet = new DataSet();
                            DA.Fill(dataSet);
                            string id = "SELECT TravelAgent_ID FROM TravelAgent WHERE TravelAgent_name = '" + comboBox1.Text + "'";
                            SqlCommand cmd4 = new SqlCommand(id, sqlConnection);
                            string res2 = cmd4.ExecuteScalar().ToString();
                            int resint2 = Convert.ToInt32(res2);
                            string user_id = "SELECT User_ID FROM UserData WHERE User_login = '" + textBox4.Text + "'";
                            SqlCommand cmd5 = new SqlCommand(user_id, sqlConnection);
                            string res3 = cmd5.ExecuteScalar().ToString();
                            int resint3 = Convert.ToInt32(res3);
                            string str1 = "INSERT INTO Customer VALUES(" + resint2 + ", '" + textBox1.Text + "', '" + dateTimePicker1.Value.ToString("dd.MM.yyyy") + "', '" + textBox2.Text + "', '" + textBox3.Text + "', " + resint3 + ")";
                            DA = new SqlDataAdapter(str1, sqlConnection);
                            dataSet = new DataSet();
                            DA.Fill(dataSet);
                            sqlConnection.Close();
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с такими паспортными данными уже существует!", "Ошибка", MessageBoxButtons.OK);
                    }

                }
                else if (comboBox2.Text == "Турагент")
                {
                    sqlConnection.Open();
                    string passport = "SELECT COUNT(Customer_passport) FROM Customer WHERE Customer_passport = '" + textBox2.Text + "'";
                    string passport1 = "SELECT COUNT(TravelAgent_passport) FROM TravelAgent WHERE TravelAgent_passport = '" + textBox2.Text + "'";
                    string passport2 = "SELECT COUNT(Manager_passport) FROM Manager WHERE Manager_passport = '" + textBox2.Text + "'";
                    string login = "SELECT COUNT(User_login) FROM UserData WHERE User_login = '" + textBox4.Text + "'";
                    SqlCommand cmd = new SqlCommand(passport, sqlConnection);
                    SqlCommand cmd1 = new SqlCommand(login, sqlConnection);
                    SqlCommand cmd2 = new SqlCommand(passport1, sqlConnection);
                    SqlCommand cmd3 = new SqlCommand(passport2, sqlConnection);
                    string res = cmd.ExecuteScalar().ToString() + cmd2.ExecuteScalar().ToString() + cmd3.ExecuteScalar().ToString();
                    string res1 = cmd1.ExecuteScalar().ToString();
                    sqlConnection.Close();
                    if (res == "000")
                    {
                        if (res1 == "0")
                        {
                            sqlConnection.Open();
                            string str = "INSERT INTO UserData VALUES('Турагент', '" + textBox4.Text + "', '" + hash(textBox5.Text) + "')";
                            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                            DataSet dataSet = new DataSet();
                            DA.Fill(dataSet);
                            string user_id = "SELECT User_ID FROM UserData WHERE User_login = '" + textBox4.Text + "'";
                            SqlCommand cmd4 = new SqlCommand(user_id, sqlConnection);
                            string res3 = cmd4.ExecuteScalar().ToString();
                            int resint3 = Convert.ToInt32(res3);
                            string str1 = "INSERT INTO TravelAgent VALUES('" + textBox1.Text + "', '" + dateTimePicker1.Value.ToString("dd.MM.yyyy") + "', '" + textBox2.Text + "', '" + textBox3.Text + "', " + resint3 + ", DEFAULT)";
                            DA = new SqlDataAdapter(str1, sqlConnection);
                            dataSet = new DataSet();
                            DA.Fill(dataSet);
                            sqlConnection.Close();
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с такими паспортными данными уже существует!", "Ошибка", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private string hash(string text)
        {
            byte[] data = Encoding.Default.GetBytes(text);
            var result = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
    }
}
