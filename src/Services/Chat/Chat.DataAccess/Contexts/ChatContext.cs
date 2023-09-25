using Chat.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Chat.DataAccess.Contexts
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions options) : base(options) { }

        public DbSet<ChatParticipant> ChatParticipants { get; set; }

        public DbSet<ChatRole> ChatRoles { get; set; }

        public DbSet<ChatRoom> ChatRooms { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessengerUser> MessangerUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
