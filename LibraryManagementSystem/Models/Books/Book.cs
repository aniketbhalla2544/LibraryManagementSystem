using System;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Models.Books
{
    internal class Book
    {
        protected const string NOT_ASSIGNED = "Not Assigned";

        protected const string BOOK_TYPE_PHYSICAL = "Physical Book";
        protected const string BOOK_TYPE_EBOOK = "E Book";
        protected const string BOOK_TYPE_NOT_ASSIGNED = NOT_ASSIGNED;

        string _bookId;
        string _title = NOT_ASSIGNED;
        string _author = NOT_ASSIGNED;
        string _ISBN = NOT_ASSIGNED; // 13 digit unique number
        bool _isBorrowed = false;
        string _type = BOOK_TYPE_NOT_ASSIGNED;

        public Book(string title, string author)
        {
            IntializeBookObject();
            Title = title;
            Author = author;
        }

        protected Book(string title, string author, string type)
        {
            IntializeBookObject();
            Title = title;
            Author = author;
            Type = FormatBookType(type);
        }

        public string BookId { get => _bookId; protected set => _bookId = !string.IsNullOrEmpty(value) ? value : _bookId; }
        public string Title { get => _title; protected set => _title = !string.IsNullOrEmpty(value) ? value : _title; }
        public string Author { get => _author; protected set => _author = !string.IsNullOrEmpty(value) ? value : _author; }
        public string ISBN { get => _ISBN; protected set => _ISBN = !string.IsNullOrEmpty(value) ? value : _ISBN; }
        public bool IsBorrowed { get => _isBorrowed; set { _isBorrowed = value; } }
        public string Type { get => _type; protected set => _type = IsValidBookType(value) ? FormatBookType(value) : BOOK_TYPE_NOT_ASSIGNED; }


        public override string ToString()
        {
            return $"Book details\n\tbook id: '{BookId}',\n\ttitle: '{Title}'," +
                $"\n\tauthor: '{Author}',\n\tISBN: '{ISBN}',\n\tis borrowed: {IsBorrowed}," +
                $"\n\ttype: {Type}";
        }


        void IntializeBookObject()
        {
            BookId = CustomUtils.GenerateUniqueID(0, 8);
            ISBN = CustomUtils.GenerateUniqueID();
        }

        protected bool IsValidBookType(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            string _formattedValue = value.Trim().ToLower();

            return _formattedValue.Equals(BOOK_TYPE_PHYSICAL.ToLower()) || _formattedValue.Equals(BOOK_TYPE_EBOOK.ToLower());
        }

        string FormatBookType(string value)
        {
            if (string.IsNullOrEmpty(value) || !IsValidBookType(value))
                throw new ArgumentException("Cannot format invalid book type value");

            string _formattedValue = value.Trim().ToLower();
            bool isPhysicalBookType = _formattedValue.Equals(BOOK_TYPE_PHYSICAL.ToLower());

            return isPhysicalBookType ? BOOK_TYPE_PHYSICAL : BOOK_TYPE_EBOOK;
        }
    }
}
