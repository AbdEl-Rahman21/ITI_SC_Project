using ITI_SC_Project.Context.Configurations;
using ITI_SC_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_SC_Project.Context
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());

            modelBuilder.ApplyConfiguration(new ResidentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
