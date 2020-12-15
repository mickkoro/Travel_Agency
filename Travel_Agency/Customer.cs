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
    public partial class Customer : Form
    {
        SqlConnection sqlConnection;
        private string login;
        public Customer(string log)
        {
            InitializeComponent();
            this.login = log;
            string connection = @"Data Source=DESKTOP-V2FKAKC\SQL_EXPRESS;Initial Catalog=TravelAgency;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connection);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            form.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "SELECT Tour_name, Tour_description, Tour_dateFrom, Tour_dateTo, Tour_price FROM Tour";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string str = "SELECT * FROM Popular_tours";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string str = "SELECT Tour_name, Group_number FROM Tour INNER JOIN GroupTour ON Tour_ID = Group_tour INNER JOIN GroupList ON GroupList.Group_ID = GroupTour.Group_ID INNER JOIN Customer ON Customer.Customer_ID = GroupList.Customer_ID INNER JOIN UserData ON UserData.User_ID = Customer.User_ID WHERE User_login = '" + login + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyAccount form = new MyAccount(login);
            form.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            string st = "SELECT Customer_passport FROM UserData, Customer WHERE Customer.User_ID = UserData.User_ID AND User_login = '" + login + "'";
            SqlCommand cm = new SqlCommand(st, sqlConnection);
            string res = cm.ExecuteScalar().ToString();
            string tour = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string str = "DECLARE @num_of_customer AS int, @num_of_gr AS int ";
            str += "SET @num_of_customer = (SELECT Customer_ID FROM Customer WHERE Customer_passport = '" + res + "') ";
            str += "SET @num_of_gr = (SELECT TOP 1 GroupTour.Group_ID FROM GroupTour, Tour, GroupList WHERE Group_tour = Tour_ID ";
            str += "AND GroupList.Group_ID = GroupTour.Group_ID AND Tour_name = '" + tour + "' ";
            str += "GROUP BY GroupTour.Group_ID ";
            str += "ORDER BY COUNT(GroupList.GROUP_ID)) ";
            str += "INSERT INTO GroupList ";
            str += "VALUES (@num_of_customer, @num_of_gr)";
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DataSet dataSet = new DataSet();
            DA.Fill(dataSet);
            st = "SELECT Customer_ID FROM UserData, Customer WHERE Customer.User_ID = UserData.User_ID AND User_login = '" + login + "'";
            cm = new SqlCommand(st, sqlConnection);
            res = cm.ExecuteScalar().ToString();
            string st1 = "SELECT Tour_ID FROM Tour WHERE Tour_name = '" + tour + "'";
            SqlCommand cm1 = new SqlCommand(st1, sqlConnection);
            string res1 = cm1.ExecuteScalar().ToString();
            str = "INSERT INTO OrderTour VALUES(" + res + ", " + res1 + ", '" + DateTime.Now.ToShortDateString() + "', DEFAULT)";
            DA = new SqlDataAdapter(str, sqlConnection);
            dataSet = new DataSet();
            DA.Fill(dataSet);
            sqlConnection.Close();
        }
    }
}
