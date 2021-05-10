using System;
using System.Data;
using System.Windows.Forms;
using ProjectWF.Helpers;

namespace ProjectWF
{
    public partial class FormUsers : Form
    {
        ControlHelper control;
        SqlHelper sqlHelper = new SqlHelper();
        DataTable dataTableUser;

        public FormUsers()
        {
            InitializeComponent();
        }

        #region Methods
        private void GetUsers()
        {
            string query = "SELECT * FROM TableUsers";

            dataTableUser = sqlHelper.ExecuteQuery(SqlHelper.defaultConnStr, query, CommandType.Text);

            dgvUser.DataSource = dataTableUser;
        }

        private void AddUser()
        {
            DataRow newRow = dataTableUser.NewRow();

            newRow["FullName"] = txtFullName.Text;
            newRow["UserName"] = txtUserName.Text;
            newRow["PassWord"] = MyUtils.MD5Hash(txtPassWord.Text);

            dataTableUser.Rows.Add(newRow);
            sqlHelper.Update(dataTableUser);
            GetUsers();
        }

        private void EditUser()
        {
            int curRowIdx = dgvUser.CurrentRow.Index;

            DataRow editRow = dataTableUser.Rows[curRowIdx];

            editRow["FullName"] = txtFullName.Text;
            editRow["UserName"] = txtUserName.Text;
            editRow["PassWord"] = MyUtils.MD5Hash(txtPassWord.Text);

            sqlHelper.Update(dataTableUser);
        }
        #endregion

        #region Events
        private void FormUsers_Load(object sender, EventArgs e)
        {
            // Khởi tạo control helper
            control = new ControlHelper();
            control.AddBtnControls(btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            control.AddTextBoxs(txtFullName, txtUserName, txtPassWord);
            // Mode mặc định
            control.SwitchMode(ControlHelper.ControlMode.None);
            // Lấy thông tin các user và hiển thị lên dataGridView
            dgvUser.AutoGenerateColumns = false;
            GetUsers();
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
            if (MyMessageBox.Question("Bạn có chắn xóa bản ghi đã chọn không?"))
            {
                int curRowIdx = dgvUser.CurrentRow.Index;
                dataTableUser.Rows[curRowIdx].Delete();
                sqlHelper.Update(dataTableUser);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (!UsersHelpers.IsFullNameInvalid(txtFullName.Text))
            {
                txtFullName.Focus();
            }
            else if (!UsersHelpers.isUserNameInvalid(txtUserName.Text))
            {
                txtUserName.Focus();
            }
            else if (!UsersHelpers.IsPassWordInvalid(txtPassWord.Text))
            {
                txtPassWord.Focus();
            }
            else
            {
                // Cập nhật data
                switch (control.GetMode())
                {
                    case ControlHelper.ControlMode.Add:
                        {
                            if (UsersHelpers.CheckUserNameExist(txtUserName.Text))
                            {
                                MyMessageBox.Warning("Tên đăng nhập đã được sử dụng!");
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
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            control.SwitchMode(ControlHelper.ControlMode.None);
            // this.ActiveControl = null;
        }

        private void dgvUser_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            txtFullName.Text = dgvUser.Rows[idx].Cells["FullName"].Value.ToString().Trim();
            txtUserName.Text = dgvUser.Rows[idx].Cells["UserName"].Value.ToString().Trim();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
