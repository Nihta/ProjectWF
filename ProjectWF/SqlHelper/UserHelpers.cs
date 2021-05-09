using System.Data;
using System.Data.SqlClient;

namespace ProjectWF.Parameter
{
    class UsersHelpers
    {
        #region SqlParameter
        public static SqlParameter UserIDParam(int value, string paramName = "UserID")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.Int);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter FullNameParam(string value, string paramName = "FullName")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.NVarChar, 30);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter UserNameParam(string value, string paramName = "UserName")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.NChar, 30);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter PassWordParam(string value, string paramName = "PassWord")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.NChar, 32);
            parameter.Value = value;
            return parameter;
        }
        #endregion

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="userName">Tên tài khoản</param>
        /// <param name="passWord">Mật khẩu</param>
        /// <returns>Return true nếu đăng nhập thành công</returns>
        public static bool Login(string userName, string passWord)
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(
                SqlHelper.defaultConnStr,
                "dbo.Login",
                CommandType.StoredProcedure,
                UserNameParam(userName), PassWordParam(MyUtils.MD5Hash(passWord))
                );
            return reader.HasRows;
        }

        public static SqlDataReader getUserData(int userId)
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
    }
}
