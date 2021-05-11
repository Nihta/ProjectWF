using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ProjectWF.Helpers
{
    class SupplierHelpers
    {
        // SupplierID SupplierName Address Phone Email
        private static int sizeSupplierName = 50;
        private static int sizeAddress = 50;
        private static int sizePhone = 11;
        private static int sizeEmail = 30;

        #region SqlParameter
        public static SqlParameter SupplierIDParam(int value, string paramName = "SupplierID")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.Int);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter SupplierNameParam(int value, string paramName = "SupplierName")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.NVarChar, sizeSupplierName);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter AddressParam(int value, string paramName = "Address")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.NVarChar, sizeAddress);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter PhoneParam(int value, string paramName = "Phone")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.VarChar, sizePhone);
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter EmailParam(int value, string paramName = "Email")
        {
            SqlParameter parameter = new SqlParameter($"@{paramName}", SqlDbType.VarChar, sizeEmail);
            parameter.Value = value;
            return parameter;
        }
        #endregion

        #region Validation
        public static bool IsSupplierNameInvalid(string text)
        {
            string name = "Tên nhà cung cấp";
            if (MyValidation.IsEmpty(text, name))
            {
                return false;
            }
            else if (!MyValidation.IsInRange(text, 5, sizeSupplierName, name))
            {
                return false;
            }

            return true;
        }

        public static bool IsAddressInvalid(string text)
        {
            string name = "Địa chỉ";
            if (MyValidation.IsEmpty(text, name))
            {
                return false;
            }
            else if (!MyValidation.IsInRange(text, 5, sizeAddress, name))
            {
                return false;
            }
            return true;
        }


        public static bool IsPhoneInvalid(string text)
        {
            string name = "Số điện thoại";
            if (MyValidation.IsEmpty(text, name))
            {
                return false;
            }
            else if (!MyValidation.IsInRange(text, 9, sizePhone, name))
            {
                return false;
            }
            else if (!Regex.IsMatch(text, @"^[0-9]*$"))
            {
                MyMessageBox.Warning($"{name} không hợp lệ!\n{name} chỉ bao gồm các số 0->9");
                return false;
            }
            return true;
        }


        public static bool IsEmailInvalid(string text)
        {
            string name = "Email";
            if (MyValidation.IsEmpty(text, name))
            {
                return false;
            }
            else if (!Regex.IsMatch(text, @"^\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b$"))
            {
                MyMessageBox.Warning($"{name} không hợp lệ!");
                return false;
            }
            return true;
        }
        #endregion
    }
}
