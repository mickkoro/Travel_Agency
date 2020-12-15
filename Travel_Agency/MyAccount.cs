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
    public partial class MyAccount : Form
    {
        SqlConnection sqlConnection;
        private string login;
        public MyAccount(string log)
        {
            InitializeComponent();
            this.login = log;
            string connection = @"Data Source=DESKTOP-V2FKAKC\SQL_EXPRESS;Initial Catalog=TravelAgency;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();
            textBox5.Text = login;
            string st = "SELECT User_role FROM UserData WHERE User_login = '" + login + "'";
            SqlCommand cm = new SqlCommand(st, sqlConnection);
            string result = cm.ExecuteScalar().ToString();
            if (result == "Менеджер")
            {
                string fio = "SELECT Manager_name FROM Manager, UserData WHERE Manager.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                string dateOfBirth = "SELECT Manager_dateOfBirth FROM Manager, UserData WHERE Manager.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                string passport = "SELECT Manager_passport FROM Manager, UserData WHERE Manager.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                string phoneNumber = "SELECT Manager_phoneNumber FROM Manager, UserData WHERE Manager.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                SqlCommand cmd = new SqlCommand(fio, sqlConnection);
                SqlCommand cmd1 = new SqlCommand(dateOfBirth, sqlConnection);
                SqlCommand cmd2 = new SqlCommand(passport, sqlConnection);
                SqlCommand cmd3 = new SqlCommand(phoneNumber, sqlConnection);
                string res = cmd.ExecuteScalar().ToString();
                string res1 = cmd1.ExecuteScalar().ToString();
                string res2 = cmd2.ExecuteScalar().ToString();
                string res3 = cmd3.ExecuteScalar().ToString();
                textBox1.Text = res;
                textBox2.Text = res1.Substring(0, 10);
                textBox3.Text = res2;
                textBox4.Text = res3;
            }
            else if (result == "Клиент")
            {
                string fio = "SELECT Customer_name FROM Customer, UserData WHERE Customer.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                string dateOfBirth = "SELECT Customer_dateOfBirth FROM Customer, UserData WHERE Customer.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                string passport = "SELECT Customer_passport FROM Customer, UserData WHERE Customer.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                string phoneNumber = "SELECT Customer_phoneNumber FROM Customer, UserData WHERE Customer.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                SqlCommand cmd = new SqlCommand(fio, sqlConnection);
                SqlCommand cmd1 = new SqlCommand(dateOfBirth, sqlConnection);
                SqlCommand cmd2 = new SqlCommand(passport, sqlConnection);
                SqlCommand cmd3 = new SqlCommand(phoneNumber, sqlConnection);
                string res = cmd.ExecuteScalar().ToString();
                string res1 = cmd1.ExecuteScalar().ToString();
                string res2 = cmd2.ExecuteScalar().ToString();
                string res3 = cmd3.ExecuteScalar().ToString();
                textBox1.Text = res;
                textBox2.Text = res1.Substring(0, 10);
                textBox3.Text = res2;
                textBox4.Text = res3;
            }
            else if (result == "Турагент")
            {
                string fio = "SELECT TravelAgent_name FROM TravelAgent, UserData WHERE TravelAgent.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                string dateOfBirth = "SELECT TravelAgent_dateOfBirth FROM TravelAgent, UserData WHERE TravelAgent.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                string passport = "SELECT TravelAgent_passport FROM TravelAgent, UserData WHERE TravelAgent.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                string phoneNumber = "SELECT TravelAgent_phoneNumber FROM TravelAgent, UserData WHERE TravelAgent.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                SqlCommand cmd = new SqlCommand(fio, sqlConnection);
                SqlCommand cmd1 = new SqlCommand(dateOfBirth, sqlConnection);
                SqlCommand cmd2 = new SqlCommand(passport, sqlConnection);
                SqlCommand cmd3 = new SqlCommand(phoneNumber, sqlConnection);
                string res = cmd.ExecuteScalar().ToString();
                string res1 = cmd1.ExecuteScalar().ToString();
                string res2 = cmd2.ExecuteScalar().ToString();
                string res3 = cmd3.ExecuteScalar().ToString();
                textBox1.Text = res;
                textBox2.Text = res1.Substring(0, 10);
                textBox3.Text = res2;
                textBox4.Text = res3;
            }
            sqlConnection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            string st = "SELECT User_role FROM UserData WHERE User_login = '" + login + "'";
            SqlCommand cm = new SqlCommand(st, sqlConnection);
            string result = cm.ExecuteScalar().ToString();
            if (result == "Менеджер")
            {
                string str = "SELECT Manager_name, Manager_dateOfBirth, Manager_passport, Manager_phoneNumber, User_login FROM UserData, Manager WHERE Manager.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                DataTable table = new DataTable();
                SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                DA.Fill(table);
                dataGridView1.DataSource = table;
            }
            else if (result == "Клиент")
            {
                string str = "SELECT Customer_name, Customer_dateOfBirth, Customer_passport, Customer_phoneNumber, User_login FROM UserData, Customer WHERE Customer.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                DataTable table = new DataTable();
                SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                DA.Fill(table);
                dataGridView1.DataSource = table;
            }
            else if (result == "Турагент")
            {
                string str = "SELECT TravelAgent_name, TravelAgent_dateOfBirth, TravelAgent_passport, TravelAgent_phoneNumber, User_login FROM UserData, TravelAgent WHERE TravelAgent.User_ID = UserData.User_ID AND User_login = '" + login + "'";
                DataTable table = new DataTable();
                SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                DA.Fill(table);
                dataGridView1.DataSource = table;
            }
            sqlConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            string str = "SELECT User_role FROM UserData WHERE User_login = '" + login + "'";
            SqlCommand cmd = new SqlCommand(str, sqlConnection);
            string res = cmd.ExecuteScalar().ToString();
            sqlConnection.Close();
            if (res == "Менеджер")
            {
                Manager form = new Manager(login);
                form.Show();
                this.Close();
            }
            else if (res == "Клиент")
            {
                Customer form = new Customer(login);
                form.Show();
                this.Close();
            }
            else if (res == "Турагент")
            {
                TravelAgent form = new TravelAgent(login);
                form.Show();
                this.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("Введите старый пароль!", "Ошибка", MessageBoxButtons.OK);
            }
            else if (textBox7.Text == "")
            {
                MessageBox.Show("Введите новый пароль!", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                sqlConnection.Open();
                string str = "SELECT User_password FROM UserData WHERE User_login = '" + login + "'";
                SqlCommand cmd = new SqlCommand(str, sqlConnection);
                string res = cmd.ExecuteScalar().ToString();
                sqlConnection.Close();
                if (hash(textBox6.Text) == res)
                {
                    if (hash(textBox6.Text) == hash(textBox7.Text))
                    {
                        MessageBox.Show("Новый пароль не может быть как старый пароль!", "Ошибка", MessageBoxButtons.OK);
                    }
                    else
                    {
                        string str1 = "UPDATE UserData SET User_password = '" + hash(textBox7.Text) + "' WHERE User_login = '" + login + "'";
                        SqlDataAdapter DA = new SqlDataAdapter(str1, sqlConnection);
                        DataSet dataSet = new DataSet();
                        DA.Fill(dataSet);
                        MessageBox.Show("Пароль успешно изменен", "Успешно", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Старый пароль введен неверно!", "Ошибка", MessageBoxButtons.OK);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите ФИО!", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                sqlConnection.Open();
                string st = "SELECT User_role FROM UserData WHERE User_login = '" + login + "'";
                SqlCommand cm = new SqlCommand(st, sqlConnection);
                string result = cm.ExecuteScalar().ToString();
                sqlConnection.Close();
                if (result == "Менеджер")
                {
                    string str = "UPDATE Manager SET Manager_name = '" + textBox1.Text + "' FROM UserData WHERE UserData.User_ID = Manager.User_ID AND User_login = '" + login + "'";
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DataSet dataSet = new DataSet();
                    DA.Fill(dataSet);
                }
                else if (result == "Турагент")
                {
                    string str = "UPDATE TravelAgent SET TravelAgent_name = '" + textBox1.Text + "' FROM UserData WHERE UserData.User_ID = TravelAgent.User_ID AND User_login = '" + login + "'";
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DataSet dataSet = new DataSet();
                    DA.Fill(dataSet);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Введите дату рождения!", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                sqlConnection.Open();
                string st = "SELECT User_role FROM UserData WHERE User_login = '" + login + "'";
                SqlCommand cm = new SqlCommand(st, sqlConnection);
                string result = cm.ExecuteScalar().ToString();
                if (result == "Менеджер")
                {
                    string str = "UPDATE Manager SET Manager_dateOfBirth = '" + textBox2.Text + "' FROM UserData WHERE UserData.User_ID = Manager.User_ID AND User_login = '" + login + "'";
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DataSet dataSet = new DataSet();
                    DA.Fill(dataSet);
                }
                else if (result == "Турагент")
                {
                    string str = "UPDATE TravelAgent SET TravelAgent_dateOfBirth = '" + textBox2.Text + "' FROM UserData WHERE UserData.User_ID = TravelAgent.User_ID AND User_login = '" + login + "'";
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DataSet dataSet = new DataSet();
                    DA.Fill(dataSet);
                }
                sqlConnection.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Введите номер телефона!", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                sqlConnection.Open();
                string st = "SELECT User_role FROM UserData WHERE User_login = '" + login + "'";
                SqlCommand cm = new SqlCommand(st, sqlConnection);
                string result = cm.ExecuteScalar().ToString();
                if (result == "Менеджер")
                {
                    string str = "UPDATE Manager SET Manager_phoneNumber = '" + textBox4.Text + "' FROM UserData WHERE UserData.User_ID = Manager.User_ID AND User_login = '" + login + "'";
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DataSet dataSet = new DataSet();
                    DA.Fill(dataSet);
                }
                else if (result == "Турагент")
                {
                    string str = "UPDATE TravelAgent SET TravelAgent_phoneNumber = '" + textBox4.Text + "' FROM UserData WHERE UserData.User_ID = TravelAgent.User_ID AND User_login = '" + login + "'";
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DataSet dataSet = new DataSet();
                    DA.Fill(dataSet);
                }
                sqlConnection.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите паспортные данные!", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                sqlConnection.Open();
                string passport = "SELECT COUNT(Customer_passport) FROM Customer WHERE Customer_passport = '" + textBox3.Text + "'";
                string passport1 = "SELECT COUNT(TravelAgent_passport) FROM TravelAgent WHERE TravelAgent_passport = '" + textBox3.Text + "'";
                string passport2 = "SELECT COUNT(Manager_passport) FROM Manager WHERE Manager_passport = '" + textBox3.Text + "'";
                SqlCommand cmd = new SqlCommand(passport, sqlConnection);
                SqlCommand cmd1 = new SqlCommand(passport1, sqlConnection);
                SqlCommand cmd2 = new SqlCommand(passport2, sqlConnection);
                string res = cmd.ExecuteScalar().ToString() + cmd1.ExecuteScalar().ToString() + cmd2.ExecuteScalar().ToString();
                if (res == "000")
                {
                    string st = "SELECT User_role FROM UserData WHERE User_login = '" + login + "'";
                    SqlCommand cm = new SqlCommand(st, sqlConnection);
                    string result = cm.ExecuteScalar().ToString();
                    if (result == "Менеджер")
                    {
                        string str = "UPDATE Manager SET Manager_passport = '" + textBox3.Text + "' FROM UserData WHERE UserData.User_ID = Manager.User_ID AND User_login = '" + login + "'";
                        SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                        DataSet dataSet = new DataSet();
                        DA.Fill(dataSet);
                    }
                    else if (result == "Турагент")
                    {
                        string str = "UPDATE TravelAgent SET TravelAgent_passport = '" + textBox3.Text + "' FROM UserData WHERE UserData.User_ID = TravelAgent.User_ID AND User_login = '" + login + "'";
                        SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                        DataSet dataSet = new DataSet();
                        DA.Fill(dataSet);
                    }
                    sqlConnection.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь с такими паспортными данными уже существует!", "Ошибка", MessageBoxButtons.OK);
                }
            }
        }

        private void updateLogin(string log)
        {
            if (log == "")
            {
                MessageBox.Show("Введите логин!", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                sqlConnection.Open();
                string newlogin = "SELECT COUNT(User_login) FROM UserData WHERE User_login = '" + log + "'";
                SqlCommand cmd = new SqlCommand(newlogin, sqlConnection);
                string res = cmd.ExecuteScalar().ToString();
                sqlConnection.Close();
                if (res == "0")
                {
                    string str = "UPDATE UserData SET User_login = '" + log + "' FROM UserData WHERE User_login = '" + login + "'";
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DataSet dataSet = new DataSet();
                    DA.Fill(dataSet);
                    login = log;
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            updateLogin(textBox5.Text);
        }

        public string testUpdate(string log)
        {
            updateLogin(log);
            return login;
        }

        private void MyAccount_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();
            string str = "SELECT User_role FROM UserData WHERE User_login = '" + login + "'";
            SqlCommand cmd = new SqlCommand(str, sqlConnection);
            string res = cmd.ExecuteScalar().ToString();
            sqlConnection.Close();
            if (res == "Клиент")
            {
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
            }
        }

        private string hash(string text)
        {
            byte[] data = Encoding.Default.GetBytes(text);
            var result = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
    }
}
