using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class HouseDbContext : DbContext
    {
        public DbSet<HouseEntity> Houses { get; set; }
        public DbSet<BidEntity> Bids { get; set; }

        public HouseDbContext(DbContextOptions<HouseDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=houses.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData.Seed(modelBuilder);
        }
    }
}