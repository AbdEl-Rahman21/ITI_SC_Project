using ITI_SC_Project.Context.Configurations;
using ITI_SC_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_SC_Project.Context
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());

            modelBuilder.ApplyConfiguration(new ResidentConfiguration());

            modelBuilder.ApplyConfiguration(new RoomConfiguration());

            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
