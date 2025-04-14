using BookTrackr.Application.Interfaces.Auth;
using BookTrackr.Application.Utils;
using BookTrackr.Domain.Entities.Auth;
using BookTrackr.Domain.Exceptions;
using BookTrackr.Domain.Models.Requests.Auth;
using BookTrackr.Domain.Models.Responses.Auth;
using BookTrackr.Infrastructure.Interfaces.Repositories;

namespace BookTrackr.Application.Services
{

    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly PasswordUtils passwordUtils;
        private readonly JWTUtils jwtUtils;
        public AuthService(IAuthRepository authRepository, PasswordUtils passwordUtils, JWTUtils jwtUtils)
        {
            this.authRepository = authRepository;
            this.passwordUtils = passwordUtils;
            this.jwtUtils = jwtUtils;
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await authRepository.getUserByUsername(request.Username);

            if (user == null)
            {
                throw new AuthException("User not found");
            }
            if (!passwordUtils.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new AuthException("Invalid password");
            }
            var token = jwtUtils.GenerateToken(user);
            return new LoginResponse
            {
                UserId = user.Id,
                Name = user.Name,
                Username = user.Username,
                Token = token
            };
        }


        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var user = await authRepository.getUserByUsername(request.Username);
            if (user != null)
            {
                throw new Exception("User already exists");
            }

            await authRepository.CreateUser(new User
            {
                Username = request.Username,
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passwordUtils.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            });

            return new RegisterResponse
            {
                Username = request.Username,
                Name = request.Name,
                Email = request.Email
            };
        }
    }
}
