
namespace LibraryManagementSystem.Interfaces.Infrastrcture.Services
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
