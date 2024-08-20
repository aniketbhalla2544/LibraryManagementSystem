namespace LibraryManagementSystem.Models.Books
{
    internal class PhysicalBook : Book
    {
        public string ShelfLocation { get; set; }

        public PhysicalBook(string title, string author, string shelfLocation) : base(title, author, BookType.Physical)
        {
            ShelfLocation = shelfLocation;
        }

        public int CalculatePhysicalBookWeight() => 12;
       
        public override string ToString()
        {
            return base.ToString() +
                $"\n\tShelf Location: {ShelfLocation}";
        }
    }
}
