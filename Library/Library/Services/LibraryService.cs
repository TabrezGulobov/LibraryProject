using System;
using Library.Entites;
using Library.InterFace;

namespace Library.Services;

public class LibraryService
{
    public List<User> RegisteredUsers { get; set; } = new List<User>();
    public User CurrentUser { get; set; } = null;
    public List<Book> Books { get; set; } = new List<Book>();
    public bool Registration(string username, string password)
    {
        if (RegisteredUsers.Any(user => user.UserName == username))
        {
            Console.WriteLine("❌ Пользователь с таким именем уже существует.❌");
            return false;
        }

        var newUser = new User(username, password, Role.User);
        RegisteredUsers.Add(newUser);
        Console.WriteLine($"✅Пользователь {username} успешно зарегистрирован.✅");
        return true;
    }
    public bool Login(string username, string password)
    {
        var user = RegisteredUsers.FirstOrDefault(user => user.UserName == username && user.Password == password);
        if (user == null)
        {
            Console.WriteLine("Неверное имя пользователя или пароль ❗");
            return false;
        }

        CurrentUser = user;
        Console.WriteLine($"Добро пожаловать, {username} ❕");
        return true;
    }
    public void GetAccountInfo(string userName)
    {
        var user = RegisteredUsers.FirstOrDefault(u => u.UserName == userName);
        if (user == null)
        {
            Console.WriteLine("Пользователь не найден.🤷");
            return;
        }
        
    }
    public void AddBook(string title, string author, string genre, int year)
    { 
        var newBook = new Book(title, author, genre, year); 
        Books.Add(newBook); 
        Console.WriteLine($"📚 Книга {title} добавлена в библиотеку.");
    }
    
    public List<Book> SearchBooks(string Title, string Author, string Genre, int Year)
    {
        string choosenGenre = Genre;
        string choosenTitle = Title;
        string choosenAuthor = Author;
        int choosenYear = Year;
        
        List<Book> filteredBooks = new List<Book>();
        foreach (var book in Books)
        {
            if (book.Title == choosenTitle && book.Genre == choosenGenre && book.Author == choosenAuthor)
            {
                filteredBooks.Add(book);
            }
            Console.WriteLine($"Вот похожие варианты по вашему вопросу {book.Title}, {book.Genre}, {book.Author} 📚");
        }
        return Books;
    }
}   