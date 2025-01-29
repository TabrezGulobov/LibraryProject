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
            Console.WriteLine("Пользователь с таким именем уже существует.");
            return false;
        }

        var newUser = new User(username, password, Role.User);
        RegisteredUsers.Add(newUser);
        Console.WriteLine($"Пользователь {username} успешно зарегистрирован.");
        return true;
    }

    public bool Login(string username, string password)
    {
        var user = RegisteredUsers.FirstOrDefault(user => user.UserName == username && user.Password == password);
        return false;
    }
}