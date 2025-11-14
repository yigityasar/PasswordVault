using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class addForm : Form
    {
        PasswordService passwordService = new PasswordService();
        public addForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGenerate_MouseEnter(object sender, EventArgs e)
        {
            passwordLabel.Visible = true;
        }

        private void btnGenerate_MouseLeave(object sender, EventArgs e)
        {
            passwordLabel.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                mtxtPassword.UseSystemPasswordChar = !mtxtPassword.UseSystemPasswordChar;
        }

        private void addForm_Load(object sender, EventArgs e)
        {
            mtxtPassword.UseSystemPasswordChar = true;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            mtxtPassword.Text = passwordService.GenerateRandomPassword(20);
        }
    }
}
