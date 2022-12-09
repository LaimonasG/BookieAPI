using Bookie.data.entities;
namespace Bookie.data.dtos.Genres
{
        public record GenreDto(int Id, string? Name);
        public record CreateGenreDto(GenreNames? name);
        public record UpdateGenreDto(GenreNames? name);


}
