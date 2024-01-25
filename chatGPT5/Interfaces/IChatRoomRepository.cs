namespace chatGPT5.Interfaces;

public interface IChatRoomRepository
{
    Task<List<User>> GetUsersByRoomIdAsync(int roomId);
}