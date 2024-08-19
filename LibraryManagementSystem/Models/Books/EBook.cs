using System;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Models.Books
{
    internal class EBook : Book
    {
        string _downloadLink = string.Empty;
        
        public EBook(string title, string author, string downloadLink) : base(title, author, BookType.Ebook)
        {
            DownloadLink = downloadLink;
        }

        // class props
        public string DownloadLink { get => _downloadLink; protected set => _downloadLink = ValidateURL(value); }

        // methods
        public override string ToString()
        {
            return base.ToString() +
                $"\n\tdownload link: {DownloadLink}";
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
