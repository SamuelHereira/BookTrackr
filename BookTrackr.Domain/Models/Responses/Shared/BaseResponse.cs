namespace BookTrackr.Domain.Models.Responses.Shared
{
    public abstract class BaseResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        protected BaseResponse(int code, string message)
        {
            StatusCode = code;
            Message = message;
        }

    }
}