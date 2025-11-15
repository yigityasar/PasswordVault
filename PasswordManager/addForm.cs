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
        public PasswordItem EditingItem { get; set; }
        public bool IsEditMode => EditingItem != null;
        PasswordService passwordService = new PasswordService();
        public addForm()
        {
            InitializeComponent();
            this.Load += addForm_Load;
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
            if (IsEditMode)
            {
                txtPlatform.Text = EditingItem.Platform;
                txtUsername.Text = EditingItem.Username;
                mtxtPassword.Text = CryptoHelper.Decrypt(EditingItem.Password);
                rtxtNotes.Text = EditingItem.Notes;

                btnSave.Text = "Update";
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string platform = txtPlatform.Text;
                string username = txtUsername.Text;
                string password = mtxtPassword.Text;
                string notes = rtxtNotes.Text;

                string encryptedPassword = CryptoHelper.Encrypt(password);
                var items = Storage.LoadPasswords();

                if (IsEditMode)
                {
                    int index = items.FindIndex(x =>
                    x.Platform == EditingItem.Platform &&
                    x.Username == EditingItem.Username &&
                    x.Password == EditingItem.Password
                );

                if (index >= 0)
                {
                    items[index].Platform = platform;
                    items[index].Username = username;
                    items[index].Password = encryptedPassword;
                    items[index].Notes = notes;
                }
            }

                else
                {
                    PasswordItem newItem = new PasswordItem
                    {
                        Platform = platform,
                        Username = username,
                        Password = encryptedPassword,
                        Notes = notes
                    };
                    items.Add(newItem);
                }

                Storage.Save(items);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving the password: " + ex.Message);
            }
        }

        private void btnGenerate_Click_1(object sender, EventArgs e)
        {
            mtxtPassword.Text = passwordService.GenerateRandomPassword(20);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
