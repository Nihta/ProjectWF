
using ProjectWF.Parameter;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace ProjectWF
{
    public partial class FormAccountProfile : Form
    {
        public FormAccountProfile()
        {
            InitializeComponent();
        }

        private void FormAccountProfile_Load(object sender, EventArgs e)
        {
            // ID của user đang đăng nhập
            int curUserID = 1;

            SqlDataReader reader = UsersHelpers.getUserData(curUserID);
            if (reader.HasRows)
            {
                reader.Read();
                txtUserName.Text = reader["UserName"].ToString();
                txtFullName.Text = reader["FullName"].ToString();
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
