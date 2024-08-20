using System;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Services
{
    internal static class MenuService
    {
        const string PHYSICAL_BOOK_CREATED_ACTION_TEXT = "[ACTION]: Create Physical Book";
        const string PHYSICAL_BOOK_BORROWED_ACTION_TEXT = "[ACTION]: Borrow Physical book";
        const string PHYSICAL_BOOK_RETURNED_ACTION_TEXT = "[ACTION]: Return Physical book";

        const string EBOOK_CREATED_ACTION_TEXT = "[ACTION]: Create Ebook";
        const string EBOOK_BORROWED_ACTION_TEXT = "[ACTION]: Borrow Ebook";
        const string EBOOK_RETURNED_ACTION_TEXT = "[ACTION]: Return Ebook";

        const string REGISTER_MEMBER_TEXT = "[ACTION]: Register Member";

        // update min and max of IntInValidRange() upon updating Actions
        enum Actions
        {
            ClearConsole = -2,
            Exit,
            DisplayMenu,
            CreatePhysicalBook,
            BorrowPhysicalBook,
            ReturnPhysicalBook,
            CreateEBook,
            BorrowEBook,
            ReturnEBook,
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
            Console.WriteLine("Enter 1 to create a physical book");
            Console.WriteLine("Enter 2 to borrow a physical book");
            Console.WriteLine("Enter 3 to return a physical book");
            Console.WriteLine("Enter 4 to create an e-book");
            Console.WriteLine("Enter 5 to borrow an e-book");
            Console.WriteLine("Enter 6 to return an e-book");
            Console.WriteLine("Enter 7 to get total physical books");
            Console.WriteLine("Enter 8 to get total e-books");
            Console.WriteLine("Enter 9 to get total borrowed physical books");
            Console.WriteLine("Enter 10 to get total borrowed e-books");
            Console.WriteLine("Enter 11 to get all book titles");
            Console.WriteLine("Enter 12 to get system's total books count");
            Console.WriteLine("Enter 13 to register a member");
            Console.WriteLine("Enter 14 to get total members count");
            Console.WriteLine("Enter 15 to get total teacher members count");
            Console.WriteLine("Enter 16 to get total student members count");

            Console.Write("\n\n");
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
            LibManagementSystem libraryManagementSystem = new LibManagementSystem();

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
                        AskActionNumberUserInput = false;
                        break;
                    case (int)Actions.DisplayMenu:
                        Console.WriteLine();
                        DisplayMenu();
                        continue;
                    case (int)Actions.CreatePhysicalBook:
                        Console.WriteLine($"\n{PHYSICAL_BOOK_CREATED_ACTION_TEXT}");
                        libraryManagementSystem.CreatePhysicalBook();
                        break;
                    case (int)Actions.BorrowPhysicalBook:
                        libraryManagementSystem.BorrowPhysicalBook();
                        break;
                    case (int)Actions.ReturnPhysicalBook:
                        libraryManagementSystem.ReturnPhysicalBook();
                        break;
                    case (int)Actions.CreateEBook:
                        Console.WriteLine($"\n{EBOOK_CREATED_ACTION_TEXT}");
                        libraryManagementSystem.CreateEBook();
                        break;
                    case (int)Actions.BorrowEBook:
                        libraryManagementSystem.BorrowEBook();
                        break;
                    case (int)Actions.ReturnEBook:
                        libraryManagementSystem.ReturnEBook();
                        break;
                    case (int)Actions.GetTotalPhysicalBooks:
                        Console.WriteLine($"Total physical books: {libraryManagementSystem.PhysicalBooks.Count}");
                        break;
                    case (int)Actions.GetTotalEBooks:
                        Console.WriteLine($"Total e-books: {libraryManagementSystem.EBooks.Count}");
                        break;
                    case (int)Actions.GetTotalBorrowedPhysicalBooks:
                        Console.WriteLine($"Total borrowed physicalBooks: {libraryManagementSystem.TotalBorrowedPhysicalBooks}");
                        break;
                    case (int)Actions.GetTotalBorrowedEBooks:
                        Console.WriteLine($"Total borrowed e-books: {libraryManagementSystem.TotalBorrowedEBooks}");
                        break;
                    case (int)Actions.GetAllBookTitles:
                        libraryManagementSystem.ConsoleAllBookTitles();
                        break;
                    case (int)Actions.GetTotalBooksCount:
                        Console.WriteLine($"System's total books count: {libraryManagementSystem.TotalBooksCount}");
                        break;
                    case (int)Actions.RegisterMember:
                        Console.WriteLine($"\n{REGISTER_MEMBER_TEXT}");
                        libraryManagementSystem.RegisterMember();
                        break;
                    case (int)Actions.GetTotalMembersCount:
                        Console.WriteLine($"Total members count: {libraryManagementSystem.TotalMembersCount}");
                        break;
                    case (int)Actions.GetTotalTeacherMembersCount:
                        Console.WriteLine($"Total members count: {libraryManagementSystem.TotalTeacherMembersCount}");
                        break;
                    case (int)Actions.GetTotalStudentMembersCount:
                        Console.WriteLine($"Total members count: {libraryManagementSystem.TotalStudentMembersCount}");
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
