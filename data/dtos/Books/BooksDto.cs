using Bookie.data.entities;
namespace Bookie.data.dtos.Books
{
    public record BookDto(int Id,string Name, int genreID, string Author, DateTime Created);
    public record CreateBookDto(string name, int genreID,string author);
    public record UpdateBookDto(string name, int genreID);

}
