using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ProjectWF
{
    class CustomerLinq
    {
        public static void DataGridViewHelper(DataGridView dataGridView, string field = "", string value = "")
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                IQueryable<TableCustomer> queryCustomer;
                switch (field)
                {
                    case "Tên":
                        queryCustomer = db.TableCustomers.Where(item => SqlMethods.Like(item.LastName, $"%{value}%"));
                        break;
                    case "Số điện thoại":
                        queryCustomer = db.TableCustomers.Where(item => SqlMethods.Like(item.Phone, $"%{value}%"));
                        break;
                    case "Địa chỉ":
                        queryCustomer = db.TableCustomers.Where(item => SqlMethods.Like(item.Address, $"%{value}%"));
                        break;
                    case "Email":
                        queryCustomer = db.TableCustomers.Where(item => SqlMethods.Like(item.Email, $"%{value}%"));
                        break;
                    default:
                        queryCustomer = db.TableCustomers;
                        break;
                }

                var resQueryable = queryCustomer.OrderByDescending(item => item.CustomerID)
                    .Select(item => new
                    {
                        CustomerID = item.CustomerID,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Address = item.Address,
                        Phone = item.Phone,
                        Email = item.Email,
                    });

                dataGridView.DataSource = resQueryable;
            }
        }



        public static void Add(string firstName, string lastName, string address, string phone, string email)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNewAdd = new TableCustomer();

                itemNewAdd.FirstName = firstName;
                itemNewAdd.LastName = lastName;
                itemNewAdd.Address = address;
                itemNewAdd.Phone = phone;
                itemNewAdd.Email = email;

                db.TableCustomers.InsertOnSubmit(itemNewAdd);
                db.SubmitChanges();
            }
        }

        public static void Edit(int customerID, string firstName, string lastName, string address, string phone, string email)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNeedEdit = db.TableCustomers.First(item => item.CustomerID == customerID);

                itemNeedEdit.FirstName = firstName;
                itemNeedEdit.LastName = lastName;
                itemNeedEdit.Address = address;
                itemNeedEdit.Phone = phone;
                itemNeedEdit.Email = email;

                db.SubmitChanges();
            }
        }

        public static void Delete(int customerID)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNeedDel = db.TableCustomers.First(item => item.CustomerID == customerID);

                db.TableCustomers.DeleteOnSubmit(itemNeedDel);
                db.SubmitChanges();
            }
        }
    }
}
