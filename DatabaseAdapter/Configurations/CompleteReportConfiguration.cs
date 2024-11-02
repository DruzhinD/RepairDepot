using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class CompleteReportConfiguration : IEntityTypeConfiguration<CompleteReport>
    {
        public void Configure(EntityTypeBuilder<CompleteReport> entity)
        {
            entity.HasKey(e => e.Id).HasName("CompleteReport_pkey");

            entity.ToTable("CompleteReport", tb => tb.HasComment("Отчет о выполнении работ"));

            entity.HasIndex(e => e.RepairTaskId, "CompleteReport_repair_task_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateStartFact).HasColumnName("date_start_fact");
            entity.Property(e => e.DateStopFact).HasColumnName("date_stop_fact");
            entity.Property(e => e.RepairTaskId).HasColumnName("repair_task_id");

            entity.HasOne(d => d.RepairTask).WithOne(p => p.CompleteReport)
                .HasForeignKey<CompleteReport>(d => d.RepairTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_complete_report_repair_task");
        }
    }
}
