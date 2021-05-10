using System;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormUsers : Form
    {

        ControlHelper control;

        public FormUsers()
        {
            InitializeComponent();
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            // Khởi tạo control helper
            control = new ControlHelper();
            control.AddBtnControls(btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            control.AddTextBoxs(txtFullName, txtUserName, txtPassWord);

            control.SwitchMode(ControlHelper.ControlMode.None);
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            control.SwitchMode(ControlHelper.ControlMode.Add);
            txtFullName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            control.SwitchMode(ControlHelper.ControlMode.Edit);
            txtFullName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            control.SwitchMode(ControlHelper.ControlMode.None);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            control.SwitchMode(ControlHelper.ControlMode.None);
            //this.ActiveControl = null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
