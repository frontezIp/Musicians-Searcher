using Chat.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DataAccess.Configurations
{
    internal class MessengerUserConfiguration : IEntityTypeConfiguration<MessengerUser>
    {
        public void Configure(EntityTypeBuilder<MessengerUser> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Username)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
