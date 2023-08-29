using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistance.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
