using chatGPT5.Models.network;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using chatGPT5.Interfaces;
using chatGPT5.Models.dto;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly IMessageRepository _messageRepository;

    public ChatController(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    
    [HttpGet("messages")]
    public async Task<ActionResult<IEnumerable<Message>>> GetMessages([FromQuery] int? userId, [FromQuery] int? roomId)
    {
        IEnumerable<Message> messages;
        
        if (!userId.HasValue)
        {
            return BadRequest("Invalid userId data.");
        }
        
        if (!roomId.HasValue)
        {
            return BadRequest("Invalid userId data.");
        }

        messages = await _messageRepository.GetMessagesByUserAndRoomAsync(userId.Value, roomId.Value);
        return Ok(messages);
    }

    
    [HttpPost("send")]
    public async Task<ActionResult> SendMessage([FromBody] MessageDto messageDto)
    {
        await _messageRepository.AddMessageAsync(messageDto);
        return Ok();
    }
}