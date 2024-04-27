using Microsoft.EntityFrameworkCore;

namespace Flights_Serve.Domain.Models
{
    public class FlightsContexts : DbContext
    {
        public FlightsContexts(DbContextOptions<FlightsContexts> options) : base(options)
        {

        }
        public DbSet<Flights> Flights { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flights>().HasIndex(c => c.Origin).IsUnique();
        }

    }
}
