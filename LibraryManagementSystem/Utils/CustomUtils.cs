using System;
using System.Text.RegularExpressions;


namespace LibraryManagementSystem.Utils
{
    internal static class CustomUtils
    {
        public static string GenerateUniqueID(int startIndex = 0, int length = 32)
        {
            return Guid.NewGuid().ToString("N").Substring(startIndex, length);
        }

        public static bool IntInValidRange(int check, int max, int min)
        {
            if (max < min)
                throw new ArgumentException("IntInValidRange(): invalid arg passed");

            return check >= min && check <= max;
        }
    
    }
}
