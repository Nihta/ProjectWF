using System;
using System.Diagnostics;
using System.Linq;

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
    }
}
