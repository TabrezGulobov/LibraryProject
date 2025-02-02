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
            if (user.UserName == username) // Сравнение без Equals
            {
                Console.WriteLine("Пользователь с таким именем уже занят ❌");
                return false;
            }
        }

        var newUser = new User(username, password, Role.User);
        RegisteredUsers.Add(newUser);
        Console.WriteLine($"✅ Пользователь {username} успешно зарегистрирован.");
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

    public void SearchBooks(string title, string author, string genre, int year)
    {
        List<Book> filteredBooks = new List<Book>();

        foreach (var book in Books)
        {
            bool matchesTitle = string.IsNullOrEmpty(title) || book.Title.Equals(title, StringComparison.OrdinalIgnoreCase);
            bool matchesAuthor = string.IsNullOrEmpty(author) || book.Author.Equals(author, StringComparison.OrdinalIgnoreCase);
            bool matchesGenre = string.IsNullOrEmpty(genre) || book.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase);
            bool matchesYear = (year == 0 || book.Year == year);

            if (matchesTitle && matchesAuthor && matchesGenre && matchesYear)
            {
                filteredBooks.Add(book);
            }
        }

        if (filteredBooks.Count == 0)
        {
            Console.WriteLine("❌ Такой книги нет в нашей библиотеке.");
        }
        else
        {
            Console.WriteLine("📚 Найденные книги:");
            foreach (var book in filteredBooks)
            {
                Console.WriteLine($"📖 {book.Title} - {book.Author} ({book.Genre}, {book.Year})");
            }
        }
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
    public bool IsAdmin()
    {
        return CurrentUser != null && CurrentUser.UserRole == Role.Admin;
    }

}