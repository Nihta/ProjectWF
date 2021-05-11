using System.Data;

namespace ProjectWF.Helpers
{
    class ProductHelpers
    {

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
        public static bool EditProduct(int productIDNeedEdit, string ProductName, int Price, string Description, int CategoryID, int ProductID)
        {
            string cmd = $"UPDATE dbo.TableProducts SET ProductName = N'{ProductName}', Price = {Price}, Description = N'{Description}', CategoryID = {CategoryID}, SupplierID = {CategoryID} WHERE ProductID = {productIDNeedEdit}";

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
    }
}
