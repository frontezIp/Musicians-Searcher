using Chat.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DataAccess.Configurations
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()");
            
            builder.Property(b => b.UpdatedAt)
                .HasDefaultValueSql("getdate()");

            builder.Property(b => b.Text)
                .IsRequired()
                .HasMaxLength(400);

            builder.HasOne(b => b.ChatRoom)
                .WithMany(b => b.Messages)
                .HasForeignKey(b => b.ChatRoomId);

            builder.HasOne(b => b.MessengerUser)
                .WithMany(b => b.Messages)
                .HasForeignKey(b => b.MessengerUserId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
