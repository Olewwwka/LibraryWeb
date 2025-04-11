using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lib.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ISBN = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Genre = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    BorrowTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReturnTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Birthday", "Country", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d472"), new DateTime(1828, 9, 8, 20, 0, 0, 0, DateTimeKind.Utc), "Россия", "Лев", "Толстой" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), new DateTime(1821, 11, 10, 21, 0, 0, 0, DateTimeKind.Utc), "Россия", "Фёдор", "Достоевский" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d474"), new DateTime(1860, 1, 28, 21, 0, 0, 0, DateTimeKind.Utc), "Россия", "Антон", "Чехов" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d475"), new DateTime(1799, 6, 5, 20, 0, 0, 0, DateTimeKind.Utc), "Россия", "Александр", "Пушкин" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"), new DateTime(1814, 10, 14, 20, 0, 0, 0, DateTimeKind.Utc), "Россия", "Михаил", "Лермонтов" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d471"), "user@library.com", "User", "$2a$11$/Hm.Ef6QWWSH.spPmrv8wOF84pOcIULA8srvm8jSGvXjoizkmd7tu", "User" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), "admin@library.com", "Admin", "$2a$11$t9PVfLca4D1.dArfDxgAhOP/8sgjOpkPbdqO/nCvNiovdP8dOu3wG", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "BorrowTime", "Description", "Genre", "ISBN", "ImagePath", "Name", "ReturnTime", "UserId" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d181"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d475"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Роман в стихах Александра Пушкина", 10, "978-5-17-090000-13", "default_image.jpg", "Евгений Онегин", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d182"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d475"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Роман Александра Пушкина", 11, "978-5-17-090000-14", "default_image.jpg", "Капитанская дочка", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d183"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d475"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Повесть Александра Пушкина", 10, "978-5-17-090000-15", "default_image.jpg", "Пиковая дама", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d184"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d475"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Поэма Александра Пушкина", 10, "978-5-17-090000-16", "default_image.jpg", "Руслан и Людмила", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d185"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Роман Михаила Лермонтова", 2, "978-5-17-090000-17", "default_image.jpg", "Герой нашего времени", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d186"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Поэма Михаила Лермонтова", 10, "978-5-17-090000-18", "default_image.jpg", "Демон", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d187"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Поэма Михаила Лермонтова", 10, "978-5-17-090000-19", "default_image.jpg", "Мцыри", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d188"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Стихотворение Михаила Лермонтова", 10, "978-5-17-090000-20", "default_image.jpg", "Бородино", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d189"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d474"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Пьеса Антона Чехова", 11, "978-5-17-090000-12", "default_image.jpg", "Дядя Ваня", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d477"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d472"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Роман-эпопея Льва Толстого", 3, "978-5-17-090000-1", "default_image.jpg", "Война и мир", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d478"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d472"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Роман Льва Толстого", 0, "978-5-17-090000-2", "default_image.jpg", "Анна Каренина", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d481"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d472"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Повесть Льва Толстого", 13, "978-5-17-090000-4", "default_image.jpg", "Смерть Ивана Ильича", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d482"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Роман Фёдора Достоевского", 6, "978-5-17-090000-5", "default_image.jpg", "Преступление и наказание", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d483"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Роман Фёдора Достоевского", 16, "978-5-17-090000-6", "default_image.jpg", "Идиот", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d484"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Роман Фёдора Достоевского", 1, "978-5-17-090000-7", "default_image.jpg", "Братья Карамазовы", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d485"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Роман Фёдора Достоевского", 3, "978-5-17-090000-8", "default_image.jpg", "Бесы", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d486"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d474"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Пьеса Антона Чехова", 11, "978-5-17-090000-9", "default_image.jpg", "Вишнёвый сад", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d487"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d474"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Пьеса Антона Чехова", 11, "978-5-17-090000-10", "default_image.jpg", "Чайка", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d488"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d474"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Пьеса Антона Чехова", 11, "978-5-17-090000-11", "default_image.jpg", "Три сестры", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d489"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d472"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Роман Льва Толстого", 13, "978-5-17-090000-3", "default_image.jpg", "Воскресение", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserId",
                table: "Books",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
