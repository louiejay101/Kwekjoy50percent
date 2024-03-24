using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Login
{
    public partial class Register : Form
    {
        string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=KwekjoyDB;Integrated Security=True";

        public Register()
        {
            InitializeComponent();
        }

        // Method to hash the password
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
        private void button1_Click(object sender, EventArgs e)
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
                    Login loginForm = new Login();
                    loginForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void emailtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            passtxt.UseSystemPasswordChar = !checkBox1.Checked;
            confirmtxt.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void roletxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void confirmtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void passtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void usertxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void nametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
