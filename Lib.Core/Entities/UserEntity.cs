namespace Lib.Core.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public List<BookEntity> BorrowedBooks { get; set; } = new();
    }
}
