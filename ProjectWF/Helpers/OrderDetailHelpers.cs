using System.Data;
using System.Data.SqlClient;

namespace ProjectWF
{
    class OrderDetailHelpers
    {
        #region Params
        public static readonly int sizeNoteParam = 150;
        public enum Param
        {
            OrderDetailID,
            Quantity,
            Note,
            ProductID,
            OrderID,
        };

        // Param type string
        public static SqlParameter CreateParam(Param param, string value)
        {
            SqlParameter parameter = null;

            switch (param)
            {
                case Param.Note:
                    parameter = new SqlParameter("@Note", SqlDbType.NVarChar, sizeNoteParam);
                    break;
            }

            parameter.Value = value;
            return parameter;
        }

        // Param type int
        public static SqlParameter CreateParam(Param param, int value)
        {
            SqlParameter parameter = null;

            switch (param)
            {
                case Param.OrderDetailID:
                    parameter = new SqlParameter("@OrderDetailID", SqlDbType.Int);
                    break;
                case Param.Quantity:
                    parameter = new SqlParameter("@Quantity", SqlDbType.Int);
                    break;
                case Param.ProductID:
                    parameter = new SqlParameter("@ProductID", SqlDbType.Int);
                    break;
                case Param.OrderID:
                    parameter = new SqlParameter("@OrderID", SqlDbType.Int);
                    break;
            }

            parameter.Value = value;
            return parameter;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="note"></param>
        /// <param name="productID"></param>
        /// <param name="orderID"></param>
        /// <returns>
        /// Return true nếu thêm thành công
        /// </returns>
        public static bool Add(int quantity, string note, int productID, int orderID)
        {
            string cmd = @"
                insert into TableOrderDetails (Quantity, Note, ProductID, OrderID)
                values(@Quantity, @Note, @ProductID, @OrderID)";

            int numOfRowsAffected = SqlHelper.ExecuteNonQuery(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.Text,
                CreateParam(Param.Quantity, quantity),
                CreateParam(Param.Note, note),
                CreateParam(Param.ProductID, productID),
                CreateParam(Param.ProductID, orderID)
            );

            return numOfRowsAffected == 1;
        }
    }
}
