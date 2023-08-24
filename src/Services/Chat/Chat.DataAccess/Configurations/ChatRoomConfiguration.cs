using Chat.DataAccess.Converters;
using Chat.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccess.Configurations
{
    internal class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(b => b.CreatedAt)
                .HasConversion(new DateOnlyToDateTimeConverter())
                .HasDefaultValueSql("getdate()");

            builder.Property(b => b.UpdatedAt)
                .HasConversion(new DateOnlyToDateTimeConverter())
                .HasDefaultValueSql("getdate()");

            builder.HasOne(b => b.Creater)
                .WithMany(b => b.CreatedChatRooms)
                .HasForeignKey(b => b.CreaterId);

        }
    }
}
