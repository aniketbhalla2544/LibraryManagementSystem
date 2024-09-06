using System;
using LibraryManagementSystem.Models.Books;
using LibraryManagementSystem.Models.Member;
using LibraryManagementSystem.Utils;
using System.Collections.Generic;

namespace LibraryManagementSystem.Services
{
    internal static class Menu
    {
        const string ADD_BOOK_ACTION_TEXT = "[ACTION]: Add Book";
        const string BORROW_BOOK_ACTION_TEXT = "[ACTION]: Borrow book";
        const string RETURN_BOOK_ACTION_TEXT = "[ACTION]: Return book";
        const string REGISTER_MEMBER_TEXT = "[ACTION]: Register Member";

        // update min and max of IntInValidRange() upon updating Actions
        enum Actions
        {
            ClearConsole = -2,
            Exit,
            DisplayMenu,
            AddBook,
            BorrowBook,
            ReturnBook,
            GetTotalPhysicalBooks,
            GetTotalEBooks,
            GetTotalBorrowedPhysicalBooks,
            GetTotalBorrowedEBooks,
            GetAllBookTitles,
            GetTotalBooksCount,
            RegisterMember,
            GetTotalMembersCount,
            GetTotalTeacherMembersCount,
            GetTotalStudentMembersCount
        }

        static void DisplayMenu()
        {
            // update Actions enum upon updating Menu
            Console.WriteLine("[MENU]:");
            Console.WriteLine("Enter -2 to clear console");
            Console.WriteLine("Enter -1 to exit");
            Console.WriteLine("Enter 0 to see menu");
            Console.WriteLine("Enter 1 to add book");
            Console.WriteLine("Enter 2 to borrow book");
            Console.WriteLine("Enter 3 to return book");
            Console.WriteLine("Enter 4 to get total physical books");
            Console.WriteLine("Enter 5 to get total e-books");
            Console.WriteLine("Enter 6 to get total borrowed physical books");
            Console.WriteLine("Enter 7 to get total borrowed e-books");
            Console.WriteLine("Enter 8 to get all book titles");
            Console.WriteLine("Enter 9 to get system's total books count");
            Console.WriteLine("Enter 10 to register a member");
            Console.WriteLine("Enter 11 to get total members count");
            Console.WriteLine("Enter 12 to get total teacher members count");
            Console.WriteLine("Enter 13 to get total student members count");
            Console.Write("\n");
        }


        static int AskUserValidActionNumber()
        {
            bool askUserInput = true;
            int userInput = 0;

            while (askUserInput)
            {
                Console.Write("Enter an action number: ");
                bool isValidUserInput = int.TryParse(Console.ReadLine(), out int userInputInt);

                // checking if user input is a valid integer
                if (!isValidUserInput)
                {
                    Console.WriteLine("[ALERT]: Enter valid action number from Menu\n");
                    continue;
                };

                // TODO: get total actions
                //int totalActions = Enum.GetValues(typeof(Actions)).Length;
                //Console.WriteLine($"total actions: {totalActions}");

                // checking if user input integer is in valid range of "Actions" numbers
                bool userInputInValidRange = CustomUtils.IntInValidRange(check: userInputInt, max: (int)Actions.GetTotalStudentMembersCount, min: (int)Actions.ClearConsole);
                if (!userInputInValidRange)
                {
                    Console.WriteLine("[ALERT]: Enter valid action number from Menu\n");
                    continue;
                };

                userInput = userInputInt;
                askUserInput = false;
            }

            return userInput;
        }


        public static void Start()
        {
            Library library = Library.Instance;

            Console.WriteLine("============= WELCOME TO LIBRARY MANAGEMENT SYSTEM ==============\n");
            DisplayMenu();


            bool AskActionNumberUserInput = true;
            while (AskActionNumberUserInput)
            {
                int userInput = AskUserValidActionNumber();

                switch (userInput)
                {
                    case (int)Actions.ClearConsole:
                        Console.Clear();
                        DisplayMenu();
                        break;
                    case (int)Actions.Exit:
                        Environment.Exit(0);
                        //AskActionNumberUserInput = false;
                        break;
                    case (int)Actions.DisplayMenu:
                        Console.WriteLine();
                        DisplayMenu();
                        continue;
                    case (int)Actions.AddBook:
                        Console.WriteLine($"\n{ADD_BOOK_ACTION_TEXT}");
                        library.AddBook();
                        break;
                    case (int)Actions.ReturnBook:
                        Console.WriteLine($"\n{RETURN_BOOK_ACTION_TEXT}");
                        library.ReturnBook();
                        break;
                    case (int)Actions.BorrowBook:
                        Console.WriteLine($"\n{BORROW_BOOK_ACTION_TEXT}");
                        library.BorrowBook();
                        break;
                    case (int)Actions.GetTotalPhysicalBooks:
                        Console.WriteLine($"Total physical books: {library.PhysicalBooks.Count}");
                        break;
                    case (int)Actions.GetTotalEBooks:
                        Console.WriteLine($"Total e-books: {library.EBooks.Count}");
                        break;
                    case (int)Actions.GetTotalBorrowedPhysicalBooks:
                        Console.WriteLine($"Total borrowed physicalBooks: {library.TotalBorrowedPhysicalBooks}");
                        break;
                    case (int)Actions.GetTotalBorrowedEBooks:
                        Console.WriteLine($"Total borrowed e-books: {library.TotalBorrowedEBooks}");
                        break;
                    case (int)Actions.GetAllBookTitles:
                        library.ConsoleAllBookTitles();
                        break;
                    case (int)Actions.GetTotalBooksCount:
                        Console.WriteLine($"System's total books count: {library.TotalBooksCount}");
                        break;
                    case (int)Actions.RegisterMember:
                        Console.WriteLine($"\n{REGISTER_MEMBER_TEXT}");
                        library.RegisterMember();
                        break;
                    case (int)Actions.GetTotalMembersCount:
                        Console.WriteLine($"Total members count: {library.TotalMembersCount}");
                        break;
                    case (int)Actions.GetTotalTeacherMembersCount:
                        Console.WriteLine($"Total members count: {library.TotalTeacherMembersCount}");
                        break;
                    case (int)Actions.GetTotalStudentMembersCount:
                        Console.WriteLine($"Total members count: {library.TotalStudentMembersCount}");
                        break;
                    default:
                        Console.WriteLine("[ERROR]: Something went wrong while choosing menu");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
