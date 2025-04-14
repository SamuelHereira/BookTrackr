namespace BookTrackr.Domain.Models.DTO
{
    public class ReadedBookDTO
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}