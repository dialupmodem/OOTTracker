using Microsoft.EntityFrameworkCore;

namespace OOTTracker.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Collectable> Collectables { get; set; }
        public DbSet<CollectableType> CollectableTypes { get; set; }
        public DbSet<InventoryEquipment> InventoryEquipment { get; set; }
        public DbSet<ItemAgeRequirement> ItemAgeRequirements { get; set; }
        public DbSet<ItemCheckRequirement> ItemCheckRequirements { get; set; }
        public DbSet<ItemCheck> ItemChecks { get; set; }
        public DbSet<ItemCheckType> ItemCheckTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Playthrough> Playthroughs { get; set; }
        public DbSet<PlaythroughItemCheck> PlaythroughItemChecks { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CollectableType>()
                .HasMany(c => c.Collectables)
                .WithOne(c => c.CollectableType)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InventoryEquipment>()
                .HasMany(i => i.ItemCheckRequirements)
                .WithOne(i => i.InventoryEquipment)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemAgeRequirement>()
                .HasMany(i => i.ItemChecks)
                .WithOne(i => i.ItemAgeRequirement)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemCheck>()
                .HasMany(i => i.ItemCheckRequirements)
                .WithOne(i => i.ItemCheck)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemCheck>()
                .HasMany(i => i.PlaythroughItemChecks)
                .WithOne(p => p.ItemCheck)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemCheckType>()
                .HasMany(i => i.ItemChecks)
                .WithOne(i => i.ItemCheckType)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Collectables)
                .WithOne(c => c.Location)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.ItemChecks)
                .WithOne(i => i.Location)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Playthrough>()
                .HasMany(p => p.PlaythroughItemChecks)
                .WithOne(p => p.Playthrough)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
