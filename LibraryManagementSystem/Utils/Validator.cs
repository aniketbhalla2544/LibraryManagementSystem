using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Utils
{
    internal static class Validator
    {
        public static bool IsValidEmail(string email)
        {
            bool isValid = false;

            // if email is null, empty or white space then return false
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                return isValid;

            email = email.Trim().ToLower();

            const string EMAIL_VALIDATION_REGEX_PATTERN = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            isValid = Regex.IsMatch(email, EMAIL_VALIDATION_REGEX_PATTERN);

            return isValid;
        }
    }
}
