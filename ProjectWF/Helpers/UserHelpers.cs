using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectWF
{
    class UsersHelpers
    {
        public static readonly int sizeFullNameParam = 30;
        public static readonly int sizeUserNameParam = 30;
        public static readonly int sizePassWordParam = 32;

        #region SqlParameter
        public static SqlParameter UserIDParam(int value, string paramName = "UserID")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.Int);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter FullNameParam(string value, string paramName = "FullName")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.NVarChar, sizeFullNameParam);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter UserNameParam(string value, string paramName = "UserName")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.NChar, sizeUserNameParam);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter PassWordParam(string value, string paramName = "PassWord")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.NChar, sizePassWordParam);
            parameter.Value = value;
            return parameter;
        }
        #endregion


        #region Validation
        /// <summary>
        /// Validation mật khẩu
        /// </summary>
        /// <param name="passWord"></param>
        /// <param name="name"></param>
        /// <returns>
        /// Return true nếu hợp lệ
        /// </returns>
        public static bool IsPassWordInvalid(string passWord, string name = "Mật khẩu")
        {
            if (!NewValidation.IsTextInvalid(passWord, 3, sizePassWordParam, name))
            {
                return false;
            }

            if (!Regex.IsMatch(passWord, @"^[a-z0-9]*$"))
            {
                MyMessageBox.Warning($"{name} không hợp lệ!\nMật khẩu chỉ bao gồm các kí tự a->z, 0->9");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validation mật khẩu
        /// </summary>
        /// <param name="txtPassWord"></param>
        /// <param name="name"></param>
        /// <returns>
        /// Return true nếu hợp lệ
        /// </returns>
        public static bool IsPassWordInvalid(TextBox txtPassWord, string name = "Mật khẩu")
        {
            if (IsPassWordInvalid(txtPassWord.Text, name))
            {
                return true;
            }
            else
            {
                txtPassWord.Focus();
                return false;
            }

        }

        /// <summary>
        /// Validation họ và tên
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns>
        /// Return true nếu hợp lệ
        /// </returns>
        public static bool IsFullNameInvalid(string fullName)
        {
            if (!NewValidation.IsTextInvalid(fullName, 5, sizeFullNameParam, "Họ và tên"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validation tên đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>
        /// Return true nếu hợp lệ
        /// </returns>
        public static bool isUserNameInvalid(string userName)
        {
            if (!(NewValidation.IsTextInvalid(userName, 5, sizeUserNameParam, "Tên đăng nhập")))
            {
                return false;
            }

            if (!Regex.IsMatch(userName, @"^[a-z][a-z0-9]*$"))
            {
                MyMessageBox.Warning("Tên đăng nhập không hợp lệ!\nTên đăng nhập chỉ bao gồm các kí tự a->z, 0->9");
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="userName">Tên tài khoản</param>
        /// <param name="passWord">Mật khẩu</param>
        /// <returns>
        /// Trả về UserID nếu thành công, ngược lại trả về -1 
        /// </returns>
        public static int Login(string userName, string passWord)
        {
            string cmd = "dbo.Login";

            SqlDataReader reader = SqlHelper.ExecuteReader(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.StoredProcedure,
                UserNameParam(userName), PassWordParam(MyUtils.MD5Hash(passWord))
                );

            if (reader.HasRows)
            {
                reader.Read();
                return Convert.ToInt32(reader["UserID"].ToString());
            }

            return -1;
        }

        /// <summary>
        /// Lấy dữ liệu người dùng bằng id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static SqlDataReader GetUserData(int userId)
        {
            string query = "select UserName, FullName from TableUsers where UserID = @UserID";

            SqlDataReader reader = SqlHelper.ExecuteReader(
                SqlHelper.defaultConnStr,
                query,
                CommandType.Text,
                UserIDParam(userId)
                );

            return reader;
        }

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="fullName"></param>
        /// <param name="passWord"></param>
        /// <returns>
        /// Return true nếu thành công
        /// </returns>
        public static bool UpdateUser(int userID, string fullName, string passWord)
        {
            string cmd = "dbo.UpdateUser";

            int res = SqlHelper.ExecuteNonQuery(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.StoredProcedure,
                UserIDParam(userID),
                FullNameParam(fullName),
                PassWordParam(MyUtils.MD5Hash(passWord))
                );

            return res == 1;
        }

        /// <summary>
        /// Kiểm tra tên đăng nhập đã được sử dụng hay chưa
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>
        /// Return true nếu đã được sử dụng
        /// </returns>
        public static bool CheckUserNameExist(string userName, int ignoreUserId = -1)
        {
            // Trong cơ sở dữ liệu luôn UserID >= 1 
            string query = "SELECT * FROM TableUsers WHERE UserName = @UserName AND UserID != @UserID";

            SqlDataReader reader = SqlHelper.ExecuteReader(
                SqlHelper.defaultConnStr,
                query,
                CommandType.Text,
                UserNameParam(userName, "UserName"),
                UserIDParam(ignoreUserId, "UserID")
                );

            return reader.HasRows;
        }

        public static DataTable DataGridViewHelper(SqlHelper sql, DataGridView dataGridView)
        {
            string query = "SELECT UserID, FullName, UserName, PassWord FROM TableUsers order by UserID";

            DataTable dataTable = sql.ExecuteQuery(SqlHelper.defaultConnStr, query, CommandType.Text);

            dataGridView.DataSource = dataTable;

            return dataTable;
        }
    }
}
