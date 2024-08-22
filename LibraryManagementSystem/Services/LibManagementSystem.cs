using System;
using System.Collections.Generic;
using System.IO;
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
            bool memberTypeSelection = Member.SelectMemberTypeUsingMenuSelector(out Member.MemberType memberType);
            if (!memberTypeSelection)
            {
                Console.WriteLine("[ERROR]: Error while selecting member type");
                return;
            }

            // registering members according to selected member type
            if (memberType == Member.MemberType.Student)
            {
                StudentMember newMember = new StudentMember(firstName, lastName, email);
                bool memberRegistered = Members.Add(newMember);
                if (!memberRegistered)
                {
                    Console.WriteLine($"[ALERT]: member with email = '{email.Trim().ToLower()}' has already been registered in the system!!");
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
                    Console.WriteLine($"[ALERT]: member with email = '{email.Trim().ToLower()}' has already been registered in the system!!");
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

        // bool to check if some kind of error while finding member or if member exists or not
        bool FindMemberByEmail(string email, out Member result)
        {
            bool operationSuccess = false;
            result = null;

            if (!Validator.IsValidEmail(email))
                return operationSuccess;

            email = email.Trim();

            // checking if member exists
            Member member = Members.FirstOrDefault(m => m.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (!(member == null))
            {
                operationSuccess = true;
                result = member;
            }

            return operationSuccess;
        }

        // done
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
            bool bookTypeSelectionSuccess = Book.SelectBookTypeUsingMenuSelector(out Book.BookType selectedBookType);

            if (!bookTypeSelectionSuccess)
            {
                Console.WriteLine("[ERROR]: Error while book type selection.");
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

        /*
             *  Flow: 
             *  - input member email with email validation, if member exists then book can be borowed and if not, then not
             *  - if member exists, ask book details
             *  -   input title, author and type with their validation
             *      - check if book exists
             *          -   if exists,
             *              -   check if book has already been borrowed or not
             *                  - if yes
             *                      - alert user
             *                  - if not
             *                      - borrow book method on book
             *                      - add book id to the borrowed books list of member
             *          -   if not, then alert user
        */
        public void BorrowBook()
        {
            // member email input
            Console.Write("Enter member email: ");
            string memberEmail = Console.ReadLine().Trim();

            // validation: member email input
            if (!Validator.IsValidEmail(memberEmail))
            {
                Console.WriteLine($"[INVALID INPUT]: Received invalid email, entered value = '{memberEmail}'");
                return;
            }

            // checking if member exists
            bool memberExists = FindMemberByEmail(memberEmail, out Member member);
            if (!memberExists)
            {
                Console.WriteLine($"[NOT FOUND ERROR]: Operation failed because member with email = '{memberEmail}' doesn't exists in the system");
                Console.WriteLine("[SYSTEM SUGGESTION]: Signup by creating a new member in the system");
                return;
            }

            // asking book details
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
            string bookAuthor = Console.ReadLine().Trim().ToLower();

            // validating book author
            if (Validator.IsStringNullOrEmptyOrWhitespace(bookAuthor))
            {
                Console.WriteLine($"[Invalid Input]: book author can't be empty, or only contains whitespace, entered value = '{bookAuthor}'");
                return;
            }

            // book type input
            bool bookTypeSelectionSuccess = Book.SelectBookTypeUsingMenuSelector(out Book.BookType selectedBookType);

            // system error
            if (!bookTypeSelectionSuccess)
            {
                Console.WriteLine("[ERROR]: Error while book type selection.");
                return;
            }

            // checking if book exists with given book details 
            Book book = Books.FirstOrDefault(_book => _book.Title.Equals(bookTitle) && _book.Author.Equals(bookAuthor) && _book.Type.Equals(selectedBookType) && !_book.IsBorrowed);
            if (book == null)
            {
                Console.WriteLine("[NOT FOUND]: book with entered details doesn't exist in the system!!");
                return;
            }

            // check if book has already been borrowed
            if (book.IsBorrowed)
            {
                Console.WriteLine("[ALERT]: book with entered details has already been borrowed!!");
                return;
            }

            // Add book to the 'borrowed books list' of member
            bool memberBorrowBookOperationSuccess = member.BorrowBook(bookId: book.BookId, out bool validationError);
            if (!memberBorrowBookOperationSuccess)
            {
                if (validationError)
                {
                    Console.WriteLine("[SYSTEM ERROR]: Error while borrowing book to member");
                    return;
                }
                Console.WriteLine("[ALERT]: Book with given details has already been borrowed by the member");
                return;
            }

            // set book isBorrowed to true 
            book.BorrowBook();

            Console.WriteLine($"[SUCCESS]: Book with title: '{bookTitle}' has been successfully borrowed by member with name: '{member.Name}' and email: '{member.Email}'");
        }

        public void ReturnPhysicalBook()
        {

        }

        public void ReturnEBook()
        {

        }

        // done
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
