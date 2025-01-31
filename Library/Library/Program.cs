using System;
using Library.Entites;
using Library.Services;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryService libraryService = new LibraryService();
            User admin = new User("Admin", "adminestrator", Role.Admin );
            libraryService.RegisteredUsers.Add(admin);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("📚БИБЛИОТЕКА📚");
                Console.WriteLine("Регистрация");
            }
        }
    }
}

