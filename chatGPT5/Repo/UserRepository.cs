using chatGPT5.Enums;
using chatGPT5.Interfaces;
using chatGPT5.Utilities;
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
    
    public async Task<User> AuthenticateAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (user == null || user.Password != PasswordHasher.ComputeSha256Hash(password))
        {
            return null;
        }

        return user;
    }
    
    public async Task<bool> UpdateUserRoleAsync(int userId, Roles newRole)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return false;
        }

        user.Role = newRole;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> LeaveRoomAsync(int userId, int roomId)
    {
        var user = await _context.Users.FindAsync(userId);
        var room = await _context.ChatRooms.Include(r => r.Users).FirstOrDefaultAsync(r => r.Id == roomId);

        if (user != null && room != null && room.Users.Contains(user))
        {
            room.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("User or room not found or user is not a member of the room");
        }

        return true;
    }
    
}
