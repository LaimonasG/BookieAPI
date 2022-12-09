using Bookie.data.entities;

namespace Bookie.data.dtos.Baskets
{
    public class BasketsDto
    { 
        public record BasketDto(int Id, string userId, ICollection<Book>? Books);
        public record CreateBasketDto(string userId);
        public record UpdateBasketDto(ICollection<Book>? Books);
    }
}
