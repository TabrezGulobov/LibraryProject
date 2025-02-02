using System;
using Library.Entites;
using Library.Services;

namespace Library
{
    class Program
    {
        static  void Main(string[] args)
        {
            LibraryService libraryService = new LibraryService();
            User admin = new User("Admin", "admin", Role.Admin );
            libraryService.RegisteredUsers.Add(admin);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("📚БИБЛИОТЕКА📚");
                Console.WriteLine("1. Регистрация📝 ");
                Console.WriteLine("2. Вход🔑");
                Console.WriteLine("3. Добавить книгу (Только админ)➕📚");
                Console.WriteLine("4. Найти книгу🔍📚");
                Console.WriteLine("5. Взять книгу🤝");
                Console.WriteLine("6. Вернуть книгу🔙📚");
                Console.WriteLine("7. Выйти🚪➡️");
                Console.Write("Выберите действие:");
                
                string choice = Console.ReadLine();
                


                string genre;
                string author;
                string title;
                switch (choice)
                {
                    case "1":
                        Console.Write("Введите имя пользователя: ");
                        string regUsername = Console.ReadLine();
                        Console.Write("Введите пароль: ");
                        string regPassword = Console.ReadLine();
                        if (libraryService.Registration(regUsername, regPassword))
                        {
                            Console.WriteLine("Регистрация успешна!");
                        }

                        break;


                    case "2":
                        Console.Write("Введите имя пользователя: ");
                        string loginUsername = Console.ReadLine();
                        Console.Write("Введите пароль: ");
                        string loginPassword = Console.ReadLine();

                        if (libraryService.Login(loginUsername, loginPassword))
                        {
                            if (LibraryService.CurrentUser != null && LibraryService.CurrentUser.UserRole == Role.Admin)
                            {
                                Console.WriteLine($"Добро пожаловать, администратор {loginUsername}!");
                            }
                            else if (LibraryService.CurrentUser != null)
                            {
                                Console.WriteLine($"Добро пожаловать, {loginUsername}!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверное имя пользователя или пароль.");
                        }

                        break;

                    case "3":
                        if (LibraryService.CurrentUser?.UserRole != Role.Admin)
                        {
                            Console.WriteLine("❌ Доступ запрещен.");
                            break;
                        }

                        Console.Write("Название книги: ");
                        title = Console.ReadLine();
                        Console.Write("Автор: ");
                        author = Console.ReadLine();
                        Console.Write("Жанр: ");
                        genre = Console.ReadLine();
                        Console.Write("Год: ");
                        int year = int.Parse(Console.ReadLine());
                        libraryService.AddBook(title, author, genre, year);
                        break;
                       
                    case "4": 
                        Console.Write("Введите название книги: "); 
                        title = Console.ReadLine(); 
                        Console.Write("Введите автора книги: ");
                        author = Console.ReadLine();
                        Console.Write("Введите жанр книги: ");
                        genre = Console.ReadLine();
                        Console.Write("Введите год издания книги: ");
                        int.TryParse(Console.ReadLine(), out int Year);

                        libraryService.SearchBooks(title, author, genre, Year);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Действие не найденно");
                        break;
                }
            }
        }
    }
}

