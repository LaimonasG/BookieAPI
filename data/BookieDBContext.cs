using Bookie.data.Auth.Model;
using Bookie.data.entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookie.data
{
    public class BookieDBContext : IdentityDbContext<BookieRestUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<Genre> Genres { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=ForumDb2");
            //  optionsBuilder.UseSqlServer("Server=tcp:bookie.database.windows.net,1433;Initial Catalog=Bookie_db;Persist Security Info=False;User ID=namas;Password=AdminBasket18+;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
