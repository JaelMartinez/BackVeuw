using Microsoft.EntityFrameworkCore;
using MyBackend.Models;

namespace MyBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}
