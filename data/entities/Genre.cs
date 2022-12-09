namespace Bookie.data.entities
{
    public class Genre 
    {
        public int Id { get; set; }

        public GenreNames? Name { get; set; }
    }
    public enum GenreNames
    {
        Fantastic,
        Detective,
        Poetry,
        Childrens,
        Romance,
        Prose
    }
}
