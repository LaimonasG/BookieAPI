using Bookie.data.Auth.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Bookie.data.entities
{
    public class Comment : IUserOwnedResource
    {
        public int Id { get; set; }
        
        public int BookId { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        [Required]
        public string UserId { get; set; }
        public string Username { get; set; }

        public BookieRestUser User { get; set; }

    }
}
