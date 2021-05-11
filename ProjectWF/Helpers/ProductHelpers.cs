using System.Data;
using System.Data.SqlClient;

namespace ProjectWF.Helpers
{
    class ProductHelpers
    {
        public static DataTable GetDataTable()
        {
            string cmd = @"
                            select TP.ProductID,
                                   TP.ProductName,
                                   TP.Price,
                                   TP.Description,
                                   TP.CategoryID,
                                   TP.SupplierID,
                                   TS.SupplierName,
                                   TC.CategoryName
                            from TableProducts TP
                                     join TableCategorys TC on TC.CategoryID = TP.CategoryID
                                     join TableSuppliers TS on TS.SupplierID = TP.SupplierID";
            SqlDataReader dataReader = SqlHelper.ExecuteReader(SqlHelper.defaultConnStr, cmd, CommandType.Text);

            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);

            return dataTable;
        }

        public static bool AddProduct(string ProductName, int Price, string Description, int CategoryID, int SupplierID)
        {
            string cmd = $"INSERT dbo.TableProducts (ProductName, Price, Description, CategoryID, SupplierID) VALUES(N'{ProductName}', {Price}, N'{Description}', {CategoryID}, {SupplierID});";

            int numOfRowsAffected = SqlHelper.ExecuteNonQuery(SqlHelper.defaultConnStr, cmd, CommandType.Text);

            return numOfRowsAffected == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="rowIdxNeedEdit"></param>
        /// <param name="productIDNeedEdit"></param>
        /// <param name="ProductName"></param>
        /// <param name="Price"></param>
        /// <param name="Description"></param>
        /// <param name="CategoryID"></param>
        /// <param name="ProductID"></param>
        /// <returns>
        /// Return true nếu sửa thành công
        /// </returns>
        public static bool EditProduct(int productIDNeedEdit, string ProductName, int Price, string Description, int CategoryID, int SupplierID)
        {
            string cmd = $"UPDATE dbo.TableProducts SET ProductName = N'{ProductName}', Price = {Price}, Description = N'{Description}', CategoryID = {CategoryID}, SupplierID = {SupplierID} WHERE ProductID = {productIDNeedEdit}";

            int numOfRowsAffected = SqlHelper.ExecuteNonQuery(SqlHelper.defaultConnStr, cmd, CommandType.Text);

            return numOfRowsAffected == 1;
        }

        /// <summary>
        /// Xoá bản bản ghi
        /// </summary>
        /// <param name="idNeedDel">
        /// ID của sản phẩm cần xoá
        /// </param>
        /// <returns>
        /// Return true nếu xoá thành công
        /// </returns>
        public static bool Delete(int idNeedDel)
        {
            string cmd = $"DELETE FROM dbo.TableProducts WHERE ProductID = {idNeedDel}";

            int numOfRowsAffected = SqlHelper.ExecuteNonQuery(SqlHelper.defaultConnStr, cmd, CommandType.Text);

            return numOfRowsAffected == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="ignoreProductID"></param>
        /// <returns>
        /// Return true nếu đã được sử dụng
        /// </returns>
        public static bool CheckProductNameExist(string productName, int ignoreProductID = -1)
        {
            string query = $"SELECT * FROM dbo.TableProducts WHERE ProductName = N'{productName}' AND ProductID != {ignoreProductID}";
            SqlDataReader reader = SqlHelper.ExecuteReader(
                SqlHelper.defaultConnStr,
                query,
                CommandType.Text
            );
            return reader.HasRows;
        }
    }
}
