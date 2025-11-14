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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnNewPassword_Click(object sender, EventArgs e)
        {
            using(var addForm = new addForm())
            {
                if(addForm.ShowDialog() == DialogResult.OK)
                {
                    // Yeni şifre eklendiğinde yapılacak işlemler
                }
            }
        }
    }
}
