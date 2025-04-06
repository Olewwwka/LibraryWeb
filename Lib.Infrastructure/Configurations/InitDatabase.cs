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
                Name = "Лев",
                Surname = "Толстой",
                Country = "Россия",
                Birthday = new DateTime(1828, 9, 9)
            },
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = "Фёдор",
                Surname = "Достоевский",
                Country = "Россия",
                Birthday = new DateTime(1821, 11, 11)
            },
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = "Антон",
                Surname = "Чехов",
                Country = "Россия",
                Birthday = new DateTime(1860, 1, 29)
            },
            new AuthorEntity        
            {
                Id = Guid.NewGuid(),
                Name = "Александр",
                Surname = "Пушкин",
                Country = "Россия",
                Birthday = new DateTime(1799, 6, 6)
            },
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = "Михаил",
                Surname = "Лермонтов",
                Country = "Россия",
                Birthday = new DateTime(1814, 10, 15)
            }
        };

        context.Authors.AddRange(authors);

        var books = new List<BookEntity>
        {
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Война и мир",
                ISBN = "978-5-17-090000-1",
                Genre = Genre.ScienceFiction,
                Description = "Роман-эпопея Льва Толстого",
                AuthorId = authors[0].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Анна Каренина",
                ISBN = "978-5-17-090000-2",
                Genre = Genre.Fiction,
                Description = "Роман Льва Толстого",
                AuthorId = authors[0].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Воскресение",
                ISBN = "978-5-17-090000-3",
                Genre = Genre.Adventure,
                Description = "Роман Льва Толстого",
                AuthorId = authors[0].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Смерть Ивана Ильича",
                ISBN = "978-5-17-090000-4",
                Genre = Genre.Adventure,
                Description = "Повесть Льва Толстого",
                AuthorId = authors[0].Id
            },

            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Преступление и наказание",
                ISBN = "978-5-17-090000-5",
                Genre = Genre.Thriller,
                Description = "Роман Фёдора Достоевского",
                AuthorId = authors[1].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Идиот",
                ISBN = "978-5-17-090000-6",
                Genre = Genre.Other,
                Description = "Роман Фёдора Достоевского",
                AuthorId = authors[1].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Братья Карамазовы",
                ISBN = "978-5-17-090000-7",
                Genre = Genre.NonFiction,
                Description = "Роман Фёдора Достоевского",
                AuthorId = authors[1].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Бесы",
                ISBN = "978-5-17-090000-8",
                Genre = Genre.ScienceFiction,
                Description = "Роман Фёдора Достоевского",
                AuthorId = authors[1].Id
            },

            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Вишнёвый сад",
                ISBN = "978-5-17-090000-9",
                Genre = Genre.Drama,
                Description = "Пьеса Антона Чехова",
                AuthorId = authors[2].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Чайка",
                ISBN = "978-5-17-090000-10",
                Genre = Genre.Drama,
                Description = "Пьеса Антона Чехова",
                AuthorId = authors[2].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Три сестры",
                ISBN = "978-5-17-090000-11",
                Genre = Genre.Drama,
                Description = "Пьеса Антона Чехова",
                AuthorId = authors[2].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Дядя Ваня",
                ISBN = "978-5-17-090000-12",
                Genre = Genre.Drama,
                Description = "Пьеса Антона Чехова",
                AuthorId = authors[2].Id
            },

            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Евгений Онегин",
                ISBN = "978-5-17-090000-13",
                Genre = Genre.Poetry,
                Description = "Роман в стихах Александра Пушкина",
                AuthorId = authors[3].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Капитанская дочка",
                ISBN = "978-5-17-090000-14",
                Genre = Genre.Drama,
                Description = "Роман Александра Пушкина",
                AuthorId = authors[3].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Пиковая дама",
                ISBN = "978-5-17-090000-15",
                Genre = Genre.Poetry,
                Description = "Повесть Александра Пушкина",
                AuthorId = authors[3].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Руслан и Людмила",
                ISBN = "978-5-17-090000-16",
                Genre = Genre.Poetry,
                Description = "Поэма Александра Пушкина",
                AuthorId = authors[3].Id
            },

            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Герой нашего времени",
                ISBN = "978-5-17-090000-17",
                Genre = Genre.Mystery,
                Description = "Роман Михаила Лермонтова",
                AuthorId = authors[4].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Демон",
                ISBN = "978-5-17-090000-18",
                Genre = Genre.Poetry,
                Description = "Поэма Михаила Лермонтова",
                AuthorId = authors[4].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Мцыри",
                ISBN = "978-5-17-090000-19",
                Genre = Genre.Poetry,
                Description = "Поэма Михаила Лермонтова",
                AuthorId = authors[4].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = "Бородино",
                ISBN = "978-5-17-090000-20",
                Genre = Genre.Poetry,
                Description = "Стихотворение Михаила Лермонтова",
                AuthorId = authors[4].Id
            }
        };

        context.Books.AddRange(books);
        context.SaveChanges();
    }
}