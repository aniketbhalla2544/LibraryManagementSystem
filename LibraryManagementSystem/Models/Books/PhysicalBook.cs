namespace LibraryManagementSystem.Models.Books
{
    /*
     * TODO: Add book copies
    */
    internal class PhysicalBook : Book
    {
        public string ShelfLocation { get; set; } = string.Empty;

        public PhysicalBook(string title, string author, string shelfLocation) : base(title, author, BookType.Physical)
        {
            ShelfLocation = shelfLocation;
        }

        public int CalculatePhysicalBookWeight() => 12;

        public override string ToString()
        {
            return base.ToString() +
                $"\n\tShelf-location: '{ShelfLocation}'";
        }
    }
}
