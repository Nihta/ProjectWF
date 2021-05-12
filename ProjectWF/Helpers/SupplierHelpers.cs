using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ProjectWF
{
    class SupplierHelpers
    {
        #region Params
        public static readonly int sizeSupplierName = 50;
        public static readonly int sizeAddress = 50;
        public static readonly int sizePhone = 11;
        public static readonly int sizeEmail = 30;

        public enum Param
        {
            SupplierID,
            SupplierName,
            Address,
            Phone,
            Email,
        };

        // Param type int
        public static SqlParameter CreateParam(Param param, int value)
        {
            SqlParameter parameter = null;

            switch (param)
            {
                case Param.SupplierID:
                    parameter = new SqlParameter("@SupplierID", SqlDbType.Int);
                    break;
            }

            parameter.Value = value;
            return parameter;
        }

        // Param type string
        public static SqlParameter CreateParam(Param param, string value)
        {
            SqlParameter parameter = null;

            switch (param)
            {
                case Param.SupplierName:
                    parameter = new SqlParameter("@SupplierName", SqlDbType.NVarChar, sizeSupplierName);
                    break;
                case Param.Address:
                    parameter = new SqlParameter("@Address", SqlDbType.NVarChar, sizeAddress);
                    break;
                case Param.Phone:
                    parameter = new SqlParameter("@Phone", SqlDbType.VarChar, sizePhone);
                    break;
                case Param.Email:
                    parameter = new SqlParameter("@Email", SqlDbType.VarChar, sizeEmail);
                    break;
            }

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
