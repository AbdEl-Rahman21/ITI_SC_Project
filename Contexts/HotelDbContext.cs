using ITI_SC_Project.Contexts.Configurations;
using ITI_SC_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_SC_Project.Contexts
{
    public class HotelDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BoardingType> BoardingTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ResidentConfiguration());

            modelBuilder.ApplyConfiguration(new RoomConfiguration());

            modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());

            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            modelBuilder.ApplyConfiguration(new BoardingTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
