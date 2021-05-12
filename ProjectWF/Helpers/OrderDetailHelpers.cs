using System.Data;

namespace ProjectWF.Helpers
{
    class OrderDetailHelpers
    {
        public static bool Add(int Quantity, string Note, int ProductID, int OrderID)
        {
            string cmd = $"insert into TableOrderDetails (Quantity, Note, ProductID, OrderID) values({Quantity}, '{Note}', {ProductID}, {OrderID})";

            int numOfRowsAffected = SqlHelper.ExecuteNonQuery(
                SqlHelper.defaultConnStr,
                cmd,
                CommandType.Text
            );

            return numOfRowsAffected == 1;
        }
    }
}
