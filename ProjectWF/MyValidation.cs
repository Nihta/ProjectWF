using System.Text.RegularExpressions;

namespace ProjectWF
{
    class MyValidation
    {
        public static bool IsEmpty(string text, string name = "")
        {
            if (name != "" && text.Length == 0)
            {
                MyMessageBox.Warning($"{name} không được để trống!");
            }

            return text.Length == 0;
        }

        public static bool IsNumeric(string text, string name)
        {
            if (!Regex.IsMatch(text, @"^\d+$"))
            {
                MyMessageBox.Warning($"{name} không phải là số hợp lệ!");
                return false;
            }
            return true;
        }

        public static bool IsInRange(string text, int min, int max, string name)
        {
            if (text.Length < min)
            {
                MyMessageBox.Warning($"{name} quá ngắn!");
                return false;
            }
            else if (text.Length > max)
            {
                MyMessageBox.Warning($"{name} quá dài!");
                return false;
            }
            return true;
        }

        public static bool CommonValidation(string text, int min, int max, string name)
        {
            if (IsEmpty(text, name))
            {
                return false;
            }
            else if (!IsInRange(text, min, max, name))
            {
                return false;
            }
            return true;
        }

        public static bool IsPhoneInvalid(string text)
        {
            string name = "Số điện thoại";
            if (!Regex.IsMatch(text, @"^[0-9]*$"))
            {
                MyMessageBox.Warning($"Số điện thoại không hợp lệ!\n{name} chỉ bao gồm các số 0->9");
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
