using Bookie.data.entities;
using Microsoft.EntityFrameworkCore;
using Bookie.data.dtos.Genres;
using bookie.Helpers;

namespace Bookie.data.Repositories
{
    public interface IGenreRepository
    {
        Task CreateAsync(Genre genre);
        Task DeleteAsync(Genre genre);
        Task<Genre?> GetAsync(int genreId);
        Task<IReadOnlyList<Genre>> GetManyAsync();
        Task<PagedList<Genre>> GetManyAsync(GenresSearchParameters parameters);
        Task UpdateAsync(Genre genre);
    }

    public class GenreRepository : IGenreRepository
    {
        private readonly BookieDBContext bookieDBContext;
        public GenreRepository(BookieDBContext context)
        {
            bookieDBContext = context;
        }

        public async Task<Genre?> GetAsync(int genreId)
        {
            return await bookieDBContext.Genres.FirstOrDefaultAsync(x => x.Id == genreId);
        }

        public async Task<IReadOnlyList<Genre>> GetManyAsync()
        {
            return await bookieDBContext.Genres.ToListAsync();
        }

        public async Task<PagedList<Genre>> GetManyAsync(GenresSearchParameters parameters)
        {
            var queryable = bookieDBContext.Genres.AsQueryable().OrderBy(o => o.Name);

            return await PagedList<Genre>.CreateAsync(queryable, parameters.pageNumber,
                parameters.PageSize);
        }

        public async Task CreateAsync(Genre genre)
        {
            bookieDBContext.Genres.Add(genre);

            await bookieDBContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            bookieDBContext.Genres.Update(genre);
            await bookieDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Genre genre)
        {
            bookieDBContext.Genres.Remove(genre);
            await bookieDBContext.SaveChangesAsync();
        }
    }
}
