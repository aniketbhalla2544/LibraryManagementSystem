
using System.Text.RegularExpressions;

namespace LibraryManagementSystem.Utils
{
    internal static class Validator
    {
        public static bool IsStringNullOrEmptyOrWhitespace(string s) => string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);

        public static bool IsValidURL(string url, out string result)
        {
            bool isValid = false;
            result = string.Empty;

            // Check for null, empty, or whitespace string
            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
                return isValid;

            const string REGEX_URL_PATTERN = @"^https?:\/\/([a-zA-Z0-9-\.]+)(\.[a-zA-Z]{2,})(\:\d+)?(\/[\w\-\.~]*)*(\?[;&a-zA-Z0-9\-\.=_~%]*)?(\#[-a-zA-Z0-9_]*)?$";

            // validating url with regex pattern
            if (Regex.IsMatch(url, REGEX_URL_PATTERN))
            {
                isValid = true;
                result = url.Trim();
            }

            return isValid;
        }
        public static bool IsValidEmail(string email)
        {
            // if email is null, empty or white space then return false
            if (IsStringNullOrEmptyOrWhitespace(email))
                return false;

            email = email.Trim().ToLower();

            const string EMAIL_VALIDATION_REGEX_PATTERN = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, EMAIL_VALIDATION_REGEX_PATTERN);
        }
    }
}
