using System.Globalization;
using System.Text.RegularExpressions;

namespace DotCraftUmbraco.Helpers
{
    public static class Validation
    {
        public static bool IsNameValid(string value, out string error)
        {
            error = string.Empty;
            if (String.IsNullOrWhiteSpace(value))
            {
                error = "Name is required.";
                return false;
            }
            return true;
        }

        public static bool IsDobValid(string value, out string error, out DateTime result)
        {
            error = string.Empty;
            result = DateTime.MinValue;

            if (string.IsNullOrEmpty(value))
            {
                error = "Date of Birth is required.";
                return false;
            }

            if (!DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                error = "Please enter a valid date in the format DD-MM-YYYY.";
                return false;
            }

            return true;
        }

        public static bool IsEmailValid(string value, out string error)
        {
            error = string.Empty;
            string pattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, pattern))
            {
                error = "Please enter a valid email address.";
                return false;
            }
            return true;
        }
    }
}
