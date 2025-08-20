using Flights.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flights.Server.Data
{
    public class Entities : DbContext
    {
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Flight> Flights => Set<Flight>();

        public Entities(DbContextOptions<Entities> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasKey(p => p.Email); // Configure the PK for the Passenger entity

            modelBuilder.Entity<Flight>()
                .Property(p => p.RemainingNumberOfSeats)
                .IsConcurrencyToken(); // Concurrency token allows us to avoid race conditions, if two users
                                       // try to book the same flight at the same time, one of them will fail
                                       // and then, the flight won't be overbooked.

            modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);
        }
    }
}
