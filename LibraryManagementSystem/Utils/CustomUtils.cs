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
            if (max < min || min > max)
                throw new ArgumentException("IntInValidRange(): invalid arg passed because either max < min || min > max");
            
            bool isInRange = false;

            for (int i = min; i <= max; i++)
            {
                if (i == check)
                {
                    isInRange = true;
                    break;
                }

            }

            return isInRange;
        }
    
    }
}
