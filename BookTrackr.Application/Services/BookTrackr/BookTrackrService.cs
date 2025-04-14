using BookTrackr.Application.Interfaces.Services.BookTrackr;
using BookTrackr.Domain.Entities.BookTrackr;
using BookTrackr.Domain.Models.DTO;
using BookTrackr.Domain.Models.Requests.BookTrackr;
using BookTrackr.Infrastructure.Interfaces.Repositories;

namespace BookTrackr.Application.Services.BookTrackr
{

    public class BookTrackrService : IBookTrackrService
    {
        private readonly IBookTrackrRepository bookTrackrRepository;
        public BookTrackrService(IBookTrackrRepository bookTrackrRepository)
        {
            this.bookTrackrRepository = bookTrackrRepository;
        }

        public async Task<BookDTO> AddBook(CreateBookRequest book)
        {
            var bookData = await bookTrackrRepository.AddBook(new Book
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Description = book.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            });

            return new BookDTO
            {
                Id = bookData.Id,
                Title = bookData.Title,
                Author = bookData.Author,
                Genre = bookData.Genre,
                Description = bookData.Description,
            };


        }

        public Task<List<BookDTO>> GetBooks()
        {
            var books = bookTrackrRepository.GetBooks().Result.Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Genre = b.Genre,
                Description = b.Description,
            }).ToList();

            return Task.FromResult(books);
        }

        public Task<ReadedBookDTO> AddReadedBook(ReadedBookDTO readBook)
        {
            var readedBook = new UserBook
            {
                BookId = readBook.BookId,
                UserId = readBook.UserId,
                Rating = readBook.Rating,
                Review = readBook.Review,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };

            var addedReadedBook = bookTrackrRepository.AddReadedBook(readedBook).Result;

            return Task.FromResult(new ReadedBookDTO
            {
                BookId = addedReadedBook.BookId,
                UserId = addedReadedBook.UserId,
                Rating = addedReadedBook.Rating,
                Review = addedReadedBook.Review,
            });
        }

        public Task<List<ReadedBookDTO>> GetReadedBooks(int userId)
        {
            var readedBooks = bookTrackrRepository.GetReadedBooksByUserId(userId);

            var readedBooksDTO = readedBooks.Result.Select(rb => new ReadedBookDTO
            {
                BookId = rb.BookId,
                UserId = rb.UserId,
                Rating = rb.Rating,
                Review = rb.Review,
            }).ToList();

            return Task.FromResult(readedBooksDTO);
        }

    }
}