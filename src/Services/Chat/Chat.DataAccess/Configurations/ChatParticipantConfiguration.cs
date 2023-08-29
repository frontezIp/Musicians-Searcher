using Chat.DataAccess.Converters;
using Chat.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DataAccess.Configurations
{
    internal class ChatParticipantConfiguration : IEntityTypeConfiguration<ChatParticipant>
    {
        public void Configure(EntityTypeBuilder<ChatParticipant> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(b => b.ChatRoom)
                .WithMany(b => b.ChatParticipants)
                .HasForeignKey(b => b.ChatRoomId);

            builder.Property(b => b.CreatedAt)
                .HasConversion(new DateOnlyToDateTimeConverter())
                .HasDefaultValueSql("getdate()");

            builder.Property(b => b.UpdatedAt)
                .HasConversion(new DateOnlyToDateTimeConverter())
                .HasDefaultValueSql("getdate()");

            builder.HasOne(b => b.MessangerUser)
                .WithMany(b => b.ParticipatedChats)
                .HasForeignKey(b => b.MessangerUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.ChatRole);
        }
    }
}
