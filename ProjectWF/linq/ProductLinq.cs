using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ProjectWF
{
    class ProductLinq
    {
        public static void DataGridViewHelper(DataGridView dataGridView, string productName = "", int categoryID = -1, int supplierID = -1)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var query = from product in db.TableProducts
                            join category in db.TableCategories on product.CategoryID equals category.CategoryID
                            join sup in db.TableSuppliers on product.SupplierID equals sup.SupplierID
                            where
                                   (productName == "" || SqlMethods.Like(product.ProductName, $"%{productName}%"))
                                && (categoryID == -1 || product.CategoryID == categoryID)
                                && (supplierID == -1 || product.SupplierID == supplierID)
                            select new
                            {
                                ProductID = product.ProductID,
                                ProductName = product.ProductName,
                                Price = product.Price,
                                Description = product.Description,
                                CategoryID = product.CategoryID,
                                SupplierID = product.SupplierID,
                                SupplierName = sup.SupplierName,
                                CategoryName = category.CategoryName,
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

        /// <summary>
        /// Sửa mặt hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="productName"></param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
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
