using ProjectWF.Parameter;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormLogin : Form
    {
        private DataProvider dataProvider;


        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            try
            {
                dataProvider = new DataProvider();
            }
            catch (SqlException ex)
            {
                MyMessageBox.Error(ex.Message);
                Application.Exit();
            }
        }

        private void HandleLogin(string userName, string passWord)
        {
            if (UsersHelpers.Login(userName, passWord))
            {
                FormMain formMain = new FormMain();
                this.Hide();
                formMain.ShowDialog();
                this.Show();
            }
            else
            {
                MyMessageBox.Warning("Tên đăng nhập hoặc mật khẩu không chính xác!");
            }
        }

        private bool isUserNameValid(string userName)
        {
            if (MyValidation.IsEmpty(userName))
            {
                MyMessageBox.Warning("Tên đăng nhập không được để trống!");
                return false;
            }
            if (userName.Length < 5)
            {
                MyMessageBox.Warning("Tên đăng nhập quá ngắn!");
                return false;
            }
            if (userName.Length > 30)
            {
                MyMessageBox.Warning("Tên đăng nhập quá dài!");
                return false;
            }
            if (!Regex.IsMatch(userName, @"^[a-z][a-z0-9]*$"))
            {
                MyMessageBox.Warning("Tên đăng nhập không hợp lệ!");
                return false;
            }
            return true;
        }

        private bool isPassWordValid(string passWord)
        {
            if (MyValidation.IsEmpty(passWord))
            {
                MyMessageBox.Warning("Mật khẩu không được để trống!");
                return false;
            }
            if (passWord.Length < 3)
            {
                MyMessageBox.Warning("Mật khẩu quá ngắn!");
                return false;
            }
            if (passWord.Length > 30)
            {
                MyMessageBox.Warning("Mật khẩu quá dài!");
                return false;
            }
            if (!Regex.IsMatch(passWord, @"^[a-z0-9]*$"))
            {
                MyMessageBox.Warning("Mật khẩu không hợp lệ!");
                return false;
            }
            return true;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string userName = textBoxUserName.Text;
            string passWord = textBoxPassWord.Text;

            if (!isUserNameValid(userName))
            {
                textBoxUserName.Focus();
            }
            else if (!isPassWordValid(passWord))
            {
                textBoxPassWord.Focus();
            }
            else
            {
                HandleLogin(userName, passWord);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
    }
}
