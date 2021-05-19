using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ProjectWF
{
    class UserLinq
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns>
        /// Đăng nhập thành công trả về mã người dùng, ngược lại trả về -1
        /// </returns>
        public static int Login(string userName, string passWord)
        {
            string passWordEndcode = MyUtils.MD5Hash(passWord);

            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                IQueryable<TableUser> query = from item in db.TableUsers
                                              where (item.UserName == userName) && (item.PassWord == passWordEndcode)
                                              select item;

                if (query.Count() != 0)
                {
                    return query.First().UserID;
                }
            }

            return -1;
        }

        public static void DataGridViewHelper(DataGridView dataGridView)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var queryUsers = from item in db.TableUsers
                                 orderby item.UserID descending
                                 select new
                                 {
                                     UserID = item.UserID,
                                     FullName = item.FullName,
                                     UserName = item.UserName,
                                     PassWord = item.PassWord,
                                 };

                dataGridView.DataSource = queryUsers;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="ignoreUserId"></param>
        /// <returns>
        /// Return true nếu đã được sử dụng
        /// </returns>
        public static bool CheckUserNameExist(string userName, int ignoreUserId = -1)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                int cnt = db.TableUsers.Count(user => user.UserName == userName && user.UserID != ignoreUserId);
                return cnt > 0;
            }
        }

        public static TableUser GetUserById(int userID)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TableUser query = db.TableUsers.First(user => user.UserID == userID);

                return query;
            }
        }

        public static void Add(string fullName, string userName, string passWord)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                TableUser itemNewAdd = new TableUser();

                string passWordEndcode = MyUtils.MD5Hash(passWord);
                itemNewAdd.FullName = fullName;
                itemNewAdd.UserName = userName;
                itemNewAdd.PassWord = passWordEndcode;

                db.TableUsers.InsertOnSubmit(itemNewAdd);
                db.SubmitChanges();
            }
        }

        public static bool UpdateUser(int userID, string fullName, string passWord)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                try
                {
                    TableUser objUser = db.TableUsers.Single(user => user.UserID == userID);

                    string passWordEndcode = MyUtils.MD5Hash(passWord);

                    objUser.FullName = fullName;
                    objUser.PassWord = passWordEndcode;

                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    // Debug
                    Debug.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public static void Edit(int userID, string fullName, string userName, string passWord)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNeedEdit = db.TableUsers.First(user => user.UserID == userID);

                string passWordEndcode = MyUtils.MD5Hash(passWord);
                itemNeedEdit.FullName = fullName;
                itemNeedEdit.UserName = userName;
                itemNeedEdit.PassWord = passWordEndcode;

                db.SubmitChanges();
            }
        }

        public static void Delete(int userID)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var itemNeedDel = db.TableUsers.First(user => user.UserID == userID);

                db.TableUsers.DeleteOnSubmit(itemNeedDel);
                db.SubmitChanges();
            }
        }
    }
}
