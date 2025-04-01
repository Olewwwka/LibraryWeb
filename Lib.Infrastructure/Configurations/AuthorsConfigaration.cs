using Lib.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lib.Infrastructure.Configurations
{
    public class AuthorsConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> authorBuilder)
        {
            authorBuilder.ToTable("Authors").HasKey(author => author.Id);

            authorBuilder.Property(author => author.Id).ValueGeneratedOnAdd();

            authorBuilder.HasMany(author => author.Books)
                .WithOne(book => book.Author)
                .HasForeignKey(book => book.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
