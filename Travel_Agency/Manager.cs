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
    public partial class Manager : Form
    {

        SqlConnection sqlConnection;
        private string login;

        public Manager(string log)
        {
            InitializeComponent();
            this.login = log;
            string connection = @"Data Source=DESKTOP-V2FKAKC\SQL_EXPRESS;Initial Catalog=TravelAgency;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConnection = new SqlConnection(connection);
        }

        private void add_account(object sender, EventArgs e)
        {
            AddAccount form = new AddAccount(login);
            form.Show();
            this.Close();
        }

        private void tours(object sender, EventArgs e)
        {
            Tours form = new Tours(login);
            form.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoginForm start = new LoginForm();
            start.Show();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sales start = new Sales(login);
            start.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Customers start = new Customers(login);
            start.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MyAccount start = new MyAccount(login);
            start.Show();
            this.Close();
        }

        private void Manager_Load(object sender, EventArgs e)
        {

        }
    }
}
