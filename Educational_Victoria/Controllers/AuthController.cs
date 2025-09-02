using Educational_Victoria.DTOs.LoginDto;
using Educational_Victoria.Interfaces.IServices;
using Educational_Victoria.Services.AuthService;
using Educational_Victoria.Services.JwtService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Victoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var user = await _authService.AuthenticateAsync(request);
                return Ok(user); // já contém Token, Email e Role
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
