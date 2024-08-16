using System;
using System.Reflection;
using System.Runtime.InteropServices;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Models.Books
{
    internal class EBook : Book
    {
        string _downloadLink = string.Empty;
        public static long TotalEBooks = 0;
        public static long TotalBorrowedEBooks = 0;

        public EBook(string title, string author, string downloadLink) : base(title, author, BOOK_TYPE_EBOOK)
        {
            DownloadLink = downloadLink;
            TotalEBooks++;
        }

        // class props
        public string DownloadLink { get => _downloadLink; protected set => _downloadLink = ValidateURL(value); }

        // methods
        public override string ToString()
        {
            return base.ToString() +
                $"\n\tdownload link: {DownloadLink}";
        }

        public override void BorrowBook()
        {
            base.BorrowBook();
            TotalBorrowedEBooks++;
        }

        public override void ReturnBook()
        {
            base.ReturnBook();
            TotalBorrowedEBooks--;
        }

        static string ValidateURL(string url)
        {
            if (CustomUtils.IsValidURL(url, out string result))
                return result;

            Console.WriteLine($"Invalid ebook url: {url}");

            throw new ArgumentException("Invalid URL can't be set as a download link of an Ebook");
        }

    }
}
