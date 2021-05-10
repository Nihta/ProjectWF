using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectWF.Helpers
{
    class UsersHelpers
    {
        public static readonly int sizeFullName = 30;
        public static readonly int sizeUserName = 30;
        public static readonly int sizePassWord = 32;

        #region SqlParameter
        public static SqlParameter UserIDParam(int value, string paramName = "UserID")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.Int);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter FullNameParam(string value, string paramName = "FullName")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.NVarChar, sizeFullName);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter UserNameParam(string value, string paramName = "UserName")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.NChar, sizeUserName);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter PassWordParam(string value, string paramName = "PassWord")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.NChar, sizePassWord);
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
            if (MyValidation.IsEmpty(passWord))
            {
                MyMessageBox.Warning($"{name} không được để trống!");
                return false;
            }
            else if (passWord.Length < 3)
            {
                MyMessageBox.Warning($"{name} quá ngắn!");
                return false;
            }
            else if (passWord.Length > sizePassWord)
            {
                MyMessageBox.Warning($"{name} quá dài!");
                return false;
            }
            else if (!Regex.IsMatch(passWord, @"^[a-z0-9]*$"))
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
            if (MyValidation.IsEmpty(fullName))
            {
                MyMessageBox.Warning($"Họ và tên không được để trống!");
                return false;
            }
            else if (fullName.Length < 4)
            {
                MyMessageBox.Warning($"Họ và tên quá ngắn!");
                return false;
            }
            else if (fullName.Length > sizeFullName)
            {
                MyMessageBox.Warning($"Họ và tên quá dài!");
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
            if (userName.Length > sizeUserName)
            {
                MyMessageBox.Warning("Tên đăng nhập quá dài!");
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
            SqlDataReader reader = SqlHelper.ExecuteReader(
                SqlHelper.defaultConnStr,
                "dbo.Login",
                CommandType.StoredProcedure,
                UserNameParam(userName), PassWordParam(MyUtils.MD5Hash(passWord))
                );

            if (reader.HasRows)
            {
                reader.Read();
                return int.Parse(reader["UserID"].ToString());
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
            int res = SqlHelper.ExecuteNonQuery(
                SqlHelper.defaultConnStr,
                "dbo.UpdateUser",
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
    }
}
