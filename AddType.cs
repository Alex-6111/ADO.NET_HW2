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

namespace ADO.NET_HW2
{
    public partial class AddType : Form
    {
        private string connString;

        public AddType(string connStr)
        {
            InitializeComponent();
            connString = connStr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                string queryStr = $"Insert into Types VALUES(N'{textBox1.Text}')";
                SqlCommand command = new SqlCommand(queryStr, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void AddType_Load(object sender, EventArgs e)
        {

        }
    }
}
