using chatGPT5.Models.dto;
using chatGPT5.Models.network;

namespace chatGPT5.Interfaces;

public interface IMessageRepository
{
    Task AddMessageAsync(MessageDto messageDto);
    Task<IEnumerable<Message>> GetMessagesByUserAndRoomAsync(int userId, int roomId);
}
