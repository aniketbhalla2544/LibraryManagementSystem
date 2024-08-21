using System;
using System.Collections.Generic;
using System.Linq;
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

        public static List<string> BookTypeNames = Enum.GetNames(typeof(BookType)).ToList();
        public string BookId { get => _bookId; }
        public string Title
        {
            get => _title;
            protected set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException($"Trying to set invalid book's title = '{value}'");
                _title = value.Trim().ToLower();
            }
        }
        public string Author
        {
            get => _author;
            protected set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException($"Trying to set invalid book's author name = '{value}'");
                _author = value.Trim();
            }
        }
        public string ISBN { get => _ISBN; }
        public bool IsBorrowed { get => _isBorrowed; protected set { _isBorrowed = value; } }
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
                $"\n\tauthor: '{Author}',\n\tISBN: '{ISBN}',\n\tisBorrowed: {IsBorrowed}," +
                $"\n\ttype: {Type}";
        }

        // uniqueness = title + author + type
        public override bool Equals(object obj)
        {
            if (obj is Book otherBook)
            {
                // check if both book's title, author and type are same or not
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

        public static bool SelectBookTypeUsingMenuSelector(out BookType result, string message = "Use the arrow keys to navigate and press Enter to select book type:")
        {
            string selectedBookTypeInput = MenuSelector.SelectOption(BookTypeNames, message);
            bool isValidSelectedBookType = Enum.TryParse(selectedBookTypeInput, false, out BookType validBookType);
            result = validBookType;
            return isValidSelectedBookType;
        }

        public void BorrowBook() => IsBorrowed = true;
    }
}
