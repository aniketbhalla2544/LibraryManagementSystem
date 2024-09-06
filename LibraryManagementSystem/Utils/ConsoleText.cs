using System;

namespace LibraryManagementSystem.Utils
{
    internal static class ConsoleText
    {
        public static void WriteWithColor(ConsoleColor color, Action action)
        {
            Console.ForegroundColor = color;
            action();
            Console.ResetColor();
        }

        public static void WriteWithDarkRed(Action action) => WriteWithColor(ConsoleColor.DarkRed, action);
        public static void WriteWithDarkYellow(Action action) => WriteWithColor(ConsoleColor.DarkYellow, action);
        public static void WriteWithDarkGreen(Action action) => WriteWithColor(ConsoleColor.DarkGreen, action);

    }
}
