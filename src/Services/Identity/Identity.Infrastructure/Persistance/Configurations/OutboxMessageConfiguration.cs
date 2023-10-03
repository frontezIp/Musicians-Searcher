using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Messages;

namespace Identity.Infrastructure.Persistance.Configurations
{
    internal class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            builder.Property(b => b.Topic)
                .IsRequired();

            builder.Property(b => b.MessageType)
                .IsRequired();
        }
    }
}
