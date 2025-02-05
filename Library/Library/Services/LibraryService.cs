using System;
using System.Collections.Generic;
using Library.Entities;
using Library.Interfaces;

namespace Library.Services
{
    public class LibraryService : ILibraryService
    {
        public List<User> RegisteredUsers { get; set; } = new List<User>();
        public static User CurrentUser { get; set; } = null;
        public List<Book> Books { get; set; } = new List<Book>();

       
        public bool Registration(string username, string password)
        {
            bool userExists = false;
            foreach (var user in RegisteredUsers)
            {
                if (user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase))
                {
                    userExists = true;
                    break;
                }
            }
            if (userExists)
            {
                Console.WriteLine("Пользователь с таким именем уже занят ❌");
                return false;
            }

            var newUser = new User(username, password, Role.User);
            RegisteredUsers.Add(newUser);
            Console.WriteLine($"✅ Пользователь {username} успешно зарегистрирован.");
            return true;
        }
        
        public bool Login(string username, string password)
        {
            User foundUser = null;
            foreach (var user in RegisteredUsers)
            {
                if (user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                    user.Password == password)
                {
                    foundUser = user;
                    break;
                }
            }

            if (foundUser != null)
            {
                CurrentUser = foundUser;
                Console.WriteLine($"Добро пожаловать, {username}❕");
                return true;
            }
            else
            {
                Console.WriteLine("Неверный логин или пароль 🤷");
                return false;
            }
        }

        
        public void ListUsers()
        {
            if (CurrentUser == null || CurrentUser.UserRole != Role.Admin)
            {
                Console.WriteLine("❌ Доступ запрещён. Только администратор может просматривать список пользователей.");
                return;
            }

            Console.WriteLine("Список зарегистрированных пользователей:");
            foreach (var user in RegisteredUsers)
            {
                Console.WriteLine($"- {user.UserName} ({user.UserRole})");
            }
        }
        
        public bool DeleteUser(string username)
        {
            if (CurrentUser == null || CurrentUser.UserRole != Role.Admin)
            {
                Console.WriteLine("❌ Доступ запрещён. Только администратор может удалять пользователей.");
                return false;
            }

            User userToDelete = null;
            foreach (var user in RegisteredUsers)
            {
                if (user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase))
                {
                    userToDelete = user;
                    break;
                }
            }

            if (userToDelete == null)
            {
                Console.WriteLine("❌ Пользователь не найден.");
                return false;
            }

            if (userToDelete.UserRole == Role.Admin)
            {
                Console.WriteLine("❌ Нельзя удалить администратора.");
                return false;
            }

            RegisteredUsers.Remove(userToDelete);
            Console.WriteLine($"✅ Пользователь {username} успешно удалён.");
            return true;
        }

        
        public void AddBook(string title, string author, string genre, int year)
        {
            var newBook = new Book(title, author, genre, year);
            Books.Add(newBook);
            Console.WriteLine($"📚 Книга '{title}' добавлена в библиотеку.");
        }

        
        public List<Book> SearchBooks(string title, string author, string genre, int year)
        {
            List<Book> filteredBooks = new List<Book>();

            foreach (var book in Books)
            {
                bool matchesTitle = string.IsNullOrEmpty(title) ||
                    book.Title.Equals(title, StringComparison.OrdinalIgnoreCase);
                bool matchesAuthor = string.IsNullOrEmpty(author) ||
                    book.Author.Equals(author, StringComparison.OrdinalIgnoreCase);
                bool matchesGenre = string.IsNullOrEmpty(genre) ||
                    book.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase);
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

            return filteredBooks;
        }
        
        public void GetBookStatus(string title)
        {
            Book foundBook = null;
            foreach (var book in Books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    foundBook = book;
                    break;
                }
            }

            if (foundBook == null)
            {
                Console.WriteLine("❌ Книга не найдена.");
                return;
            }

            if (foundBook.IsAvailable)
            {
                Console.WriteLine($"✅ Книга '{foundBook.Title}' доступна.");
            }
            else
            {
                Console.WriteLine($"❗ Книга '{foundBook.Title}' выдана пользователю {foundBook.Borrower.UserName}. " +
                                  $"Срок возврата: {foundBook.DueDate?.ToString("dd.MM.yyyy") ?? "не установлен"}");
            }
        }

        
        public List<Book> SortAndFilterBooks(string genre = "")
        {
            List<Book> filteredBooks = new List<Book>();
            
            foreach (var book in Books)
            {
                if (string.IsNullOrEmpty(genre) || book.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                {
                    filteredBooks.Add(book);
                }
            }
            
            filteredBooks.Sort((a, b) => string.Compare(a.Title, b.Title, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("📚 Список книг:");
            foreach (var book in filteredBooks)
            {
                string status = book.IsAvailable
                    ? "Доступна"
                    : $"Выдана ({book.Borrower.UserName}, до {book.DueDate?.ToString("dd.MM.yyyy")})";
                Console.WriteLine($"📖 {book.Title} - {book.Author} ({book.Genre}, {book.Year}) - {status}");
            }

            return filteredBooks;
        }
        
        public void BorrowBook(string title, int days)
        {
            if (CurrentUser == null)
            {
                Console.WriteLine("❌ Зарегистрируйтесь или выполните вход, чтобы взять книгу.");
                return;
            }

            Book foundBook = null;
            foreach (var book in Books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    foundBook = book;
                    break;
                }
            }

            if (foundBook == null)
            {
                Console.WriteLine("❌ Книга не найдена в библиотеке.");
                return;
            }

            if (!foundBook.IsAvailable)
            {
                Console.WriteLine("❌ Книга уже выдана другому пользователю.");
                return;
            }

            foundBook.IsAvailable = false;
            foundBook.Borrower = CurrentUser;
            foundBook.DueDate = DateTime.Now.AddDays(days);
            Console.WriteLine($"✅ Вы успешно взяли книгу '{foundBook.Title}'. " +
                              $"Срок возврата: {foundBook.DueDate?.ToString("dd.MM.yyyy")}");
        }
        
        public void ReturnBook(string title)
        {
            if (CurrentUser == null)
            {
                Console.WriteLine("❌ Зарегистрируйтесь или выполните вход, чтобы вернуть книгу.");
                return;
            }

            Book foundBook = null;
            foreach (var book in Books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && book.Borrower == CurrentUser)
                {
                    foundBook = book;
                    break;
                }
            }

            if (foundBook == null)
            {
                Console.WriteLine("❌ Книга не найдена или не принадлежит вам.");
                return;
            }

            if (foundBook.DueDate.HasValue && DateTime.Now > foundBook.DueDate.Value)
            {
                int overdueDays = (DateTime.Now - foundBook.DueDate.Value).Days;
                decimal fine = overdueDays * 1; // Штраф 1 у.е. за день
                Console.WriteLine($"⚠ Книга просрочена на {overdueDays} дн. Ваш штраф: {fine} у.е.");
            }
            else
            {
                Console.WriteLine("✅ Книга возвращена вовремя.");
            }

            foundBook.IsAvailable = true;
            foundBook.Borrower = null;
            foundBook.DueDate = null;
        }
    }
}
