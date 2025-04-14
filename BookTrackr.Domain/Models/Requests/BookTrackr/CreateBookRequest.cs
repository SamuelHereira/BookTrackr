namespace BookTrackr.Domain.Models.Requests.BookTrackr
{
    public class CreateBookRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
    }
}