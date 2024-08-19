using System;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Models.Books
{
    internal abstract class Book
    {
        public enum BookType
        {
            None = -1,
            Physical,
            Ebook
        }

        string _bookId;
        string _title = string.Empty;
        string _author = string.Empty;
        string _ISBN = string.Empty; // 13 digit unique number
        bool _isBorrowed = false;
        BookType _type = BookType.None;

        protected Book(string title, string author, BookType type)
        {
            BookId = CustomUtils.GenerateUniqueID(0, 8);
            ISBN = CustomUtils.GenerateUniqueID();
            Title = title;
            Author = author;
            Type = type;
        }

        public string BookId { get => _bookId; protected set => _bookId = !string.IsNullOrEmpty(value) ? value : _bookId; }
        public string Title { get => _title; protected set => _title = !string.IsNullOrEmpty(value) ? value : _title; }
        public string Author { get => _author; protected set => _author = !string.IsNullOrEmpty(value) ? value : _author; }
        public string ISBN { get => _ISBN; protected set => _ISBN = !string.IsNullOrEmpty(value) ? value : _ISBN; }
        public bool IsBorrowed { get => _isBorrowed; set { _isBorrowed = value; } }
        public BookType Type
        {
            get => _type;
            protected set
            {
                if (value.Equals(BookType.None))
                    throw new ArgumentException("Book cann't be created with type 'None'");
                _type = value;
            }
        }


        public override string ToString()
        {
            return $"Book details\n\tbook id: '{BookId}',\n\ttitle: '{Title}'," +
                $"\n\tauthor: '{Author}',\n\tISBN: '{ISBN}',\n\tis borrowed: {IsBorrowed}," +
                $"\n\ttype: {Type}";
        }
    }
}
