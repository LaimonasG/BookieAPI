using Bookie.data.entities;
using Microsoft.EntityFrameworkCore;
using Bookie.data.dtos.Books;
using bookie.Helpers;

namespace Bookie.data.Repositories
{
    public interface IBookRepository
    {
        Task CreateAsync(Book book,int genreId);
        Task DeleteAsync(Book book);
        Task<Book?> GetAsync(int bookId,int genreId);
        Task<IReadOnlyList<Book>> GetManyAsync();

        Task<PagedList<Book>> GetManyAsync(BooksSearchParameters parameters);
        Task UpdateAsync(Book book);
    }

    public class BookRepository : IBookRepository
    {
        private readonly BookieDBContext bookieDBContext;
        public BookRepository(BookieDBContext context)
        {
            bookieDBContext = context;
        }

        public async Task<Book?> GetAsync(int bookId,int genreId)
        {
            return await bookieDBContext.Books.FirstOrDefaultAsync(x => x.Id == bookId && x.GenreId == genreId);
        }

        public async Task<IReadOnlyList<Book>> GetManyAsync()
        {
            return await bookieDBContext.Books.ToListAsync();
        }

        public async Task<IReadOnlyList<Book>> GetUserBooksAsync(string userId)
        {
            return await bookieDBContext.Books.Where(x => x.UserId== userId).ToListAsync();
        }

        public async Task<PagedList<Book>> GetManyAsync(BooksSearchParameters parameters)
        {
            var queryable = bookieDBContext.Books.AsQueryable().OrderBy(o => o.Name);

            return await PagedList<Book>.CreateAsync(queryable, parameters.pageNumber,
                parameters.PageSize);
        }

        public async Task CreateAsync(Book book,int genreId)
        {
            var genre = bookieDBContext.Genres.FirstOrDefault(x => (int) x.Name == genreId);
            if (genre != null) book.GenreId = genreId;
            bookieDBContext.Books.Add(book);
            await bookieDBContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            bookieDBContext.Books.Update(book);
            await bookieDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            bookieDBContext.Books.Remove(book);
            await bookieDBContext.SaveChangesAsync();
        }
    }
}
