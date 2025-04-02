using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Models
{
    public class Author
    {
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
    }
}
