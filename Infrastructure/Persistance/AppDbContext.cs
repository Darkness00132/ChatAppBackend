using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class AppDbContext : IdentityDbContext<User,Role,Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ChatRoom>().HasData(new ChatRoom { Id = 1 , Name = "General" });
            builder.Entity<ChatRoom>().HasData(new ChatRoom { Id = 2, Name = "Frontend" });
            builder.Entity<ChatRoom>().HasData(new ChatRoom { Id = 3, Name = "Backend" });
            builder.Entity<ChatRoom>().HasData(new ChatRoom { Id = 4, Name = "Random" });

            base.OnModelCreating(builder);
        }
    }
}
