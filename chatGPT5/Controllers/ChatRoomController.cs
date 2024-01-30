using Microsoft.AspNetCore.Mvc;
using chatGPT5.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace chatGPT5.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatRoomController(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        [HttpGet("{roomId}/users")]
        public async Task<ActionResult<List<User>>> GetUsersByRoom(int roomId)
        {
            var users = await _chatRoomRepository.GetUsersByRoomIdAsync(roomId);
            return Ok(users);
        }
        
    }
}
