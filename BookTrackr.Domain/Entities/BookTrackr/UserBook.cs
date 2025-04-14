using BookTrackr.Domain.Entities.Shared;
using BookTrackr.Domain.Entities.Auth;

namespace BookTrackr.Domain.Entities.BookTrackr
{
    public class UserBook : BaseEntity
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; } = string.Empty;

        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}