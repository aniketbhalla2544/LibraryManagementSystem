using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Utils
{
    internal static class MenuSelector
    {
        public static string SelectOption(List<string> options, string message = "Use the arrow keys to navigate and press Enter to select:", bool beepSound = false)
        {
            int currentSelection = 0;
            ConsoleKey key;

            Console.WriteLine(message);

            // Store the initial cursor position
            int cursorTop = Console.CursorTop;

            do
            {
                // Display the options
                for (int i = 0; i < options.Count; i++)
                {
                    // Move the cursor to the correct position
                    Console.SetCursorPosition(0, cursorTop + i);

                    if (i == currentSelection)
                    {
                        // Highlight the currently selected option
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {options[i]}");
                    }
                }

                // Capture the key press
                key = Console.ReadKey(true).Key;
                if(beepSound) Console.Beep();

                // Update the current selection based on the key press
                if (key == ConsoleKey.UpArrow)
                {
                    currentSelection--;
                    if (currentSelection < 0)
                        currentSelection = options.Count - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    currentSelection++;
                    if (currentSelection >= options.Count)
                        currentSelection = 0;
                }

            } while (key != ConsoleKey.Enter);

            // Return the selected option
            return options[currentSelection];
        }
    }
}
