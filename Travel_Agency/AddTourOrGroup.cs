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
    public partial class AddTourOrGroup : Form
    {
        SqlConnection sqlConnection;
        private string login;
        public AddTourOrGroup(string log)
        {
            InitializeComponent();
            this.login = log;
            string connection = @"Data Source=DESKTOP-V2FKAKC\SQL_EXPRESS;Initial Catalog=TravelAgency;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connection);
            SelectT();
            FillTour();
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

        private void SelectT()
        {
            string str = "SELECT Tour_name, Tour_description, Tour_dateFrom, Tour_dateTo, Tour_price FROM Tour";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void SelectGr()
        {
            string str = "SELECT Tour_name, Group_number FROM Tour INNER JOIN GroupTour ON Group_tour = Tour_ID";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tours start = new Tours(login);
            start.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите все данные", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                string into = "SELECT COUNT(Tour_name) FROM Tour WHERE Tour_name = '" + textBox1.Text + "'";
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(into, sqlConnection);
                string name = cmd.ExecuteScalar().ToString();
                sqlConnection.Close();
                if (name != "0")
                {
                    MessageBox.Show("Тур с таким названием уже существует!", "Ошибка", MessageBoxButtons.OK);
                }
                else if (Convert.ToInt32(textBox3.Text) <= 0)
                {
                    MessageBox.Show("Цена не может быть меньше либо равна 0!", "Ошибка", MessageBoxButtons.OK);
                }
                else
                    try
                    {

                        string str = "INSERT INTO Tour ";
                        str += "VALUES('" + this.textBox1.Text + "', '" + this.textBox2.Text + "', '" + dateTimePicker1.Value.ToString("dd.MM.yyyy") + "', '" + dateTimePicker2.Value.ToString("dd.MM.yyyy") + "', " + this.textBox3.Text + ")";
                        SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                        DataSet dataSet = new DataSet();
                        DA.Fill(dataSet);
                        SelectT();
                    }
                    catch
                    {
                        MessageBox.Show("Дата начала не может быть позже даты окончания!", "Ошибка", MessageBoxButtons.OK);
                    }
 
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Пожалуйста, введите все данные", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    string str = "DECLARE @num_of_tour AS int ";
                    str += "SET @num_of_tour = (SELECT Tour_ID FROM Tour WHERE Tour_name = '" + comboBox1.Text + "') ";
                    str += "INSERT INTO GroupTour ";
                    str += "VALUES(" + textBox5.Text + ", @num_of_tour)";
                    SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                    DataSet dataSet = new DataSet();
                    DA.Fill(dataSet);
                    SelectGr();
                }
            }
            catch
            {
                MessageBox.Show("Группа с таким номером уже существует.\nПопробуйте другой номер группы.", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        public void addTour()
        {
            string str = "INSERT INTO Tour VALUES('Золотое кольцо России', 'Это интересно!', '07.11.2021', '14.11.2021', 34000)";
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DataSet dataSet = new DataSet();
            DA.Fill(dataSet);
        }

        public void deleteTour()
        {
            string str = "DELETE FROM Tour WHERE Tour_name = 'Золотое кольцо России'";
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DataSet dataSet = new DataSet();
            DA.Fill(dataSet);
        }
    }
}
