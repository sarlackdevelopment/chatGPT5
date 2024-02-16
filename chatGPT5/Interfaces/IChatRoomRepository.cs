using chatGPT5.models;

namespace chatGPT5.Interfaces;

public interface IChatRoomRepository
{
    Task<List<User>> GetUsersByRoomIdAsync(int roomId);
    Task AddChatRoomAsync(ChatRoom chatRoom);
    Task<List<ChatRoom>> GetAllChatRoomsAsync();
    Task<ChatRoom> GetRoomByIdAsync(int roomId);
    Task<bool> DeleteChatRoomAsync(int roomId);
}