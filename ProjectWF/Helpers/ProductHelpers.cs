using System;
using System.Data;
using System.Data.SqlClient;

namespace ProjectWF
{
    class ProductHelpers
    {
        #region Params
        public enum Param
        {
            ProductID,
            ProductName,
            Price,
            Description,
            CategoryID,
            SupplierID
        };

        public static SqlParameter CreateParam(Param param, string value)
        {
            SqlParameter parameter = null;

            switch (param)
            {
                case Param.ProductName:
                    parameter = new SqlParameter("@ProductName", SqlDbType.NVarChar, 50);
                    break;
                case Param.Description:
                    parameter = new SqlParameter("@Description", SqlDbType.NVarChar, 150);
                    break;
            }

            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter CreateParam(Param param, int value)
        {
            SqlParameter parameter = null;

            switch (param)
            {
                case Param.ProductID:
                    parameter = new SqlParameter("@ProductID", SqlDbType.Int);
                    break;
                case Param.CategoryID:
                    parameter = new SqlParameter("@CategoryID", SqlDbType.Int);
                    break;
                case Param.SupplierID:
                    parameter = new SqlParameter("@SupplierID", SqlDbType.Int);
                    break;
                case Param.Price:
                    parameter = new SqlParameter("@Price", SqlDbType.Int);
                    break;
            }

            parameter.Value = value;
            return parameter;
        }
        #endregion

        public static DataTable GetDataTable(string whereQuery = "")
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
                            join TableSuppliers TS on TS.SupplierID = TP.SupplierID " + whereQuery;
            SqlDataReader dataReader = SqlHelper.ExecuteReader(SqlHelper.defaultConnStr, cmd, CommandType.Text);

            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);

            dataReader.Close();

            return dataTable;
        }

        public static int GetPrice(int id)
        {
            string cmd = @"select Price from TableProducts where  ProductID = @ProductID";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.defaultConnStr, cmd, CommandType.Text, CreateParam(Param.ProductID, id)).ToString());
        }

        public static bool AddProduct(string ProductName, int Price, string Description, int CategoryID, int SupplierID)
        {
            string cmd = @"
                INSERT dbo.TableProducts (ProductName, Price, Description, CategoryID, SupplierID)
                VALUES(@ProductName, @Price, @Description, @CategoryID, @SupplierID)";

            int numOfRowsAffected = SqlHelper.ExecuteNonQuery(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.Text,
                CreateParam(Param.ProductName, ProductName),
                CreateParam(Param.Price, Price),
                CreateParam(Param.Description, Description),
                CreateParam(Param.CategoryID, CategoryID),
                CreateParam(Param.SupplierID, SupplierID)
            );

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
            string cmd = @"
                UPDATE dbo.TableProducts
                SET ProductName = @ProductName,
                    Price = @Price,
                    Description = @Description,
                    CategoryID = @CategoryID,
                    SupplierID = @SupplierID
                WHERE ProductID = @ProductID";

            int numOfRowsAffected = SqlHelper.ExecuteNonQuery(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.Text,
                CreateParam(Param.ProductName, ProductName),
                CreateParam(Param.Price, Price),
                CreateParam(Param.Description, Description),
                CreateParam(Param.CategoryID, CategoryID),
                CreateParam(Param.SupplierID, SupplierID),
                CreateParam(Param.ProductID, productIDNeedEdit)
            );

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
            string cmd = @"
                DELETE FROM dbo.TableProducts
                WHERE ProductID = @ProductID";

            int numOfRowsAffected = SqlHelper.ExecuteNonQuery(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.Text,
                CreateParam(Param.ProductID, idNeedDel)
                );

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
            string cmd = @"
                SELECT *
                FROM dbo.TableProducts
                WHERE ProductName = @ProductName AND ProductID != @ProductID";

            SqlDataReader reader = SqlHelper.ExecuteReader(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.Text,
                CreateParam(Param.ProductName, productName),
                CreateParam(Param.ProductID, ignoreProductID)
            );

            return reader.HasRows;
        }


        public static string GetWhereQuery(string productName, int categoryID = -1, int supplierID = -1)
        {
            string search = "";

            search += productName.Length != 0 ? $"  TP.ProductName like N'%{productName}%' " : " 1 = 1 ";

            search += categoryID != -1 ? $" AND TP.CategoryID = {categoryID} " : " 1 = 1 ";

            if (categoryID != -1)
            {
                search += $" AND  TP.SupplierID = {supplierID} ";
            }

            return search.Length != 0 ? $"where {search}" : " ";
        }
    }
}
