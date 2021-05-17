using System;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        #region Methods
        private void HandleLogin(string userName, string passWord)
        {
            int curUserID = UserLinq.Login(userName, passWord);
            if (curUserID == -1)
            {
                MyMessageBox.Warning("Tên đăng nhập hoặc mật khẩu không chính xác!");
            }
            else
            {
                FormMain formMain = new FormMain(curUserID);
                this.Hide();
                // Xoá mật khẩu
                textBoxPassWord.Clear();
                formMain.ShowDialog();
                this.Show();
                textBoxPassWord.Focus();
            }
        }

        public bool IsInputInvalid()
        {
            string userName = textBoxUserName.Text;
            string passWord = textBoxPassWord.Text;

            if (!UsersHelpers.isUserNameInvalid(userName))
            {
                textBoxUserName.Focus();
                return false;
            }

            if (!UsersHelpers.IsPassWordInvalid(passWord))
            {
                textBoxPassWord.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Events
        private void FormLogin_Load(object sender, EventArgs e)
        {
            // ! Để cho quá trình __DEV__ nhanh hơn
            textBoxUserName.Text = "nihta";
            textBoxPassWord.Text = "123";
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (IsInputInvalid())
            {
                HandleLogin(textBoxUserName.Text, textBoxPassWord.Text);
            }
        }

        private void textBoxUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                textBoxPassWord.Focus();
            }
        }

        private void textBoxPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBoxUserName.Focus();
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                buttonLogin.Focus();
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
