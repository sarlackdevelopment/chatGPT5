using chatGPT5.models;
using Microsoft.EntityFrameworkCore;

namespace chatGPT5
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<UserChatRoom> UserChatRooms { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserChatRoom>()
                .HasKey(uc => new { uc.UserId, uc.ChatRoomId });

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserChatRooms)
                .WithOne() // Удалено навигационное свойство
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<ChatRoom>()
                .HasMany(c => c.UserChatRooms)
                .WithOne() // Удалено навигационное свойство
                .HasForeignKey(uc => uc.ChatRoomId);

            // Другие настройки моделей...
        }
    }
}
