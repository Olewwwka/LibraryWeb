using Lib.Core.Abstractions;

namespace Lib.Infrastructure.Identity
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password) =>
           BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string password, string passworHash) =>
           BCrypt.Net.BCrypt.Verify(password, passworHash);
    }
}
