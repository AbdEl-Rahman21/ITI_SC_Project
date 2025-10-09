using ITI_SC_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI_SC_Project.Contexts.Configurations
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(rt => rt.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(rt => rt.Name)
                   .IsUnique();

            builder.Property(rt => rt.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.HasMany(rt => rt.Rooms)
                   .WithOne(r => r.RoomType)
                   .HasForeignKey(r => r.RoomTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
