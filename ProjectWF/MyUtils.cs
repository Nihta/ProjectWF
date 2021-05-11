using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ProjectWF
{
    class MyUtils
    {
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static DataGridViewColumn CreateCol(int width, string name, string headerText = "", string dataPropertyName = "")
        {
            if (dataPropertyName == "")
            {
                dataPropertyName = name;
            }

            if (headerText == "")
            {
                headerText = name;
            }

            DataGridViewColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = dataPropertyName;
            col.Name = name;
            col.HeaderText = headerText;
            col.Width = width;

            return col;
        }

        public static DataTable FillComboBoxWithDataCategory(ComboBox comboBoxCategory)
        {
            DataTable dataTable = new DataTable();

            string query = "select CategoryID, CategoryName from TableCategorys";
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.defaultConnStr, query, CommandType.Text);

            dataTable.Load(reader);

            comboBoxCategory.DataSource = dataTable;
            comboBoxCategory.DisplayMember = "CategoryName";
            comboBoxCategory.ValueMember = "CategoryID";

            return dataTable;
        }

        public static DataTable FillComboBoxWithDataSupplier(ComboBox comboBoxSupplier)
        {
            DataTable dataTable = new DataTable();

            string query = "select s.SupplierID, s.SupplierName from TableSuppliers s";
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.defaultConnStr, query, CommandType.Text);

            dataTable.Load(reader);

            comboBoxSupplier.DataSource = dataTable;
            comboBoxSupplier.DisplayMember = "SupplierName";
            comboBoxSupplier.ValueMember = "SupplierID";

            return dataTable;
        }
    }
}
