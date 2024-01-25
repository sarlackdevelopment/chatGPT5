using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using chatGPT5.Interfaces;
using chatGPT5.models;

namespace chatGPT5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatRoomController : ControllerBase
    {
        // private readonly ChatContext _context;
        //
        // public ChatRoomController(ChatContext context)
        // {
        //     _context = context;
        // }
        //
        // // GET: ChatRoom
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRooms()
        // {
        //     return await _context.ChatRooms.ToListAsync();
        // }
        //
        // // GET: ChatRoom/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<ChatRoom>> GetChatRoom(int id)
        // {
        //     var chatRoom = await _context.ChatRooms.FindAsync(id);
        //
        //     if (chatRoom == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return chatRoom;
        // }
        //
        // // POST: ChatRoom
        // [HttpPost]
        // public async Task<ActionResult<ChatRoom>> CreateChatRoom(ChatRoom chatRoom)
        // {
        //     _context.ChatRooms.Add(chatRoom);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction(nameof(GetChatRoom), new { id = chatRoom.Id }, chatRoom);
        // }
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
