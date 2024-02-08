using chatGPT5.Interfaces;
using chatGPT5.Models.dto;
using chatGPT5.Models.network;
using Microsoft.EntityFrameworkCore;

namespace chatGPT5.Repo;

public class MessageRepository : IMessageRepository
{
    private readonly ChatContext _context;

    public MessageRepository(ChatContext context)
    {
        _context = context;
    }

    public async Task AddMessageAsync(MessageDto messageDto)
    {
        var message = new Message
        {
            Content = messageDto.Content,
            UserId = messageDto.UserId,
            ChatRoomId = messageDto.ChatRoomId,
            Timestamp = DateTime.UtcNow
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Message>> GetMessagesByUserAndRoomAsync(int userId, int roomId)
    {
        return await _context.Messages
            .Where(m => m.UserId == userId && m.ChatRoomId == roomId)
            .ToListAsync();
    }
}
