using Microsoft.AspNetCore.Mvc;
using UserLoginApi.Dtos;
using UserLoginApi.Interfaces;

namespace UserLoginApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            if (await _userRepository.UserExistsAsync(dto.Username))
                return BadRequest("User already exists");

            var user = await _userRepository.RegisterUserAsync(dto.Username, dto.Password);
            return Ok(new { user.Id, user.Username });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var token = await _userRepository.LoginUserAsync(dto.Username, dto.Password);
            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { Token = token });
        }
    }
}
