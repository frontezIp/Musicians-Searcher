using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.Name).
                IsRequired().
                HasMaxLength(50);

            builder.Property(b => b.SecondName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.CityId)
                .IsRequired();

            builder.Property(b => b.Biography)
                .HasMaxLength(200);

            builder.Property(b => b.Age)
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            builder.Property(b => b.SexTypeId).IsRequired();

            builder.HasOne(u => u.City)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CityId);
        }
    }
}
