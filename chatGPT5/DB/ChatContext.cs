using Microsoft.EntityFrameworkCore;

public class ChatContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ChatContext(DbContextOptions<ChatContext> options)
        : base(options)
    {
    }
}