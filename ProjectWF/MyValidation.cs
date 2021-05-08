using System.Text.RegularExpressions;

namespace ProjectWF
{
    class MyValidation
    {
        public static bool IsEmpty(string text)
        {
            return text.Length == 0;
        }

        public static bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, @"^\d+$");
        }
    }
}
