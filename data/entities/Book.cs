using Bookie.data.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace Bookie.data.entities
{
    public class Book : IUserOwnedResource
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        [Required]
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public double Price { get; set; }

        public string Quality { get; set; }

        public DateTime Created { get; set; }

        public Comment Comment { get; set; }

        public BookieRestUser User { get; set; }
    }

    
}
