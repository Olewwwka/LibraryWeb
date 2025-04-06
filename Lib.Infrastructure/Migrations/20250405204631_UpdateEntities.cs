using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lib.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("12111111-1111-1111-1111-111111111111"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "admin@example.com", "admin", "$2a$11$HHF3BUTQioU8yYNuaRxwTevqZslfXX8nXEEqM3tDhz8a2qJ8jaGoW", "Admin" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), "user@example.com", "user", "$2a$11$.7kLttpQnGdfu8s5Ac3MPe7NNAUUpxmqoNnHl3CZnEA3XKjzdoAIa", "User" }
                });
        }
    }
}
