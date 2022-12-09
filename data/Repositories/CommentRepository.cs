using Bookie.data.entities;
using Microsoft.EntityFrameworkCore;
using Bookie.data.dtos.Comments;
using bookie.Helpers;

namespace Bookie.data.Repositories
{
    
    public interface ICommentRepository
    {
        Task CreateAsync(Comment cm,int bookId);
        Task DeleteAsync(Comment cm);
        Task<Comment?> GetAsync(int cmId,int bookId);
        Task<IReadOnlyList<Comment>> GetManyAsync();
        Task<PagedList<Comment>> GetManyAsync(CommentsSearchParameters parameters);
        Task UpdateAsync(Comment cm);
    }

    public class CommentRepository : ICommentRepository
    {
        private readonly BookieDBContext bookieDBContext;
        private readonly IBookRepository bookRepository;
        public CommentRepository(BookieDBContext context)
        {
            bookieDBContext = context;
        }

        public async Task<Comment?> GetAsync(int cmId,int bookId)
        {
            return await bookieDBContext.Comments.FirstOrDefaultAsync(x => x.Id == cmId && x.BookId == bookId);
        }

        public async Task<IReadOnlyList<Comment>> GetManyAsync()
        {
            return await bookieDBContext.Comments.ToListAsync();
        }

        public async Task<PagedList<Comment>> GetManyAsync(CommentsSearchParameters parameters)
        {
            var queryable = bookieDBContext.Comments.AsQueryable().OrderBy(o => o.Date);

            return await PagedList<Comment>.CreateAsync(queryable, parameters.pageNumber,
                parameters.PageSize);
        }

        public async Task CreateAsync(Comment cm,int bookId)
        {
            var book=bookieDBContext.Books.FirstOrDefault(x => x.Id == bookId);
            if (book != null) cm.BookId = bookId;
            bookieDBContext.Comments.Add(cm);
            await bookieDBContext.SaveChangesAsync();
        }



        public async Task UpdateAsync(Comment cm)
        {
            bookieDBContext.Comments.Update(cm);
            await bookieDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Comment cm)
        {
            bookieDBContext.Comments.Remove(cm);
            await bookieDBContext.SaveChangesAsync();
        }


    }
}
