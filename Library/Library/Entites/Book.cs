using System;

namespace Library.Entities
{
    public class Book
    {
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public DateTime? DueDate { get; set; }
        public User Borrower { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string title, string author, string genre, int year)
        {
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            IsAvailable = true;
        }
    }
}