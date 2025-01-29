
namespace Library.Entites;

public class Book
{
    Guid Id { get; set; }
    string Title { get; set; }
    string Author { get; set; }
    string Genre { get; set; }
    int Year { get; set; }
    bool IsAvaliable { get; set; }
    DateTime? DauDate { get; set; }
    User Borrower { get; set; }

    public Book()
    {
        Id = Guid.NewGuid();
        Title = string.Empty;
        Author = string.Empty;
        Genre = string.Empty;
        Year = 0;
        IsAvaliable = false;
        DauDate = null; 
    }

    public Book(string title, string author, string genre, int year)
    {
        Id = Guid.NewGuid();
        Title = title;
        Author = author;
        Genre = genre;
        Year = year;
        IsAvaliable = true;
    }
}