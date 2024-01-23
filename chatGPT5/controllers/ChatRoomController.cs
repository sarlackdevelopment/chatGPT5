using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using chatGPT5.models;

namespace chatGPT5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly ChatContext _context;

        public ChatRoomController(ChatContext context)
        {
            _context = context;
        }

        // GET: ChatRoom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRooms()
        {
            return await _context.ChatRooms.ToListAsync();
        }

        // GET: ChatRoom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatRoom>> GetChatRoom(int id)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(id);
        
            if (chatRoom == null)
            {
                return NotFound();
            }
        
            return chatRoom;
        }

        // POST: ChatRoom
        [HttpPost]
        public async Task<ActionResult<ChatRoom>> CreateChatRoom(ChatRoom chatRoom)
        {
            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChatRoom), new { id = chatRoom.Id }, chatRoom);
        }

        // // PUT: ChatRoom/5
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateChatRoom(int id, ChatRoom chatRoom)
        // {
        //     if (id != chatRoom.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(chatRoom).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ChatRoomExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }
        //
        // // DELETE: ChatRoom/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteChatRoom(int id)
        // {
        //     var chatRoom = await _context.ChatRooms.FindAsync(id);
        //     if (chatRoom == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.ChatRooms.Remove(chatRoom);
        //     await _context.SaveChangesAsync();
        //
        //     return NoContent();
        // }

        private bool ChatRoomExists(int id)
        {
            return _context.ChatRooms.Any(e => e.Id == id);
        }
    }
}
