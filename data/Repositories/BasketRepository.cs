using Bookie.data.entities;
using Microsoft.EntityFrameworkCore;
using Bookie.data.dtos.Books;
using bookie.Helpers;

namespace Bookie.data.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket?> GetAsync(string userId);
        Task CreateBasketIfNone(Basket basket, string userId);
        Task UpdateAsync(Basket basket);
    }

    public class BasketRepository : IBasketRepository
    {
        private readonly BookieDBContext bookieDBContext;
        public BasketRepository(BookieDBContext context)
        {
            bookieDBContext = context;
        }

        public async Task<Basket?> GetAsync(string userId)
        {
            return await bookieDBContext.Baskets.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task CreateBasketIfNone(Basket basket, string userId)
        {
            bookieDBContext.Baskets.Add(basket);
            await bookieDBContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Basket basket)
        {
            bookieDBContext.Baskets.Update(basket);
            await bookieDBContext.SaveChangesAsync();
        }
    }
}
