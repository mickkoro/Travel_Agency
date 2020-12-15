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
    public partial class Sales : Form
    {
        SqlConnection sqlConnection;
        private string login;
        public Sales(string log)
        {
            InitializeComponent();
            this.login = log;
            string connection = @"Data Source=DESKTOP-V2FKAKC\SQL_EXPRESS;Initial Catalog=TravelAgency;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connection);
            string str = "SELECT Tour_name FROM Tour";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            comboBox2.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
                comboBox2.Items.Add(table.Rows[i].ItemArray[0].ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedState = comboBox1.Text;
            switch (selectedState)
            {
                case "Январь":
                    string str = "SELECT * FROM January";
                    DataTable table = new DataTable();
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;
                case "Февраль":
                    str = "SELECT * FROM February";
                    table = new DataTable();
                    DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;
                case "Март":
                    str = "SELECT * FROM March";
                    table = new DataTable();
                    DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;
                case "Апрель":
                    str = "SELECT * FROM April";
                    table = new DataTable();
                    DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;
                case "Май":
                    str = "SELECT * FROM May";
                    table = new DataTable();
                    DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;
                case "Июнь":
                    str = "SELECT * FROM June";
                    table = new DataTable();
                    DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;
                case "Июль":
                    str = "SELECT * FROM July";
                    table = new DataTable();
                    DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;
                case "Август":
                    str = "SELECT * FROM August";
                    table = new DataTable();
                    DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;
                case "Сентябрь":
                    str = "SELECT * FROM September";
                    table = new DataTable();
                    DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;
                case "Октябрь":
                    str = "SELECT * FROM October";
                    table = new DataTable();
                    DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;
                case "Ноябрь":
                    str = "SELECT * FROM November";
                    table = new DataTable();
                    DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;
                case "Декабрь":
                    str = "SELECT * FROM December";
                    table = new DataTable();
                    DA = new SqlDataAdapter(str, sqlConnection);
                    DA.Fill(table);
                    dataGridView1.DataSource = table;
                    break;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Manager start = new Manager(login);
            start.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string str = "SELECT Tour_name, Order_date, TravelAgent_name FROM Tour, OrderTour, Customer, TravelAgent WHERE Tour.Tour_ID = OrderTour.Tour_ID AND Customer.Customer_ID = OrderTour.Customer_ID AND TravelAgent.TravelAgent_ID = Customer_travelAgent AND Order_date >= '" + dateTimePicker1.Value.ToString("dd.MM.yyyy") + "' AND Order_date <= '" + dateTimePicker2.Value.ToString("dd.MM.yyyy") + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "(SELECT 'Январь' AS Месяц, SUM(Tour_price) AS Сумма FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.01.2020' AND Order_date <= '31.01.2020') ";
            str += "UNION ALL ";
            str += "(SELECT 'Февраль', SUM(Tour_price) FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.02.2020' AND Order_date <= '29.02.2020') ";
            str += "UNION ALL ";
            str += "(SELECT 'Март', SUM(Tour_price) FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.03.2020' AND Order_date <= '31.03.2020') ";
            str += "UNION ALL "; str += "(SELECT 'Апрель', SUM(Tour_price) FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.04.2020' AND Order_date <= '30.04.2020') ";
            str += "UNION ALL "; str += "(SELECT 'Май', SUM(Tour_price) FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.05.2020' AND Order_date <= '31.05.2020') ";
            str += "UNION ALL "; str += "(SELECT 'Июнь', SUM(Tour_price) FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.06.2020' AND Order_date <= '30.06.2020') ";
            str += "UNION ALL "; str += "(SELECT 'Июль', SUM(Tour_price) FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.07.2020' AND Order_date <= '31.07.2020') ";
            str += "UNION ALL "; str += "(SELECT 'Август', SUM(Tour_price) FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.08.2020' AND Order_date <= '31.08.2020') ";
            str += "UNION ALL "; str += "(SELECT 'Сентябрь', SUM(Tour_price) FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.09.2020' AND Order_date <= '30.09.2020') ";
            str += "UNION ALL "; str += "(SELECT 'Октябрь', SUM(Tour_price) FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.10.2020' AND Order_date <= '31.10.2020') ";
            str += "UNION ALL "; str += "(SELECT 'Ноябрь', SUM(Tour_price) FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.11.2020' AND Order_date <= '30.11.2020') ";
            str += "UNION ALL "; str += "(SELECT 'Декабрь', SUM(Tour_price) FROM Tour INNER JOIN OrderTour ";
            str += "ON OrderTour.Tour_ID = Tour.Tour_ID AND Order_date >= '01.12.2020' AND Order_date <= '31.12.2020') ";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string str = "SELECT TravelAgent_name, AVG(Tour_price) AS Sales FROM TravelAgent INNER JOIN Customer ON ";
            str += "Customer_travelAgent = TravelAgent_ID INNER JOIN GroupList ON Customer.Customer_ID = GroupList.Customer_ID ";
            str += "INNER JOIN GroupTour ON GroupList.Group_ID = GroupTour.Group_ID ";
            str += "INNER JOIN Tour ON Group_tour = Tour_ID GROUP BY TravelAgent_name";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string str = "SELECT TOP 5 Tour_name FROM Tour INNER JOIN OrderTour ON OrderTour.Tour_ID = Tour.Tour_ID GROUP BY Tour_name ORDER BY COUNT(Order_status)";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string str = "SELECT Customer_name, TravelAgent_name FROM Customer INNER JOIN OrderTour ON OrderTour.Customer_ID = Customer.Customer_ID INNER JOIN TravelAgent ON Customer_travelAgent = TravelAgent_ID INNER JOIN Tour ON OrderTour.Tour_ID = Tour.Tour_ID AND Tour_name = '" + comboBox2.Text + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string str = "SELECT TravelAgent_name FROM Customer INNER JOIN TravelAgent ";
            str += "ON Customer_travelAgent = TravelAgent_ID ";
            str += "GROUP BY TravelAgent_name ";
            str += "HAVING COUNT(Customer_travelAgent) = ";
            str += "(SELECT TOP 1 COUNT(Customer_travelAgent) FROM Customer ";
            str += "GROUP BY Customer_travelAgent ";
            str += "ORDER BY COUNT(Customer_travelAgent))";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
