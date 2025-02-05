using System.Collections.Generic;
using Library.Entities;

namespace Library.Interfaces
{
    public interface ILibraryService
    {
        bool Registration(string userName, string password);
        bool Login(string userName, string password);
        void ListUsers();
        bool DeleteUser(string userName); 
        void AddBook(string title, string author, string genre, int year);
        List<Book> SearchBooks(string title, string author, string genre, int year);
        void GetBookStatus(string title);
        List<Book> SortAndFilterBooks(string genre = "");
        void BorrowBook(string title, int days);
        void ReturnBook(string title);
    }
}