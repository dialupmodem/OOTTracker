using Microsoft.EntityFrameworkCore;

namespace OOTTracker.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Collectable> Collectables { get; set; }
        public DbSet<CollectableType> CollectableTypes { get; set; }
        public DbSet<Location> Locations { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
