using ITI_SC_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI_SC_Project.Context.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(b => b.CheckInDate)
                   .IsRequired();

            builder.Property(b => b.CheckOutDate)
                   .IsRequired();

            builder.Property(b => b.TotalCost)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
        }
    }
}
