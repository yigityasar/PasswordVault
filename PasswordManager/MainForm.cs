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
            this.Load += MainForm_Load;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex < 0)  return;

            var items = Storage.LoadPasswords();
            var selectedItem = items[listBox1.SelectedIndex];

            using(var editForm = new addForm())
            {
                editForm.EditingItem = selectedItem;

                if(editForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshPasswordList();
                }
            }



        }

        private void btnNewPassword_Click(object sender, EventArgs e)
        {
            using(var addForm = new addForm())
            {
                if(addForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshPasswordList();
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshPasswordList();
        }

        private void RefreshPasswordList()
        {
            listBox1.Items.Clear();

            var items = Storage.LoadPasswords();

            foreach (var item in items)
            {
                listBox1.Items.Add(item.Platform);
            }
        }

       
    }
}
