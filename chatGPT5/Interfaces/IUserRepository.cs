using chatGPT5.models;

namespace chatGPT5.Interfaces;

public interface IUserRepository
{
    Task<bool> UserExistsAsync(string username);
    Task AddUserAsync(User user);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task JoinRoomAsync(int userId, int roomId);
    Task<User> AuthenticateAsync(string username, string password);
}