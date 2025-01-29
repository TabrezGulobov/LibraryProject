namespace Library.Entites;


public enum Role 
{ 
    Admin, 
    User
} 
public sealed class User
{ 
    Guid Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    string? Email { get; set; }
    string? Phone { get; set; }
    Role UserRole { get; set; }
    List<string> BorowedBooks { get; set; }
    
    public User() 
    { 
    
        Id = Guid.NewGuid();
        UserName = string.Empty;
        Password = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        UserRole = Role.User; 
    }


    public User(string userName, string password, Role role)
    {
        Id = Guid.NewGuid();
        UserName = userName;    
        Password = password;
        UserRole = role;
    }
}
