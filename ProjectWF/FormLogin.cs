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

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // ! Để cho quá trình __DEV__ nhanh hơn
            textBoxUserName.Text = "nihta";
            textBoxPassWord.Text = "123";
        }

        private void HandleLogin(string userName, string passWord)
        {
            int curUserID = UsersHelpers.Login(userName, passWord);
            if (curUserID == -1)
            {
                MyMessageBox.Warning("Tên đăng nhập hoặc mật khẩu không chính xác!");
            }
            else
            {
                FormMain formMain = new FormMain(curUserID);
                this.Hide();
                textBoxPassWord.Text = "";
                formMain.ShowDialog();
                this.Show();
                textBoxPassWord.Focus();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string userName = textBoxUserName.Text;
            string passWord = textBoxPassWord.Text;

            if (!UsersHelpers.isUserNameInvalid(userName))
            {
                textBoxUserName.Focus();
            }
            else if (!UsersHelpers.IsPassWordInvalid(passWord))
            {
                textBoxPassWord.Focus();
            }
            else
            {
                HandleLogin(userName, passWord);
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
    }
}
