using chatGPT5.Enums;

namespace chatGPT5.Interfaces;

public interface IUserRepository
{
    Task<bool> UserExistsAsync(string username);
    Task AddUserAsync(User user);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task JoinRoomAsync(int userId, int roomId);
    Task<User> AuthenticateAsync(string username, string password);
    Task<bool> UpdateUserRoleAsync(int userId, Roles newRole);
}