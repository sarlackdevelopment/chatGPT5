using Microsoft.EntityFrameworkCore;

namespace chatGPT5
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
        }
    }
}