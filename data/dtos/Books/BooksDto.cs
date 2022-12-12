using Bookie.data.entities;
namespace Bookie.data.dtos.Books
{
    public record BookDto(int Id,string Name, int genreID, string Author, double Price,string Quality, DateTime Created,string UserId);
    public record CreateBookDto(string name, int genreID,string author, double price, string quality);
    public record UpdateBookDto(string name, int genreID);

}
