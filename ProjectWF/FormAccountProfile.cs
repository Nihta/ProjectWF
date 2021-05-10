using ProjectWF.Helpers;

using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace ProjectWF
{
    public partial class FormAccountProfile : Form
    {
        private int userId;

        public FormAccountProfile(int userId)
        {
            this.userId = userId;
            InitializeComponent();
        }

        private void GetUserData()
        {
            SqlDataReader reader = UsersHelpers.GetUserData(this.userId);
            if (reader.HasRows)
            {
                reader.Read();
                txtUserName.Text = reader["UserName"].ToString().Trim();
                txtFullName.Text = reader["FullName"].ToString().Trim();
            }
        }

        private void FormAccountProfile_Load(object sender, EventArgs e)
        {
            txtCfmNewPassWord.Enabled = false;
            GetUserData();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string fullName = txtFullName.Text;
            string oldPassWord = txtOldPassWord.Text;
            string newPassWord = txtNewPassWord.Text;
            string cfmNewPassWord = txtCfmNewPassWord.Text;

            // Validation họ và tên (tên hiển thị)
            if (!UsersHelpers.IsFullNameInvalid(fullName))
            {
                txtFullName.Focus();
                return;
            }

            // newPassWord.Length == 0 là trường hợp người dùng không muốn đổi mật khẩu
            if (newPassWord.Length != 0)
            {
                // Validation mật khẩu mới
                if (!UsersHelpers.IsPassWordInvalid(txtNewPassWord, "Mật khẩu mới"))
                {
                    return;
                }

                if (cfmNewPassWord.Length == 0)
                {
                    MyMessageBox.Warning("Bạn chưa nhập mật khẩu xác nhận!");
                    txtCfmNewPassWord.Focus();
                    return;
                }

                if (newPassWord != cfmNewPassWord)
                {
                    MyMessageBox.Warning("Mật khẩu xác nhận không khớp!");
                    txtCfmNewPassWord.Focus();
                    return;
                }
            }

            if (oldPassWord.Length == 0)
            {
                MyMessageBox.Warning("Mật khẩu là bắt buộc!");
                txtOldPassWord.Focus();
                return;
            }

            // Kiểm tra mật khẩu hiện tại
            if (UsersHelpers.Login(userName, oldPassWord) == -1)
            {
                MyMessageBox.Warning("Mật khẩu không chính xác!");
                txtOldPassWord.Focus();
                return;
            }

            // Cập nhật thông tin tài khoản
            // Trường hợp không muốn đổi mật khẩu
            string finalPass = newPassWord.Length == 0 ? oldPassWord : newPassWord;
            if (UsersHelpers.UpdateUser(this.userId, fullName, finalPass))
            {
                txtOldPassWord.Text = "";
                txtNewPassWord.Text = "";
                txtCfmNewPassWord.Text = "";
                MyMessageBox.Information("Cập nhật thông tin tài khoản thành công!");
            }
            else
            {
                MyMessageBox.Information("Cập nhật thông tin tài khoản thất bại!");
            }
        }

        private void txtNewPassWord_TextChanged(object sender, EventArgs e)
        {
            if (txtNewPassWord.Text.Length == 0)
            {
                txtCfmNewPassWord.Text = "";
                txtCfmNewPassWord.Enabled = false;
            }
            else
            {
                txtCfmNewPassWord.Enabled = true;
            }
        }
    }
}
