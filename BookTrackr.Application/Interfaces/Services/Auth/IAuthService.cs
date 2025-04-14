using BookTrackr.Domain.Models.Requests.Auth;
using BookTrackr.Domain.Models.Responses.Auth;

namespace BookTrackr.Application.Interfaces.Auth
{

    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<RegisterResponse> Register(RegisterRequest request);
    }
}