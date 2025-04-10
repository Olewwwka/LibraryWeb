using Lib.Core.Enums;

namespace Lib.Core.Entities
{
    public class BookEntity
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Genre Genre { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = "default_image.jpg";

        public Guid AuthorId { get; set; }
        public AuthorEntity Author { get; set; } = null!;

        public Guid? UserId { get; set; } = null;
        public UserEntity? User { get; set; } = null;
        public DateTime BorrowTime { get; set; } = DateTime.MinValue;
        public DateTime ReturnTime { get; set; } = DateTime.MinValue;
    }
}
