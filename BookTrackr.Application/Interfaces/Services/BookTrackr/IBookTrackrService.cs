using BookTrackr.Domain.Models.DTO;
using BookTrackr.Domain.Models.Requests.BookTrackr;

namespace BookTrackr.Application.Interfaces.Services.BookTrackr
{

    public interface IBookTrackrService
    {
        Task<BookDTO> AddBook(CreateBookRequest book);
        Task<List<BookDTO>> GetBooks();
        Task<ReadedBookDTO> AddReadedBook(ReadedBookDTO readBook);
        Task<List<ReadedBookDTO>> GetReadedBooks(int userId);

    }
}