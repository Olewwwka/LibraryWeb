using Lib.Core.Enums;

namespace Lib.Application.Models
{
    public class Book
    {
        public Guid? Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Genre Genre { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid AuthorId { get; set; } 
        public Author Author { get; set; }
        public Guid? UserId { get; set; }
        public string ImagePath { get; set; } = "default_image.jpg";
        public DateTime? BorrowTime { get; set; }
        public DateTime? ReturnTime { get; set; }
        public bool IsBorrowed => UserId.HasValue;
        public bool IsOverdue => ReturnTime.HasValue && ReturnTime < DateTime.UtcNow;

        public Book(string isbn, string name, Genre genre, string description, Guid authorId)
        {
            ISBN = isbn;
            Name = name;
            Genre = genre;
            Description = description;
            AuthorId = authorId;
            ImagePath = "default_image.jpg";
        }
    }
}
