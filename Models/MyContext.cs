using Microsoft.EntityFrameworkCore;

namespace ChefsNdishes.Models
{
    public class Mycontext: DbContext
    {
        public Mycontext(DbContextOptions options) : base(options) { }

        public DbSet<Chef> Chefs {get; set;}
        public DbSet<Dish> Dishes {get; set;}
    }
}