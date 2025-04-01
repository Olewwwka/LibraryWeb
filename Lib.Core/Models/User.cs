using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.DTOs
{
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public User(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
