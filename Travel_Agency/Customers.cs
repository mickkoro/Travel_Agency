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

namespace Travel_Agency
{
    public partial class Customers : Form
    {
        SqlConnection sqlConnection;
        private string login;
        public Customers(string log)
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
            {
                comboBox1.Items.Add(table.Rows[i].ItemArray[0].ToString());
                comboBox4.Items.Add(table.Rows[i].ItemArray[0].ToString());
            }
            str = "SELECT Tour_name FROM Tour";
            table = new DataTable();
            DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
                comboBox7.Items.Add(table.Rows[i].ItemArray[0].ToString());
            FillCustomers();
        }

        private void FillCustomers()
        {
            string str = "SELECT Customer_name FROM Customer";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox2.Items.Add(table.Rows[i].ItemArray[0].ToString());
                comboBox3.Items.Add(table.Rows[i].ItemArray[0].ToString());
                comboBox5.Items.Add(table.Rows[i].ItemArray[0].ToString());
                comboBox8.Items.Add(table.Rows[i].ItemArray[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            string agent = "SELECT User_login FROM UserData INNER JOIN TravelAgent ON TravelAgent.User_ID = UserData.User_ID AND TravelAgent_name = '" + comboBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(agent, sqlConnection);
            string res = (string)cmd.ExecuteScalar();
            string str = "SELECT Customer_name FROM Customer INNER JOIN TravelAgent ON ";
            str += "Customer_travelAgent = TravelAgent_ID  INNER JOIN UserData ON ";
            str += "UserData.User_ID = TravelAgent.User_ID AND User_login = '" + res + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
            sqlConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manager form = new Manager(login);
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = "SELECT Customer_name, TravelAgent_name, Customer_dateOfBirth, Customer_passport, Customer_phoneNumber, User_login FROM Customer, TravelAgent, UserData WHERE TravelAgent_ID = Customer_travelAgent AND Customer_name = '" + comboBox2.Text + "' AND Customer.User_ID = UserData.User_ID AND Customer_passport = '" + textBox3.Text + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "" || comboBox4.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите все данные!", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                string str = "DECLARE @new_id AS int ";
                str += "SET @new_id = (SELECT TravelAgent_ID FROM TravelAgent WHERE TravelAgent_name = '" + comboBox4.Text + "') ";
                str += "UPDATE Customer ";
                str += "SET Customer_travelAgent = @new_id WHERE Customer_name = '" + comboBox3.Text + "'";
                SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                DataSet dataSet = new DataSet();
                DA.Fill(dataSet);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox5.Text == "" || comboBox6.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите все данные!", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                string selectedState1 = comboBox5.Text;
                string selectedState2 = comboBox6.Text;
                string result = textBox1.Text;
                switch (selectedState2)
                {
                    case "ФИО":
                        string str = "UPDATE Customer SET Customer_name = '" + result + "' WHERE Customer_name = '" + selectedState1 + "'";
                        SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                        DataSet dataSet = new DataSet();
                        DA.Fill(dataSet);
                        FillCustomers();
                        break;
                    case "Дата рождения":
                        str = "UPDATE Customer SET Customer_dateOfBirth = '" + result + "' WHERE Customer_name = '" + selectedState1 + "'";
                        DA = new SqlDataAdapter(str, sqlConnection);
                        dataSet = new DataSet();
                        DA.Fill(dataSet);
                        break;
                    case "Паспортные данные":
                        string passport = "SELECT COUNT(Customer_passport) FROM Customer WHERE Customer_passport = '" + result + "'";
                        string passport1 = "SELECT COUNT(TravelAgent_passport) FROM TravelAgent WHERE TravelAgent_passport = '" + result + "'";
                        string passport2 = "SELECT COUNT(Manager_passport) FROM Manager WHERE Manager_passport = '" + result + "'";
                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand(passport, sqlConnection);
                        SqlCommand cmd1 = new SqlCommand(passport1, sqlConnection);
                        SqlCommand cmd2 = new SqlCommand(passport2, sqlConnection);
                        string name = cmd.ExecuteScalar().ToString() + cmd1.ExecuteScalar().ToString() + cmd2.ExecuteScalar().ToString();
                        sqlConnection.Close();
                        if (name == "000")
                        {
                            string st = "UPDATE Customer SET Customer_passport = '" + result + "' WHERE Customer_name = '" + selectedState1 + "'";
                            SqlDataAdapter DA1 = new SqlDataAdapter(st, sqlConnection);
                            DataSet dataSet1 = new DataSet();
                            DA1.Fill(dataSet1);
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с такими паспортными данным уже существует!", "Ошибка", MessageBoxButtons.OK);
                        }
                        break;
                    case "Номер телефона":
                        str = "UPDATE Customer SET Customer_phoneNumber = '" + result + "' WHERE Customer_name = '" + selectedState1 + "'";
                        DA = new SqlDataAdapter(str, sqlConnection);
                        dataSet = new DataSet();
                        DA.Fill(dataSet);
                        break;
                    case "Логин":
                        string into = "SELECT COUNT(User_login) FROM UserData WHERE User_login = '" + result + "'";
                        sqlConnection.Open();
                        cmd = new SqlCommand(into, sqlConnection);
                        name = cmd.ExecuteScalar().ToString();
                        sqlConnection.Close();
                        if (name == "0")
                        {
                            string st = "UPDATE UserData SET User_login = '" + result + "' FROM Customer WHERE Customer_name = '" + selectedState1 + "' AND Customer.User_ID = UserData.User_ID";
                            SqlDataAdapter DA1 = new SqlDataAdapter(st, sqlConnection);
                            DataSet dataSet1 = new DataSet();
                            DA1.Fill(dataSet1);
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK);
                        }
                        break;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string str = "SELECT Customer_name, Order_date FROM Customer INNER JOIN OrderTour ON Customer.Customer_ID = OrderTour.Customer_ID INNER JOIN Tour ON ";
            str += "OrderTour.Tour_ID = Tour.Tour_ID WHERE Tour_name = '" + comboBox7.Text + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox8.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите все данные!", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                string str = "SELECT Tour_name, Order_date FROM Tour INNER JOIN OrderTour ON OrderTour.Tour_ID = Tour.Tour_ID INNER JOIN Customer ON ";
                str += "Customer.Customer_ID = OrderTour.Customer_ID WHERE Customer_name = '" + comboBox8.Text + "' AND Customer_passport = '" + textBox2.Text + "'";
                DataTable table = new DataTable();
                SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                DA.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string str = "SELECT * FROM Customers";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }
    }
}
