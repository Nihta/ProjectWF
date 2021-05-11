using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ProjectWF
{
    class MyUtils
    {
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static DataGridViewColumn CreateCol(int width, string name, string headerText = "", string dataPropertyName = "")
        {
            if (dataPropertyName == "")
            {
                dataPropertyName = name;
            }

            if (headerText == "")
            {
                headerText = name;
            }

            DataGridViewColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = dataPropertyName;
            col.Name = name;
            col.HeaderText = headerText;
            col.Width = width;

            return col;
        }
    }
}
