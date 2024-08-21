using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models.Books;
using LibraryManagementSystem.Models.Member;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Services
{
    internal class LibManagementSystem
    {
        public HashSet<Member> Members { get; private set; } = new HashSet<Member>();
        public HashSet<Book> Books { get; private set; } = new HashSet<Book>();

        // drived class props
        public List<StudentMember> StudentMembers { get => Members.OfType<StudentMember>().ToList(); }
        public List<TeacherMember> TeacherMembers { get => Members.OfType<TeacherMember>().ToList(); }
        public List<PhysicalBook> PhysicalBooks { get => Books.OfType<PhysicalBook>().ToList(); }
        public List<EBook> EBooks { get => Books.OfType<EBook>().ToList(); }
        public long TotalStudentMembersCount { get => StudentMembers.Count; }
        public long TotalTeacherMembersCount { get => TeacherMembers.Count; }
        public long TotalMembersCount { get => TotalStudentMembersCount + TotalTeacherMembersCount; }
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
            // adding demo physical and online books
            // delete them afterwards
            //for (int i = 1; i <= 3; i++)
            //{
            //    EBooks.Add(new EBook(title: $"E-Book {i}", author: $"author{i}", downloadLink: $"https://bookstore.com/e-book/{i}"));
            //}
            //for (int i = 1; i <= 3; i++)
            //{
            //    PhysicalBooks.Add(new PhysicalBook(title: $"Physical Book {i}", author: $"author{i}", shelfLocation: $"a{i}"));
            //}
        }

        //methods
        // done
        public void RegisterMember()
        {
            // TODO: Validate member input details

            // member first name input
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine().Trim().ToLower();

            // validation: first name input
            if (Validator.IsStringNullOrEmptyOrWhitespace(firstName))
            {
                Console.WriteLine($"[Invalid Input]: member's firstname can't be empty, or only contains whitespace, entered value = '{firstName}'");
                return;
            }

            // member last name input
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine().Trim().ToLower();

            // validation: last name input
            if (Validator.IsStringNullOrEmptyOrWhitespace(lastName))
            {
                Console.WriteLine($"[Invalid Input]: member's lastName can't be empty, or only contains whitespace, entered value = '{lastName}'");
                return;
            }

            // member email input
            Console.Write("Enter email: ");
            string email = Console.ReadLine().Trim();

            // validation: email input
            if (!Validator.IsValidEmail(email))
            {
                Console.WriteLine($"[INVAID INPUT]: Received invalid email, entered value = '{email}'");
                return;
            }

            // taking valid member type input and validating it
            string memberTypeInput = MenuSelector.SelectOption(Member.MemberTypeNames, message: "Use the arrow keys to navigate and press Enter to select member type:");
            bool isValidMemberType = Enum.TryParse(memberTypeInput, true, out Member.MemberType memberType);
            if (!isValidMemberType)
            {
                Console.WriteLine("[ERROR]: Received invalid member type while registering member in the system.");
                return;
            }

            // registering members according to selected member type
            if (memberType == Member.MemberType.Student)
            {
                StudentMember newMember = new StudentMember(firstName, lastName, email);
                bool memberRegistered = Members.Add(newMember);
                if (!memberRegistered)
                {
                    Console.WriteLine($"[ALERT]: Student member with email = '{email.Trim().ToLower()}' has already been registered in the system!!");
                    return;
                }

                Console.WriteLine("[SUCCESS]: Student member successfully registered with following details: !!");
                Console.WriteLine(newMember);
                return;
            }
            else if (memberType == Member.MemberType.Teacher)
            {
                TeacherMember newMember = new TeacherMember(firstName, lastName, email);
                bool memberRegistered = Members.Add(newMember);
                if (!memberRegistered)
                {
                    Console.WriteLine($"[ALERT]: Teacher member with email = '{email.Trim().ToLower()}' has already been registered in the system!!");
                    return;
                }

                Console.WriteLine("[SUCCESS]: Teacher member successfully registered with following details: !!");
                Console.WriteLine(newMember);
                return;
            }
            else
            {
                Console.WriteLine("[ERROR]: Received invalid member type while registering member in the system.");
                return;
            }
        }

        public void AddBook()
        {
            // book title input
            Console.Write("Enter book title: ");
            string bookTitle = Console.ReadLine().Trim().ToLower();

            // validating book title
            if (Validator.IsStringNullOrEmptyOrWhitespace(bookTitle))
            {
                Console.WriteLine($"[Invalid Input]: book title can't be empty, or only contains whitespace, entered value = '{bookTitle}'");
                return;
            }

            // book author input
            Console.Write("Enter book author: ");
            string BookAuthor = Console.ReadLine().Trim().ToLower();

            // validating book author
            if (Validator.IsStringNullOrEmptyOrWhitespace(BookAuthor))
            {
                Console.WriteLine($"[Invalid Input]: book author can't be empty, or only contains whitespace, entered value = '{BookAuthor}'");
                return;
            }

            // book type input
            string selectedBookTypeInput = MenuSelector.SelectOption(Book.BookTypeNames, message: "Use the arrow keys to navigate and press Enter to select book type:");
            bool isValidBookType = Enum.TryParse(selectedBookTypeInput, out Book.BookType selectedBookType);
            if (!isValidBookType)
            {
                Console.WriteLine("[ERROR]: Received invalid book type while registering member in the system.");
                return;
            }

            // taking actions according to selected book type
            if (selectedBookType.Equals(Book.BookType.Physical))  // for physical book creation
            {
                // book shelfLocation input
                Console.Write("Enter book shelfLocation: ");
                string bookShelfLocation = Console.ReadLine().Trim();

                // validating book shelfLocation
                if (Validator.IsStringNullOrEmptyOrWhitespace(bookShelfLocation))
                {
                    Console.WriteLine($"[Invalid Input]: book shelfLocation can't be empty, or only contains whitespace, entered value = '{bookShelfLocation}'");
                    return;
                }

                Book newBook = new PhysicalBook(title: bookTitle, author: BookAuthor, shelfLocation: bookShelfLocation);
                bool bookAdded = Books.Add(newBook);

                // checking if book already exists in the "books" hashset, if not book added then it already exists
                if (!bookAdded)
                {
                    Console.WriteLine($"[ALERT]: Physical book with the following details already exists in the system!!");
                    Console.WriteLine(newBook);
                    return;
                }

                Console.WriteLine("[SUCCESS]: Physical book has been successfully added to the system!!");
                Console.WriteLine(newBook);
            }
            else if (selectedBookType.Equals(Book.BookType.Ebook))  // for e-book creation
            {
                // book download link input
                Console.Write("Enter book download link: ");
                string downloadLinkInput = Console.ReadLine().Trim();

                // validating book  download link, can't be empty or contains whitespace only
                if (Validator.IsStringNullOrEmptyOrWhitespace(downloadLinkInput))
                {
                    Console.WriteLine($"[Invalid Input]: book download link can't be empty, or only contains whitespace, entered value = '{downloadLinkInput}'");
                    return;
                }

                // validating book download link, URL validation
                bool isValidDownloadLink = Validator.IsValidURL(downloadLinkInput, out string downloadLink);
                if (!isValidDownloadLink)
                {
                    Console.WriteLine($"[Invalid Input]: book download link URL is invalid, entered value = '{downloadLinkInput}'");
                    return;
                }

                Book newBook = new EBook(title: bookTitle, author: BookAuthor, downloadLink: downloadLink);
                bool bookAdded = Books.Add(newBook);

                // checking if book already exists in the "books" hashset, if not book added then it already exists
                if (!bookAdded)
                {
                    Console.WriteLine($"[ALERT]: E-book with the following details already exists in the system!!");
                    Console.WriteLine(newBook);
                    return;
                }

                Console.WriteLine("[SUCCESS]: E-book has been successfully added to the system!!");
                Console.WriteLine(newBook);
            }
            else
            {
                Console.WriteLine("[ERROR]: Received invalid member type while registering member in the system.");
                return;
            }
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
