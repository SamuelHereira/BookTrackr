using BookTrackr.Domain.Entities.Shared;

namespace BookTrackr.Domain.Entities.BookTrackr
{

    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
    }
}