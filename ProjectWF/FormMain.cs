using System;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormMain : Form
    {
        private int curUserId;

        public FormMain()
        {
            InitializeComponent();
        }

        public FormMain(int userId)
        {
            InitializeComponent();
            this.curUserId = userId;
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAccountProfile f = new FormAccountProfile(curUserId);
            f.ShowDialog();
        }

        private void tàiKhoảnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormUsers f = new FormUsers();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSuppliers f = new FormSuppliers();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCategory f = new FormCategory();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCustomer f = new FormCustomer();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProducts f = new FormProducts();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}
