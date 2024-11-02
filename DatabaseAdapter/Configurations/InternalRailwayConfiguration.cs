using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class InternalRailwayConfiguration : IEntityTypeConfiguration<InternalRailway>
    {
        public void Configure(EntityTypeBuilder<InternalRailway> entity)
        {
            entity.HasKey(e => e.Id).HasName("InternalRailway_pkey");

            entity.ToTable("InternalRailway", tb => tb.HasComment("Внутренняя ЖД"));

            entity.HasIndex(e => e.RailwayId, "InternalRailway_railway_id_key").IsUnique();

            entity.Property(e => e.RailwayId).HasColumnName("railway_id");

            entity.HasOne(d => d.Railway).WithOne(p => p.InternalRailway)
                .HasForeignKey<InternalRailway>(d => d.RailwayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("InternalRailway_railway_id_fkey");
        }
    }
}
