using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class RepairTaskConfiguration : IEntityTypeConfiguration<RepairTask>
    {
        public void Configure(EntityTypeBuilder<RepairTask> entity)
        {
            entity.HasKey(e => e.Id).HasName("RepairTask_pkey");

            entity.ToTable("RepairTask", tb => tb.HasComment("Задание на ремонт"));

            entity.HasIndex(e => e.RepairOrderId, "RepairTask_repair_order_id_key").IsUnique();

            entity.HasIndex(e => e.RepairOrderId, "RepairTask_repair_order_id_key1").IsUnique();

            entity.Property(e => e.ForemanId).HasColumnName("foreman_id");
            entity.Property(e => e.RepairOrderId).HasColumnName("repair_order_id");

            entity.HasOne(d => d.RepairOrder).WithOne(p => p.RepairTask)
                .HasForeignKey<RepairTask>(d => d.RepairOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_repair_task_repair_order");
        }
    }
}
