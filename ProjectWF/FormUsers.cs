using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormUsers : Form
    {
        ControlHelper control;

        public FormUsers()
        {
            InitializeComponent();

            control = new ControlHelper();

            ConfigDataGridView();
        }

        #region Methods
        private void ConfigDataGridView()
        {
            dgvUser.AutoGenerateColumns = false;
            dgvUser.Columns.Add(MyUtils.CreateCol(30, "UserID", "ID"));
            dgvUser.Columns.Add(MyUtils.CreateCol(200, "FullName", "Học và tên"));
            dgvUser.Columns.Add(MyUtils.CreateCol(200, "UserName", "Tên đăng nhập"));
        }

        private void RenderDataGridView()
        {
            UserLinq.DataGridViewHelper(dgvUser);
        }

        private int GetCurrentItemID()
        {
            int curRowIdx = dgvUser.CurrentRow.Index;
            int id = Convert.ToInt32(dgvUser.Rows[curRowIdx].Cells["UserID"].Value.ToString());
            return id;
        }

        private void AddUser()
        {
            UserLinq.Add(txtFullName.Text, txtUserName.Text, txtPassWord.Text);
        }

        private void EditUser()
        {
            int userIdNeedEdit = GetCurrentItemID();
            UserLinq.Edit(userIdNeedEdit, txtFullName.Text, txtUserName.Text, txtPassWord.Text);
        }

        public bool IsInvalid()
        {
            if (!UsersHelpers.IsFullNameInvalid(txtFullName.Text))
            {
                txtFullName.Focus();
                return false;
            }

            if (!UsersHelpers.isUserNameInvalid(txtUserName.Text))
            {
                txtUserName.Focus();
                return false;
            }

            if (!UsersHelpers.IsPassWordInvalid(txtPassWord.Text))
            {
                txtPassWord.Focus();
                return false;
            }

            return true;
        }

        private void HandleRowEnter(int idx)
        {
            txtFullName.Text = dgvUser.Rows[idx].Cells["FullName"].Value.ToString().Trim();
            txtUserName.Text = dgvUser.Rows[idx].Cells["UserName"].Value.ToString().Trim();
        }
        #endregion

        #region Events
        private void FormUsers_Load(object sender, EventArgs e)
        {
            // Khởi tạo control helper
            control.AddBtnControls(btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            control.AddTextBoxs(txtFullName, txtUserName, txtPassWord);
            control.AddDataGridView(dgvUser);

            // Mode mặc định
            control.SwitchMode(ControlHelper.ControlMode.None);

            RenderDataGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            control.SwitchMode(ControlHelper.ControlMode.Add);
            control.ClearTextBox();
            txtFullName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            control.SwitchMode(ControlHelper.ControlMode.Edit);
            // Không cho phép sửa tên đăng nhập
            txtUserName.Enabled = false;
            txtFullName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUser.CurrentRow != null)
            {
                if (MyMessageBox.Question("Bạn có chắn xóa tài khoản đã chọn không?"))
                {
                    int userIdNeedDel = GetCurrentItemID();
                    UserLinq.Delete(userIdNeedDel);
                    RenderDataGridView();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsInvalid())
            {
                // Cập nhật data
                switch (control.GetMode())
                {
                    case ControlHelper.ControlMode.Add:
                        {
                            if (UserLinq.CheckUserNameExist(txtUserName.Text))
                            {
                                MyMessageBox.Warning("Tên đăng nhập đã được sử dụng!\nVui lòng chọn một tên tài khoản khác.");
                                txtUserName.Focus();
                                return;
                            }
                            AddUser();
                        }
                        break;
                    case ControlHelper.ControlMode.Edit:
                        {
                            EditUser();
                        }
                        break;
                }
                // Sau khi lưu
                control.SwitchMode(ControlHelper.ControlMode.None);
                RenderDataGridView();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            control.SwitchMode(ControlHelper.ControlMode.None);
        }

        private void dgvUser_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            HandleRowEnter(idx);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
