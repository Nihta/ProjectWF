using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectWF
{
    class CategoryHelpers
    {
        #region Params
        public static readonly int sizeCategoryNameParam = 50;
        public static readonly int sizeDescriptionParam = 100;

        public enum Param
        {
            CategoryID,
            CategoryName,
            Description,
        };

        // Param type int
        public static SqlParameter CreateParam(Param param, int value)
        {
            SqlParameter parameter = null;

            switch (param)
            {
                case Param.CategoryID:
                    parameter = new SqlParameter("@CategoryID", SqlDbType.Int);
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
                case Param.CategoryName:
                    parameter = new SqlParameter("@CategoryName", SqlDbType.NChar, sizeCategoryNameParam);
                    break;
                case Param.Description:
                    parameter = new SqlParameter("@Description", SqlDbType.NChar, sizeDescriptionParam);
                    break;
            }

            parameter.Value = value;
            return parameter;
        }
        #endregion

        public static DataTable DataGridViewHelper(SqlHelper sql, DataGridView dataGridView)
        {
            string query = "select c.CategoryID, c.CategoryName, c.Description from dbo.TableCategorys c order by c.CategoryID desc";

            DataTable dataTable = sql.ExecuteQuery(SqlHelper.defaultConnStr, query, CommandType.Text);

            dataGridView.DataSource = dataTable;

            return dataTable;
        }
    }
}
