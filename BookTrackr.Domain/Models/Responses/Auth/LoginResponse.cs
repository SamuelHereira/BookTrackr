namespace BookTrackr.Domain.Models.Responses.Auth
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}