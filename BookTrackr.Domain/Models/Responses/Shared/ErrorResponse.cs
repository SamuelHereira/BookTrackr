namespace BookTrackr.Domain.Models.Responses.Shared
{
    public class ErrorResponse : BaseResponse
    {
        public Error Error { get; set; }

        public ErrorResponse(int statusCode, string message, Error error) : base(statusCode, message)
        {
            Error = error;
        }
    }

    public struct Error
    {
        public int Code { get; set; }
        public String ErrorMessage { get; set; }
    }
}