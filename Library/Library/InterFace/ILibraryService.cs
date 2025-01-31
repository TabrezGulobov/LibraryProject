using Library.Entites;

namespace Library.InterFace
{
    public interface ILibraryService
    {
        bool Registration(string userName, string password); 
        bool Login(string userName, string password);
        void AddBook(string title, string author, string genre, int year);
        List<Book> SearchBooks(string Title, string Author, string Genre, int Year);
        void GetUserInfo(string userName);
    }
}


