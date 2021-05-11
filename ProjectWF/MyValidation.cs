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

        public static bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, @"^\d+$");
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
    }
}
