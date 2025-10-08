using ITI_SC_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI_SC_Project.Context.Configurations
{
    public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(w => w.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(w => w.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(w => w.Phone)
                   .HasMaxLength(20);

            builder.Property(w => w.Salary)
                   .HasColumnType("decimal(18,2)");

            builder.Property(w => w.JobTitle)
                   .HasMaxLength(100);
        }
    }
}
