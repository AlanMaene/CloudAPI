
using Cocktail_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    public class RecipesContext : DbContext
    {
        public RecipesContext(DbContextOptions<RecipesContext> options): base(options)
        {
        }

        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Bartender> Bartenders { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Measurements> Measurements { get; set; }
    }
}
