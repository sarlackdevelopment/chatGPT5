﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    [HttpGet("messages")]
    public ActionResult<IEnumerable<Message>> GetMessages()
    {
        return Ok(ChatRepository.Messages);
    }
    
    [HttpPost("send")]
    public ActionResult SendMessage([FromBody] Message message)
    {
        ChatRepository.Messages.Add(message);
        return Ok();
    }
}