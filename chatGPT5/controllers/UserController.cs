using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("register")]
    public ActionResult<User> Register([FromBody] User user)
    {
        ChatRepository.Users.Add(user);
        return Ok(user);
    }

    [HttpGet("users")]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(ChatRepository.Users);
    }
}