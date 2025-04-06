using Lib.Core.Entities;
using Lib.Core.Enums;
using Lib.Infrastructure;

namespace Library.Infrastructure.Data;

public static class InitDatabase
{
    public static void Initialize(LibraryDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            return;
        }

        var admin = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = "Admin",
            Email = "admin@library.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
            Role = "Admin"
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = "User",
            Email = "user@library.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
            Role = "User"
        };

        context.Users.AddRange(admin, user);

        var authors = new List<AuthorEntity>
        {
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = "���",
                Surname = "�������",
                Country = "������",
                Birthday = new DateTime(1828, 9, 9)
            },
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = "Ը���",
                Surname = "�����������",
                Country = "������",
                Birthday = new DateTime(1821, 11, 11)
            },
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = "�����",
                Surname = "�����",
                Country = "������",
                Birthday = new DateTime(1860, 1, 29)
            },
            new AuthorEntity        
            {
                Id = Guid.NewGuid(),
                Name = "���������",
                Surname = "������",
                Country = "������",
                Birthday = new DateTime(1799, 6, 6)
            },
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = "������",
                Surname = "���������",
                Country = "������",
                Birthday = new DateTime(1814, 10, 15)
            }
        };

        context.Authors.AddRange(authors);

        var books = new List<BookEntity>
        {
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "����� � ���",
                ISBN = "978-5-17-090000-1",
                Genre = Genre.ScienceFiction,
                Description = "�����-������ ���� ��������",
                AuthorId = authors[0].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "���� ��������",
                ISBN = "978-5-17-090000-2",
                Genre = Genre.Fiction,
                Description = "����� ���� ��������",
                AuthorId = authors[0].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "�����������",
                ISBN = "978-5-17-090000-3",
                Genre = Genre.Adventure,
                Description = "����� ���� ��������",
                AuthorId = authors[0].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "������ ����� ������",
                ISBN = "978-5-17-090000-4",
                Genre = Genre.Adventure,
                Description = "������� ���� ��������",
                AuthorId = authors[0].Id
            },

            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "������������ � ���������",
                ISBN = "978-5-17-090000-5",
                Genre = Genre.Thriller,
                Description = "����� Ը���� ������������",
                AuthorId = authors[1].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "�����",
                ISBN = "978-5-17-090000-6",
                Genre = Genre.Other,
                Description = "����� Ը���� ������������",
                AuthorId = authors[1].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "������ ����������",
                ISBN = "978-5-17-090000-7",
                Genre = Genre.NonFiction,
                Description = "����� Ը���� ������������",
                AuthorId = authors[1].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "����",
                ISBN = "978-5-17-090000-8",
                Genre = Genre.ScienceFiction,
                Description = "����� Ը���� ������������",
                AuthorId = authors[1].Id
            },

            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "������� ���",
                ISBN = "978-5-17-090000-9",
                Genre = Genre.Drama,
                Description = "����� ������ ������",
                AuthorId = authors[2].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "�����",
                ISBN = "978-5-17-090000-10",
                Genre = Genre.Drama,
                Description = "����� ������ ������",
                AuthorId = authors[2].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "��� ������",
                ISBN = "978-5-17-090000-11",
                Genre = Genre.Drama,
                Description = "����� ������ ������",
                AuthorId = authors[2].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "���� ����",
                ISBN = "978-5-17-090000-12",
                Genre = Genre.Drama,
                Description = "����� ������ ������",
                AuthorId = authors[2].Id
            },

            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "������� ������",
                ISBN = "978-5-17-090000-13",
                Genre = Genre.Poetry,
                Description = "����� � ������ ���������� �������",
                AuthorId = authors[3].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "����������� �����",
                ISBN = "978-5-17-090000-14",
                Genre = Genre.Drama,
                Description = "����� ���������� �������",
                AuthorId = authors[3].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "������� ����",
                ISBN = "978-5-17-090000-15",
                Genre = Genre.Poetry,
                Description = "������� ���������� �������",
                AuthorId = authors[3].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "������ � �������",
                ISBN = "978-5-17-090000-16",
                Genre = Genre.Poetry,
                Description = "����� ���������� �������",
                AuthorId = authors[3].Id
            },

            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "����� ������ �������",
                ISBN = "978-5-17-090000-17",
                Genre = Genre.Mystery,
                Description = "����� ������� ����������",
                AuthorId = authors[4].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "�����",
                ISBN = "978-5-17-090000-18",
                Genre = Genre.Poetry,
                Description = "����� ������� ����������",
                AuthorId = authors[4].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "�����",
                ISBN = "978-5-17-090000-19",
                Genre = Genre.Poetry,
                Description = "����� ������� ����������",
                AuthorId = authors[4].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "��������",
                ISBN = "978-5-17-090000-20",
                Genre = Genre.Poetry,
                Description = "������������� ������� ����������",
                AuthorId = authors[4].Id
            }
        };

        context.Books.AddRange(books);
        context.SaveChanges();
    }
}