using System.Linq;
using System.Windows.Forms;

namespace ProjectWF
{
    class CustomerLinq
    {
        public static void DataGridViewHelper(DataGridView dataGridView)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var query = from item in db.TableCategories
                            orderby item.CategoryID descending
                            select new
                            {
                                CategoryID = item.CategoryID,
                                CategoryName = item.CategoryName,
                                Description = item.Description
                            };

                dataGridView.DataSource = query;
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
