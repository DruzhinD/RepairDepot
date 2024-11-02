using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class WagonConfiguration : IEntityTypeConfiguration<Wagon>
    {
        public void Configure(EntityTypeBuilder<Wagon> entity)
        {
            entity.HasKey(e => e.Id).HasName("Wagon_pkey");

            entity.ToTable("Wagon");

            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.RailwayId).HasColumnName("railway_id");
            entity.Property(e => e.RegNumber).HasColumnName("reg_number");
            entity.Property(e => e.ServiceDirectorateId).HasColumnName("service_directorate_id");

            entity.HasOne(d => d.Railway).WithMany(p => p.Wagons)
                .HasForeignKey(d => d.RailwayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_wagon_railway");

            entity.HasOne(d => d.ServiceDirectorate).WithMany(p => p.Wagons)
                .HasForeignKey(d => d.ServiceDirectorateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_wagon_service_directorate");
        }
    }
}
