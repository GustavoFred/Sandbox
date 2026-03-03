using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
            entity.Property(t => t.SecondName).IsRequired().HasMaxLength(50);
            entity.Property(t => t.Email).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Password).IsRequired();

        }
    }
}
