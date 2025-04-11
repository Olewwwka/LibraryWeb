namespace Lib.Application.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Country { get; set; } = string.Empty;
        public List<Book>? Books { get; set; } = new();
        public Author(string name, string surname, DateTime birthday, string country)
        {
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Country = country;
        }
        public Author()
        {
            
        }
    }
}
