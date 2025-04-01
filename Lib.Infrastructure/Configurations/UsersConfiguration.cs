using Lib.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lib.Infrastructure.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> userBuilder)
        {
            userBuilder.ToTable("Users").HasKey(user => user.Id);

            userBuilder.Property(user => user.Id).ValueGeneratedOnAdd();
        }
    }
}
