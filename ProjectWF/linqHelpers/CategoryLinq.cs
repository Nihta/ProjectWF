using System.Linq;
using System.Windows.Forms;

namespace ProjectWF
{
    class CategoryLinq
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

        public static void Add(string categoryName, string description)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNewAdd = new TableCategory();

                itemNewAdd.CategoryName = categoryName;
                itemNewAdd.Description = description;

                db.TableCategories.InsertOnSubmit(itemNewAdd);
                db.SubmitChanges();
            }
        }
        public static void Edit(int categoryID, string categoryName, string description)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNeedEdit = db.TableCategories.First(item => item.CategoryID == categoryID);

                itemNeedEdit.CategoryName = categoryName;
                itemNeedEdit.Description = description;

                db.SubmitChanges();
            }
        }

        public static void Delete(int categoryID)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNeedDel = db.TableCategories.First(item => item.CategoryID == categoryID);

                db.TableCategories.DeleteOnSubmit(itemNeedDel);
                db.SubmitChanges();
            }
        }
    }
}
