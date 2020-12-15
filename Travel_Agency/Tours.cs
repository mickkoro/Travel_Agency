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

    public partial class Tours : Form
    {
        SqlConnection sqlConnection;
        private string login;
        public Tours(string log)
        {
            InitializeComponent();
            this.login = log;
            string connection = @"Data Source=DESKTOP-V2FKAKC\SQL_EXPRESS;Initial Catalog=TravelAgency;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connection);
            SelectT();
            test("correctType");
            FillTour();
            FillGroup();
        }

        private void SelectT()
        {
            string str = "SELECT Tour_name, Tour_description, Tour_dateFrom, Tour_dateTo, Tour_price FROM Tour";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void FillTour()
        {
            string str = "SELECT Tour_name FROM Tour";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            comboBox1.Items.Clear();
            comboBox4.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox1.Items.Add(table.Rows[i].ItemArray[0].ToString());
                comboBox4.Items.Add(table.Rows[i].ItemArray[0].ToString());
            }
        }

        private void FillGroup()
        {
            string str = "SELECT Group_number FROM GroupTour";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            comboBox3.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
                comboBox3.Items.Add(table.Rows[i].ItemArray[0].ToString());
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
            SelectT();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "SELECT * FROM Popular_tours";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Manager start = new Manager(login);
            start.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox1.SelectedItem.ToString();
            string str = "SELECT Tour_name, Tour_description, Tour_dateFrom, Tour_dateTo, Tour_price FROM Tour WHERE Tour_name = '" + selectedState + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string res = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string str = "DELETE FROM Tour WHERE Tour_name = '" + res + "'";
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить тур " + res + "?", "Внимание!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                DataSet dataSet = new DataSet();
                DA.Fill(dataSet);
                SelectT();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите все данные!", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                string selectedState = comboBox2.Text;
                string result = textBox1.Text;
                string res = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                switch (selectedState)
                {
                    case "Название":
                        string into = "SELECT COUNT(Tour_name) FROM Tour WHERE Tour_name = '" + result + "'";
                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand(into, sqlConnection);
                        string name = cmd.ExecuteScalar().ToString();
                        sqlConnection.Close();
                        if (name == "0")
                        {
                            string st = "UPDATE Tour SET Tour_name = '" + result + "' WHERE Tour_name = '" + res + "'";
                            SqlDataAdapter DA1 = new SqlDataAdapter(st, sqlConnection);
                            DataSet dataSet1 = new DataSet();
                            DA1.Fill(dataSet1);
                            SelectT();
                        }
                        else
                        {
                            MessageBox.Show("Тур с таким названием уже существует", "Ошибка", MessageBoxButtons.OK);
                        }
                        break;
                    case "Описание":
                        string str = "UPDATE Tour SET Tour_description = '" + result + "' WHERE Tour_name = '" + res + "'";
                        SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                        DataSet dataSet = new DataSet();
                        DA.Fill(dataSet);
                        SelectT();
                        break;
                    case "Дата отправления":
                        str = "EXEC Change_dateFrom '" + result + "', '" + res + "'";
                        DA = new SqlDataAdapter(str, sqlConnection);
                        dataSet = new DataSet();
                        DA.Fill(dataSet);
                        SelectT();
                        break;
                    case "Дата прибытия":
                        str = "EXEC Change_dateTo '" + result + "', '" + res + "'";
                        DA = new SqlDataAdapter(str, sqlConnection);
                        dataSet = new DataSet();
                        DA.Fill(dataSet);
                        SelectT();
                        break;
                    case "Цена":
                        try
                        {
                            str = "UPDATE Tour SET Tour_price = " + Convert.ToInt32(result) + " WHERE Tour_name = '" + res + "'";
                            DA = new SqlDataAdapter(str, sqlConnection);
                            dataSet = new DataSet();
                            DA.Fill(dataSet);
                            SelectT();
                        }
                        catch
                        {
                            MessageBox.Show("Цена не может быть меньше либо равна нулю", "Ошибка", MessageBoxButtons.OK);
                        }
                        break;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SelectGr();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox4.SelectedItem.ToString();
            string str = "SELECT Group_Number FROM GroupTour INNER JOIN Tour ON Group_tour = Tour_ID AND Tour_name = '" + selectedState + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox3.SelectedItem.ToString();
            string str = "SELECT Tour_name FROM Tour INNER JOIN GroupTour ON Group_tour = Tour_ID AND Group_number = '" + selectedState + "'";
            DataTable table = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
            DA.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string res = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string str = "DELETE FROM GroupTour WHERE Group_number = " + res;
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить группу " + res + "?", "Внимание!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SqlDataAdapter DA = new SqlDataAdapter(str, sqlConnection);
                DataSet dataSet = new DataSet();
                DA.Fill(dataSet);
                SelectGr();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddTourOrGroup start = new AddTourOrGroup(login);
            start.Show();
            this.Close();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        public string[] test(string name)
        {
            if (name == "showTours")
            {
                SelectT();
                string[] res = new string[5];
                for(int i=0; i < dataGridView1.ColumnCount; i++)
                {
                    res[i] = dataGridView1.Columns[i].HeaderText;
                }
                return res;
            }
            else if (name == "showGroups")
            {
                SelectGr();
                string[] res = new string[2];
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    res[i] = dataGridView1.Columns[i].HeaderText;
                }
                return res;
            }
            else if (name == "correctType")
            {
                SelectT();
                string[] res = new string[1];
                res[0] = dataGridView1.Rows[0].Cells[4].Value.GetType().ToString();
                return res;
            }
            string[] error = { "ERROR" };
            return error;
        }

        public int countRows()
        {
            SelectT();
            int res = dataGridView1.RowCount;
            return res;
        }
    }
}
