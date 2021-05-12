using System;
using System.Data;

namespace ProjectWF.Helpers
{
    class OrderHelpers
    {
        #region Params
        public static readonly int sizeNoteParam = 150;
        public enum Param
        {
            OrderID,
            OrderDate,
            TotalAmount,
            CustemerID,
        };

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
        #endregion


        public static bool Add(string OrderDate, int TotalAmount, int CustemerID)
        {
            string cmd = $"insert into TableOrders (OrderDate, TotalAmount, CustemerID) values ('{OrderDate}', {TotalAmount}, {CustemerID})";

            int numOfRowsAffected = SqlHelper.ExecuteNonQuery(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.Text
            );

            return numOfRowsAffected == 1;
        }


        public static int GetLastId()
        {
            string cmd = $"select top 1 OrderID from TableOrders order by  CustemerID desc";

            var tmp = SqlHelper.ExecuteScalar(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.Text
            );

            return Convert.ToInt32(tmp.ToString());
        }
    }
}
