using System;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Models.Books
{
    // uniqueness = title + author + type
    internal abstract class Book
    {
        public enum BookType
        {
            Physical,
            Ebook
        }

        readonly string _bookId = CustomUtils.GenerateUniqueID(0, 8);
        readonly string _ISBN = CustomUtils.GenerateUniqueID(); // 13 digit unique number
        string _title = string.Empty; 
        string _author = string.Empty;
        bool _isBorrowed = false;
        BookType _type;

        public string BookId { get => _bookId; }
        public string Title
        {
            get => _title; 
            protected set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException($"Trying to set invalid book's title = '{value}'");
                _title = value.Trim().ToUpperInvariant();
            }
        }
        public string Author
        {
            get => _author;
            protected set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException($"Trying to set invalid book's author name = '{value}'");
                _title = value.Trim().ToUpperInvariant();
            }
        }
        public string ISBN { get => _ISBN; }
        public bool IsBorrowed { get => _isBorrowed; set { _isBorrowed = value; } }
        public BookType Type { get => _type; protected set => _type = value; }
     
        protected Book(string title, string author, BookType type)
        {
            Title = title;
            Author = author;
            Type = type;
        }

        public override string ToString()
        {
            return $"Book details\n\tbook id: '{BookId}',\n\ttitle: '{Title}'," +
                $"\n\tauthor: '{Author}',\n\tISBN: '{ISBN}',\n\tis borrowed: {IsBorrowed}," +
                $"\n\ttype: {Type}";
        }

        // uniqueness = title + author + type
        public override bool Equals(object obj)
        {
            if (obj is Book otherBook)
            {
                return otherBook.Title.Equals(Title) && otherBook.Author.Equals(Author) && otherBook.Type == Type;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hashTitle = Title.GetHashCode();
            int hashAuthor = Author.GetHashCode();
            int hashType = Type.GetHashCode();

            return hashTitle ^ hashAuthor ^ (hashType * 17);
        }
    }
}
