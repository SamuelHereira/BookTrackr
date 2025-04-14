using BookTrackr.Domain.Entities.BookTrackr;
using BookTrackr.Domain.Models.DTO;
using BookTrackr.Domain.Models.Requests.Auth;
using BookTrackr.Domain.Models.Requests.BookTrackr;
using BookTrackr.Domain.Models.Responses.Auth;
using System.Threading.Tasks;

namespace BookTrackr.Infrastructure.Interfaces.Repositories
{


    public interface IBookTrackrRepository
    {
        Task<List<Book>> GetBooks();
        Task<Book> AddBook(Book book);
        Task<List<UserBook>> GetReadedBooksByUserId(int userId);
        Task<UserBook> AddReadedBook(UserBook readBook);

    }
}