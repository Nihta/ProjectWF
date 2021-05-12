using System;
using System.Data;
using System.Data.SqlClient;

namespace ProjectWF.Helpers
{
    class OrderHelpers
    {
        #region Params
        public static readonly int sizeOrderDateParam = 10;
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
                case Param.OrderID:
                    parameter = new SqlParameter("@OrderID", SqlDbType.Int);
                    break;
                case Param.OrderDate:
                    parameter = new SqlParameter("@OrderDate", SqlDbType.Int);
                    break;
                case Param.CustemerID:
                    parameter = new SqlParameter("@CustemerID", SqlDbType.Int);
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
                case Param.OrderDate:
                    parameter = new SqlParameter("@OrderDate", SqlDbType.NChar, sizeOrderDateParam);
                    break;
            }

            parameter.Value = value;
            return parameter;
        }
        #endregion


        public static bool Add(string orderDate, int totalAmount, int custemerID)
        {
            string cmd = @"
                insert into TableOrders (OrderDate, TotalAmount, CustemerID)
                values (@OrderDate, @TotalAmount, @CustemerID)";

            int numOfRowsAffected = SqlHelper.ExecuteNonQuery(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.Text,
                CreateParam(Param.OrderDate, orderDate),
                CreateParam(Param.TotalAmount, totalAmount),
                CreateParam(Param.CustemerID, custemerID)
            );

            return numOfRowsAffected == 1;
        }

        /// <summary>
        /// Lấy id của order mới nhất vừa được thêm
        /// </summary>
        /// <returns></returns>
        public static int GetLastOrderID()
        {
            string cmd = $"select top 1 OrderID from TableOrders order by CustemerID desc";

            var tmp = SqlHelper.ExecuteScalar(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.Text
            );

            return Convert.ToInt32(tmp.ToString());
        }
    }
}
