using System.Linq;
using System.Windows.Forms;

namespace ProjectWF
{
    class ProductLinq
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

        /// <summary>
        /// Thêm mặt hàng
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
        public static void Add(string productName, int price, string description, int categoryID, int supplierID)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNewAdd = new TableProduct
                {
                    ProductName = productName,
                    Price = price,
                    Description = description,
                    CategoryID = categoryID,
                    SupplierID = supplierID
                };

                db.TableProducts.InsertOnSubmit(itemNewAdd);
                db.SubmitChanges();
            }
        }

        public static void Edit(int productID, string productName, int price, string description, int categoryID, int supplierID)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNeedEdit = db.TableProducts.First(item => item.ProductID == productID);

                itemNeedEdit.ProductName = productName;
                itemNeedEdit.Price = price;
                itemNeedEdit.Description = description;
                itemNeedEdit.CategoryID = categoryID;
                itemNeedEdit.SupplierID = supplierID;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Xoá mặt hàng
        /// </summary>
        /// <param name="productID">Mã mặt hàng</param>
        public static void Delete(int productID)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNeedDel = db.TableProducts.First(item => item.ProductID == productID);

                db.TableProducts.DeleteOnSubmit(itemNeedDel);
                db.SubmitChanges();
            }
        }
    }
}
