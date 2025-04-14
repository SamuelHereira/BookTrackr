namespace BookTrackr.Domain.Models.Responses.Shared
{
    public class SuccessResponse<T> : BaseResponse
    {

        public T Data { get; set; }

        public SuccessResponse(int statusCode, string message, T data) : base(statusCode, message)
        {
            Data = data;
        }
    }
}