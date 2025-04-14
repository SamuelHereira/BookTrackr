using BookTrackr.Domain.Entities.Auth;
using BookTrackr.Domain.Models.Requests.Auth;
using BookTrackr.Domain.Models.Responses.Auth;


namespace BookTrackr.Infrastructure.Interfaces.Repositories
{

    public interface IAuthRepository
    {
        Task<User> CreateUser(User user);
        Task<User> getUserByUsername(string username);
        Task<User> GetUserById(int id);
    }
}