using System;
using Library.Entities;
using Library.Services;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryService libraryService = new LibraryService(); 
            User admin = new User("Admin", "admin", Role.Admin);
            libraryService.RegisteredUsers.Add(admin);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("📚 БИБЛИОТЕКА 📚");
                Console.WriteLine("1. Регистрация 📝");
                Console.WriteLine("2. Вход 🔑");
                Console.WriteLine("3. Добавить книгу (только админ) ➕📚");
                Console.WriteLine("4. Поиск книги 🔍📚");
                Console.WriteLine("5. Выдать книгу 🤝");
                Console.WriteLine("6. Вернуть книгу 🔙📚");
                Console.WriteLine("7. Список пользователей (админ) 👥");
                Console.WriteLine("8. Удалить пользователя (админ) ❌");
                Console.WriteLine("9. Просмотр статуса книги");
                Console.WriteLine("10. Сортировка/фильтрация книг");
                Console.WriteLine("0. Выход 🚪");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите имя пользователя: ");
                        string regUsername = Console.ReadLine();
                        Console.Write("Введите пароль: ");
                        string regPassword = Console.ReadLine();
                        libraryService.Registration(regUsername, regPassword);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Write("Введите имя пользователя: ");
                        string loginUsername = Console.ReadLine();
                        Console.Write("Введите пароль: ");
                        string loginPassword = Console.ReadLine();
                        libraryService.Login(loginUsername, loginPassword);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "3":
                        if (LibraryService.CurrentUser?.UserRole != Role.Admin)
                        {
                            Console.WriteLine("❌ Доступ запрещён.");
                        }
                        else
                        {
                            Console.Write("Название книги: ");
                            string title = Console.ReadLine();
                            Console.Write("Автор: ");
                            string author = Console.ReadLine();
                            Console.Write("Жанр: ");
                            string genre = Console.ReadLine();
                            Console.Write("Год: ");
                            int year = int.Parse(Console.ReadLine());
                            libraryService.AddBook(title, author, genre, year);
                        }
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Write("Введите название книги (или оставьте пустым): ");
                        string searchTitle = Console.ReadLine();
                        Console.Write("Введите автора (или оставьте пустым): ");
                        string searchAuthor = Console.ReadLine();
                        Console.Write("Введите жанр (или оставьте пустым): ");
                        string searchGenre = Console.ReadLine();
                        Console.Write("Введите год (или 0 для пропуска): ");
                        int.TryParse(Console.ReadLine(), out int searchYear);
                        libraryService.SearchBooks(searchTitle, searchAuthor, searchGenre, searchYear);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "5":
                        Console.Write("Введите название книги, которую хотите взять: ");
                        string borrowTitle = Console.ReadLine();
                        Console.Write("На сколько дней взять книгу: ");
                        int.TryParse(Console.ReadLine(), out int days);
                        libraryService.BorrowBook(borrowTitle, days);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "6":
                        Console.Write("Введите название книги для возврата: ");
                        string returnTitle = Console.ReadLine();
                        libraryService.ReturnBook(returnTitle);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "7":
                        libraryService.ListUsers();
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "8":
                        Console.Write("Введите имя пользователя для удаления: ");
                        string deleteUsername = Console.ReadLine();
                        libraryService.DeleteUser(deleteUsername);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "9":
                        Console.Write("Введите название книги для проверки статуса: ");
                        string statusTitle = Console.ReadLine();
                        libraryService.GetBookStatus(statusTitle);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "10":
                        Console.Write("Введите жанр для фильтрации (или оставьте пустым для вывода всех): ");
                        string filterGenre = Console.ReadLine();
                        libraryService.SortAndFilterBooks(filterGenre);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Действие не найдено.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
