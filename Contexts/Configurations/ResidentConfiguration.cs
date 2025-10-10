using ITI_SC_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI_SC_Project.Contexts.Configurations
{
    public class ResidentConfiguration : IEntityTypeConfiguration<Resident>
    {
        public void Configure(EntityTypeBuilder<Resident> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.ResidentId)
                   .HasMaxLength(20)
                   .IsRequired()
                   .IsUnicode(false);

            builder.HasIndex(r => r.ResidentId)
                   .IsUnique();

            builder.Property(r => r.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(r => r.Email)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(r => r.Phone)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasMany(r => r.Bookings)
                   .WithOne(b => b.Resident)
                   .HasForeignKey(b => b.ResidentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
