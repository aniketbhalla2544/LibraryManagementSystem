
namespace LibraryManagementSystem.Interfaces.Infrastrcture.Models
{
    internal interface ILibraryService
    {
        void RegisterMember();
        void AddBook();
        void BorrowBook();
        void ReturnBook();
        void ConsoleAllBookTitles();
    }
}
