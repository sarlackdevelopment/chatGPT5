using System.Security.Cryptography;
using System.Text;
using chatGPT5.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace chatGPT5.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ChatContext _context;

        public UserController(ChatContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                return BadRequest("User already exists.");
            }
    
            user.Password = ComputeSha256Hash(user.Password);
            
            // if (user.UserChatRooms != null)
            // {
            //     foreach (var userChatRoom in user.UserChatRooms)
            //     {
            //         // Обработка связи с комнатой, например, проверка наличия комнаты в БД
            //     }
            // }
    
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
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
            var user = await _context.Users.FindAsync(userId);
            var room = await _context.ChatRooms.FindAsync(roomId);

            if (user == null || room == null)
            {
                return NotFound();
            }

            // // Проверяем, состоит ли пользователь уже в комнате
            // if (!user.UserChatRooms.Any(uc => uc.ChatRoomId == roomId))
            // {
            //     user.UserChatRooms.Add(new UserChatRoom() { UserId = userId, ChatRoomId = roomId });
            //     await _context.SaveChangesAsync();
            // }
            var userChatRoom = new UserChatRoom { UserId = userId, ChatRoomId = roomId };
            _context.UserChatRooms.Add(userChatRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        // [HttpGet("{userId}/rooms")]
        // public async Task<ActionResult<IEnumerable<ChatRoom>>> GetUserRooms(int userId)
        // {
        //     var user = await _context.Users
        //         .Include(u => u.UserChatRooms)
        //         .ThenInclude(uc => uc.ChatRoom)
        //         .FirstOrDefaultAsync(u => u.Id == userId);
        //
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var rooms = user.UserChatRooms.Select(uc => uc.ChatRoom).ToList();
        //     return Ok(rooms);
        // }
        
        [HttpGet("{userId}/rooms")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetUserRooms(int userId)
        {
            // Проверяем, существует ли пользователь
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                return NotFound();
            }

            // Получаем ID комнат, в которых состоит пользователь
            var roomIds = await _context.UserChatRooms
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.ChatRoomId)
                .ToListAsync();

            // Получаем комнаты на основе этих ID
            var rooms = await _context.ChatRooms
                .Where(r => roomIds.Contains(r.Id))
                .ToListAsync();

            return Ok(rooms);
        }

        
    }
}
