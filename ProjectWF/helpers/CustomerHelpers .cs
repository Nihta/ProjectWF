using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectWF
{
    class CustomerHelpers
    {
        #region Params
        public static readonly int sizeFirstNameParam = 20;
        public static readonly int sizeLastNameParam = 20;
        public static readonly int sizeAddressParam = 50;
        public static readonly int sizePhoneParam = 11;
        public static readonly int sizeEmailParam = 30;

        public enum Param
        {
            CustomerID,
            FirstName,
            LastName,
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
                case Param.CustomerID:
                    parameter = new SqlParameter("@CustomerID", SqlDbType.Int);
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
                case Param.FirstName:
                    parameter = new SqlParameter("@CategoryName", SqlDbType.NChar, sizeFirstNameParam);
                    break;
                case Param.LastName:
                    parameter = new SqlParameter("@Description", SqlDbType.NChar, sizeLastNameParam);
                    break;
                case Param.Address:
                    parameter = new SqlParameter("@Address", SqlDbType.NChar, sizeAddressParam);
                    break;
                case Param.Phone:
                    parameter = new SqlParameter("@Phone", SqlDbType.NChar, sizePhoneParam);
                    break;
                case Param.Email:
                    parameter = new SqlParameter("@Email", SqlDbType.NChar, sizeEmailParam);
                    break;
            }

            parameter.Value = value;
            return parameter;
        }
        #endregion

        public static DataTable DataGridViewHelper(SqlHelper sql, DataGridView dataGridView)
        {
            string query = "select c.CustomerID, c.FirstName, c.LastName, c.Address, c.Phone, c.Email from TableCustomers c order by c.CustomerID desc";

            DataTable dataTable = sql.ExecuteQuery(SqlHelper.defaultConnStr, query, CommandType.Text);

            dataGridView.DataSource = dataTable;

            return dataTable;
        }
    }
}
