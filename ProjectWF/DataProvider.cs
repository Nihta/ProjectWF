using System.Data;
using System.Data.SqlClient;


namespace ProjectWF
{
    class DataProvider
    {
        private static SqlConnection connection = null;
        private SqlDataAdapter adapter;

        private string defaultConStr = @"Data Source=.\sqlexpress;Initial Catalog=dbProject;Integrated Security=True;Pooling=False";

        public DataProvider()
        {
            if (connection == null)
            {
                this.Connection(defaultConStr);
            }
        }

        /// <summary>
        /// Kết nối database
        /// </summary>
        /// <param name="server"></param>
        /// <param name="database"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        public void Connection(string server, string database, string uid, string pwd)
        {
            string connectionString = $"server={server};database={database};uid ='{uid}';pwd ='{pwd}'";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
        }

        /// <summary>
        /// Kết nối database
        /// </summary>
        public void Connection(string connectionString)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
        }

        public DataTable ExecuteQuery(string query)
        {

            DataTable dataTable = new DataTable();

            adapter = new SqlDataAdapter(query, connection);

            SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(adapter);

            adapter.Fill(dataTable);

            return dataTable;

        }

        public void Update(DataTable dataTable)
        {
            adapter.Update(dataTable);
        }

        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns>
        /// The number of rows affected.
        /// </returns>
        public int ExecuteNonQuery(string cmdText)
        {
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            return sqlCommand.ExecuteNonQuery();
        }
    }
}
