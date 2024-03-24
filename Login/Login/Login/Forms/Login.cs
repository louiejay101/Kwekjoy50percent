using Login.Forms;
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

namespace Login
{
    public partial class Login : Form
    {
        string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=KwekjoyDB;Integrated Security=True";
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void regbtn_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            string username = usertxt.Text;
            string password = passwordtxt.Text;

            // Authenticate user
            string role = AuthenticateUser(username, password);

            if (role == "admin")
            {
                // Open Admin Form
                Admin1 adminForm = new Admin1();
                adminForm.Show();

                // Close the login form
                this.Hide();
            }
            else if (role == "customer")
            {
                // Open Customer Form
                Customer customerForm = new Customer();
                customerForm.Show();

                // Close the login form
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private string AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT role FROM customers WHERE username = @username AND password = @password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    string role = (string)command.ExecuteScalar();

                    return role;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return null;
                }
              
            }
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle password visibility based on checkbox state
            passwordtxt.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void passwordtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
    }
