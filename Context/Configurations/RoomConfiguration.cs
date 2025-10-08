using ITI_SC_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI_SC_Project.Context.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.Type)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(r => r.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.HasMany(r => r.Bookings)
                   .WithOne(b => b.Room)
                   .HasForeignKey(b => b.RoomId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
