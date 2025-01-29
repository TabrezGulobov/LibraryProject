namespace Library.InterFace
{
    public interface ILibraryService
    {
        bool Registration(string userName, string password); 
        bool Login(string userName, string password);
        void Book(Guid UserId, string username);
        void GetUserInfo(string userName);
    }
}


