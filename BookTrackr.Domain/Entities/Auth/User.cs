using BookTrackr.Domain.Entities.BookTrackr;
using BookTrackr.Domain.Entities.Shared;

namespace BookTrackr.Domain.Entities.Auth
{

    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
    }
}