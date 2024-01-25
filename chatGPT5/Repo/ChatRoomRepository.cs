using chatGPT5;
using chatGPT5.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ChatRoomRepository : IChatRoomRepository
{
    private readonly ChatContext _context;

    public ChatRoomRepository(ChatContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsersByRoomIdAsync(int roomId)
    {
        var room = await _context.ChatRooms
            .Include(r => r.Users)
            .FirstOrDefaultAsync(r => r.Id == roomId);

        return room?.Users.ToList() ?? new List<User>();
    }
    
}