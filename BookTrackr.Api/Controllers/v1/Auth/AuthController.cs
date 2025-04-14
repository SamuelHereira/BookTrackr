
using BookTrackr.Application.Interfaces.Auth;
using BookTrackr.Domain.Models.Requests.Auth;
using BookTrackr.Domain.Models.Responses.Auth;
using BookTrackr.Domain.Models.Responses.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BookTrackr.Api.Controllers.v1.Auth
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<SuccessResponse<LoginResponse>>> Login(LoginRequest request)
        {
            var result = await _authService.Login(request);
            return Ok(new SuccessResponse<LoginResponse>(200, "Login successful", result));
        }

        [HttpPost("register")]
        public async Task<ActionResult<SuccessResponse<RegisterResponse>>> Register(RegisterRequest request)
        {
            var result = await _authService.Register(request);
            return Ok(new SuccessResponse<RegisterResponse>(200, "Registration successful", result));
        }
    }
}