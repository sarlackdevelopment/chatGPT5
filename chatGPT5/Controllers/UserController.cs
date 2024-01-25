using System.Security.Cryptography;
using System.Text;
using chatGPT5.Interfaces;
using chatGPT5.models;
using Microsoft.AspNetCore.Mvc;

namespace chatGPT5.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            if (await _userRepository.UserExistsAsync(user.Username))
            {
                return BadRequest("User already exists.");
            }
    
            user.Password = ComputeSha256Hash(user.Password);
    
            await _userRepository.AddUserAsync(user);
            return Ok(user);
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        
        [HttpPost("{userId}/joinRoom/{roomId}")]
        public async Task<IActionResult> JoinRoom(int userId, int roomId)
        {
            try
            {
                await _userRepository.JoinRoomAsync(userId, roomId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        // [HttpGet("{userId}/rooms")]
        // public async Task<ActionResult<IEnumerable<ChatRoom>>> GetUserRooms(int userId)
        // {
        //     try
        //     {
        //         var rooms = await _userRepository.GetUserRoomsAsync(userId);
        //         return Ok(rooms);
        //     }
        //     catch (Exception ex)
        //     {
        //         // Обработка ошибок, например, если пользователь не найден
        //         return BadRequest(ex.Message);
        //     }
        // }
    }
}
