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
        const string REGISTER_MEMBER_TEXT = "[ACTION]: Add Member";

        // DEV NOTE: update min and max of IntInValidRange() upon updating Actions
        enum Actions
        {
            Exit,
            DisplayMenu,
            ClearConsole,

            AddBook,
            BorrowBook,
            ReturnBook,
            GetAllBookTitles,

            RegisterMember,

            GetTotalBooksCount,
            GetTotalEBooks,
            GetTotalMembersCount,
            GetTotalPhysicalBooks,
            GetTotalTeacherMembersCount,
            GetTotalStudentMembersCount,
            GetTotalBorrowedEBooks,
            GetTotalBorrowedPhysicalBooks,
        }

        static void DisplayMenu()
        {
            // DEV NOTE: Actions enum upon updating Menu
            Console.WriteLine("--- MENU ----");
            Console.WriteLine("APP ACTIONS:");
            Console.WriteLine("0 - exit");
            Console.WriteLine("1 - see menu");
            Console.WriteLine("2 - clear console");
            Console.WriteLine();

            // book actions
            Console.WriteLine("BOOK ACTIONS:");
            Console.WriteLine("3 - add");
            Console.WriteLine("4 - borrow");
            Console.WriteLine("5 - return");
            Console.WriteLine("6 - all book titles");
            Console.WriteLine();

            // member actions
            Console.WriteLine("MEMBER ACTIONS:");
            Console.WriteLine("7 - add");
            Console.WriteLine();

            // library insights
            Console.WriteLine("LIBRARY INSIGHTS:");
            Console.WriteLine("8 - total books count");
            Console.WriteLine("9 - total e-books count");
            Console.WriteLine("10 - total members count");
            Console.WriteLine("11 - total physical books count");
            Console.WriteLine("12 - total teacher members count");
            Console.WriteLine("13 - total student members count");
            Console.WriteLine("14 - total borrowed e-books count");
            Console.WriteLine("15 - total borrowed physical books count");
            Console.Write("\n");
        }


        static int AskUserValidActionNumber()
        {
            bool askUserInput = true;
            int userInput = 0;

            while (askUserInput)
            {
                Console.Write("Enter an action to continue: ");
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
                bool userInputInValidRange = CustomUtils.IntInValidRange(check: userInputInt, max: (int)Actions.GetTotalBorrowedPhysicalBooks, min: (int)Actions.Exit);
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
                        Console.WriteLine($"Total teacher members count: {library.TotalTeacherMembersCount}");
                        break;
                    case (int)Actions.GetTotalStudentMembersCount:
                        Console.WriteLine($"Total student members count: {library.TotalStudentMembersCount}");
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
