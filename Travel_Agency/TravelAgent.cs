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
    public partial class TravelAgent : Form
    {
        SqlConnection sqlConnection;
        private string login;
        public TravelAgent(string log)
        {
            InitializeComponent();
            this.login = log;
            string connection = @"Data Source=DESKTOP-V2FKAKC\SQL_EXPRESS;Initial Catalog=TravelAgency;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connection);
            FillTour();
            FillGroup();
        }

        private void FillTour()
        {
            string str = "SELECT Tour_name FROM Tour";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
                comboBox1.Items.Add(table.Rows[i].ItemArray[0].ToString());
        }

        private void FillGroup()
        {
            string str = "SELECT Group_number FROM GroupTour";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox2.Items.Add(table.Rows[i].ItemArray[0].ToString());
                comboBox3.Items.Add(table.Rows[i].ItemArray[0].ToString());
                comboBox4.Items.Add(table.Rows[i].ItemArray[0].ToString());
                comboBox5.Items.Add(table.Rows[i].ItemArray[0].ToString());
                comboBox6.Items.Add(table.Rows[i].ItemArray[0].ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            form.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyAccount form = new MyAccount(login);
            form.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
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
        
        public void customer()
        {
            string str = "SELECT TravelAgent_passport FROM UserData, TravelAgent ";
            str += "WHERE TravelAgent.User_ID = UserData.User_ID ";
            str += "AND User_login = 'TestUser'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string str = "SELECT Customer_name, Customer_passport FROM Customer, UserData, TravelAgent ";
            str += "WHERE TravelAgent.User_ID = UserData.User_ID ";
            str += "AND Customer_travelAgent = TravelAgent_ID AND User_login = '" + login + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string str = "SELECT Customer_name, Tour_name, Order_date FROM Customer, Tour, OrderTour, TravelAgent, UserData ";
            str += "WHERE Customer.Customer_ID = OrderTour.Customer_ID AND Tour.Tour_ID = OrderTour.Tour_ID ";
            str += "AND Customer_travelAgent = TravelAgent_ID AND TravelAgent.User_ID = UserData.User_ID ";
            str += "AND User_login = '" + login + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "" || textBox1.Text == "")
                {
                    MessageBox.Show("Пожалуйста, введите все данные", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    string str = "DECLARE @num_of_tour AS int ";
                    str += "SET @num_of_tour = (SELECT Tour_ID FROM Tour WHERE Tour_name = '" + this.comboBox1.Text + "') ";
                    str += "INSERT INTO GroupTour ";
                    str += "VALUES(" + this.textBox1.Text + ", @num_of_tour)";
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DataSet dataSet = new DataSet();
                    DA.Fill(dataSet);
                    FillGroup();
                }
            }
            catch
            {
                MessageBox.Show("Группа с таким номером уже существует.\nПопробуйте другой номер группы.", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void Groups()
        {
            string str = "SELECT Tour_name, Group_number FROM Tour INNER JOIN GroupTour ON Group_tour = Tour_ID";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Groups();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите все данные", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                sqlConnection.Open();
                string st = "SELECT Group_ID FROM GroupTour WHERE Group_number = '" + comboBox2.Text + "'";
                string st1 = "SELECT Group_ID FROM GroupTour WHERE Group_number = '" + comboBox3.Text + "'";
                SqlCommand cm = new SqlCommand(st, sqlConnection);
                SqlCommand cm1 = new SqlCommand(st1, sqlConnection);
                string res = cm.ExecuteScalar().ToString();
                string res1 = cm1.ExecuteScalar().ToString();
                int gr1 = Convert.ToInt32(res);
                int gr2 = Convert.ToInt32(res1);
                string str = "DECLARE @prev_group AS int, @next_group AS int ";
                str += "SET @prev_group = " + gr1 + " ";
                str += "SET @next_group = " + gr2 + " ";
                str += "EXEC Change_group @prev_group, @next_group";
                SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                DataSet dataSet = new DataSet();
                DA.Fill(dataSet);
                sqlConnection.Close();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string str = "SELECT Customer_name, Tour_name, Group_number FROM Customer INNER JOIN TravelAgent ON TravelAgent_ID = Customer_travelAgent ";
            str += "INNER JOIN UserData ON TravelAgent.User_ID = UserData.User_ID INNER JOIN GroupList ON Customer.Customer_ID = GroupList.Customer_ID ";
            str += "INNER JOIN GroupTour ON GroupList.Group_ID = GroupTour.Group_ID INNER JOIN Tour ON Group_tour = Tour_ID WHERE User_login = '" + login + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string str = "SELECT Customer_name FROM Customer INNER JOIN GroupList ON Customer.Customer_ID = GroupList.Customer_ID ";
            str += "INNER JOIN GroupTour ON GroupList.Group_ID = GroupTour.Group_ID AND Group_number = '" + comboBox4.Text + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string str = "SELECT Customer_name, Tour_name, Group_number FROM Customer INNER JOIN TravelAgent ON TravelAgent_ID = Customer_travelAgent ";
            str += "INNER JOIN UserData ON TravelAgent.User_ID = UserData.User_ID INNER JOIN GroupList ON Customer.Customer_ID = GroupList.Customer_ID ";
            str += "INNER JOIN GroupTour ON GroupList.Group_ID = GroupTour.Group_ID INNER JOIN Tour ON Group_tour = Tour_ID WHERE User_login = '" + login + "'" ;
            str += "AND Customer_passport = '" + textBox2.Text + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || comboBox5.Text == "" || comboBox6.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите все данные", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                sqlConnection.Open();
                string cnt = "SELECT COUNT(Customer_passport) FROM Customer WHERE Customer_passport = '" + textBox3.Text + "'";
                SqlCommand cn = new SqlCommand(cnt, sqlConnection);
                string count = cn.ExecuteScalar().ToString();
                sqlConnection.Close();
                if (count == "0")
                {
                    MessageBox.Show("Пользователя с такими паспортными данными не существует!", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    sqlConnection.Open();
                    string st = "SELECT Group_ID FROM GroupTour WHERE Group_number = '" + comboBox5.Text + "'";
                    string st1 = "SELECT Group_ID FROM GroupTour WHERE Group_number = '" + comboBox6.Text + "'";
                    SqlCommand cm = new SqlCommand(st, sqlConnection);
                    SqlCommand cm1 = new SqlCommand(st1, sqlConnection);
                    string res = cm.ExecuteScalar().ToString();
                    string res1 = cm1.ExecuteScalar().ToString();
                    int gr1 = Convert.ToInt32(res);
                    int gr2 = Convert.ToInt32(res1);
                    string str = "UPDATE GroupList ";
                    str += "SET Group_ID = " + gr2 + " FROM Customer WHERE Customer_passport = '" + textBox3.Text + "' ";
                    str += "AND Customer.Customer_ID = GroupList.Customer_ID AND Group_ID = " + gr1;
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DataSet dataSet = new DataSet();
                    DA.Fill(dataSet);
                    sqlConnection.Close();
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите все данные", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                sqlConnection.Open();
                string cnt = "SELECT COUNT(Customer_passport) FROM Customer WHERE Customer_passport = '" + textBox4.Text + "'";
                SqlCommand cn = new SqlCommand(cnt, sqlConnection);
                string count = cn.ExecuteScalar().ToString();
                sqlConnection.Close();
                if (count == "0")
                {
                    MessageBox.Show("Пользователя с такими паспортными данными не существует!", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    sqlConnection.Open();
                    string tour = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    string str = "DECLARE @num_of_customer AS int, @num_of_gr AS int ";
                    str += "SET @num_of_customer = (SELECT Customer_ID FROM Customer WHERE Customer_passport = '" + textBox4.Text + "') ";
                    str += "SET @num_of_gr = (SELECT TOP 1 GroupTour.Group_ID FROM GroupTour, Tour, GroupList WHERE Group_tour = Tour_ID ";
                    str += "AND GroupList.Group_ID = GroupTour.Group_ID AND Tour_name = '" + tour + "' ";
                    str += "GROUP BY GroupTour.Group_ID ";
                    str += "ORDER BY COUNT(GroupList.GROUP_ID)) ";
                    str += "INSERT INTO GroupList ";
                    str += "VALUES (@num_of_customer, @num_of_gr)";
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DataSet dataSet = new DataSet();
                    DA.Fill(dataSet);
                    string st = "SELECT Customer_ID FROM Customer WHERE Customer_passport = '" + textBox4.Text + "'";
                    SqlCommand cm = new SqlCommand(st, sqlConnection);
                    string res = cm.ExecuteScalar().ToString();
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

        public int testCount()
        {
            Groups();
            int count = dataGridView1.RowCount;
            return count;
        }

        public void addTest()
        {
            string str = "DECLARE @num_of_tour AS int ";
            str += "SET @num_of_tour = (SELECT Tour_ID FROM Tour WHERE Tour_name = 'Греция') ";
            str += "INSERT INTO GroupTour ";
            str += "VALUES(100, @num_of_tour)";
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DataSet dataSet = new DataSet();
            DA.Fill(dataSet);
        }
        public void deleteTest()
        {
            string str = "DELETE FROM GroupTour WHERE Group_number = 100";
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DataSet dataSet = new DataSet();
            DA.Fill(dataSet);
        }

        public string test()
        {
            customer();
            string str = dataGridView1.Rows[0].Cells[0].Value.ToString();
            if (str.Length == 11 && str[4] == ' ')
            {
                return "YES";
            }
            return "ERROR";
        }
    }
}
