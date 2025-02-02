using System;
using System.Collections;
using Library.Entites;
using Library.InterFace;

namespace Library.Services;

public class LibraryService
{
    public List<User> RegisteredUsers { get; set; } = new List<User>();
    public static User CurrentUser { get; set; } = null;
    public List<Book> Books { get; set; } = new List<Book>();

    public bool Registration(string username, string password)
    {
        foreach (var user in RegisteredUsers)
        {
            if (user.UserName == username && user.Password == password)
            {
                Console.WriteLine("Пользователть с таким именем уже занят");
                return false;
            }
        }
        var newUser = new User(username, password, Role.User);
        RegisteredUsers.Add(newUser);
        Console.WriteLine($"Пользователь {username} успешно зарегистрирован.");
        return true;
    }


    public bool Login(string username, string password)
    {
        foreach (var user in RegisteredUsers)
        {
            if (user.UserName == username && user.Password == password)
            {
                Console.WriteLine($"Добро пожаловать, {username}❕");
                return true;
            }
            else
            {
                Console.WriteLine("Неверны логин или пароль🤷");
                return false;
            }
        }

        return true;
    }

    public void GetAccountInfo(string userName)
    {
        foreach (var user in RegisteredUsers)
        {
            if (user.UserName == userName)
            {
                Console.WriteLine($"Информация о пользователе: Имя: {userName}, Пароль: {user.Password}, Роль:{user.UserRole}");
            }
            else
            {
                Console.WriteLine("Пользователь с таким именем не найден 🤷");
            }
        }
    }

    public void AddBook(string title, string author, string genre, int year)
    {
        var newBook = new Book(title, author, genre, year);
        Books.Add(newBook);
        Console.WriteLine($"📚 Книга '{title}' добавлена в библиотеку.");
    }

    public bool SearchBooks(string Title, string Author, string Genre, int Year)
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

        return true;
    }

    public void BorrowBook(string title, int days)
    {
        if (CurrentUser == null)
        {
            Console.WriteLine("❌ Зарегестрируйтесь, чтобы взять книгу.");
            return;
        }

        foreach (var book in Books)
        {
            if (book.Title == title)
            {
                Console.WriteLine($"Книга '{title}' есть в нашей библиотеке ❕");       
            }
            else
            {
                Console.WriteLine("❌ Книга не найдена в библиотеке или уже взята другим пользователем");
            }
        }
        
    }

    public void ReturnBook(string title)
    {
        if (CurrentUser == null)
        {
            Console.WriteLine("❌ Зарегестрируйтесь, чтобы вернуть книгу библиотеке.");
            return;
        }

        var book = Books.FirstOrDefault(b =>
            b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && b.Borrower == CurrentUser);
        if (book == null)
        {
            Console.WriteLine("❌ Книга не найдена или не принадлежит вам.");
            return;
        }

    }
    
}