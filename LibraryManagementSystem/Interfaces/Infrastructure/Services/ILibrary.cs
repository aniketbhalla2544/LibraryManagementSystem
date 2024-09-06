
namespace LibraryManagementSystem.Interfaces.Infrastrcture.Services
{
    internal interface ILibrary
    {
        void RegisterMember();
        void AddBook();
        void BorrowBook();
        void ReturnBook();
        void ConsoleAllBookTitles();
    }
}
