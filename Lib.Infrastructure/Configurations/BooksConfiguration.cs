using Lib.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lib.Infrastructure.Configurations
{
    public class BooksConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> bookBuilder)
        {
            bookBuilder.ToTable("Books").HasKey(book => book.Id);

            bookBuilder.Property(book => book.Id).ValueGeneratedOnAdd();

            bookBuilder.Property(book => book.BorrowTime)
                .HasColumnType("timestamp with time zone")
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            bookBuilder.Property(book => book.ReturnTime)
                .HasColumnType("timestamp with time zone")
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            bookBuilder.HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            bookBuilder.HasOne(book => book.User)
                .WithMany(user => user.BorrowedBooks)
                .HasForeignKey(book => book.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
