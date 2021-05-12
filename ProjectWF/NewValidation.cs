using System.Text.RegularExpressions;

namespace ProjectWF
{
    class NewValidation
    {
        public static bool IsNumeric(string text, string name)
        {
            if (!Regex.IsMatch(text, @"^\d+$"))
            {
                MyMessageBox.Warning($"{name} không phải là số hợp lệ!");
                return false;
            }

            return true;
        }


        public static bool IsTextInvalid(string text, int min, int max, string name, bool isRequire = true)
        {
            if (!isRequire && text.Length == 0)
            {
                return true;
            }

            if (text.Length == 0)
            {
                MyMessageBox.Warning($"{name} là bắt buộc !");
                return false;
            }

            if (text.Length < min)
            {
                MyMessageBox.Warning($"{name} quá ngắn!");
                return false;
            }

            if (text.Length > max)
            {
                MyMessageBox.Warning($"{name} quá dài!");
                return false;
            }

            return true;
        }


        public static bool IsPhoneInvalid(string text)
        {
            if (!Regex.IsMatch(text, @"^[0-9]*$"))
            {
                MyMessageBox.Warning($"Số điện thoại không hợp lệ!\nSố điện thoại chỉ bao gồm các số 0->9");
                return false;
            }

            return true;
        }


        public static bool IsEmailInvalid(string text)
        {
            if (!Regex.IsMatch(text, @"^\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b$"))
            {
                MyMessageBox.Warning($"Email không hợp lệ!");
                return false;
            }
              
            return true;
        }
    }
}
