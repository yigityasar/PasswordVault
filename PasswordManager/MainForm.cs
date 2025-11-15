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
        private ListViewItem _hoveredItem = null;

        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
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
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.BorderStyle = BorderStyle.None;
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            listView1.BackColor = Color.FromArgb(45, 45, 45); // #2D2D2D
            listView1.ForeColor = Color.White;
            listView1.Font = new Font("Segoe UI", 11);

            foreach (ColumnHeader col in listView1.Columns)
                col.TextAlign = HorizontalAlignment.Center;

            listView1.OwnerDraw = true;
            listView1.DrawItem += listView1_DrawItem;
            listView1.DrawSubItem += listView1_DrawSubItem;
            listView1.DrawColumnHeader += listView1_DrawColumnHeader;

            // Hover efekti
            listView1.MouseMove += listView1_MouseMove;
            listView1.MouseLeave += listView1_MouseLeave;

            listView1.Columns[0].Width = 170; // Platform
            listView1.Columns[1].Width = 170;
        }

        private void RefreshPasswordList()
        {
            listView1.Items.Clear();

            var items = Storage.LoadPasswords();

            foreach (var item in items)
            {
                var lvi = new ListViewItem(new[] { item.Platform, item.Username });
                listView1.Items.Add(lvi);
            }
        }

        private void ctxDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;

            var result = MessageBox.Show("Are you sure that you want to delete this save?",
                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            var items = Storage.LoadPasswords();
            int index = listView1.SelectedIndices[0];

            if (index < 0 || index >= items.Count) return;

            items.RemoveAt(index);
            Storage.Save(items);

            RefreshPasswordList();
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = listView1.HitTest(e.Location);
                if (hit.Item != null)
                {
                    listView1.FocusedItem = hit.Item;
                    hit.Item.Selected = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) return;

            var items = Storage.LoadPasswords();
            int index = listView1.SelectedIndices[0];

            if (index < 0 || index >= items.Count) return;

            var selectedItem = items[index];
            using (var editForm = new addForm())
            {
                editForm.EditingItem = selectedItem;
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshPasswordList();
                }
            }
        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Color bgColor = (e.Item == _hoveredItem) ? Color.FromArgb(70, 70, 70) :
                    (e.ItemIndex % 2 == 0 ? Color.FromArgb(45, 45, 45) : Color.FromArgb(40, 40, 40));

            if (e.Item.Selected)
                bgColor = Color.FromArgb(60, 150, 250);

            using (SolidBrush bgBrush = new SolidBrush(bgColor))
            {
                e.Graphics.FillRectangle(bgBrush, e.Bounds);
            }

            e.DrawFocusRectangle();
        }

        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
            TextRenderer.DrawText(e.Graphics, e.SubItem.Text, listView1.Font, e.Bounds, Color.White, flags);
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            var item = listView1.GetItemAt(e.X, e.Y);
            if (_hoveredItem != item)
            {
                _hoveredItem = item;
                listView1.Invalidate();
            }
        }

        private void listView1_MouseLeave(object sender, EventArgs e)
        {
            _hoveredItem = null;
            listView1.Invalidate();
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/yigityasar/",
                UseShellExecute = true
            });
        }
    }
}
