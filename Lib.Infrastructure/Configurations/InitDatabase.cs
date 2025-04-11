using Lib.Core.Entities;
using Lib.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure;

public static class InitDatabase
{
    public static void Initialize(ModelBuilder modelBuilder)
    {



        var users = new List<UserEntity>
        {
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                Name = "Admin",
                Email = "admin@library.com",
                PasswordHash = "$2a$11$t9PVfLca4D1.dArfDxgAhOP/8sgjOpkPbdqO/nCvNiovdP8dOu3wG",
                Role = "Admin"
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d471"),
                Name = "User",
                Email = "user@library.com",
                PasswordHash = "$2a$11$/Hm.Ef6QWWSH.spPmrv8wOF84pOcIULA8srvm8jSGvXjoizkmd7tu",
                Role = "User"
            }
        };

        var authors = new List<AuthorEntity>
        {
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d472"),
                Name = "Лев",
                Surname = "Толстой",
                Country = "Россия",
                Birthday = new DateTime(1828, 9, 9)
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d473"),
                Name = "Фёдор",
                Surname = "Достоевский",
                Country = "Россия",
                Birthday = new DateTime(1821, 11, 11)
            },
           new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d474"),
                Name = "Антон",
                Surname = "Чехов",
                Country = "Россия",
                Birthday = new DateTime(1860, 1, 29)
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d475"),
                Name = "Александр",
                Surname = "Пушкин",
                Country = "Россия",
                Birthday = new DateTime(1799, 6, 6)
            },
           new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d476"),
                Name = "Михаил",
                Surname = "Лермонтов",
                Country = "Россия",
                Birthday = new DateTime(1814, 10, 15)
            }
        };

        var books = new List<BookEntity>
        {
           new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d477"),
                Name = "Война и мир",
                ISBN = "978-5-17-090000-1",
                Genre = Genre.ScienceFiction,
                Description = "Роман-эпопея Льва Толстого",
                AuthorId = authors[0].Id
            },
           new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d478"),
                Name = "Анна Каренина",
                ISBN = "978-5-17-090000-2",
                Genre = Genre.Fiction,
                Description = "Роман Льва Толстого",
                AuthorId = authors[0].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d489"),
                Name = "Воскресение",
                ISBN = "978-5-17-090000-3",
                Genre = Genre.Adventure,
                Description = "Роман Льва Толстого",
                AuthorId = authors[0].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d481"),
                Name = "Смерть Ивана Ильича",
                ISBN = "978-5-17-090000-4",
                Genre = Genre.Adventure,
                Description = "Повесть Льва Толстого",
                AuthorId = authors[0].Id
            },

            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d482"),
                Name = "Преступление и наказание",
                ISBN = "978-5-17-090000-5",
                Genre = Genre.Thriller,
                Description = "Роман Фёдора Достоевского",
                AuthorId = authors[1].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d483"),
                Name = "Идиот",
                ISBN = "978-5-17-090000-6",
                Genre = Genre.Other,
                Description = "Роман Фёдора Достоевского",
                AuthorId = authors[1].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d484"),
                Name = "Братья Карамазовы",
                ISBN = "978-5-17-090000-7",
                Genre = Genre.NonFiction,
                Description = "Роман Фёдора Достоевского",
                AuthorId = authors[1].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d485"),
                Name = "Бесы",
                ISBN = "978-5-17-090000-8",
                Genre = Genre.ScienceFiction,
                Description = "Роман Фёдора Достоевского",
                AuthorId = authors[1].Id
            },

            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d486"),
                Name = "Вишнёвый сад",
                ISBN = "978-5-17-090000-9",
                Genre = Genre.Drama,
                Description = "Пьеса Антона Чехова",
                AuthorId = authors[2].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d487"),
                Name = "Чайка",
                ISBN = "978-5-17-090000-10",
                Genre = Genre.Drama,
                Description = "Пьеса Антона Чехова",
                AuthorId = authors[2].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d488"),
                Name = "Три сестры",
                ISBN = "978-5-17-090000-11",
                Genre = Genre.Drama,
                Description = "Пьеса Антона Чехова",
                AuthorId = authors[2].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d189"),
                Name = "Дядя Ваня",
                ISBN = "978-5-17-090000-12",
                Genre = Genre.Drama,
                Description = "Пьеса Антона Чехова",
                AuthorId = authors[2].Id
            },

            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d181"),
                Name = "Евгений Онегин",
                ISBN = "978-5-17-090000-13",
                Genre = Genre.Poetry,
                Description = "Роман в стихах Александра Пушкина",
                AuthorId = authors[3].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d182"),
                Name = "Капитанская дочка",
                ISBN = "978-5-17-090000-14",
                Genre = Genre.Drama,
                Description = "Роман Александра Пушкина",
                AuthorId = authors[3].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d183"),
                Name = "Пиковая дама",
                ISBN = "978-5-17-090000-15",
                Genre = Genre.Poetry,
                Description = "Повесть Александра Пушкина",
                AuthorId = authors[3].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d184"),
                Name = "Руслан и Людмила",
                ISBN = "978-5-17-090000-16",
                Genre = Genre.Poetry,
                Description = "Поэма Александра Пушкина",
                AuthorId = authors[3].Id
            },

            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d185"),
                Name = "Герой нашего времени",
                ISBN = "978-5-17-090000-17",
                Genre = Genre.Mystery,
                Description = "Роман Михаила Лермонтова",
                AuthorId = authors[4].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d186"),
                Name = "Демон",
                ISBN = "978-5-17-090000-18",
                Genre = Genre.Poetry,
                Description = "Поэма Михаила Лермонтова",
                AuthorId = authors[4].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d187"),
                Name = "Мцыри",
                ISBN = "978-5-17-090000-19",
                Genre = Genre.Poetry,
                Description = "Поэма Михаила Лермонтова",
                AuthorId = authors[4].Id
            },
            new() {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d188"),
                Name = "Бородино",
                ISBN = "978-5-17-090000-20",
                Genre = Genre.Poetry,
                Description = "Стихотворение Михаила Лермонтова",
                AuthorId = authors[4].Id
            }
        };

        modelBuilder.Entity<UserEntity>().HasData(users);
        modelBuilder.Entity<AuthorEntity>().HasData(authors);
        modelBuilder.Entity<BookEntity>().HasData(books);
    }



}