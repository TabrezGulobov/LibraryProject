using System;

namespace Library.Entities
{
    public enum Role 
    { 
        Admin, 
        User 
    }

    public sealed class User
    {
        public Guid Id { get; private set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }

        public User(string userName, string password, Role role)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Password = password;
            UserRole = role;
        }
    }
}