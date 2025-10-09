using ITI_SC_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI_SC_Project.Contexts.Configurations
{
    public class BoardingTypeConfiguration : IEntityTypeConfiguration<BoardingType>
    {
        public void Configure(EntityTypeBuilder<BoardingType> builder)
        {
            builder.HasKey(bt => bt.Id);

            builder.Property(bt => bt.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(bt => bt.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(bt => bt.Name)
                   .IsUnique();

            builder.Property(bt => bt.PriceModifier)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.HasMany(bt => bt.Bookings)
                   .WithOne(b => b.BoardingType)
                   .HasForeignKey(b => b.BoardingTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
