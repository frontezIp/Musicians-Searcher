using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.Infrastructure.Persistance.Contexts
{
    public class IdentityContext : IdentityDbContext<User, Role, Guid>
    {
        public IdentityContext(DbContextOptions options) : base(options) { }


        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
