using System;
using System.Collections.Generic;
using LibraryManagementSystem.Models.Books;

namespace LibraryManagementSystem.Services
{
    internal class LibManagementSystem
    {
        const string PHYSICAL_BOOK_CREATED_ACTION_TEXT = "[ACTION]: Create Physical Book";
        const string PHYSICAL_BOOK_BORROWED_ACTION_TEXT = "[ACTION]: Borrow Physical book";
        const string PHYSICAL_BOOK_RETURNED_ACTION_TEXT = "[ACTION]: Return Physical book";

        const string EBOOK_CREATED_ACTION_TEXT = "[ACTION]: Create Ebook";
        const string EBOOK_BORROWED_ACTION_TEXT = "[ACTION]: Borrow Ebook";
        const string EBOOK_RETURNED_ACTION_TEXT = "[ACTION]: Return Ebook";

        // class props
        public List<PhysicalBook> PhysicalBooks { get; private set; }
        public List<EBook> EBooks { get; private set; }
        public long TotalBorrowedPhysicalBooks { get => PhysicalBooks.FindAll(book => book.IsBorrowed).Count; }
        public long TotalBorrowedEBooks { get => EBooks.FindAll(book => book.IsBorrowed).Count; }
        public List<string> PhysicalBookTitlesList { get => PhysicalBooks.ConvertAll(book => book.Title); }
        public List<string> EBookTitlesList { get => EBooks.ConvertAll(book => book.Title); }
        public List<string> AllBookTitlesList
        {
            get
            {
                List<string> _allBookTitlesList = new List<string>();
                _allBookTitlesList.AddRange(PhysicalBookTitlesList);
                _allBookTitlesList.AddRange(EBookTitlesList);
                return _allBookTitlesList;
            }
        }
        public long TotalBooksCount { get => PhysicalBooks.Count + EBooks.Count; }

        public LibManagementSystem()
        {
            PhysicalBooks = new List<PhysicalBook>();
            EBooks = new List<EBook>();

            // adding demo physical and online books
            // delete them afterwards
            for (int i = 1; i <= 3; i++)
            {
                EBooks.Add(new EBook(title: $"E-Book {i}", author: $"author{i}", downloadLink: $"https://bookstore.com/e-book/{i}"));
            }
            for (int i = 1; i <= 3; i++)
            {
                PhysicalBooks.Add(new PhysicalBook(title: $"Physical Book {i}", author: $"author{i}", shelfLocation: $"a{i}"));
            }
        }



        //methods
        public void CreatePhysicalBook()
        {
            Console.WriteLine($"\n{PHYSICAL_BOOK_CREATED_ACTION_TEXT}");

            Console.Write("Enter book title: ");
            string bookTitle = Console.ReadLine().Trim();

            Console.Write("Enter book author: ");
            string author = Console.ReadLine().Trim();

            Console.Write("Enter book shelfLocation: ");
            string shelfLocation = Console.ReadLine().Trim();

            // --- validations

            // check for existing book with same title
            bool bookExists = PhysicalBooks.Exists(book => book.Title.ToLower().Equals(bookTitle.ToLower()));

            if (bookExists)
            {
                Console.WriteLine($"[ALERT]: Physical book with title = '{bookTitle}' already exists in the system!!");
                return;
            }

            PhysicalBook newPhysicalBook = new PhysicalBook(title: bookTitle, author: author, shelfLocation: shelfLocation);

            PhysicalBooks.Add(newPhysicalBook);

            Console.WriteLine($"\n{newPhysicalBook}");
        }

        public void BorrowPhysicalBook()
        {
            Console.Write("Enter title of physical book to be borrowed: ");
            string bookTitle = Console.ReadLine().Trim();

            // if bookTitle is null, empty or whitespace then "book not found"
            if (string.IsNullOrEmpty(bookTitle) || string.IsNullOrWhiteSpace(bookTitle))
            {
                Console.WriteLine($"[ALERT]: Book with title '{bookTitle}' not found in the system!!");
                return;
            }

            // finding the physical book with title and making IsBorrowed = true
            PhysicalBook foundBook = PhysicalBooks.Find(book => book.Title.ToLower().Equals(bookTitle.ToLower()) && !book.IsBorrowed);

            // check if book doesn't exists
            if (Equals(foundBook, null))
            {
                Console.WriteLine($"[ALERT]: Book with title '{bookTitle}' has already been borrowed or not found in the system!!!!");
                return;
            }

            foundBook.IsBorrowed = true;
            Console.WriteLine($"[SUCCESS]: Book with title '{bookTitle}' has been successfully borrowed.");
        }

        public void ReturnPhysicalBook()
        {

        }

        public void CreateEBook()
        {
            Console.WriteLine($"\n{EBOOK_CREATED_ACTION_TEXT}");

            Console.Write("Enter book title: ");
            string title = Console.ReadLine();

            Console.Write("Enter book author: ");
            string author = Console.ReadLine();

            Console.Write("Enter book's downloadLink: ");
            string url = Console.ReadLine();

            EBook newEBook = new EBook(title: title, author: author, downloadLink: url);

            EBooks.Add(newEBook);

            Console.WriteLine($"\n{newEBook}");
        }

        public void BorrowEBook()
        {

        }

        public void ReturnEBook()
        {

        }

        public void ConsoleAllBookTitles()
        {
            if (AllBookTitlesList.Count == 0)
            {
                Console.WriteLine("[ALERT]: No book titles found!!");
                return;
            }

            Console.WriteLine("Book titles:");
            foreach (string title in AllBookTitlesList)
                Console.Write($"'{title}',\n");
        }
    }
}
