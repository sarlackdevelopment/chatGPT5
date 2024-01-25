using chatGPT5.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chatGPT5.Repo;

public class UserRepository : IUserRepository
{
    private readonly ChatContext _context;

    public UserRepository(ChatContext context)
    {
        _context = context;
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
    
    public async Task JoinRoomAsync(int userId, int roomId)
    {
        var user = await _context.Users.FindAsync(userId);
        var room = await _context.ChatRooms.FindAsync(roomId);

        if (user != null && room != null)
        {
            room.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("User or room not found");
        }
    }
}
