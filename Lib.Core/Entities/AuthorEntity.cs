namespace Lib.Core.Entities
{
    public class AuthorEntity
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Country { get; set; } = string.Empty;
        public List<BookEntity> Books { get; set; }
    }
}
