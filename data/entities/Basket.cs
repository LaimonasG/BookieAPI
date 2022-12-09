using System.ComponentModel.DataAnnotations;

namespace Bookie.data.entities
{
    public class Basket
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
