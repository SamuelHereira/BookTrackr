using BookTrackr.Application.Interfaces.Services.BookTrackr;
using BookTrackr.Domain.Models.DTO;
using BookTrackr.Domain.Models.Requests.BookTrackr;
using BookTrackr.Domain.Models.Responses.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTrackr.Api.Controllers.v1.BookTrackr
{
    [Route("api/v1/booktrackr")]
    [ApiController]
    [Authorize]
    public class BookTrackrController : ControllerBase
    {
        private readonly IBookTrackrService _bookTrackrService;

        public BookTrackrController(IBookTrackrService bookTrackrService)
        {
            _bookTrackrService = bookTrackrService;
        }

        [HttpGet("books")]
        public async Task<ActionResult<SuccessResponse<List<BookDTO>>>> GetBooks()
        {
            var result = await _bookTrackrService.GetBooks();
            return Ok(new SuccessResponse<List<BookDTO>>(200, "Books retrieved successfully", result));
        }

        [HttpPost("books")]
        public async Task<ActionResult<SuccessResponse<BookDTO>>> AddBook(CreateBookRequest request)
        {
            var result = await _bookTrackrService.AddBook(request);
            return Ok(new SuccessResponse<BookDTO>(201, "Book added successfully", result));
        }

        [HttpGet("readed-books")]
        public async Task<ActionResult<SuccessResponse<List<ReadedBookDTO>>>> GetReadedBooks([FromQuery] int userId)
        {
            var result = await _bookTrackrService.GetReadedBooks(userId);
            return Ok(new SuccessResponse<List<ReadedBookDTO>>(200, "Readed books retrieved successfully", result));
        }



    }

}