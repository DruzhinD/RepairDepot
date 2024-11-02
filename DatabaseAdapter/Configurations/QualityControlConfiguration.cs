using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class QualityControlConfiguration : IEntityTypeConfiguration<QualityControl>
    {
        public void Configure(EntityTypeBuilder<QualityControl> entity)
        {
            entity.HasKey(e => e.Id).HasName("QualityControl_pkey");

            entity.ToTable("QualityControl", tb => tb.HasComment("Акт контроля качества"));

            entity.HasIndex(e => e.CompleteReportId, "QualityControl_complete_report_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasMaxLength(100)
                .HasColumnName("comment");
            entity.Property(e => e.CompleteReportId).HasColumnName("complete_report_id");
            entity.Property(e => e.Quality).HasColumnName("quality");

            entity.HasOne(d => d.CompleteReport).WithOne(p => p.QualityControl)
                .HasForeignKey<QualityControl>(d => d.CompleteReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_quality_control_complete_report");
        }
    }
}
