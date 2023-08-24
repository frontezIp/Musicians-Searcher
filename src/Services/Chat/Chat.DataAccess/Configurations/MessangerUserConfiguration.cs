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
