using System;
using System.Data;

namespace ProjectWF.Helpers
{
    class OrderHelpers
    {

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
