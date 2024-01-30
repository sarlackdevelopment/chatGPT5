﻿using AutoMapper;
using chatGPT5.Interfaces;
using chatGPT5.Models;
using chatGPT5.Models.dto;
using chatGPT5.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chatGPT5.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, JwtTokenService jwtTokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _userRepository.AuthenticateAsync(login.Username, login.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            
            var token = _jwtTokenService.GenerateToken(user);
            return Ok(new { token });
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            if (await _userRepository.UserExistsAsync(user.Username))
            {
                return BadRequest("User already exists.");
            }
    
            user.Password = PasswordHasher.ComputeSha256Hash(user.Password);
    
            await _userRepository.AddUserAsync(user);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }
        
        [Authorize]
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
    }
}
