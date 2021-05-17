using System.Linq;
using System.Windows.Forms;

namespace ProjectWF
{
    class SupplierLinq
    {
        public static void DataGridViewHelper(DataGridView dataGridView)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var query = from item in db.TableSuppliers
                            orderby item.SupplierID descending
                            select new
                            {
                                SupplierID = item.SupplierID,
                                SupplierName = item.SupplierName,
                                Address = item.Address,
                                Phone = item.Phone,
                                Email = item.Email,
                            };

                dataGridView.DataSource = query;
            }
        }

        public static void Add(string supplierName, string address, string phone, string email)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNewAdd = new TableSupplier();

                itemNewAdd.SupplierName = supplierName;
                itemNewAdd.Address = address;
                itemNewAdd.Phone = phone;
                itemNewAdd.Email = email;

                db.TableSuppliers.InsertOnSubmit(itemNewAdd);
                db.SubmitChanges();
            }
        }
        public static void Edit(int supplierID, string supplierName, string address, string phone, string email)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNeedEdit = db.TableSuppliers.First(item => item.SupplierID == supplierID);

                itemNeedEdit.SupplierName = supplierName;
                itemNeedEdit.Address = address;
                itemNeedEdit.Phone = phone;
                itemNeedEdit.Email = email;

                db.SubmitChanges();
            }
        }

        public static void Delete(int supplierID)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNeedDel = db.TableSuppliers.First(item => item.SupplierID == supplierID);

                db.TableSuppliers.DeleteOnSubmit(itemNeedDel);
                db.SubmitChanges();
            }
        }
    }
}
