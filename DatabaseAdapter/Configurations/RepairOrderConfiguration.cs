using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class RepairOrderConfiguration : IEntityTypeConfiguration<RepairOrder>
    {
        public void Configure(EntityTypeBuilder<RepairOrder> entity)
        {
            entity.HasKey(e => e.Id).HasName("RepairOrder_pkey");

            entity.ToTable("RepairOrder", tb => tb.HasComment("Наряд на ремонт"));

            entity.HasIndex(e => e.RepairRequestId, "RepairOrder_repair_request_id_key").IsUnique();

            entity.Property(e => e.DateStart).HasColumnName("date_start");
            entity.Property(e => e.DateStop).HasColumnName("date_stop");
            entity.Property(e => e.Money)
                .HasColumnType("money")
                .HasColumnName("money");
            entity.Property(e => e.RepairRequestId).HasColumnName("repair_request_id");

            entity.HasOne(d => d.RepairRequest).WithOne(p => p.RepairOrder)
                .HasForeignKey<RepairOrder>(d => d.RepairRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_repair_order_repair_request");
        }
    }
}
