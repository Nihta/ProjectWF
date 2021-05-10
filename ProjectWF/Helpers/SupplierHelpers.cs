using System.Data;
using System.Data.SqlClient;

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

    }
}
