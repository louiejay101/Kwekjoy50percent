using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Login.Forms
{
    public partial class Admin1 : Form
    {
        string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=KwekjoyDB;Integrated Security=True";

        public Admin1()
        {
            InitializeComponent();
        }

        private void Admin1_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM customers";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    viewgrid.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void viewgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = viewgrid.Rows[e.RowIndex];

                // Fill text boxes with data from the selected row
                customertxt.Text = row.Cells["customer_id"].Value.ToString();
                nametxt.Text = row.Cells["name"].Value.ToString();
                usertxt.Text = row.Cells["username"].Value.ToString();
                passtxt.Text = row.Cells["password"].Value.ToString();
                emailtxt.Text = row.Cells["email"].Value.ToString();
                roletxt.Text = row.Cells["role"].Value.ToString();
            }
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (viewgrid.SelectedRows.Count > 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        DataGridViewRow selectedRow = viewgrid.SelectedRows[0];
                        int customerId = Convert.ToInt32(selectedRow.Cells["customer_id"].Value);

                        string query = "UPDATE customers SET name = @name, username = @username, password = @password, email = @email, role = @role WHERE customer_id = @customerId";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@name", nametxt.Text);
                        command.Parameters.AddWithValue("@username", usertxt.Text);
                        command.Parameters.AddWithValue("@password", passtxt.Text);
                        command.Parameters.AddWithValue("@email", emailtxt.Text);
                        command.Parameters.AddWithValue("@role", roletxt.Text);
                        command.Parameters.AddWithValue("@customerId", customerId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully.");
                            PopulateDataGridView(); // Refresh DataGridView after update
                        }
                        else
                        {
                            MessageBox.Show("No records updated.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (viewgrid.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            DataGridViewRow selectedRow = viewgrid.SelectedRows[0];
                            int customerId = Convert.ToInt32(selectedRow.Cells["customer_id"].Value);

                            string query = "DELETE FROM customers WHERE customer_id = @customerId";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@customerId", customerId);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record deleted successfully.");
                                PopulateDataGridView(); // Refresh DataGridView after delete
                            }
                            else
                            {
                                MessageBox.Show("No records deleted.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }
        private void viewgrid_SelectionChanged(object sender, EventArgs e)
        {
            if (viewgrid.SelectedRows.Count > 0)
            {
                DataGridViewRow row = viewgrid.SelectedRows[0];

                // Fill text boxes with data from the selected row
                customertxt.Text = row.Cells["customer_id"].Value.ToString();
                nametxt.Text = row.Cells["name"].Value.ToString();
                usertxt.Text = row.Cells["username"].Value.ToString();
                passtxt.Text = row.Cells["password"].Value.ToString();
                emailtxt.Text = row.Cells["email"].Value.ToString();
                roletxt.Text = row.Cells["role"].Value.ToString();
            }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            Adminadd admin = new Adminadd();
            admin.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin2 admin2 = new Admin2();
            admin2.Show();
            this.Hide();
        }
    }
}
