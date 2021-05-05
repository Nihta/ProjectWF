using System;
using System.Data;
using System.Data.SqlClient;
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
            string queryString = $"EXEC dbo.Login @userName = '{userName}', @passWord = '{MyUtils.MD5Hash(passWord)}'";

            DataTable dataTable = dataProvider.ExecuteQuery(queryString);

            if (dataTable.Rows.Count == 0)
            {
                MyMessageBox.Warning("Tên đăng nhập hoặc mật khẩu không chính xác!");
            }
            else
            {
                MyMessageBox.Information("Đăng nhập thành công!");
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string userName = textBoxUserName.Text.Trim();
            string passWord = textBoxPassWord.Text;

            if (userName.Length == 0)
            {
                textBoxUserName.Focus();
                MyMessageBox.Warning("Tên đăng nhập không được để trống!");
            }
            else if (passWord.Length == 0)
            {
                textBoxPassWord.Focus();
                MyMessageBox.Warning("Mật khẩu không được bỏ trống!");
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
