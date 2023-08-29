using Chat.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DataAccess.Configurations
{
    internal class MessangerUserConfiguration : IEntityTypeConfiguration<MessangerUser>
    {
        public void Configure(EntityTypeBuilder<MessangerUser> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.FullName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
