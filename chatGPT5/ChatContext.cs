using chatGPT5.models;
using chatGPT5.Models.network;
using Microsoft.EntityFrameworkCore;

namespace chatGPT5
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
        }
    }
}
