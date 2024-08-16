using System;
using LibraryManagementSystem.Models.Books;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem
{
    internal class Program
    {
        enum Actions
        {
            Start = -2,
            Exit,
            DisplayMenu,
            CreatePhysicalBook,
            BorrowPhysicalBook,
            ReturnPhysicalBook,
            CreateEBook,
            BorrowEBook,
            ReturnEBook,
        }

        static void DisplayMenu()
        {
            Console.WriteLine("[MENU]:");
            Console.WriteLine("Enter -1 to exit");
            Console.WriteLine("Enter 0 to see menu");
            Console.WriteLine("Enter 1 to create a physical book");
            Console.WriteLine("Enter 2 to borrow a physical book");
            Console.WriteLine("Enter 3 to return a physical book");
            Console.WriteLine("Enter 4 to create an e-book");
            Console.WriteLine("Enter 5 to borrow an e-book");
            Console.WriteLine("Enter 6 to return an e-book");
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

                // checking if user input integer is in valid range of "Actions" numbers
                bool userInputInValidRange = CustomUtils.IntInValidRange(check: userInputInt, max: (int)Actions.ReturnEBook, min: (int)Actions.Exit);
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


        static void Main(string[] args)
        {
            const string PHYSICAL_BOOK_CREATED_ACTION_TEXT = "[ACTION]: Create Physical Book";
            const string PHYSICAL_BOOK_BORROWED_ACTION_TEXT = "[ACTION]: Borrow Physical book";
            const string PHYSICAL_BOOK_RETURNED_ACTION_TEXT = "[ACTION]: Return Physical book";

            const string EBOOK_CREATED_ACTION_TEXT = "[ACTION]: Create Ebook";
            const string EBOOK_BORROWED_ACTION_TEXT = "[ACTION]: Borrow Ebook";
            const string EBOOK_RETURNED_ACTION_TEXT = "[ACTION]: Return Ebook";



            Console.WriteLine("============= WELCOME TO LIBRARY MANAGEMENT SYSTEM ==============\n");


            bool AskActionNumberUserInput = true;
            while (AskActionNumberUserInput)
            {
                DisplayMenu();
                int userInput = AskUserValidActionNumber();

                switch (userInput)
                {
                    case (int)Actions.Exit:
                        AskActionNumberUserInput = false;
                        Console.WriteLine("User chose to exit");
                        break;
                    case (int)Actions.DisplayMenu:
                        Console.WriteLine();
                        continue;
                    case (int)Actions.CreatePhysicalBook:
                        Console.WriteLine("User chose to CreatePhysicalBook");
                        break;
                    case (int)Actions.BorrowPhysicalBook:
                        Console.WriteLine("User chose to BorrowPhysicalBook");
                        break;
                    case (int)Actions.ReturnPhysicalBook:
                        Console.WriteLine("User chose to ReturnPhysicalBook");
                        break;
                    case (int)Actions.CreateEBook:
                        Console.WriteLine("User chose to CreateEBook");
                        break;
                    case (int)Actions.BorrowEBook:
                        Console.WriteLine("User chose to BorrowEBook");
                        break;
                    case (int)Actions.ReturnEBook:
                        Console.WriteLine("User chose to ReturnEBook");
                        break;
                    default:
                        throw new Exception("Something went wrong while choosing menu");
                }
                Console.WriteLine();

            }


            //// Actions
            //Console.WriteLine(PHYSICAL_BOOK_CREATED_ACTION_TEXT);
            //Book phybook1 = new PhysicalBook(title: "Earth System Science", author: "Timothy Lenton", shelfLocation: "A1");
            //Console.WriteLine(phybook1);



            //Console.WriteLine($"\n{EBOOK_CREATED_ACTION_TEXT}");
            ////EBook ebook1 = new EBook(title: "Charlie and the Chocolate Factory", author: "Roald Dahl", URL: "");
            //EBook ebook1 = new EBook(title: "Charlie and the Chocolate Factory", author: "Roald Dahl", URL: @"https://openlibrary.org/works/OL45790W/Charlie_and_the_Chocolate_Factory");
            //Console.WriteLine(ebook1);




            // logging system details
            Console.WriteLine("\n\n============== SYSTEM DETAILS ===================");
            Console.WriteLine($"Total books = {PhysicalBook.TotalPhysicalBooks + EBook.TotalEBooks}");

            Console.WriteLine($"Physical Books:\n" +
                $"\tTotal = {PhysicalBook.TotalPhysicalBooks}\n" +
                $"\tTotal borrowed = {PhysicalBook.TotalBorrowedPhysicalBooks}");

            Console.WriteLine($"EBooks:\n" +
                $"\tTotal = {EBook.TotalEBooks}\n" +
                $"\tTotal borrowed = {EBook.TotalBorrowedEBooks}");

            Console.ReadLine();
        }
    }
}
