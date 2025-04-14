using BookTrackr.Domain.Entities.BookTrackr;
using BookTrackr.Domain.Models.DTO;
using BookTrackr.Domain.Models.Requests.BookTrackr;
using BookTrackr.Infrastructure.Database.Contexts;
using BookTrackr.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookTrackr.Infrastructure.Repositories
{
    public class BookTrackrRepository : IBookTrackrRepository
    {

        private readonly DatabaseContext _dbContext;
        public BookTrackrRepository(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public async Task<List<Book>> GetBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> AddBook(Book book)
        {


            await _dbContext.Books.AddAsync(book);

            return book;
        }

        public async Task<List<UserBook>> GetReadedBooksByUserId(int userId)
        {
            return await _dbContext.UserBooks.Where(ub => ub.UserId == userId).ToListAsync();
        }

        public async Task<UserBook> AddReadedBook(UserBook readBook)
        {
            await _dbContext.UserBooks.AddAsync(readBook);
            await _dbContext.SaveChangesAsync();

            return readBook;
        }

    }
}