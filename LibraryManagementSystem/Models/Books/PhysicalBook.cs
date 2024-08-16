namespace LibraryManagementSystem.Models.Books
{
    internal class PhysicalBook : Book
    {
        public string ShelfLocation { get; set; }

        public static long TotalPhysicalBooks = 0;
        public static long TotalBorrowedPhysicalBooks = 0;

        public PhysicalBook(string title, string author, string shelfLocation) : base(title, author, BOOK_TYPE_PHYSICAL)
        {
            ShelfLocation = shelfLocation;
            TotalPhysicalBooks++;
        }

        // methods
        public override string ToString()
        {
            return base.ToString() +
                $"\n\tShelf Location: {ShelfLocation}";
        }

        public override void BorrowBook()
        {
            base.BorrowBook();
            TotalBorrowedPhysicalBooks++;
        }

        public override void ReturnBook()
        {
            base.ReturnBook();
            TotalBorrowedPhysicalBooks--;
        }
    }
}
