using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.Forms
{
    public partial class Adminadd : Form
    {
        string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=KwekjoyDB;Integrated Security=True";
        public Adminadd()
        {
            InitializeComponent();
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private bool IsValidEmail(string email)
        {
            // Use regular expression for email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
        private void registerbtn_Click(object sender, EventArgs e)
        {
            if (passtxt.Text != confirmtxt.Text)
            {
                MessageBox.Show("Password and confirm password do not match. Please try again.");
                return;
            }


            if (!IsValidEmail(emailtxt.Text))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email address.");
                return;
            }


            string hashedPassword = HashPassword(passtxt.Text);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM customers WHERE username = @username";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@username", usertxt.Text);

                    int existingUserCount = (int)checkCommand.ExecuteScalar();
                    if (existingUserCount > 0)
                    {
                        MessageBox.Show("Username already exists. Please choose a different username.");
                        return;
                    }


                    string checkEmailQuery = "SELECT COUNT(*) FROM customers WHERE email = @email";
                    SqlCommand checkEmailCommand = new SqlCommand(checkEmailQuery, connection);
                    checkEmailCommand.Parameters.AddWithValue("@email", emailtxt.Text);
                    int existingEmailCount = (int)checkEmailCommand.ExecuteScalar();
                    if (existingEmailCount > 0)
                    {
                        MessageBox.Show("Email already exists. Please use a different email address.");
                        return;
                    }


                    string query = "INSERT INTO customers (name, username, password, email, role) VALUES (@name, @username, @password, @email, @role)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@name", nametxt.Text);
                    command.Parameters.AddWithValue("@username", usertxt.Text);
                    command.Parameters.AddWithValue("@password", hashedPassword);
                    command.Parameters.AddWithValue("@email", emailtxt.Text);
                    command.Parameters.AddWithValue("@role", roletxt.Text);

                    command.ExecuteNonQuery();

                    MessageBox.Show("User added successfully.");
                    // Close the current form
                    this.Close();

                    // Open the login form
                    Admin1 admin1 = new Admin1();
                    admin1.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            Admin1 admin = new Admin1();
            admin.Show();
            this.Hide();
        }
    }
}
