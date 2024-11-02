using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class RepairRequestConfiguration : IEntityTypeConfiguration<RepairRequest>
    {
        public void Configure(EntityTypeBuilder<RepairRequest> entity)
        {
            entity.HasKey(e => e.Id).HasName("RepairRequest_pkey");

            entity.ToTable("RepairRequest", tb => tb.HasComment("Запрос на ремонт"));

            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.RepairTypeId).HasColumnName("repair_type_id");
            entity.Property(e => e.WagonId).HasColumnName("wagon_id");

            entity.HasOne(d => d.RepairType).WithMany(p => p.RepairRequests)
                .HasForeignKey(d => d.RepairTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_repair_request_repair_type");

            entity.HasOne(d => d.Wagon).WithMany(p => p.RepairRequests)
                .HasForeignKey(d => d.WagonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_repair_request_wagon");
        }
    }
}
