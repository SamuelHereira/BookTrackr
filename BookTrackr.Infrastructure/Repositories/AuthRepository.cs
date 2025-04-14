using BookTrackr.Domain.Entities.Auth;
using BookTrackr.Infrastructure.Database.Contexts;
using BookTrackr.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookTrackr.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DatabaseContext _dbContext;
        public AuthRepository(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public async Task<User> CreateUser(User user)
        {
            _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> getUserByUsername(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}