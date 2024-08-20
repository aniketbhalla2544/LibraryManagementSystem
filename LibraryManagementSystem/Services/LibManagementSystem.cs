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
        // done
        public void RegisterMember()
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter member type, 'Teacher' or 'Student': ");
            string selectedOption = MenuSelector.SelectOption(Member.MemberTypeNames);
            Console.WriteLine($"selectedOption: {selectedOption}");


            return;

            string memberType = Console.ReadLine();
            bool isValidMemberType = Member.IsValidMemberType(memberType.Trim().ToLower());
            if (!isValidMemberType)
            {
                Console.WriteLine($"[INVALID INPUT]: Invalid member type entered");
                return;
            }
            Console.WriteLine("sucess!!");

            //string lastName, string email
            StudentMember newMember = new StudentMember(firstName, lastName, email);
            StudentMembers.Add(newMember);

            Console.WriteLine("[SUCCESS]: Student member successfully registered with following details: !!");
            Console.WriteLine($"{newMember}");
        }

        // done
        public void RegisterTeacherMember()
        {

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            //string lastName, string email
            TeacherMember newMember = new TeacherMember(firstName, lastName, email);
            TeacherMembers.Add(newMember);

            Console.WriteLine("[SUCCESS]: Student member successfully registered with following details: !!");
            Console.WriteLine($"{newMember}");
        }

        // done
        public void CreatePhysicalBook()
        {
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

        // done
        public void CreateEBook()
        {
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
