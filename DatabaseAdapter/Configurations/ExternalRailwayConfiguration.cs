using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class ExternalRailwayConfiguration : IEntityTypeConfiguration<ExternalRailway>
    {
        public void Configure(EntityTypeBuilder<ExternalRailway> entity)
        {
            entity.HasKey(e => e.Id).HasName("ExternalRailway_pkey");

            entity.ToTable("ExternalRailway", tb => tb.HasComment("Внешняя ЖД"));

            entity.Property(e => e.Bank)
                .HasMaxLength(60)
                .HasColumnName("bank");
            entity.Property(e => e.BusinessAddress)
                .HasMaxLength(80)
                .HasColumnName("business_address");
            entity.Property(e => e.Inn).HasColumnName("inn");
            entity.Property(e => e.RailwayId).HasColumnName("railway_id");

            entity.HasOne(d => d.Railway).WithMany(p => p.ExternalRailways)
                .HasForeignKey(d => d.RailwayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ExternalRailway_railway_id_fkey");
        }
    }
}
