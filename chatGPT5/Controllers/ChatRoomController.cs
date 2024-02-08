using chatGPT5.Enums;
using Microsoft.AspNetCore.Mvc;
using chatGPT5.Interfaces;
using chatGPT5.models;
using Microsoft.AspNetCore.Authorization;

namespace chatGPT5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatRoomController(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        [Authorize]
        [HttpGet("{roomId}/users")]
        public async Task<ActionResult<List<User>>> GetUsersByRoom(int roomId)
        {
            var users = await _chatRoomRepository.GetUsersByRoomIdAsync(roomId);
            return Ok(users);
        }
        
        [Authorize(Roles = nameof(Roles.Moderator))]
        [HttpPost("create")]
        public async Task<IActionResult> CreateRoom([FromBody] ChatRoom chatRoom)
        {
            if (chatRoom == null || string.IsNullOrWhiteSpace(chatRoom.Name))
            {
                return BadRequest("Invalid chat room data.");
            }

            await _chatRoomRepository.AddChatRoomAsync(chatRoom);
            return CreatedAtAction(nameof(GetRoomById), new { roomId = chatRoom.Id }, chatRoom);
        }
        
        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<List<ChatRoom>>> GetAllRooms()
        {
            var rooms = await _chatRoomRepository.GetAllChatRoomsAsync();
            return Ok(rooms);
        }

        [Authorize]
        [HttpGet("{roomId}")]
        public async Task<ActionResult<ChatRoom>> GetRoomById(int roomId)
        {
            var room = await _chatRoomRepository.GetRoomByIdAsync(roomId);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }
        
    }
}
