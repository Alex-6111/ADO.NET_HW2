using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO.NET_HW2
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Warehouse;Integrated Security=True";
        private DataTable dt;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadFromDB("SELECT * FROM Products", dataGridView1);
            LoadFromDB("SELECT * FROM Types", dataGridView2);
            LoadFromDB("SELECT * FROM ProductProvider", dataGridView3);
        }

        private void LoadFromDB(string queryStr, DataGridView dataGridView)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = null;
                dt = new DataTable();
                SqlCommand cmd = new SqlCommand(queryStr, connection);
                try
                {
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    int line = 0;
                    while (reader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dt.Columns.Add(reader.GetName(i));
                            }
                        }
                        line++;
                        DataRow row = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        dt.Rows.Add(row);
                    }
                    dataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddType ad = new AddType(connectionString);
            ad.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddProductProvider app = new AddProductProvider(connectionString);
            app.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddProduct ap = new AddProduct(connectionString);
            ap.ShowDialog();
        }
    }
}
