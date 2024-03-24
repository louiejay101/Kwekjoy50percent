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

namespace Login.Forms
{
    public partial class Admin2 : Form
    {
        string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=KwekjoyDB;Integrated Security=True";
        string selectedImagePath = "";
        public Admin2()
        {
            InitializeComponent();
        }

        private void Admin2_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT product_id AS ProductID, product_name AS ProductName, price AS Price, image_path AS ImagePath FROM products";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    // Set DataGridView column DataPropertyName
                    dataGridView1.Columns["ProductID"].DataPropertyName = "ProductID";
                    dataGridView1.Columns["ProductName"].DataPropertyName = "ProductName";
                    dataGridView1.Columns["Price"].DataPropertyName = "Price";
                    dataGridView1.Columns["ImagePath"].DataPropertyName = "ImagePath";

                    // Display images in DataGridView
                    DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                    imageColumn.HeaderText = "Image";
                    imageColumn.DataPropertyName = "ImagePath";
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Adjust image layout
                    dataGridView1.Columns.Add(imageColumn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                productidtxt.Text = row.Cells["ProductID"].Value.ToString();
                productnametxt.Text = row.Cells["ProductName"].Value.ToString();
                pricetxt.Text = row.Cells["Price"].Value.ToString();
            }
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Retrieve the image path from the selected row
                string imagePath = row.Cells["ImagePath"].Value.ToString();

                // Load the image into the PictureBox
                if (!string.IsNullOrEmpty(imagePath))
                {
                    try
                    {
                        pictureBox1.Image = Image.FromFile(imagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading image: " + ex.Message);
                    }
                }
                else
                {
                    pictureBox1.Image = null; // Clear PictureBox if no image path
                }
            }
        }

        private void productidtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void productnametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void pricetxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            string productName = productnametxt.Text;
            string price = pricetxt.Text;

            if (!string.IsNullOrEmpty(productName) && !string.IsNullOrEmpty(price))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO products (product_name, price) VALUES (@productName, @price)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@productName", productName);
                    command.Parameters.AddWithValue("@price", price);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("Product added successfully!");
                        LoadData(); // Refresh DataGridView after adding product
                        ClearTextBoxes(); // Clear textboxes after adding product
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter product name and price.");
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE products SET product_name = @productName, price = @price WHERE product_id = @productId"; // Adjusted query to match column names
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@productName", productnametxt.Text);
                command.Parameters.AddWithValue("@price", pricetxt.Text);
                command.Parameters.AddWithValue("@productId", productidtxt.Text);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show("Updated successfully!");
                    LoadData(); // Reload data after update
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(productidtxt.Text))
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM products WHERE product_id = @productId"; // Adjusted query to match column name
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@productId", productidtxt.Text);

                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            MessageBox.Show("Deleted successfully!");
                            LoadData(); // Reload data after delete
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
                MessageBox.Show("Please select a record to delete.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }
        private void ClearTextBoxes()
        {
            productidtxt.Text = "";
            productnametxt.Text = "";
            pricetxt.Text = "";
        }

        private void browsebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = openFileDialog.FileName;

               
                try
                {
                    pictureBox1.Image = Image.FromFile(selectedImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
        }
    }
    }


