using System;
using System.Collections.Generic;
using LibraryManagementSystem.Models.Books;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    internal class Program
    {

        static void Main()
        {
            MenuService.Start();

            Console.ReadLine();
        }
    }
}
