using System.Linq;
using System.Windows.Forms;

namespace ProjectWF
{
    class OrderLinq
    {
        public static void DataGridViewHelper(DataGridView dataGridView)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var query = from order in db.TableOrders
                            join cus in db.TableCustomers on order.CustemerID equals cus.CustomerID
                            orderby order.OrderDate, cus.LastName
                            select new
                            {
                                order.OrderID,
                                order.OrderDate,
                                order.TotalAmount,
                                Customer = $"{cus.FirstName} {cus.LastName}",
                                Phone = cus.Phone
                            };

                dataGridView.DataSource = query;
            }
        }
    }
}
