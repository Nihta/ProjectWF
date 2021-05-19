using System.Globalization;
using System;
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

        public static DateTime StrToDateTime(string str)
        {
            return DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static void DataGridViewHelper2(DataGridView dataGridView, string timeStart, string timeEnd, bool isAll = false)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var query = db.TableOrders
                    .ToList()
                    .Where(order => isAll || (StrToDateTime(order.OrderDate) >= StrToDateTime(timeStart) && StrToDateTime(order.OrderDate) <= StrToDateTime(timeEnd)))
                    .Join(
                        db.TableCustomers,
                        order => order.CustemerID,
                        cus => cus.CustomerID,
                        (order, cus) => new
                        {
                            order.OrderID,
                            order.OrderDate,
                            order.TotalAmount,
                            Customer = $"{cus.FirstName} {cus.LastName}",
                            Phone = cus.Phone
                        }
                    ).ToList();

                dataGridView.DataSource = query;
            }
        }
    }
}
