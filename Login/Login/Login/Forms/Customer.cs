using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.Forms
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        
        }


        private void Customer_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public void DisplayProductImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
                pictureBox1.Image = Image.FromFile(imagePath);
            else
                pictureBox1.Image = null; // Clear the PictureBox if no image is available
        }
    }
}
