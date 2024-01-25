using chatGPT5.models;
using Microsoft.EntityFrameworkCore;

namespace chatGPT5
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
        }
    }
}
